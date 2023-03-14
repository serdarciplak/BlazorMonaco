import * as modelInterop from './model-interop.js';

const editors = [];
const undefinedType = typeof undefined;

//#region Static methods

/**
 * ? It could be simplied, couldn't it ?
export const colorize = monaco.editor.colorize;
export const colorizeElement = monaco.editor.colorizeElement;
 */
export const colorize = async (text, languageId, options) => await monaco.editor.colorize(text, languageId, options);

export const colorizeElement = async (elementId, options) => await monaco.editor.colorizeElement(document.getElementById(elementId), options);

export const colorizeModelLine = (uriStr, lineNumber, tabSize) => {
    const model = modelInterop.getModel(uriStr);
    if (!model)
        return null;
    return monaco.editor.colorizeModelLine(model, lineNumber, tabSize);
};

export const create = (id, options, override, dotnetRef) => {
    options = options || {};

    const oldEditor = getEditor(id, true);
    if (!!oldEditor) {
        options.value = oldEditor.getValue();
        editors.splice(editors.findIndex(item => item.id === id), 1);
        oldEditor.dispose();
    }

    if (typeof monaco === undefinedType)
        console.log("WARNING : Please check that you have the script tag for editor.main.js in your index.html file");

    if (options.lineNumbers === "function") {
        options.lineNumbers = (lineNumber) => dotnetRef.invokeMethod("LineNumbersCallback", lineNumber);
    }

    const editor = monaco.editor.create(document.getElementById(id), options, override);
    editors.push({ id, editor, dotnetRef });
};

export const createDiffEditor = (id, options, override, dotnetRef, dotnetRefOriginal, dotnetRefModified) => {
    options = options || {};

    const oldEditor = getEditor(id, true);
    let oldModel = null;
    if (!!oldEditor) {
        oldModel = oldEditor.getModel();

        editors.splice(editors.findIndex(item => item.id === id + "_original"), 1);
        editors.splice(editors.findIndex(item => item.id === id + "_modified"), 1);
        editors.splice(editors.findIndex(item => item.id === id), 1);
        oldEditor.dispose();
    }

    if (typeof monaco === undefinedType)
        console.log("WARNING : Please check that you have the script tag for editor.main.js in your index.html file");

    if (options.lineNumbers === "function") {
        options.lineNumbers = (lineNumber) => dotnetRef.invokeMethod("LineNumbersCallback", lineNumber);
    }

    const editor = monaco.editor.createDiffEditor(document.getElementById(id), options, override);
    editors.push({ id, editor, dotnetRef });
    editors.push({ id: id + "_original", editor: editor.getOriginalEditor(), dotnetRef: dotnetRefOriginal });
    editors.push({ id: id + "_modified", editor: editor.getModifiedEditor(), dotnetRef: dotnetRefModified });

    if (!oldModel)
        editor.setModel(oldModel);
};

export const createModel = (value, language, uriStr) => {
    //uri is the key; if no uri exists create one
    uriStr = uriStr || "generatedUriKey_" + uuidv4();

    const uri = monaco.Uri.parse(uriStr);
    const model = monaco.editor.createModel(value, language, uri);
    if (!model)
        return null;

    return {
        id: model.id,
        uri: model.uri.toString()
    };
};

export const defineTheme = monaco.editor.defineTheme;

export const getModel = (uriStr) => {
    const model = modelInterop.getModel(uriStr);
    if (!model)
        return null;

    return {
        id: model.id,
        uri: model.uri.toString()
    };
};

export const getModels = () => monaco.editor.getModels().map((value) => ({
    id: value.id,
    uri: value.uri.toString()
}));

export const remeasureFonts = monaco.editor.remeasureFonts;

export const setModelLanguage = (uriStr, languageId) => {
    const model = modelInterop.getModel(uriStr);
    if (!model)
        return null;
    return monaco.editor.setModelLanguage(model, languageId);
};

export const setTheme = (theme) => {
    monaco.editor.setTheme(theme);
    return true;
};

export const getEditorHolder = (id, unobstrusive = false) => {
    const editorHolder = editors.find(e => e.id === id);
    if (!editorHolder) {
        if (unobstrusive) {
            console.log("WARNING : Couldn't find the editor with id: " + id + " editors.length: " + editors.length);
            return null;
        }
        throw "Couldn't find the editor with id: " + id + " editors.length: " + editors.length;
    }
    else if (!editorHolder.editor) {
        if (unobstrusive) {
            console.log("WARNING : editor is null for editorHolder: " + editorHolder);
            return null;
        }
        throw "editor is null for editorHolder: " + editorHolder;
    }
    return editorHolder;
};

