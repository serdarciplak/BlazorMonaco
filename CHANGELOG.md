# Change Log

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
