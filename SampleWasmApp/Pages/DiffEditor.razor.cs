using BlazorMonaco;
using BlazorMonaco.Editor;

namespace SampleWasmApp.Pages;

public partial class DiffEditor
{
    private string _valueToSetOriginal = "";
    private string _valueToSetModified = "";

    private StandaloneDiffEditor? _diffEditor;

    private StandaloneDiffEditorConstructionOptions DiffEditorConstructionOptions(StandaloneDiffEditor editor)
    {
        return new StandaloneDiffEditorConstructionOptions
        {
            OriginalEditable = true
        };
    }

    private async Task EditorOnDidInit()
    {
        // Get or create the original model
        var originalModel = await BlazorMonaco.Editor.Global.GetModel(jsRuntime, "sample-diff-editor-originalModel");
        if (originalModel == null)
        {
            const string originalValue = "\"use strict\";\n" +
                                         "function Person(age) {\n" +
                                         "	if (age) {\n" +
                                         "		this.age = age;\n" +
                                         "	}\n" +
                                         "}\n" +
                                         "Person.prototype.getAge = function () {\n" +
                                         "	return this.age;\n" +
                                         "};\n";
            originalModel = await BlazorMonaco.Editor.Global.CreateModel(jsRuntime, originalValue, "javascript", "sample-diff-editor-originalModel");
        }

        // Get or create the modified model
        var modifiedModel = await BlazorMonaco.Editor.Global.GetModel(jsRuntime, "sample-diff-editor-modifiedModel");
        if (modifiedModel == null)
        {
            const string modifiedValue = "\"don't use strict\";\n" +
                                         "furction Person(age_is_just_a_number) {\n" +
                                         "	if (age_is_just_a_number) {\n" +
                                         "		this.age_is_just_a_number = age_is_just_a_number;\n" +
                                         "	}\n" +
                                         "}\n" +
                                         "Person.prototype.getAge = function () {\n" +
                                         "	return this.age_is_just_a_number;\n" +
                                         "};\n" +
                                         "//Really, it is just a number people!";
            modifiedModel = await BlazorMonaco.Editor.Global.CreateModel(jsRuntime, modifiedValue, "javascript", "sample-diff-editor-modifiedModel");
        }

        // Set the editor model
        if (_diffEditor == null)
            return;
        await _diffEditor.SetModel(new DiffEditorModel
        {
            Original = originalModel,
            Modified = modifiedModel
        });
    }

    private void EditorOnKeyUpOriginal(KeyboardEvent keyboardEvent)
    {
        Console.WriteLine("OnKeyUpOriginal : " + keyboardEvent.Code);
    }

    private void EditorOnKeyUpModified(KeyboardEvent keyboardEvent)
    {
        Console.WriteLine("OnKeyUpModified : " + keyboardEvent.Code);
    }

    private async Task SetValueOriginal()
    {
        if (_diffEditor == null)
            return;
        Console.WriteLine($"setting original value to: {_valueToSetOriginal}");
        await _diffEditor.OriginalEditor.SetValue(_valueToSetOriginal);
    }

    private async Task SetValueModified()
    {
        if (_diffEditor == null)
            return;
        Console.WriteLine($"setting modified value to: {_valueToSetModified}");
        await _diffEditor.ModifiedEditor.SetValue(_valueToSetModified);
    }

    private async Task GetValueOriginal()
    {
        if (_diffEditor == null)
            return;
        var val = await _diffEditor.OriginalEditor.GetValue();
        Console.WriteLine($"original value is: {val}");
    }

    private async Task GetValueModified()
    {
        if (_diffEditor == null)
            return;
        var val = await _diffEditor.ModifiedEditor.GetValue();
        Console.WriteLine($"modified value is: {val}");
    }

    private async Task AddCommand()
    {
        if (_diffEditor == null)
            return;
        await _diffEditor.AddCommand((int)KeyMod.CtrlCmd | (int)KeyCode.Enter, args =>
        {
            Console.WriteLine($"Ctrl+Enter : Diff Editor command is triggered. ({_diffEditor.Id})");
        });
    }

    private async Task AddAction()
    {
        if (_diffEditor == null)
            return;
        var actionDescriptor = new ActionDescriptor
        {
            Id = "testAction",
            Label = "Test Action",
            Keybindings = [(int)KeyMod.CtrlCmd | (int)KeyCode.KeyB],
            ContextMenuGroupId = "navigation",
            ContextMenuOrder = 1.5f,
            Run = (editor) =>
            {
                Console.WriteLine($"Ctrl+B : Diff Editor action is triggered. ({editor.Id})");
            }
        };
        await _diffEditor.AddAction(actionDescriptor);
    }
}