export const getEditor = (id, unobstrusive = false) => {
    const editorHolder = getEditorHolder(id, unobstrusive);
    return editorHolder?.editor;
};

//#endregion

//#region Instance methods

export const addAction = (id, actionDescriptor) => {
    const editorHolder = getEditorHolder(id);
    editorHolder.editor.addAction({
        ...actionDescriptor,
        run: (editor, args) => editorHolder.dotnetRef.invokeMethodAsync("ActionCallback", actionDescriptor.id)
    });
};

export const addCommand = (id, keybinding, context) => {
    const editorHolder = getEditorHolder(id);
    editorHolder.editor.addCommand(
        keybinding,
        (args) => editorHolder.dotnetRef.invokeMethodAsync("CommandCallback", keybinding),
        context
    );
};

export const deltaDecorations = (id, oldDecorations, newDecorations) => {
    const editor = getEditor(id);
    if (!editor)
        return null;
    return editor.deltaDecorations(oldDecorations, newDecorations);
};

export const dispose = (id) => {
    const editor = getEditor(id);
    if (!editor)
        return;
    editor.dispose();
};

export const focus = (id) => {
    const editor = getEditor(id);
    if (!editor)
        return;
    editor.focus();
};

export const executeEdits = (id, source, edits, endCursorState) => {
    const editorHolder = getEditorHolder(id);
    if (!editorHolder)
        return null;
    if (endCursorState === "function") {
        endCursorState = (inverseEditOperations) => editorHolder.dotnetRef.invokeMethod("ExecuteEditsCallback", inverseEditOperations);
    }
    return editorHolder.editor.executeEdits(source, edits, endCursorState);
};

export const getContainerDomNodeId = (id) => {
    const editor = getEditor(id);
    if (!editor)
        return null;
    const containerNode = editor.getContainerDomNode();
    if (!containerNode)
        return null;
    return containerNode.id;
};

export const getContentHeight = (id) => {
    const editor = getEditor(id);
    if (!editor)
        return null;
    return editor.getContentHeight();
};

export const getContentWidth = (id) => {
    const editor = getEditor(id);
    if (!editor)
        return null;
    return editor.getContentWidth();
};

export const getDomNodeId = (id) => {
    const editor = getEditor(id);
    if (!editor)
        return null;
    const domeNode = editor.getDomNode();
    if (!domeNode)
        return null;
    return domeNode.id;
};

export const getEditorType = (id) => {
    const editor = getEditor(id);
    if (!editor)
        return null;
    return editor.getEditorType();
};

export const getInstanceModel = (id) => {
    const editor = getEditor(id);
    if (!editor)
        return null;
    const model = editor.getModel();
    if (!model)
        return null;

    return {
        id: model.id,
        uri: model.uri.toString()
    };
};

export const getInstanceDiffModel = (id) => {
    const editor = getEditor(id);
    if (!editor)
        return null;
    const model = editor.getModel();
    if (!model)
        return null;

    return {
        original: {
            id: model.original.id,
            uri: model.original.uri.toString()
        },
        modified: {
            id: model.modified.id,
            uri: model.modified.uri.toString()
        }
    };
};

export const getLayoutInfo = (id) => {
    const editor = getEditor(id);
    if (!editor)
        return null;
    return editor.getLayoutInfo();
};

export const getOffsetForColumn = (id, lineNumber, column) => {
    const editor = getEditor(id);
    if (!editor)
        return null;
    return editor.getOffsetForColumn(lineNumber, column);
};

export const getOption = (id, optionId) => {
    const editor = getEditor(id);
    if (!editor)
        return null;
    return JSON.stringify(editor.getOption(optionId));
};

export const getOptions = (id) => {
    const editor = getEditor(id);
    if (!editor)
        return null;
    return editor.getOptions()._values.map((value) => JSON.stringify(value));
};

export const getPosition = (id) => {
    const editor = getEditor(id);
    if (!editor)
        return null;
    return editor.getPosition();
};

export const getRawOptions = (id) => {
    const editor = getEditor(id);
    if (!editor)
        return null;
    return editor.getRawOptions();
};

export const getScrollHeight = (id) => {
    const editor = getEditor(id);
    if (!editor)
        return null;
    return editor.getScrollHeight();
};

export const getScrollLeft = (id) => {
    const editor = getEditor(id);
    if (!editor)
        return null;
    return editor.getScrollLeft();
};

export const getScrollTop = (id) => {
    const editor = getEditor(id);
    if (!editor)
        return null;
    return editor.getScrollTop();
};

