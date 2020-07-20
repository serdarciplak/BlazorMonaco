<p align="center"><img src="https://raw.githubusercontent.com/serdarciplak/BlazorMonaco/master/BlazorMonaco/icon.png" width="150" height="150" /></p>

<p align="center">
<a href="https://www.nuget.org/packages/BlazorMonaco/"><img src="https://buildstats.info/nuget/BlazorMonaco" /></a>
</p>

<h1 align="center">BlazorMonaco</h1>

Blazor component for Microsoft's [Monaco Editor](https://github.com/Microsoft/monaco-editor) which powers Visual Studio Code.

Not the complete set but most of the Monaco Editor functionality is currently supported. The package will be updated regularly to cover all the features and use cases soon. Any contributions, comments or suggestions are greatly welcome. Please feel free to contact me at [@serdarciplak](https://twitter.com/serdarciplak) or via GitHub.

Current version of BlazorMonaco :
* Works with Monaco Editor v0.20.0
* Built and tested for Blazor v3.2.0

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
    ...
    <script src="_content/BlazorMonaco/lib/monaco-editor/min/vs/loader.js"></script>
    <script>require.config({ paths: { 'vs': '_content/BlazorMonaco/lib/monaco-editor/min/vs' } });</script>
    <script src="_content/BlazorMonaco/lib/monaco-editor/min/vs/editor/editor.main.js"></script>
    <script src="_content/BlazorMonaco/jsInterop.js"></script>
    ...
</body>
```

## Code Editor

* Add a `MonacoEditor` component in your .razor file and configure it as you need.
```html
<MonacoEditor Id="any-id-string" />
```

### Providing custom options

* If you provide a method that returns an `StandaloneEditorConstructionOptions` instance to the `ConstructionOptions` parameter, it will be called at creation and the return value will be used as the initial editor options.
```html
<MonacoEditor Id="any-id-string" ConstructionOptions="EditorConstructionOptions" />
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

## Diff Editor

* Add a `MonacoDiffEditor` component in your .razor file and configure it as you need.
```html
<MonacoDiffEditor Id="any-id-string" />
```

### Access to inner editor instances

* `MonacoDiffEditor` class has two properties named `OriginalEditor` and `ModifiedEditor` to access the inner editors. They're regular code editors. So, you can use them like any other `MonacoEditor` instance.

### Access to inner editor events

* You can register to inner editor events via the helper EventCallback parameters of the MonacoDiffEditor.
```html
<MonacoDiffEditor Id="any-id-string" OnKeyUpOriginal="OnKeyUpOriginal" OnKeyUpModified="OnKeyUpModified" />
```

* And add the callback methods to your razor file's `@code` block.
```csharp
private void OnKeyUpOriginal(KeyboardEvent keyboardEvent)
{
	Console.WriteLine("OnKeyUpOriginal : " + keyboardEvent.Code);
}
private void OnKeyUpModified(KeyboardEvent keyboardEvent)
{
	Console.WriteLine("OnKeyUpModified : " + keyboardEvent.Code);
}
```

## Customization

### DeltaDecorations

* You can add, edit and remove decorations to the editor via DeltaDecorations and ResetDeltaDecorations method.
```csharp
private async Task EditorOnDidInit(MonacoEditorBase editor)
{
	var newDecorations = new ModelDeltaDecoration[]
	{
		new ModelDeltaDecoration
		{
			Range = new BlazorMonaco.Bridge.Range(3,1,3,1),
			Options = new ModelDecorationOptions
			{
				IsWholeLine = true,
				ClassName = "decorationContentClass",
				GlyphMarginClassName = "decorationGlyphMarginClass"
			}
		}
	};

	decorationIds = await _editor.DeltaDecorations(null, newDecorations);
	// You can now use 'decorationIds' to change or remove the decorations
}
```

### Css styling

* When you provide a css class name in the `CssClass` property of the Monaco Editor instance, it will be added to the corresponding html div tag. So you can customize how anything looks in your css files.
```html
<MonacoEditor Id="any-id-string" CssClass="my-editor-class" />
```

### Custom themes

* You can define custom themes using the `DefineTheme` static method. Just make sure that you don't call `DefineTheme` before any editor instance is initialized. BlazorMonaco needs an `IJSRuntime` instance to call JavaScript methods and it gets one when the first instance is initialized.

```csharp
await MonacoEditorBase.DefineTheme("my-custom-theme", new StandaloneThemeData
{
	Base = "vs-dark",
	Inherit = true,
	Rules = new List<TokenThemeRule>
	{
		new TokenThemeRule { Background = "363636", Foreground = "E0E0E0" },
		new TokenThemeRule { Token = "keyword", Foreground = "59ADFF" },
		new TokenThemeRule { Token = "operator.sql", Foreground = "59ADFF" },
		new TokenThemeRule { Token = "number", Foreground = "66CC66" },
		new TokenThemeRule { Token = "string.sql", Foreground = "E65C5C" },
		new TokenThemeRule { Token = "comment", Foreground = "7A7A7A" }
	},
	Colors = new Dictionary<string, string>
	{
		["editor.background"] = "#363636",
		["editorCursor.foreground"] = "#E0E0E0",
		["editorLineNumber.foreground"] = "#7A7A7A"
	}
});
```

* After defining your custom theme, you can call `SetTheme` at any time with your custom theme name to set it active.

```csharp
await MonacoEditorBase.SetTheme("my-custom-theme");
```

### Using custom Monaco Editor setup

* If you've made changes to Monaco Editor, like adding a custom language, and need to use this edited version instead of the unmodified version packed with BlazorMonaco, just change the paths to monaco editor resources in your `index.html` file.
```html
<head>
    <link href="my-path/monaco-editor/min/vs/editor/editor.main.css" rel="stylesheet" />
</head>
<body>
    ...
    <script src="my-path/monaco-editor/min/vs/loader.js"></script>
    <script>require.config({ paths: { 'vs': 'my-path/monaco-editor/min/vs' } });</script>
    <script src="my-path/monaco-editor/min/vs/editor/editor.main.js"></script>
    ...
</body>
```

## Documentation

As BlazorMonaco is just a bridge between Javascript and Blazor, you can use Monaco Editor's [documentation](https://microsoft.github.io/monaco-editor/api/index.html).

## Change log

History and changes can be located in the [CHANGELOG.md](https://github.com/serdarciplak/BlazorMonaco/blob/master/CHANGELOG.md)

## License

MIT, see the [LICENSE](https://github.com/serdarciplak/BlazorMonaco/blob/master/LICENSE) file for detail.
