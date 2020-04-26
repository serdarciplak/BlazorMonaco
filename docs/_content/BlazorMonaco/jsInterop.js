require.config({ paths: { 'vs': '_content/BlazorMonaco/lib/monaco-editor/min/vs' } });
window.blazorMonaco = window.blazorMonaco || {};
window.blazorMonaco.editors = [];

window.blazorMonaco.editor = {

    //#region Static methods

    colorizeElement: function (id) {
        monaco.editor.colorizeElement(document.getElementById(id));
    },

    create: function (id, options) {
        if (window.blazorMonaco.editors.find(e => e.id === model.id))
            return;
        
        if (options == null)
            options = {};

        if (typeof monaco === 'undefined')
            console.log("WARNING : Please check that you have the script tag for editor.main.js in your index.html file");

        var editor = monaco.editor.create(document.getElementById(id), options);
        window.blazorMonaco.editors.push({ id: id, editor: editor });
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

    getValue: function (id) {
        let editor = this.getEditorById(id);
        return editor.getValue();
    },

    setEventListener: function (id, eventName, handler) {
        let editor = this.getEditorById(id);

        let listener = function (e) {
            handler.invokeMethodAsync("EventCallback", eventName, JSON.stringify(e));
        };

        switch (eventName) {
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

    setValue: function (id, value) {
        let editor = this.getEditorById(id);
        editor.setValue(value);
    },

    updateOptions: function (id, options) {
        let editor = this.getEditorById(id);
        editor.updateOptions(options);
    }

    //#endregion
}
