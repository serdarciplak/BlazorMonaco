using System.Text.Json;
using BlazorMonaco;
using BlazorMonaco.Editor;
using BlazorMonaco.Languages;
using Microsoft.AspNetCore.Components;

namespace SampleWebApp.Pages;

public partial class Index
{
    private string _valueToSet = "";

    private StandaloneCodeEditor? _editor;

    private static StandaloneEditorConstructionOptions EditorConstructionOptions(StandaloneCodeEditor editor)
    {
        return new StandaloneEditorConstructionOptions
        {
            Language = "javascript",
            GlyphMargin = true,
            AutomaticLayout = true,
            Value = "\"use strict\";\n" +
                    "function Person(age) {\n" +
                    "	if (age) {\n" +
                    "		this.age = age;\n" +
                    "	}\n" +
                    "}\n" +
                    "Person.prototype.getAge = function () {\n" +
                    "	return this.age;\n" +
                    "};\n"
        };
    }

    private async Task EditorOnDidInit()
    {
        if (_editor == null)
            return;

        await _editor.AddCommand((int)KeyMod.CtrlCmd | (int)KeyCode.KeyH, args =>
        {
            Console.WriteLine("Ctrl+H : Initial editor command is triggered.");
        });

        var newDecorations = new ModelDeltaDecoration[]
        {
            new() {
                Range = new BlazorMonaco.Range(3,1,3,1),
                Options = new ModelDecorationOptions
                {
                    IsWholeLine = true,
                    ClassName = "decorationContentClass",
                    GlyphMarginClassName = "decorationGlyphMarginClass"
                }
            }
        };

        var decorationIds = await _editor.DeltaDecorations(null, newDecorations);
        // You can now use '_decorationIds' to change or remove the decorations
    }

    private void OnContextMenu(EditorMouseEvent eventArg)
    {
        Console.WriteLine("OnContextMenu : " + JsonSerializer.Serialize(eventArg));
    }

    private async Task ChangeTheme(ChangeEventArgs e)
    {
        Console.WriteLine($"setting theme to: {e.Value}");
        await BlazorMonaco.Editor.Global.SetTheme(jsRuntime, e.Value?.ToString());
    }

    private async Task SetValue()
    {
        Console.WriteLine($"setting value to: {_valueToSet}");
        if (_editor == null)
            return;
        await _editor.SetValue(_valueToSet);
    }

    private async Task GetValue()
    {
        if (_editor == null)
            return;
        var val = await _editor.GetValue();
        Console.WriteLine($"value is: {val}");
    }

    private async Task AddCommand()
    {
        if (_editor == null)
            return;
        await _editor.AddCommand((int)KeyMod.CtrlCmd | (int)KeyCode.Enter, args =>
        {
            Console.WriteLine("Ctrl+Enter : Editor command is triggered.");
        });
    }

    private async Task AddAction()
    {
        if (_editor == null)
            return;
        var actionDescriptor = new ActionDescriptor
        {
            Id = "testAction",
            Label = "Test Action",
            Keybindings = [(int)KeyMod.CtrlCmd | (int)KeyCode.KeyB],
            ContextMenuGroupId = "navigation",
            ContextMenuOrder = 1.5f,
            Run = editor =>
            {
                Console.WriteLine("Ctrl+B : Editor action is triggered.");
            }
        };
        await _editor.AddAction(actionDescriptor);
    }

    private async Task RegisterCodeActionProvider()
    {
        if (_editor == null)
            return;

        // Set sample marker
        var model = await _editor.GetModel();
        var markers = new List<MarkerData>
        {
            new() {
                CodeAsObject = new MarkerCode
                {
                    TargetUri = "https://www.google.com",
                    Value = "my-value"
                },
                Message = "Marker example",
                Severity = MarkerSeverity.Warning,
                StartLineNumber = 4,
                StartColumn = 3,
                EndLineNumber = 4,
                EndColumn = 7
            }
        };
        await BlazorMonaco.Editor.Global.SetModelMarkers(jsRuntime, model, "default", markers);

        // Register quick fix for marker
        await BlazorMonaco.Languages.Global.RegisterCodeActionProvider(jsRuntime, "javascript", async (modelUri, range, context) =>
        {
            var innerModel = await BlazorMonaco.Editor.Global.GetModel(jsRuntime, modelUri);

            var codeActionList = new CodeActionList();
            if (context.Markers.Count == 0)
                return codeActionList;

            codeActionList.Actions =
            [
                new CodeAction
                {
                    Title = "Fix example",
                    Kind = "quickfix",
                    Diagnostics = markers,
                    Edit = new WorkspaceEdit
                    {
                        Edits =
                        [
                            new WorkspaceTextEdit
                            {
                                ResourceUri = modelUri,
                                TextEdit = new TextEditWithInsertAsSnippet
                                {
                                    Range = range,
                                    Text = "THIS"
                                }
                            }
                        ]
                    },
                    IsPreferred = true
                }
            ];
            return codeActionList;
        });
    }

    private async Task RegisterDocumentFormattingEditProvider()
    {
        await BlazorMonaco.Languages.Global.RegisterDocumentFormattingEditProvider(jsRuntime, "javascript", async (modelUri, options) =>
        {
            if (_editor == null)
                return [];

            var model = await _editor.GetModel();
            var lines = await model.GetLineCount();
            var columns = await model.GetLineMaxColumn(lines);

            var value = await _editor.GetValue();
            var result = value.Split("\n").Select(m => m.Trim()).ToArray();

            return [
                new TextEdit {
                    Range = new BlazorMonaco.Range(1, 1, lines, columns),
                    Text = string.Join("\n", result)
                }
            ];
        });
    }

    private async Task RegisterCompletionItemProvider()
    {
        // Register completion item to replace warning item
        await BlazorMonaco.Languages.Global.RegisterCompletionItemProvider(jsRuntime, "javascript", async (modelUri, position, context) =>
        {
            var model = await BlazorMonaco.Editor.Global.GetModel(jsRuntime, modelUri);

            var completionList = new CompletionList
            {
                Suggestions =
                [
                    new CompletionItem
                    {
                        LabelAsString = "Replace by THIS",
                        Kind = CompletionItemKind.Variable,
                        Detail = "this -> THIS",
                        InsertText = "THIS",
                        Preselect = true,
                        RangeAsObject = new BlazorMonaco.Range
                        {
                            StartLineNumber = 4,
                            StartColumn = 3,
                            EndLineNumber = 4,
                            EndColumn = 7
                        }
                    }
                ]
            };
            return completionList;
        });
    }
}
