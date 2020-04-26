<p align="center"><img src="https://raw.githubusercontent.com/serdarciplak/BlazorMonaco/master/BlazorMonaco/icon.png" width="150" height="150" /></p>

<p align="center">
<a href="https://www.nuget.org/packages/BlazorMonaco/"><img src="https://buildstats.info/nuget/BlazorMonaco" /></a>
</p>

<h1 align="center">BlazorMonaco</h1>

Blazor component for Microsoft's [Monaco Editor](https://github.com/Microsoft/monaco-editor) which powers Visual Studio Code.

Only a little more than the basic functionality of Monaco Editor is currently supported, but the package will be updated regularly to cover all the features and use cases soon. Any contributions, comments or suggestions are greatly welcome. Please feel free to contact me at [@serdarciplak](https://twitter.com/serdarciplak) or via GitHub.

Current version of BlazorMonaco :
* Works with Monaco Editor v0.20.0
* Built and tested for Blazor v3.2.0-preview5

## Demo

You may see a working example [here](https://serdarciplak.github.io/BlazorMonaco/)

## Get Started

* Add the [NuGet](https://www.nuget.org/packages/BlazorMonaco/) package to your Blazor client project.
```
dotnet add package BlazorMonaco
// or
Install-Package BlazorMonaco
```

* Add the following using directives to your root `_Imports.razor` file, or any other .razor file where you want to add a Monaco Editor.
```csharp
@using BlazorMonaco
@using BlazorMonaco.Bridge
```

* Add the below Javascript and CSS links to your `index.html` file.
```html
<head>
    <link href="_content/BlazorMonaco/lib/monaco-editor/min/vs/editor/editor.main.css" rel="stylesheet" />
</head>
<body>
    <app></app>
    ...
    <script src="_content/BlazorMonaco/lib/monaco-editor/min/vs/loader.js"></script>
    <script src="_content/BlazorMonaco/jsInterop.js"></script>
	<script src="_content/BlazorMonaco/lib/monaco-editor/min/vs/editor/editor.main.js"></script>
    <script src="_framework/blazor.webassembly.js"></script>
</body>
```

* Add a `MonacoEditor` component in your .razor file and configure it as you need.
```html
<MonacoEditor Id="any-id-string" />
```

## Customization

### Providing custom options

* If you provide a method that returns an `StandaloneEditorConstructionOptions` instance to the `ConstructionOptions` parameter, it will be called at creation and the return value will be used as the initial editor options.
```html
<MonacoEditor Id="any-id-string" ConstructionOptions="GetConstructionOptions" />
```

* In your razor file's `@code` block, add the method you provided. Return the initial options and customize the editor instance as you need. You can see all available [options](https://microsoft.github.io/monaco-editor/api/interfaces/monaco.editor.istandaloneeditorconstructionoptions.html) at Monaco Editor's documentation.
```csharp
private StandaloneEditorConstructionOptions EditorConstructionOptions(MonacoEditor editor)
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
* You can add listeners to editor events like OnDidKeyUp or OnDidPaste for any custom job to be done when that event occurs.
```html
<MonacoEditor Id="any-id-string" OnDidChangeCursorPosition="EditorDidChangeCursorPosition" />
```

* Add your event listener method in your razor file's `@code` block.
```csharp
private void EditorDidChangeCursorPosition(CursorPositionChangedEvent eventArgs)
{
	Console.WriteLine("EditorDidChangeCursorPosition");
}
```

### Css styling
* When you provide a css class name in the `CssClass` property of the Monaco Editor instance, it will be added to the corresponding html div tag. So you can customize how anything looks in your css files.
```html
<MonacoEditor Id="any-id-string" CssClass="my-editor-class" />
```

## Change log
History and changes can be located in the [CHANGELOG.md](https://github.com/serdarciplak/BlazorMonaco/blob/master/CHANGELOG.md)

## License
MIT, see the [LICENSE](https://github.com/serdarciplak/BlazorMonaco/blob/master/LICENSE) file for detail.
