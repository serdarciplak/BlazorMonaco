require.config({ paths: { 'vs': '_content/BlazorMonaco/lib/monaco-editor/min/vs' } });
window.blazorMonaco = window.blazorMonaco || {};
window.blazorMonaco.editors = [];

window.blazorMonaco.editor = {

    //#region Static methods

    colorizeElement: function (id) {
        monaco.editor.colorizeElement(document.getElementById(id));
    },

    create: function (id, options) {
        if (window.blazorMonaco.editors.find(e => e.id === model.id)) {
            return;
        }
        
        require(['vs/editor/editor.main'], function () {
            if (options == null) {
                options = {};
            }

            var editor = monaco.editor.create(document.getElementById(id), options);
            window.blazorMonaco.editors.push({ id: id, editor: editor });
        });
    },

    setTheme: function (theme) {
        monaco.editor.setTheme(theme);
        return true;
    },

    //#endregion

    //#region Instance methods

    getEditorById: function (id) {
        let editorHolder = window.blazorMonaco.editors.find(e => e.id === id);
        if (!editorHolder) {
            throw "Couldn't find the editor with id: " + id + " editors.length: " + window.blazorMonaco.editors.length;
        }
        else if (!editorHolder.editor) {
            throw "editor is null for editorHolder: " + editorHolder;
        }
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
