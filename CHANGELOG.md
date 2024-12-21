# Change Log

## 3.3.0
* Added support for **net9.0**
* Added support for `RegisterDocumentFormattingEditProvider`.
* Added async support for `RegisterCodeActionProvider` and `RegisterCompletionItemProvide`.
* Changed behaviour of overwriting options' value when recreating editor with the same ID. Now, the value is overwritten only if a new value is not provided.

## 3.2.0
* Updated to Monaco Editor **v0.46.0**
* Added support for **net8.0**

## 3.1.0
* Added support for model markers.
* Added support for `RegisterCodeActionProvider`.
* Added support for `RegisterCompletionItemProvider`.
* Added support for server-side Blazor apps.
* Bug fixes.

## 3.0.0
* Updated to Monaco Editor **v0.34.1**
* Added support for **net6.0** and **net7.0**
* Aligned with Monaco Editor's original namespace and class names.
* Cleaner integration.
* Improvements and several bug fixes.
* Breaking changes:
  * Please follow the migration guide [here](https://github.com/serdarciplak/BlazorMonaco/blob/master/MIGRATE.md).

## 2.1.0
* Added `ExecuteEdits` method.
* Added missing properties to `DiffEditorConstructionOptions`.
* Added `LineNumbersLambda` property to editor construction options to set line numbers via a lambda.
* Changed the type of the payload parameter from `JsonElement` to `Object` in `Editor.Trigger` method for convenience.
* Fixed the return type of the `GetAllDecorations` method.
* Fixed a null pointer exception that occurs when text models are null.

## 2.0.0
* Updated to Monaco Editor **v0.22.3**
* Added support for the `TextModel` methods.
* Breaking changes:
  * Please follow the migration guide [here](https://github.com/serdarciplak/BlazorMonaco/blob/master/MIGRATE.md).

## 1.6.1
* Fixed a bug that causes event listeners to trigger twice.

## 1.6.0
* Added support for **net5.0**

## 1.5.0
* Added support for the editor model events below.
  * `OnDidChangeModel`
  * `OnDidChangeModelContent`
  * `OnDidChangeModelDecorations`
  * `OnDidChangeModelLanguage`
  * `OnDidChangeModelLanguageConfiguration`
  * `OnDidChangeModelOptions`
* Editor `Id` is auto-generated if it's not provided.

## 1.4.0
* Diff Editor support is finally here. This was possibly the biggest feature missing in BlazorMonaco.
* Added support for `DefineTheme`.
* Fixed some minor bugs.

## 1.3.1
* Fixed a bug where recreating the editor instance fails.

## 1.3.0
* Added support for `DeltaDecorations` and `ResetDeltaDecorations`.

## 1.2.2
* Removed hardcoded style from the monaco editor container.
* Replaced preview dependencies with their latest stable versions.
* Updated for **Blazor v3.2.0**

## 1.2.1
* You can now use your custom monaco editor setup instead of the embedded version.
* Updated for **Blazor v3.2.0-rc1**

## 1.2.0
* Added support for the static methods below.
  * `Colorize`
  * `ColorizeElement`
  * `ColorizeModelLine`
  * `CreateModel`
  * `GetModel`
  * `GetModels`
  * `RemeasureFonts`
  * `SetModelLanguage`
* Added support for the editor instance methods below.
  * `Dispose`
  * `Focus`
  * `GetContainerDomNodeId`
  * `GetContentHeight`
  * `GetContentWidth`
  * `GetEditorType`
  * `GetLayoutInfo`
  * `GetOffsetForColumn`
  * `GetOption`
  * `GetPosition`
  * `GetRawOptions`
  * `GetScrollHeight`
  * `GetScrollLeft`
  * `GetScrollTop`
  * `GetScrollWidth`
  * `GetScrolledVisiblePosition`
  * `GetSelection`
  * `GetSelections`
  * `GetTargetAtClientPoint`
  * `GetTopForLineNumber`
  * `GetTopForPosition`
  * `GetVisibleColumnFromPosition`
  * `GetVisibleRanges`
  * `HasTextFocus`
  * `HasWidgetFocus`
  * `Layout`
  * `PushUndoStop`
  * `Render`
  * `RevealLine`
  * `RevealLineInCenter`
  * `RevealLineInCenterIfOutsideViewport`
  * `RevealLines`
  * `RevealLinesInCenter`
  * `RevealLinesInCenterIfOutsideViewport`
  * `RevealPosition`
  * `RevealPositionInCenter`
  * `RevealPositionInCenterIfOutsideViewport`
  * `RevealRange`
  * `RevealRangeAtTop`
  * `RevealRangeInCenter`
  * `RevealRangeInCenterIfOutsideViewport`
  * `SetPosition`
  * `SetScrollLeft`
  * `SetScrollPosition`
  * `SetScrollTop`
  * `SetSelection`
  * `SetSelections`
  * `Trigger`
* Added support for the editor events below.
  * `OnDidCompositionEnd`
  * `OnDidCompositionStart`
* Bug fixes.

## 1.1.0
* Added support for the editor events below.
  * `OnDidInit`
  * `OnContextMenu`
  * `OnDidBlurEditorText`
  * `OnDidBlurEditorWidget`
  * `OnDidChangeConfiguration`
  * `OnDidChangeCursorPosition`
  * `OnDidChangeCursorSelection`
  * `OnDidContentSizeChange`
  * `OnDidDispose`
  * `OnDidFocusEditorText`
  * `OnDidFocusEditorWidget`
  * `OnDidLayoutChange`
  * `OnDidPaste`
  * `OnDidScrollChange`
  * `OnKeyDown`
  * `OnKeyUp`
  * `OnMouseDown`
  * `OnMouseLeave`
  * `OnMouseMove`
  * `OnMouseUp`
* Renamed the `GetOptions` editor parameter as `ConstructionOptions`.
* Bug fixes.

## 1.0.3
* Updated for **Blazor v3.2.0-preview5**

## 1.0.2
* Bug fixes.

## 1.0.1
* Initial release.
