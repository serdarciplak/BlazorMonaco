require.config({ paths: { 'vs': 'lib/monaco-editor/min/vs' } });
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

    //#region Editor methods

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

    getValue: function (id) {
        let editor = this.getEditorById(id);
        editor.getValue();
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
