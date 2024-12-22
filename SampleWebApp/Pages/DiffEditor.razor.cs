using System.Diagnostics.CodeAnalysis;
using BlazorMonaco;
using BlazorMonaco.Editor;

namespace SampleWebApp.Pages;

public partial class DiffEditor
{
    private string _valueToSetOriginal = "";
    private string _valueToSetModified = "";

    [AllowNull]
    private StandaloneDiffEditor _diffEditor;

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
        var original_model = await Global.GetModel(jsRuntime, "sample-diff-editor-originalModel");
        if (original_model == null)
        {
            var original_value = "\"use strict\";\n" +
                            "function Person(age) {\n" +
                            "	if (age) {\n" +
                            "		this.age = age;\n" +
                            "	}\n" +
                            "}\n" +
                            "Person.prototype.getAge = function () {\n" +
                            "	return this.age;\n" +
                            "};\n";
            original_model = await Global.CreateModel(jsRuntime, original_value, "javascript", "sample-diff-editor-originalModel");
        }

        // Get or create the modified model
        var modified_model = await Global.GetModel(jsRuntime, "sample-diff-editor-modifiedModel");
        if (modified_model == null)
        {
            var modified_value = "\"don't use strict\";\n" +
                            "furction Person(age_is_just_a_number) {\n" +
                            "	if (age_is_just_a_number) {\n" +
                            "		this.age_is_just_a_number = age_is_just_a_number;\n" +
                            "	}\n" +
                            "}\n" +
                            "Person.prototype.getAge = function () {\n" +
                            "	return this.age_is_just_a_number;\n" +
                            "};\n" +
                            "//Really, it is just a number people!";
            modified_model = await Global.CreateModel(jsRuntime, modified_value, "javascript", "sample-diff-editor-modifiedModel");
        }

        // Set the editor model
        await _diffEditor.SetModel(new DiffEditorModel
        {
            Original = original_model,
            Modified = modified_model
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
        Console.WriteLine($"setting original value to: {_valueToSetOriginal}");
        await _diffEditor.OriginalEditor.SetValue(_valueToSetOriginal);
    }

    private async Task SetValueModified()
    {
        Console.WriteLine($"setting modified value to: {_valueToSetModified}");
        await _diffEditor.ModifiedEditor.SetValue(_valueToSetModified);
    }

    private async Task GetValueOriginal()
    {
        var val = await _diffEditor.OriginalEditor.GetValue();
        Console.WriteLine($"original value is: {val}");
    }

    private async Task GetValueModified()
    {
        var val = await _diffEditor.ModifiedEditor.GetValue();
        Console.WriteLine($"modified value is: {val}");
    }

    private async Task AddCommand()
    {
        await _diffEditor.AddCommand((int)KeyMod.CtrlCmd | (int)KeyCode.Enter, (args) =>
        {
            Console.WriteLine($"Ctrl+Enter : Diff Editor command is triggered. ({_diffEditor.Id})");
        });
    }

    private async Task AddAction()
    {
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
