# BlazorMonaco

[![Nuget](https://img.shields.io/nuget/dt/BlazorMonaco)](https://www.nuget.org/packages/BlazorMonaco)
[![Nuget](https://img.shields.io/nuget/v/BlazorMonaco)](https://www.nuget.org/packages/BlazorMonaco)
[![MonacoEditor](https://img.shields.io/badge/monaco--editor-0.52.2-blue)](https://github.com/microsoft/monaco-editor)
[![License-MIT](https://img.shields.io/badge/license-MIT-informational)](https://github.com/serdarciplak/BlazorMonaco/blob/master/LICENSE)

<a href="#">
    <img src="https://raw.githubusercontent.com/serdarciplak/BlazorMonaco/master/BlazorMonaco/icon.png" align="right" height="120" style="padding-left:20px;" />
</a>

Blazor component for Microsoft [Monaco Editor](https://github.com/Microsoft/monaco-editor) which powers Visual Studio Code.

Most of the main Monaco Editor feature set is supported with some less-frequently used features currently missing. Any contributions, comments or suggestions are greatly welcome. Please feel free to contact me at [x.com/serdarciplak](https://x.com/serdarciplak) or via GitHub.

The current BlazorMonaco version :
- Uses `Monaco Editor v0.52.2`
- Supports `netstandard2.0`, `net5.0`, `net6.0`, `net7.0`, `net8.0` and `net9.0`

## Demo

You can see a working sample WebAssembly app [here](https://serdarciplak.github.io/BlazorMonaco/).

## Installation

- Add the [NuGet](https://www.nuget.org/packages/BlazorMonaco/) package to your Blazor project.

    ```
    dotnet add package BlazorMonaco
    // or
    Install-Package BlazorMonaco
    ```

- Add the script tags below to the end of your html body tag. Note that they must be placed before the Blazor script tag (`blazor.webassembly.js`, `blazor.server.js` or `blazor.web.js`).

    ```html
    <script src="_content/BlazorMonaco/jsInterop.js"></script>
    <script src="_content/BlazorMonaco/lib/monaco-editor/min/vs/loader.js"></script>
    <script src="_content/BlazorMonaco/lib/monaco-editor/min/vs/editor/editor.main.js"></script>
    ```

- Everything resides in three namespaces. You can add the following using directives to your root `_Imports.razor` file, or any other place you may need them.

    ```razor
    @using BlazorMonaco
    @using BlazorMonaco.Editor
    @using BlazorMonaco.Languages
    ```

- Add a `StandaloneCodeEditor` or a `StandaloneDiffEditor` component in your razor file and you'll see the editor rendered with the default options.

    ```razor
    <StandaloneCodeEditor />
    <!-- or -->
    <StandaloneDiffEditor />
    ```

>**Note:** If you have any issues like the editor not being visible or not initializing correctly, please check the [troubleshooting](#troubleshooting) section below.

## Usage

### Providing initial options

To customize your editor's initial options, set the `ConstructionOptions` parameter and provide a method that returns a `StandaloneEditorConstructionOptions`.

```razor
<StandaloneCodeEditor Id="my-code-editor" ConstructionOptions="EditorConstructionOptions" />

@code {
    private StandaloneEditorConstructionOptions EditorConstructionOptions(StandaloneCodeEditor editor)
    {
        return new StandaloneEditorConstructionOptions
        {
            AutomaticLayout = true,
            Language = "javascript",
            Value = "function xyz() {\n" +
                    "   console.log(\"Hello world!\");\n" +
                    "}"
        };
    }
}
```

### Listening to editor events

You can subscribe to editor events (e.g. `OnDidKeyUp` or `OnDidPaste`) via the editor event parameters.

```razor
<StandaloneCodeEditor Id="my-code-editor" OnDidChangeCursorPosition="EditorDidChangeCursorPosition" />

@code {
    private void EditorDidChangeCursorPosition(CursorPositionChangedEvent eventArgs)
    {
        Console.WriteLine("EditorDidChangeCursorPosition");
    }
}
```

### Accessing a diff editor's inner editors and events

`StandaloneDiffEditor` provides two properties named `OriginalEditor` and `ModifiedEditor` for accessing the inner editor instances. You can use them like any other `StandaloneCodeEditor` instance.

You can register to inner editors' events using the helper event parameters of the main `StandaloneDiffEditor` instance.

```razor
<StandaloneDiffEditor @ref="_diffEditor" Id="my-diff-editor" OnKeyUpOriginal="OnKeyUpOriginal" OnKeyUpModified="OnKeyUpModified" />

@code {
    private StandaloneDiffEditor _diffEditor;

    private void OnKeyUpOriginal(KeyboardEvent keyboardEvent)
    {
        StandaloneCodeEditor originalEditor = _diffEditor.OriginalEditor;
        Console.WriteLine("OnKeyUpOriginal : " + keyboardEvent.Code);
    }

    private void OnKeyUpModified(KeyboardEvent keyboardEvent)
    {
        StandaloneCodeEditor modifiedEditor = _diffEditor.ModifiedEditor;
        Console.WriteLine("OnKeyUpModified : " + keyboardEvent.Code);
    }
}

```

### Css styling

There are 3 css selectors you can use to customize the css styles for your editors.
- All editor instances are contained in a `div` element that has a css class of `monaco-editor-container`.
- The `Id` value you set for your editor instance is also set as the id of its container `div` element.
- If you set your editor's `CssClass` property, that value is added to the class attribute of its container div element.

That means, the razor component below;

```razor
<StandaloneCodeEditor Id="my-editor-id" CssClass="my-editor-class" />
```

is rendered with the html below;

```html
<div id="my-editor-id" class="monaco-editor-container my-editor-class">
    ...
</div>
```

So, you can use css selectors like below to select any element in an editor and customize its styling.

```css
#my-editor-id { /* applies to a specific editor instance */
    height: 100px;
}

.my-editor-class { /* applies to all editor instances with this class */
    height: 100px;
}

.monaco-editor-container { /* applies to all editor instances */
    height: 100px;
}
```

### Global Methods

Monaco Editor JavaScript library defines some methods in the global scope. As C# does not allow global methods, BlazorMonaco groups those methods in two static classes named `BlazorMonaco.Editor.Global` and `BlazorMonaco.Language.Global`. If you need to use a Monaco Editor method in the global scope, check where in the JavaScript library that method is, and search for it in the corresponding `Global` class.

For example, you can use the `SetTheme` method like below.

```csharp
await BlazorMonaco.Editor.Global.SetTheme(jsRuntime, "my-custom-theme");
```

### Using a custom Monaco Editor installation

If you've made changes to Monaco Editor JS library, and need to use this edited version instead of the unmodified version embedded in BlazorMonaco, you can modify the script tags like below.

```html
<script src="_content/BlazorMonaco/jsInterop.js"></script>
<script>var require = { paths: { vs: 'my-path/monaco-editor/min/vs' } };</script>
<script src="my-path/monaco-editor/min/vs/loader.js"></script>
<script src="my-path/monaco-editor/min/vs/editor/editor.main.js"></script>
```

## Troubleshooting

- ***Q:*** I have added a `StandaloneCodeEditor` as described above, but it's not loading.

  ***A:*** Most possibly, the editor is actually loading but you cannot see it because it has a height of 0. How a DOM element's height can be set depends on its parent elements and their styling. So, BlazorMonaco cannot internally manage the height of the editor instances. If you don't set an editor's height, it may be invisible as it has a height of `0px`. Please add a CSS style to set your editor's height according to where it's placed.

  ```css
  #my-editor-id {
      height: 100px;
  }
  ```

- ***Q:*** The editor does not initialize correctly in my Blazor web app.

  ***A:*** To be able to work with the interactive MonacoEditor JS library, BlazorMonaco requires an interactive render mode. If you place the editor instance in a page that uses static server-side rendering (static SSR), the editor instance cannot initialize and work.

  You can set an interactive render mode globally at the app level,
  ```razor
  <!-- In your App.razor file -->
  <Routes @rendermode="InteractiveServer" />
  ```

  only for a specific page,
  ```razor
  <!-- In the razor file of your page that contains the editor -->
  @page "/editor-page"
  @rendermode InteractiveServer

  <StandaloneCodeEditor />
  ```

  or only for a component that wraps the editor instance.
  ```razor
  <!-- In the razor file of your interactive wrapper component -->
  @rendermode InteractiveServer

  <StandaloneCodeEditor />
  ```

- ***Q:*** The editor works OK the first time it's displayed. But it is broken (as if its css style is incorrect), if the user navigates to another page and returns back to the editor's page.

  ***A:*** Please check that the page that contains the editor instance is not opened with Blazor enhanced navigation. Enhanced navigation undos dynamic changes made to the DOM by MonacoEditor JS library and breaks the editor. You need to disable enhanced navigation for pages that contain an editor.

  You can set the `data-enhance-nav` attribute of your links to `false`,
  ```html
  <a href="editor-page" data-enhance-nav="false">Editor Page</a>
  ```
  set `forceLoad` parameter in your `NavigateTo()` calls to true,
  ```csharp
  Navigation.NavigateTo("editor-page", true);
  ```
  or use the Blazor docs [here](https://learn.microsoft.com/en-us/aspnet/core/blazor/fundamentals/routing?view=aspnetcore-9.0#enhanced-navigation-and-form-handling) to see all methods for disabling enhanced navigation.

## Documentation

As BlazorMonaco is just a bridge between JavaScript and Blazor, you can use Monaco Editor's [documentation](https://microsoft.github.io/monaco-editor/docs.html).

## Migration Guide

After a major version update (like from `v2.x` to `v3.x`), you may need to make some changes in your integration. Please see the [MIGRATE.md](https://github.com/serdarciplak/BlazorMonaco/blob/master/MIGRATE) file for details.

## Changelog

You can view the history and the changes in the [CHANGELOG.md](https://github.com/serdarciplak/BlazorMonaco/blob/master/CHANGELOG) file.

## License

MIT, see the [LICENSE](https://github.com/serdarciplak/BlazorMonaco/blob/master/LICENSE) file for details.
