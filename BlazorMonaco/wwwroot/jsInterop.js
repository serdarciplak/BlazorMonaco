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
        var model = this.model.getJsObject(uriStr);
        if (model == null)
            return null;
        return monaco.editor.colorizeModelLine(model, lineNumber, tabSize);
    },

    create: function (id, options, dotnetRef) {
        if (options == null)
            options = {};

        var oldEditor = this.getEditorById(id, true);
        if (oldEditor !== null) {
            options.value = oldEditor.getValue();
            window.blazorMonaco.editors.splice(window.blazorMonaco.editors.findIndex(item => item.id === id), 1);
            oldEditor.dispose();
        }

        if (typeof monaco === 'undefined')
            console.log("WARNING : Please check that you have the script tag for editor.main.js in your index.html file");

        if (options.lineNumbers == "function") {
            options.lineNumbers = function (lineNumber) {
                return dotnetRef.invokeMethod("LineNumbersCallback", lineNumber);
            }
        }

        var editor = monaco.editor.create(document.getElementById(id), options);
        window.blazorMonaco.editors.push({ id: id, editor: editor, dotnetRef: dotnetRef });
    },

    createDiffEditor: function (id, options, dotnetRef) {
        if (options == null)
            options = {};

        var oldEditor = this.getEditorById(id, true);
        var oldModel = null;
        if (oldEditor !== null) {
            oldModel = oldEditor.getModel();

            window.blazorMonaco.editors.splice(window.blazorMonaco.editors.findIndex(item => item.id === id + "_original"), 1);
            window.blazorMonaco.editors.splice(window.blazorMonaco.editors.findIndex(item => item.id === id + "_modified"), 1);
            window.blazorMonaco.editors.splice(window.blazorMonaco.editors.findIndex(item => item.id === id), 1);
            oldEditor.dispose();
        }

        if (typeof monaco === 'undefined')
            console.log("WARNING : Please check that you have the script tag for editor.main.js in your index.html file");

        if (options.lineNumbers == "function") {
            options.lineNumbers = function (lineNumber) {
                return dotnetRef.invokeMethod("LineNumbersCallback", lineNumber);
            }
        }

        var editor = monaco.editor.createDiffEditor(document.getElementById(id), options);
        window.blazorMonaco.editors.push({ id: id, editor: editor, dotnetRef: dotnetRef });
        window.blazorMonaco.editors.push({ id: id + "_original", editor: editor.getOriginalEditor() });
        window.blazorMonaco.editors.push({ id: id + "_modified", editor: editor.getModifiedEditor() });

        if (oldModel !== null)
            editor.setModel(oldModel);
    },

    createModel: function (value, language, uriStr) {
        //uri is the key; if no uri exists create one
        if (uriStr == null || uriStr == "") {
            uriStr = "generatedUriKey_" + this.uuidv4();
        }

        var uri = monaco.Uri.parse(uriStr);
        var model = monaco.editor.createModel(value, language, uri);
        if (model == null)
            return null;

        return {
            id: model.id,
            uri: model.uri.toString()
        };
    },

    defineTheme: function (themeName, themeData) {
        monaco.editor.defineTheme(themeName, themeData);
    },

    getModel: function (uriStr) {
        var model = this.model.getJsObject(uriStr);
        if (model == null)
            return null;

        return {
            id: model.id,
            uri: model.uri.toString()
        };
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
        var model = this.model.getJsObject(uriStr);
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

    getEditorById: function (id, unobstrusive = false) {
        let editorHolder = window.blazorMonaco.editors.find(e => e.id === id);
        if (!editorHolder) {
            if (unobstrusive) return null;
            throw "Couldn't find the editor with id: " + id + " editors.length: " + window.blazorMonaco.editors.length;
        }
        else if (!editorHolder.editor) {
            if (unobstrusive) return null;
            throw "editor is null for editorHolder: " + editorHolder;
        }
        return editorHolder.editor;
    },

    getEditorHolderById: function (id, unobstrusive = false) {
        let editorHolder = window.blazorMonaco.editors.find(e => e.id === id);
        if (!editorHolder) {
            if (unobstrusive) return null;
            throw "Couldn't find the editor with id: " + id + " editors.length: " + window.blazorMonaco.editors.length;
        }
        else if (!editorHolder.editor) {
            if (unobstrusive) return null;
            throw "editor is null for editorHolder: " + editorHolder;
        }
        return editorHolder;
    },

    addAction: function (id, actionId, label, keybindings, precondition, keybindingContext, contextMenuGroupId, contextMenuOrder) {
        let editorHolder = this.getEditorHolderById(id);
        editorHolder.editor.addAction({
            id: actionId,
            label: label,
            keybindings: keybindings,
            precondition: precondition,
            keybindingContext: keybindingContext,
            contextMenuGroupId: contextMenuGroupId,
            contextMenuOrder: contextMenuOrder,
            run: function () {
                editorHolder.dotnetRef.invokeMethodAsync("ActionCallback", keybindings.join(';'));
            }
        });
    },

    addCommand: function (id, keyCode) {
        let editorHolder = this.getEditorHolderById(id);
        editorHolder.editor.addCommand(keyCode, function () {
            editorHolder.dotnetRef.invokeMethodAsync("CommandCallback", keyCode);
        });
    },

    deltaDecorations: function (id, oldDecorations, newDecorations) {
        let editor = this.getEditorById(id);
        return editor.deltaDecorations(oldDecorations, newDecorations);
    },

    dispose: function (id) {
        let editor = this.getEditorById(id);
        editor.dispose();
    },

    focus: function (id) {
        let editor = this.getEditorById(id);
        editor.focus();
    },

    executeEdits: function (id, source, edits, endCursorState) {
        let editorHolder = this.getEditorHolderById(id);
        if (endCursorState == "function") {
            endCursorState = (inverseEditOperations) => {
                return editorHolder.dotnetRef.invokeMethod("ExecuteEditsCallback", inverseEditOperations);
            }
        }
        return editorHolder.editor.executeEdits(source, edits, endCursorState);
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

    getInstanceDiffModel: function (id) {
        let editor = this.getEditorById(id);
        var model = editor.getModel();
        if (model == null)
            return null;

        return {
            original: {
                id: model.original.id,
                uri: model.original.uri.toString()
            },
            modified: {
                id: model.modified.id,
                uri: model.modified.uri.toString()
            },
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

    setEventListener: function (id, eventName) {
        let editorHolder = this.getEditorHolderById(id);
        let editor = editorHolder.editor;
        let dotnetRef = editorHolder.dotnetRef;

        let listener = function (e) {
            var params = JSON.stringify(e);
            if (eventName == "OnDidChangeModel") {
                params = JSON.stringify({
                    oldModelUri: e.oldModelUrl == null ? null : e.oldModelUrl.toString(),
                    newModelUri: e.newModelUrl == null ? null : e.newModelUrl.toString(),
                });
            }
            dotnetRef.invokeMethodAsync("EventCallback", eventName, params);
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
    },

    setInstanceModel: function (id, uriStr) {
        var model = this.model.getJsObject(uriStr);
        if (model == null)
            return;

        let editor = this.getEditorById(id);
        editor.setModel(model);
    },

    setInstanceDiffModel: function (id, model) {
        var original_model = this.model.getJsObject(model.original.uri);
        var modified_model = this.model.getJsObject(model.modified.uri);
        if (original_model == null || modified_model == null)
            return;

        let editor = this.getEditorById(id);
        editor.setModel({
            original: original_model,
            modified: modified_model,
        });
    },

    setPosition: function (id, position) {
        let editor = this.getEditorById(id);
        editor.setPosition(position);
    },

    setScrollLeft: function (id, newScrollLeft, scrollType) {
        let editor = this.getEditorById(id);
        editor.setScrollLeft(newScrollLeft, scrollType);
    },

    setScrollPosition: function (id, newScrollLeft, newScrollTop, scrollType) {
        let editor = this.getEditorById(id);
        editor.setScrollPosition({
            scrollLeft: newScrollLeft,
            scrollTop: newScrollTop
        }, scrollType);
    },

    setScrollTop: function (id, newScrollTop, scrollType) {
        let editor = this.getEditorById(id);
        editor.setScrollTop(newScrollTop, scrollType);
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

    uuidv4: function () {
        return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function (c) {
            var r = Math.random() * 16 | 0, v = c == 'x' ? r : (r & 0x3 | 0x8);
            return v.toString(16);
        });
    },

    //#endregion

    //#region TextModel methods

    model: {
        getJsObject: function (uriStr) {
            var uri = monaco.Uri.parse(uriStr);
            if (uri == null)
                return null;
            return monaco.editor.getModel(uri);
        },

        getOptions: function (uriStr) {
            let model = this.getJsObject(uriStr);
            return model.getOptions();
        },

        getVersionId: function (uriStr) {
            let model = this.getJsObject(uriStr);
            return model.getVersionId();
        },

        getAlternativeVersionId: function (uriStr) {
            let model = this.getJsObject(uriStr);
            return model.getAlternativeVersionId();
        },

        setValue: function (uriStr, newValue) {
            let model = this.getJsObject(uriStr);
            return model.setValue(newValue);
        },

        getValue: function (uriStr, eol, preserveBOM) {
            let model = this.getJsObject(uriStr);
            return model.getValue(eol, preserveBOM);
        },

        getValueLength: function (uriStr, eol, preserveBOM) {
            let model = this.getJsObject(uriStr);
            return model.getValueLength(eol, preserveBOM);
        },

        getValueInRange: function (uriStr, range, eol) {
            let model = this.getJsObject(uriStr);
            return model.getValueInRange(range, eol);
        },

        getValueLengthInRange: function (uriStr, range) {
            let model = this.getJsObject(uriStr);
            return model.getValueLengthInRange(range);
        },

        getCharacterCountInRange: function (uriStr, range) {
            let model = this.getJsObject(uriStr);
            return model.getCharacterCountInRange(range);
        },

        getLineCount: function (uriStr) {
            let model = this.getJsObject(uriStr);
            return model.getLineCount();
        },

        getLineContent: function (uriStr, lineNumber) {
            let model = this.getJsObject(uriStr);
            return model.getLineContent(lineNumber);
        },

        getLineLength: function (uriStr, lineNumber) {
            let model = this.getJsObject(uriStr);
            return model.getLineLength(lineNumber);
        },

        getLinesContent: function (uriStr) {
            let model = this.getJsObject(uriStr);
            return model.getLinesContent();
        },

        getEOL: function (uriStr) {
            let model = this.getJsObject(uriStr);
            return model.getEOL();
        },

        getEndOfLineSequence: function (uriStr) {
            let model = this.getJsObject(uriStr);
            return model.getEndOfLineSequence();
        },

        getLineMinColumn: function (uriStr, lineNumber) {
            let model = this.getJsObject(uriStr);
            return model.getLineMinColumn(lineNumber);
        },

        getLineMaxColumn: function (uriStr, lineNumber) {
            let model = this.getJsObject(uriStr);
            return model.getLineMaxColumn(lineNumber);
        },

        getLineFirstNonWhitespaceColumn: function (uriStr, lineNumber) {
            let model = this.getJsObject(uriStr);
            return model.getLineFirstNonWhitespaceColumn(lineNumber);
        },

        getLineLastNonWhitespaceColumn: function (uriStr, lineNumber) {
            let model = this.getJsObject(uriStr);
            return model.getLineLastNonWhitespaceColumn(lineNumber);
        },

        validatePosition: function (uriStr, position) {
            let model = this.getJsObject(uriStr);
            return model.validatePosition(position);
        },

        modifyPosition: function (uriStr, position, offset) {
            let model = this.getJsObject(uriStr);
            return model.modifyPosition(position, offset);
        },

        validateRange: function (uriStr, range) {
            let model = this.getJsObject(uriStr);
            return model.validateRange(range);
        },

        getOffsetAt: function (uriStr, position) {
            let model = this.getJsObject(uriStr);
            return model.getOffsetAt(position);
        },

        getPositionAt: function (uriStr, offset) {
            let model = this.getJsObject(uriStr);
            return model.getPositionAt(offset);
        },

        getFullModelRange: function (uriStr) {
            let model = this.getJsObject(uriStr);
            return model.getFullModelRange();
        },

        isDisposed: function (uriStr) {
            let model = this.getJsObject(uriStr);
            return model.isDisposed();
        },

        findMatches: function (uriStr, searchString, searchScope_or_searchOnlyEditableRange, isRegex, matchCase, wordSeparators, captureMatches, limitResultCount) {
            let model = this.getJsObject(uriStr);
            return model.findMatches(searchString, searchScope_or_searchOnlyEditableRange, isRegex, matchCase, wordSeparators, captureMatches, limitResultCount);
        },

        findNextMatch: function (uriStr, searchString, searchStart, isRegex, matchCase, wordSeparators, captureMatches) {
            let model = this.getJsObject(uriStr);
            return model.findNextMatch(searchString, searchStart, isRegex, matchCase, wordSeparators, captureMatches);
        },

        findPreviousMatch: function (uriStr, searchString, searchStart, isRegex, matchCase, wordSeparators, captureMatches) {
            let model = this.getJsObject(uriStr);
            return model.findPreviousMatch(searchString, searchStart, isRegex, matchCase, wordSeparators, captureMatches);
        },

        getLanguageId: function (uriStr) {
            let model = this.getJsObject(uriStr);
            return model.getLanguageId();
        },

        getWordAtPosition: function (uriStr, position) {
            let model = this.getJsObject(uriStr);
            return model.getWordAtPosition(position);
        },

        getWordUntilPosition: function (uriStr, position) {
            let model = this.getJsObject(uriStr);
            return model.getWordUntilPosition(position);
        },

        deltaDecorations: function (uriStr, oldDecorations, newDecorations, ownerId) {
            let model = this.getJsObject(uriStr);
            return model.deltaDecorations(oldDecorations, newDecorations, ownerId);
        },

        getDecorationOptions: function (uriStr, id) {
            let model = this.getJsObject(uriStr);
            return model.getDecorationOptions(id);
        },

        getDecorationRange: function (uriStr, id) {
            let model = this.getJsObject(uriStr);
            return model.getDecorationRange(id);
        },

        getLineDecorations: function (uriStr, lineNumber, ownerId, filterOutValidation) {
            let model = this.getJsObject(uriStr);
            return model.getLineDecorations(lineNumber, ownerId, filterOutValidation);
        },

        getLinesDecorations: function (uriStr, startLineNumber, endLineNumber, ownerId, filterOutValidation) {
            let model = this.getJsObject(uriStr);
            return model.getLinesDecorations(startLineNumber, endLineNumber, ownerId, filterOutValidation);
        },

        getDecorationsInRange: function (uriStr, range, ownerId, filterOutValidation) {
            let model = this.getJsObject(uriStr);
            return model.getDecorationsInRange(range, ownerId, filterOutValidation);
        },

        getDecorationsInRange2: function (uriStr, range) {
            let model = this.getJsObject(uriStr);
            return model.getDecorationsInRange(range);
        },

        getAllDecorations: function (uriStr, ownerId, filterOutValidation) {
            let model = this.getJsObject(uriStr);
            return model.getAllDecorations(ownerId, filterOutValidation);
        },

        getInjectedTextDecorations: function (uriStr, ownerId) {
            let model = this.getJsObject(uriStr);
            return model.getInjectedTextDecorations(ownerId);
        },

        getOverviewRulerDecorations: function (uriStr, ownerId, filterOutValidation) {
            let model = this.getJsObject(uriStr);
            return model.getOverviewRulerDecorations(ownerId, filterOutValidation);
        },

        normalizeIndentation: function (uriStr, str) {
            let model = this.getJsObject(uriStr);
            return model.normalizeIndentation(str);
        },

        updateOptions: function (uriStr, newOpts) {
            let model = this.getJsObject(uriStr);
            return model.updateOptions(newOpts);
        },

        detectIndentation: function (uriStr, defaultInsertSpaces, defaultTabSize) {
            let model = this.getJsObject(uriStr);
            return model.detectIndentation(defaultInsertSpaces, defaultTabSize);
        },

        pushStackElement: function (uriStr) {
            let model = this.getJsObject(uriStr);
            return model.pushStackElement();
        },

        popStackElement: function (uriStr) {
            let model = this.getJsObject(uriStr);
            return model.popStackElement();
        },

        pushEOL: function (uriStr, eol) {
            let model = this.getJsObject(uriStr);
            return model.pushEOL(eol);
        },

        applyEdits: function (uriStr, operations) {
            let model = this.getJsObject(uriStr);
            return model.applyEdits(operations);
        },

        applyEdits: function (uriStr, operations, computeUndoEdits) {
            let model = this.getJsObject(uriStr);
            return model.applyEdits(operations, computeUndoEdits);
        },

        setEOL: function (uriStr, eol) {
            let model = this.getJsObject(uriStr);
            return model.setEOL(eol);
        },

        dispose: function (uriStr) {
            let model = this.getJsObject(uriStr);
            return model.dispose();
        }
    }

    //#endregion
}