export const getScrollWidth = (id) => {
    const editor = getEditor(id);
    if (!editor)
        return null;
    return editor.getScrollWidth();
};

export const getScrolledVisiblePosition = (id, position) => {
    const editor = getEditor(id);
    if (!editor)
        return null;
    return editor.getScrolledVisiblePosition(position);
};

export const getSelection = (id) => {
    const editor = getEditor(id);
    if (!editor)
        return null;
    return editor.getSelection();
};

export const getSelections = (id) => {
    const editor = getEditor(id);
    if (!editor)
        return null;
    return editor.getSelections();
};

export const getTargetAtClientPoint = (id, clientX, clientY) => {
    const editor = getEditor(id);
    if (!editor)
        return null;
    return editor.getTargetAtClientPoint(clientX, clientY);
};

export const getTopForLineNumber = (id, lineNumber) => {
    const editor = getEditor(id);
    if (!editor)
        return null;
    return editor.getTopForLineNumber(lineNumber);
};

export const getTopForPosition = (id, lineNumber, column) => {
    const editor = getEditor(id);
    if (!editor)
        return null;
    return editor.getTopForPosition(lineNumber, column);
};

export const getValue = (id, preserveBOM, lineEnding) => {
    const editor = getEditor(id);
    if (!editor)
        return null;
    const options = null;
    if (preserveBOM != null && lineEnding != null) {
        options = {
            preserveBOM: preserveBOM,
            lineEnding: lineEnding
        };
    }
    return editor.getValue(options);
};

export const getVisibleColumnFromPosition = (id, position) => {
    const editor = getEditor(id);
    if (!editor)
        return null;
    return editor.getVisibleColumnFromPosition(position);
};

export const getVisibleRanges = (id) => {
    const editor = getEditor(id);
    if (!editor)
        return null;
    return editor.getVisibleRanges();
};

export const hasTextFocus = (id) => {
    const editor = getEditor(id);
    if (!editor)
        return null;
    return editor.hasTextFocus();
};

export const hasWidgetFocus = (id) => {
    const editor = getEditor(id);
    if (!editor)
        return null;
    return editor.hasWidgetFocus();
};

export const layout = (id, dimension) => {
    const editor = getEditor(id);
    if (!editor)
        return;
    editor.layout(dimension);
};

export const pushUndoStop = (id) => {
    const editor = getEditor(id);
    if (!editor)
        return null;
    return editor.pushUndoStop();
};

export const popUndoStop = (id) => {
    const editor = getEditor(id);
    if (!editor)
        return null;
    return editor.popUndoStop();
};

export const render = (id, forceRedraw) => {
    const editor = getEditor(id);
    if (!editor)
        return;
    editor.render(forceRedraw);
};

export const revealLine = (id, lineNumber, scrollType) => {
    const editor = getEditor(id);
    if (!editor)
        return;
    editor.revealLine(lineNumber, scrollType);
};

export const revealLineInCenter = (id, lineNumber, scrollType) => {
    const editor = getEditor(id);
    if (!editor)
        return;
    editor.revealLineInCenter(lineNumber, scrollType);
};

export const revealLineInCenterIfOutsideViewport = (id, lineNumber, scrollType) => {
    const editor = getEditor(id);
    if (!editor)
        return;
    editor.revealLineInCenterIfOutsideViewport(lineNumber, scrollType);
};

export const revealLines = (id, startLineNumber, endLineNumber, scrollType) => {
    const editor = getEditor(id);
    if (!editor)
        return;
    editor.revealLine(startLineNumber, endLineNumber, scrollType);
};

export const revealLinesInCenter = (id, startLineNumber, endLineNumber, scrollType) => {
    const editor = getEditor(id);
    if (!editor)
        return;
    editor.revealLinesInCenter(startLineNumber, endLineNumber, scrollType);
};

export const revealLinesInCenterIfOutsideViewport = (id, startLineNumber, endLineNumber, scrollType) => {
    const editor = getEditor(id);
    if (!editor)
        return;
    editor.revealLinesInCenterIfOutsideViewport(startLineNumber, endLineNumber, scrollType);
};

export const revealPosition = (id, position, scrollType) => {
    const editor = getEditor(id);
    if (!editor)
        return;
    editor.revealPosition(position, scrollType);
};

export const revealPositionInCenter = (id, position, scrollType) => {
    const editor = getEditor(id);
    if (!editor)
        return;
    editor.revealPositionInCenter(position, scrollType);
};

