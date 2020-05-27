if (!require.getConfig().paths.vs)  // for lte v1.2.0
  require.config({ paths: { 'vs': '_content/BlazorMonaco/lib/monaco-editor/min/vs' } });
window.blazorMonaco = window.blazorMonaco || {};
window.blazorMonaco.editors = [];

window.blazorMonaco.editor = {

  //#region Static methods

  colorize: function (text, languageId, options) {
    return monaco.editor.colorize(text, languageId, options);
  },

  colorizeElement: function (elementId, options) {
    return monaco.editor.colorizeElement(document.getElementById(elementId), options);
  },

  colorizeModelLine: function (uriStr, lineNumber, tabSize) {
    var model = this.getModelObj(uriStr);
    if (model == null)
      return null;
    return monaco.editor.colorizeModelLine(model, lineNumber, tabSize);
  },

  create: function (id, options) {
    if (window.blazorMonaco.editors.find(e => e.id === id))
      return;

    if (options == null)
      options = {};

    if (typeof monaco === 'undefined')
      console.log("WARNING : Please check that you have the script tag for editor.main.js in your index.html file");

    var editor = monaco.editor.create(document.getElementById(id), options);
    window.blazorMonaco.editors.push({ id: id, editor: editor });
  },

  createModel: function (value, language, uriStr) {
    var uri = monaco.Uri.parse(uriStr);
    var model = monaco.editor.createModel(value, language, uri);
    if (model == null)
      return null;

    return {
      id: model.id,
      uri: model.uri.toString()
    };
  },

  getModel: function (uriStr) {
    var uri = monaco.Uri.parse(uriStr);
    if (uri == null)
      return null;

    var model = monaco.editor.getModel(uri);
    if (model == null)
      return null;

    return {
      id: model.id,
      uri: model.uri.toString()
    };
  },

  getModelObj: function (uriStr) {
    var uri = monaco.Uri.parse(uriStr);
    if (uri == null)
      return null;

    return monaco.editor.getModel(uri);
  },

  getModels: function () {
    return monaco.editor.getModels().map(function (value) {
      return {
        id: value.id,
        uri: value.uri.toString()
      };
    });
  },

  remeasureFonts: function () {
    monaco.editor.remeasureFonts();
  },

  setModelLanguage: function (uriStr, languageId) {
    var model = this.getModelObj(uriStr);
    if (model == null)
      return null;
    return monaco.editor.setModelLanguage(model, languageId);
  },

  setTheme: function (theme) {
    monaco.editor.setTheme(theme);
    return true;
  },

  //#endregion

  //#region Instance methods

  getEditorById: function (id) {
    let editorHolder = window.blazorMonaco.editors.find(e => e.id === id);
    if (!editorHolder)
      throw "Couldn't find the editor with id: " + id + " editors.length: " + window.blazorMonaco.editors.length;
    else if (!editorHolder.editor)
      throw "editor is null for editorHolder: " + editorHolder;
    return editorHolder.editor;
  },

  addAction: function (id, actionId, label, keybindings, precondition, keybindingContext, contextMenuGroupId, contextMenuOrder, handler) {
    let editor = this.getEditorById(id);
    editor.addAction({
      id: actionId,
      label: label,
      keybindings: keybindings,
      precondition: precondition,
      keybindingContext: keybindingContext,
      contextMenuGroupId: contextMenuGroupId,
      contextMenuOrder: contextMenuOrder,
      run: function () {
        handler.invokeMethodAsync("ActionCallback", keybindings.join(';'));
      }
    });
  },

  addCommand: function (id, keyCode, handler) {
    let editor = this.getEditorById(id);
    editor.addCommand(keyCode, function () {
      handler.invokeMethodAsync("CommandCallback", keyCode);
    });
  },

  dispose: function (id) {
    let editor = this.getEditorById(id);
    editor.dispose();
  },

  focus: function (id) {
    let editor = this.getEditorById(id);
    editor.focus();
  },

  getContainerDomNodeId: function (id) {
    let editor = this.getEditorById(id);
    let containerNode = editor.getContainerDomNode();
    if (containerNode == null)
      return null;
    return containerNode.id;
  },

  getContentHeight: function (id) {
    let editor = this.getEditorById(id);
    return editor.getContentHeight();
  },

  getContentWidth: function (id) {
    let editor = this.getEditorById(id);
    return editor.getContentWidth();
  },

  getEditorType: function (id) {
    let editor = this.getEditorById(id);
    return editor.getEditorType();
  },

  getInstanceModel: function (id) {
    let editor = this.getEditorById(id);
    var model = editor.getModel();
    if (model == null)
      return null;

    return {
      id: model.id,
      uri: model.uri.toString()
    };
  },

  getLayoutInfo: function (id) {
    let editor = this.getEditorById(id);
    return editor.getLayoutInfo();
  },

  getOffsetForColumn: function (id, lineNumber, column) {
    let editor = this.getEditorById(id);
    return editor.getOffsetForColumn(lineNumber, column);
  },

  getOption: function (id, optionId) {
    let editor = this.getEditorById(id);
    return JSON.stringify(editor.getOption(optionId));
  },

  getOptions: function (id) {
    let editor = this.getEditorById(id);
    return editor.getOptions()._values.map(function (value) {
      return JSON.stringify(value);
    });
  },

  getPosition: function (id) {
    let editor = this.getEditorById(id);
    return editor.getPosition();
  },

  getRawOptions: function (id) {
    let editor = this.getEditorById(id);
    return editor.getRawOptions();
  },

  getScrollHeight: function (id) {
    let editor = this.getEditorById(id);
    return editor.getScrollHeight();
  },

  getScrollLeft: function (id) {
    let editor = this.getEditorById(id);
    return editor.getScrollHeight();
  },

  getScrollTop: function (id) {
    let editor = this.getEditorById(id);
    return editor.getScrollHeight();
  },

  getScrollWidth: function (id) {
    let editor = this.getEditorById(id);
    return editor.getScrollHeight();
  },

  getScrolledVisiblePosition: function (id, position) {
    let editor = this.getEditorById(id);
    return editor.getScrolledVisiblePosition(position);
  },

  getSelection: function (id) {
    let editor = this.getEditorById(id);
    return editor.getSelection();
  },

  getSelections: function (id) {
    let editor = this.getEditorById(id);
    return editor.getSelections();
  },

  getTargetAtClientPoint: function (id, clientX, clientY) {
    let editor = this.getEditorById(id);
    return editor.getTargetAtClientPoint(clientX, clientY);
  },

  getTopForLineNumber: function (id, lineNumber) {
    let editor = this.getEditorById(id);
    return editor.getTopForLineNumber(lineNumber);
  },

  getTopForPosition: function (id, lineNumber, column) {
    let editor = this.getEditorById(id);
    return editor.getTopForPosition(lineNumber, column);
  },

  getValue: function (id) {
    let editor = this.getEditorById(id);
    return editor.getValue();
  },

  getVisibleColumnFromPosition: function (id, position) {
    let editor = this.getEditorById(id);
    return editor.getVisibleColumnFromPosition(position);
  },

  getVisibleRanges: function (id) {
    let editor = this.getEditorById(id);
    return editor.getVisibleRanges();
  },

  hasTextFocus: function (id) {
    let editor = this.getEditorById(id);
    return editor.hasTextFocus();
  },

  hasWidgetFocus: function (id) {
    let editor = this.getEditorById(id);
    return editor.hasWidgetFocus();
  },

  layout: function (id, dimension) {
    let editor = this.getEditorById(id);
    editor.layout(dimension);
  },

  pushUndoStop: function (id) {
    let editor = this.getEditorById(id);
    return editor.pushUndoStop();
  },

  render: function (id, forceRedraw) {
    let editor = this.getEditorById(id);
    editor.render(forceRedraw);
  },

  revealLine: function (id, lineNumber, scrollType) {
    let editor = this.getEditorById(id);
    editor.revealLine(lineNumber, scrollType);
  },

  revealLineInCenter: function (id, lineNumber, scrollType) {
    let editor = this.getEditorById(id);
    editor.revealLineInCenter(lineNumber, scrollType);
  },

  revealLineInCenterIfOutsideViewport: function (id, lineNumber, scrollType) {
    let editor = this.getEditorById(id);
    editor.revealLineInCenterIfOutsideViewport(lineNumber, scrollType);
  },

  revealLines: function (id, startLineNumber, endLineNumber, scrollType) {
    let editor = this.getEditorById(id);
    editor.revealLine(startLineNumber, endLineNumber, scrollType);
  },

  revealLinesInCenter: function (id, startLineNumber, endLineNumber, scrollType) {
    let editor = this.getEditorById(id);
    editor.revealLinesInCenter(startLineNumber, endLineNumber, scrollType);
  },

  revealLinesInCenterIfOutsideViewport: function (id, startLineNumber, endLineNumber, scrollType) {
    let editor = this.getEditorById(id);
    editor.revealLinesInCenterIfOutsideViewport(startLineNumber, endLineNumber, scrollType);
  },

  revealPosition: function (id, position, scrollType) {
    let editor = this.getEditorById(id);
    editor.revealPosition(position, scrollType);
  },

  revealPositionInCenter: function (id, position, scrollType) {
    let editor = this.getEditorById(id);
    editor.revealPositionInCenter(position, scrollType);
  },

  revealPositionInCenterIfOutsideViewport: function (id, position, scrollType) {
    let editor = this.getEditorById(id);
    editor.revealPositionInCenterIfOutsideViewport(position, scrollType);
  },

  revealRange: function (id, range, scrollType) {
    let editor = this.getEditorById(id);
    editor.revealRange(range, scrollType);
  },

  revealRangeAtTop: function (id, range, scrollType) {
    let editor = this.getEditorById(id);
    editor.revealRangeAtTop(range, scrollType);
  },

  revealRangeInCenter: function (id, range, scrollType) {
    let editor = this.getEditorById(id);
    editor.revealRangeInCenter(range, scrollType);
  },

  revealRangeInCenterIfOutsideViewport: function (id, range, scrollType) {
    let editor = this.getEditorById(id);
    editor.revealRangeInCenterIfOutsideViewport(range, scrollType);
  },

  setEventListener: function (id, eventName, handler) {
    let editor = this.getEditorById(id);

    let listener = function (e) {
      handler.invokeMethodAsync("EventCallback", eventName, JSON.stringify(e));
    };

    switch (eventName) {
      case "OnDidCompositionEnd": editor.onDidCompositionEnd(listener); break;
      case "OnDidCompositionStart": editor.onDidCompositionStart(listener); break;
      case "OnContextMenu": editor.onContextMenu(listener); break;
      case "OnDidBlurEditorText": editor.onDidBlurEditorText(listener); break;
      case "OnDidBlurEditorWidget": editor.onDidBlurEditorWidget(listener); break;
      case "OnDidChangeConfiguration": editor.onDidChangeConfiguration(listener); break;
      case "OnDidChangeCursorPosition": editor.onDidChangeCursorPosition(listener); break;
      case "OnDidChangeCursorSelection": editor.onDidChangeCursorSelection(listener); break;
      case "OnDidContentSizeChange": editor.onDidContentSizeChange(listener); break;
      case "OnDidDispose": editor.onDidDispose(listener); break;
      case "OnDidFocusEditorText": editor.onDidFocusEditorText(listener); break;
      case "OnDidFocusEditorWidget": editor.onDidFocusEditorWidget(listener); break;
      case "OnDidLayoutChange": editor.onDidLayoutChange(listener); break;
      case "OnDidPaste": editor.onDidPaste(listener); break;
      case "OnDidScrollChange": editor.onDidScrollChange(listener); break;
      case "OnKeyDown": editor.onKeyDown(listener); break;
      case "OnKeyUp": editor.onKeyUp(listener); break;
      case "OnMouseDown": editor.onMouseDown(listener); break;
      case "OnMouseLeave": editor.onMouseLeave(listener); break;
      case "OnMouseMove": editor.onMouseMove(listener); break;
      case "OnMouseUp": editor.onMouseUp(listener); break;
    }
  },

  setInstanceModel: function (id, uriStr) {
    var model = this.getModelObj(uriStr);
    if (model == null)
      return;

    let editor = this.getEditorById(id);
    editor.setModel(model);
  },

  setPosition: function (id, position) {
    let editor = this.getEditorById(id);
    editor.setPosition(position);
  },

  setScrollLeft: function (id, newScrollLeft) {
    let editor = this.getEditorById(id);
    editor.setScrollLeft(newScrollLeft);
  },

  setScrollPosition: function (id, newScrollLeft, newScrollTop) {
    let editor = this.getEditorById(id);
    editor.setScrollPosition({
      scrollLeft: newScrollLeft,
      scrollTop: newScrollTop
    });
  },

  setScrollTop: function (id, newScrollTop) {
    let editor = this.getEditorById(id);
    editor.setScrollTop(newScrollTop);
  },

  setSelection: function (id, selection) {
    let editor = this.getEditorById(id);
    editor.setSelection(selection);
  },

  setSelections: function (id, selection) {
    let editor = this.getEditorById(id);
    editor.setSelections(selection);
  },

  setValue: function (id, value) {
    let editor = this.getEditorById(id);
    editor.setValue(value);
  },

  trigger: function (id, source, handlerId, payload) {
    let editor = this.getEditorById(id);
    editor.trigger(source, handlerId, payload);
  },

  updateOptions: function (id, options) {
    let editor = this.getEditorById(id);
    editor.updateOptions(options);
  },

  //#endregion

  //#region decorations

  decorations: [],

  setDeltaDecoration: function (id, range, options) {
    let editor = this.getEditorById(id);
    this.decorations.push(editor.deltaDecorations([], [{ range: range, options: options }]));
  },

  resetDeltaDecorations: function (id) {
    this.decorations.forEach(dec =>
      window.blazorMonaco.editor.getEditorById(id).deltaDecorations(dec, [{ range: new monaco.Range(1, 1, 1, 1), options: {} }]));
  }

  //#endregion
}
