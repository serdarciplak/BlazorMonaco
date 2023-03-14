<p align="center"><img src="https://raw.githubusercontent.com/serdarciplak/BlazorMonaco/master/BlazorMonaco/icon.png" width="150" height="150" /></p>

<p align="center">
<a href="https://www.nuget.org/packages/BlazorMonaco/"><img src="https://buildstats.info/nuget/BlazorMonaco" /></a>
</p>

<h1 align="center">BlazorMonaco</h1>

Blazor component for Microsoft's [Monaco Editor](https://github.com/Microsoft/monaco-editor) which powers Visual Studio Code.

Some less-frequently used Monaco Editor features are currently missing, but the whole basic feature set is supported. The package will be updated to cover all the features and use cases over time. Any contributions, comments or suggestions are greatly welcome. Please feel free to contact me at [twitter/serdarciplak](https://twitter.com/serdarciplak) or via GitHub.

Current version of BlazorMonaco :
* Uses `Monaco Editor v0.34.1`
* Supports `netstandard2.0`, `net5.0`, `net6.0` and `net7.0`

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
<script src="_content/BlazorMonaco/lib/monaco-editor/min/vs/loader.js"></script>
<script src="_content/BlazorMonaco/lib/monaco-editor/min/vs/editor/editor.main.js"></script>
```

* Add `BlazorMonacoJsRuntime` to your DI configuration:
```
builder.Services.AddSingleton<BlazorMonacoJsRuntime>();
```

* Everything resides in two namespaces. So, you can add the following using directives to your root `_Imports.razor` file, or any other place you may need them.
```
@using BlazorMonaco
@using BlazorMonaco.Editor
```

## Code Editor

### Adding an editor instance

* To add a new editor instance, you just need to add a `StandaloneCodeEditor` component in your razor file.
```xml
<StandaloneCodeEditor Id="my-editor-instance-id" />
```

### Providing custom options

* If you set the `ConstructionOptions` parameter and provide a method that returns a `StandaloneEditorConstructionOptions` instance, it will be called when the instance is created and those options will be used to initialize the editor.
```xml
<StandaloneCodeEditor Id="my-editor-instance-id" ConstructionOptions="EditorConstructionOptions" />
```

* Then, add that method to your razor file's `@code` block and set the initial options for your editor instance.
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

* You can add listeners to editor events like `OnDidKeyUp` or `OnDidPaste` for any custom job to be done when that event occurs.
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

* `StandaloneDiffEditor` class has two properties named `OriginalEditor` and `ModifiedEditor` to access the inner editors. They're regular code editors. So, you can use them like any other `StandaloneCodeEditor` instances.

* You can register to inner editor events via the helper event callback parameters of the `StandaloneDiffEditor`.
```xml
<StandaloneDiffEditor Id="my-editor-instance-id" OnKeyUpOriginal="OnKeyUpOriginal" OnKeyUpModified="OnKeyUpModified" />
```

* And then, add the callback methods to your razor file's `@code` block.
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

### Custom themes

* You can define custom themes using the `DefineTheme` method. Just make sure that you don't call `DefineTheme` before any editor instance is initialized. BlazorMonaco needs an `IJSRuntime` instance to call JavaScript methods and it gets one when the first instance is initialized.

```csharp
await BlazorMonacoGlobals.DefineTheme("my-custom-theme", new StandaloneThemeData
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

* After defining your custom theme, you can call the `SetTheme` method at any time with your custom theme name to set it active.

```csharp
await BlazorMonacoGlobals.SetTheme("my-custom-theme");
```

### DeltaDecorations

* You can add, edit and remove decorations to editors via the `DeltaDecorations` and `ResetDeltaDecorations` methods.
```csharp
private async Task EditorOnDidInit()
{
	var newDecorations = new ModelDeltaDecoration[]
	{
		new ModelDeltaDecoration
		{
			Range = new BlazorMonaco.Range(3,1,3,1),
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

### Using custom Monaco Editor setup

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
