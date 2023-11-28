<p align="center"><img src="https://raw.githubusercontent.com/serdarciplak/BlazorMonaco/master/BlazorMonaco/icon.png" width="150" height="150" /></p>

<p align="center">
<a href="https://www.nuget.org/packages/BlazorMonaco/"><img src="https://buildstats.info/nuget/BlazorMonaco" /></a>
</p>

<h1 align="center">BlazorMonaco</h1>

Blazor component for Microsoft's [Monaco Editor](https://github.com/Microsoft/monaco-editor) which powers Visual Studio Code.

Some less-frequently used Monaco Editor features are currently missing, but a good amount of the basic feature set is supported. The package is updated regularly to cover more features and use cases. Any contributions, comments or suggestions are greatly welcome. Please feel free to contact me at [twitter/serdarciplak](https://twitter.com/serdarciplak) or via GitHub.

Current version of BlazorMonaco :
* Uses `Monaco Editor v0.34.1`
* Supports `netstandard2.0`, `net5.0`, `net6.0`, `net7.0` and `net8.0`

## Demo

You can see a working sample WebAssembly app [here](https://serdarciplak.github.io/BlazorMonaco/).

## Get Started

* Add the [NuGet](https://www.nuget.org/packages/BlazorMonaco/) package to your Blazor project.

```
dotnet add package BlazorMonaco
// or
Install-Package BlazorMonaco
```

* Add the below script tags to your `index.html` file. Please note that these script tags must be placed before the script tag for the `blazor.webassembly.js` file.

```html
<script src="_content/BlazorMonaco/jsInterop.js"></script>
<script src="_content/BlazorMonaco/lib/monaco-editor/min/vs/loader.js"></script>
<script src="_content/BlazorMonaco/lib/monaco-editor/min/vs/editor/editor.main.js"></script>
```

* Everything resides in three namespaces. You can add the following using directives to your root `_Imports.razor` file, or any other place you may need them.

```
@using BlazorMonaco
@using BlazorMonaco.Editor
@using BlazorMonaco.Languages
```

## Code Editor

### Adding an editor instance

* To add a new editor instance, you just need to add a `StandaloneCodeEditor` component in your razor file.

```xml
<StandaloneCodeEditor Id="my-editor-instance-id" />
```

### Providing custom options

* To customize your editor instance's initial options, set the `ConstructionOptions` parameter of the instance, and provide a method that returns a `StandaloneEditorConstructionOptions` instance.

```xml
<StandaloneCodeEditor Id="my-editor-instance-id" ConstructionOptions="EditorConstructionOptions" />
```

* Then, add that method to your razor file's `@code` block and return a `StandaloneEditorConstructionOptions` instance with the values you need.

```csharp
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
```

### Editor events

* You can subscribe to editor events (e.g. `OnDidKeyUp` or `OnDidPaste`) using the editor's event parameters.

```xml
<StandaloneCodeEditor Id="my-editor-instance-id" OnDidChangeCursorPosition="EditorDidChangeCursorPosition" />
```

* Then, add your event listener method to your razor file's `@code` block.

```csharp
private void EditorDidChangeCursorPosition(CursorPositionChangedEvent eventArgs)
{
	Console.WriteLine("EditorDidChangeCursorPosition");
}
```

## Diff Editor

### Adding a diff editor instance

* To add a new diff editor instance, you just need to add a `StandaloneDiffEditor` component in your razor file.

```xml
<StandaloneDiffEditor Id="my-editor-instance-id" />
```

### Access to inner editor instances and events

* `StandaloneDiffEditor` class provides two properties named `OriginalEditor` and `ModifiedEditor` for accessing the inner editor instances. You can use them like any other `StandaloneCodeEditor` instances.

* You can register to inner editors' events using the helper event parameters of the main `StandaloneDiffEditor` instance.

```xml
<StandaloneDiffEditor Id="my-editor-instance-id" OnKeyUpOriginal="OnKeyUpOriginal" OnKeyUpModified="OnKeyUpModified" />
```

## Notes

### Css styling

* There are 3 css selectors you can use to add/edit css styles for your editors.
  * The main html element of all editor instances contains the `monaco-editor-container` css class.
  * The `Id` value you set for your razor component is also set as the id of its html element.
  * You can provide a string in the `CssClass` property of an editor instance. That value will be added to the class attribute of its html element.

```xml
<StandaloneCodeEditor Id="my-editor-id" CssClass="my-editor-class" />
```

```css
.monaco-editor-container { /* for all editor instances */
	height: 100px;
}

/* or */

#my-editor-id { /* for a specific editor instance */
	height: 100px;
}

/* or */

.my-editor-class { /* for a specific editor instance */
	height: 100px;
}
```

> ⚠️ `Note : ` As an html element cannot set its height disregarding where it's placed in, BlazorMonaco cannot manage editor instance heights. So, if you don't do that yourself, editor instances may have a height of `0px` and be invisible. Please don't forget to set your editor instance heights as you need.

### Global Methods

Monaco Editor JavaScript library defines some methods in the global scope. As C# does not allow global methods, BlazorMonaco groups those methods in two static classes named `BlazorMonaco.Editor.Global` and `BlazorMonaco.Language.Global`. If you need to use a Monaco Editor method in the global scope, check where in the JavaScript library that method is, and search for it in the corresponding Global class. They're defined as static methods.

For example, you can use the `SetTheme` method as below. 

```csharp
await BlazorMonaco.Editor.Global.SetTheme(jsRuntime, "my-custom-theme");
```

### Using a custom Monaco Editor setup

* If you've made changes to Monaco Editor, and need to use this edited version instead of the unmodified version packed with BlazorMonaco, just change the paths to monaco editor resources in your `index.html` file.

```html
<script src="_content/BlazorMonaco/jsInterop.js"></script>
<script>var require = { paths: { vs: 'my-path/monaco-editor/min/vs' } };</script>
<script src="my-path/monaco-editor/min/vs/loader.js"></script>
<script src="my-path/monaco-editor/min/vs/editor/editor.main.js"></script>
```

## Documentation

As BlazorMonaco is just a bridge between JavaScript and Blazor, you can use Monaco Editor's [documentation](https://microsoft.github.io/monaco-editor/api/index.html).

## Migration Guide

After a major version update (like from `v2.x.x` to `v3.x.x`), you may need to make some changes in your integration. Please see the [MIGRATE.md](./MIGRATE.md) for details.

## Change Log

You can view the history and the changes in the [CHANGELOG.md](./CHANGELOG.md)

## License

MIT, see the [LICENSE](./LICENSE) file for detail.
