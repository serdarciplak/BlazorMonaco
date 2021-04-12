# Change Log

## 2.1.0
* Added ExecuteEdits method.
* Added missing properties to DiffEditorConstructionOptions.
* Added LineNumbersLambda property to editor construction options to set line numbers via a lambda.
* Changed the type of the payload parameter from JsomElement to Object in Editor.Trigger method for convenience.
* Fixed the return type of GetAllDecorations method.
* Fixed a null pointer exception that occurs when TextModels are null.

## 2.0.0
* Updated to Monaco Editor v0.22.3
* Added TextModel methods.
* Breaking Changes:
  * Merged 'BlazorMonaco.Bridge' namespace into 'BlazorMonaco' namespace
  * Renamed 'CommentOptions' class to 'EditorCommentsOptions'
  * Renamed 'FindOptions' class to 'EditorFindOptions'
  * Renamed 'HoverOptions' class to 'EditorHoverOptions'
  * Renamed 'LightbulbOptions' class to 'EditorLightbulbOptions'
  * Renamed 'MinimapOptions' class to 'EditorMinimapOptions'
  * Renamed 'ParameterHintOptions' class to 'EditorParameterHintOptions'
  * Renamed 'ScrollbarOptions' class to 'EditorScrollbarOptions'
  * Restructured 'EditorLayoutInfo'
  * Removed some options and properties that are removed by Monaco Editor itself

## 1.6.1
* Fixed a bug that causes event listeners to trigger twice.

## 1.6.0
* Added support for net5.0

## 1.5.0
* Added support for editor model events.
  * OnDidChangeModel
  * OnDidChangeModelContent
  * OnDidChangeModelDecorations
  * OnDidChangeModelLanguage
  * OnDidChangeModelLanguageConfiguration
  * OnDidChangeModelOptions
* Editor Id is auto-generated if it's not provided.
  
## 1.4.0
* Diff Editor support is finally here. This was possibly the biggest feature missing in BlazorMonaco.
* Added DefineTheme method
* Fixed some minor bugs

## 1.3.1
* Fixed a bug where recreating the editor instance fails.

## 1.3.0
* Added support for DeltaDecorations and ResetDeltaDecorations.

## 1.2.2
* Removed hardcoded style from the monaco editor container
* Replaced preview dependencies with their latest stable versions
* Updated for Blazor v3.2.0

## 1.2.1
* You can now use your custom monaco-editor setup instead of the packed unmodified version.
* Updated for Blazor v3.2.0-rc1

## 1.2.0
* Added support for static methods.
  * Colorize
  * ColorizeElement
  * ColorizeModelLine
  * CreateModel
  * GetModel
  * GetModels
  * RemeasureFonts
  * SetModelLanguage
* Added support for editor instance methods.
  * Dispose
  * Focus
  * GetContainerDomNodeId
  * GetContentHeight
  * GetContentWidth
  * GetEditorType
  * GetLayoutInfo
  * GetOffsetForColumn
  * GetOption
  * GetPosition
  * GetRawOptions
  * GetScrollHeight
  * GetScrollLeft
  * GetScrollTop
  * GetScrollWidth
  * GetScrolledVisiblePosition
  * GetSelection
  * GetSelections
  * GetTargetAtClientPoint
  * GetTopForLineNumber
  * GetTopForPosition
  * GetVisibleColumnFromPosition
  * GetVisibleRanges
  * HasTextFocus
  * HasWidgetFocus
  * Layout
  * PushUndoStop
  * Render
  * RevealLine
  * RevealLineInCenter
  * RevealLineInCenterIfOutsideViewport
  * RevealLines
  * RevealLinesInCenter
  * RevealLinesInCenterIfOutsideViewport
  * RevealPosition
  * RevealPositionInCenter
  * RevealPositionInCenterIfOutsideViewport
  * RevealRange
  * RevealRangeAtTop
  * RevealRangeInCenter
  * RevealRangeInCenterIfOutsideViewport
  * SetPosition
  * SetScrollLeft
  * SetScrollPosition
  * SetScrollTop
  * SetSelection
  * SetSelections
  * Trigger
* Added support for editor events.
  * OnDidCompositionEnd
  * OnDidCompositionStart
* Bug fixes.

## 1.1.0
* Added support for editor events.
  * OnDidInit
  * OnContextMenu
  * OnDidBlurEditorText
  * OnDidBlurEditorWidget
  * OnDidChangeConfiguration
  * OnDidChangeCursorPosition
  * OnDidChangeCursorSelection
  * OnDidContentSizeChange
  * OnDidDispose
  * OnDidFocusEditorText
  * OnDidFocusEditorWidget
  * OnDidLayoutChange
  * OnDidPaste
  * OnDidScrollChange
  * OnKeyDown
  * OnKeyUp
  * OnMouseDown
  * OnMouseLeave
  * OnMouseMove
  * OnMouseUp
* Changed editor GetOptions parameter name to ConstructionOptions.
* Bug fixes.

## 1.0.3
* Updated for Blazor v3.2.0-preview5

## 1.0.2
* Bug fixes.

## 1.0.1
* Initial release.