export const revealPositionInCenterIfOutsideViewport = (id, position, scrollType) => {
    const editor = getEditor(id);
    if (!editor)
        return;
    editor.revealPositionInCenterIfOutsideViewport(position, scrollType);
};

export const revealRange = (id, range, scrollType) => {
    const editor = getEditor(id);
    if (!editor)
        return;
    editor.revealRange(range, scrollType);
};

export const revealRangeAtTop = (id, range, scrollType) => {
    const editor = getEditor(id);
    if (!editor)
        return;
    editor.revealRangeAtTop(range, scrollType);
};

export const revealRangeInCenter = (id, range, scrollType) => {
    const editor = getEditor(id);
    if (!editor)
        return;
    editor.revealRangeInCenter(range, scrollType);
};

export const revealRangeInCenterIfOutsideViewport = (id, range, scrollType) => {
    const editor = getEditor(id);
    if (!editor)
        return;
    editor.revealRangeInCenterIfOutsideViewport(range, scrollType);
};

export const setEventListener = (id, eventName) => {
    const editorHolder = getEditorHolder(id);
    if (!editorHolder)
        return;
    const editor = editorHolder.editor;
    const dotnetRef = editorHolder.dotnetRef;

    const listener = (e) => {
        let eventJson = JSON.stringify(e);
        if (eventName === "OnDidChangeModel") {
            eventJson = JSON.stringify({
                oldModelUri: e.oldModelUrl?.toString(),
                newModelUri: e.newModelUrl?.toString(),
            });
        }
        else if (eventName === "OnDidChangeConfiguration") {
            eventJson = JSON.stringify(e._values);
        }
        dotnetRef.invokeMethodAsync("EventCallback", eventName, eventJson);
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
        case "OnDidChangeModel": editor.onDidChangeModel(listener); break;
        case "OnDidChangeModelContent": editor.onDidChangeModelContent(listener); break;
        case "OnDidChangeModelDecorations": editor.onDidChangeModelDecorations(listener); break;
        case "OnDidChangeModelLanguage": editor.onDidChangeModelLanguage(listener); break;
        case "OnDidChangeModelLanguageConfiguration": editor.onDidChangeModelLanguageConfiguration(listener); break;
        case "OnDidChangeModelOptions": editor.onDidChangeModelOptions(listener); break;
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
};

export const setInstanceModel = (id, uriStr) => {
    const model = modelInterop.getModel(uriStr);
    if (!model)
        return;
    const editor = getEditor(id);
    if (!editor)
        return;
    editor.setModel(model);
};

export const setInstanceDiffModel = (id, model) => {
    const original_model = modelInterop.getModel(model.original.uri);
    const modified_model = modelInterop.getModel(model.modified.uri);
    if (!original_model || !modified_model)
        return;
    const editor = getEditor(id);
    if (!editor)
        return;
    editor.setModel({
        original: original_model,
        modified: modified_model,
    });
};

export const setPosition = (id, position) => {
    const editor = getEditor(id);
    if (!editor)
        return;
    editor.setPosition(position);
};

export const setScrollLeft = (id, newScrollLeft, scrollType) => {
    const editor = getEditor(id);
    if (!editor)
        return;
    editor.setScrollLeft(newScrollLeft, scrollType);
};

export const setScrollPosition = (id, newPosition, scrollType) => {
    const editor = getEditor(id);
    if (!editor)
        return;
    editor.setScrollPosition(newPosition, scrollType);
};

export const setScrollTop = (id, newScrollTop, scrollType) => {
    const editor = getEditor(id);
    if (!editor)
        return;
    editor.setScrollTop(newScrollTop, scrollType);
};

export const setSelection = (id, selection) => {
    const editor = getEditor(id);
    if (!editor)
        return;
    editor.setSelection(selection);
};

export const setSelections = (id, selection) => {
    const editor = getEditor(id);
    if (!editor)
        return;
    editor.setSelections(selection);
};

export const setValue = (id, value) => {
    const editor = getEditor(id);
    if (!editor)
        return;
    editor.setValue(value);
};

export const trigger = (id, source, handlerId, payload) => {
    const editor = getEditor(id);
    if (!editor)
        return;
    editor.trigger(source, handlerId, payload);
};

export const updateOptions = (id, options) => {
    const editor = getEditor(id);
    if (!editor)
        return;
    editor.updateOptions(options);
};

export const uuidv4 = () => 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, (c) => {
    let r = Math.random() * 16 | 0, v = c === 'x' ? r : (r & 0x3 | 0x8);
    return v.toString(16);
});

//#endregion

export const model = modelInterop;