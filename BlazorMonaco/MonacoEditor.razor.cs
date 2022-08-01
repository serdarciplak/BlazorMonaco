using BlazorMonaco.Helpers;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BlazorMonaco.Editor
{
    public partial class MonacoEditor
    {
        #region Blazor

        [Parameter] public Func<MonacoEditor, StandaloneEditorConstructionOptions> ConstructionOptions { get; set; }
        [Parameter] public EventCallback<MonacoEditor> OnDidCompositionEnd { get; set; }
        [Parameter] public EventCallback<MonacoEditor> OnDidCompositionStart { get; set; }
        [Parameter] public EventCallback<EditorMouseEvent> OnContextMenu { get; set; }
        // onDidAttemptReadOnlyEdit(listener: () => void) : IDisposable;
        [Parameter] public EventCallback<MonacoEditor> OnDidBlurEditorText { get; set; }
        [Parameter] public EventCallback<MonacoEditor> OnDidBlurEditorWidget { get; set; }
        [Parameter] public EventCallback<MonacoEditor> OnDidChangeConfiguration { get; set; }
        [Parameter] public EventCallback<CursorPositionChangedEvent> OnDidChangeCursorPosition { get; set; }
        [Parameter] public EventCallback<CursorSelectionChangedEvent> OnDidChangeCursorSelection { get; set; }
        [Parameter] public EventCallback<ModelChangedEvent> OnDidChangeModel { get; set; }
        [Parameter] public EventCallback<ModelContentChangedEvent> OnDidChangeModelContent { get; set; }
        [Parameter] public EventCallback<ModelDecorationsChangedEvent> OnDidChangeModelDecorations { get; set; }
        [Parameter] public EventCallback<ModelLanguageChangedEvent> OnDidChangeModelLanguage { get; set; }
        [Parameter] public EventCallback<ModelLanguageConfigurationChangedEvent> OnDidChangeModelLanguageConfiguration { get; set; }
        [Parameter] public EventCallback<ModelOptionsChangedEvent> OnDidChangeModelOptions { get; set; }
        [Parameter] public EventCallback<ContentSizeChangedEvent> OnDidContentSizeChange { get; set; }
        [Parameter] public EventCallback<MonacoEditor> OnDidFocusEditorText { get; set; }
        [Parameter] public EventCallback<MonacoEditor> OnDidFocusEditorWidget { get; set; }
        [Parameter] public EventCallback<EditorLayoutInfo> OnDidLayoutChange { get; set; }
        [Parameter] public EventCallback<PasteEvent> OnDidPaste { get; set; }
        [Parameter] public EventCallback<ScrollEvent> OnDidScrollChange { get; set; }
        [Parameter] public EventCallback<KeyboardEvent> OnKeyDown { get; set; }
        [Parameter] public EventCallback<KeyboardEvent> OnKeyUp { get; set; }
        [Parameter] public EventCallback<EditorMouseEvent> OnMouseDown { get; set; }
        [Parameter] public EventCallback<EditorMouseEvent> OnMouseLeave { get; set; }
        [Parameter] public EventCallback<EditorMouseEvent> OnMouseMove { get; set; }
        [Parameter] public EventCallback<EditorMouseEvent> OnMouseUp { get; set; }

        private List<string> deltaDecorationIds { get; set; } = new List<string>();

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                // Get options
                var options = ConstructionOptions?.Invoke(this);

                // Prepare the line numbers callback
                LineNumbersLambda = options.LineNumbersLambda;
                if (LineNumbersLambda != null)
                {
                    options.LineNumbers = "function";
                    options.LineNumbersLambda = null;
                }

                // Create the editor
                await MonacoEditorBase.Create(Id, options, _dotnetObjectRef);
            }
            await base.OnAfterRenderAsync(firstRender);
        }

        internal override async Task SetEventListeners()
        {
            if (OnDidCompositionEnd.HasDelegate)
                await SetEventListener("OnDidCompositionEnd");
            if (OnDidCompositionStart.HasDelegate)
                await SetEventListener("OnDidCompositionStart");
            if (OnContextMenu.HasDelegate)
                await SetEventListener("OnContextMenu");
            if (OnDidBlurEditorText.HasDelegate)
                await SetEventListener("OnDidBlurEditorText");
            if (OnDidBlurEditorWidget.HasDelegate)
                await SetEventListener("OnDidBlurEditorWidget");
            if (OnDidChangeConfiguration.HasDelegate)
                await SetEventListener("OnDidChangeConfiguration");
            if (OnDidChangeCursorPosition.HasDelegate)
                await SetEventListener("OnDidChangeCursorPosition");
            if (OnDidChangeCursorSelection.HasDelegate)
                await SetEventListener("OnDidChangeCursorSelection");
            if (OnDidChangeModel.HasDelegate)
                await SetEventListener("OnDidChangeModel");
            if (OnDidChangeModelContent.HasDelegate)
                await SetEventListener("OnDidChangeModelContent");
            if (OnDidChangeModelDecorations.HasDelegate)
                await SetEventListener("OnDidChangeModelDecorations");
            if (OnDidChangeModelLanguage.HasDelegate)
                await SetEventListener("OnDidChangeModelLanguage");
            if (OnDidChangeModelLanguageConfiguration.HasDelegate)
                await SetEventListener("OnDidChangeModelLanguageConfiguration");
            if (OnDidChangeModelOptions.HasDelegate)
                await SetEventListener("OnDidChangeModelOptions");
            if (OnDidContentSizeChange.HasDelegate)
                await SetEventListener("OnDidContentSizeChange");
            if (OnDidFocusEditorText.HasDelegate)
                await SetEventListener("OnDidFocusEditorText");
            if (OnDidFocusEditorWidget.HasDelegate)
                await SetEventListener("OnDidFocusEditorWidget");
            if (OnDidLayoutChange.HasDelegate)
                await SetEventListener("OnDidLayoutChange");
            if (OnDidPaste.HasDelegate)
                await SetEventListener("OnDidPaste");
            if (OnDidScrollChange.HasDelegate)
                await SetEventListener("OnDidScrollChange");
            if (OnKeyDown.HasDelegate)
                await SetEventListener("OnKeyDown");
            if (OnKeyUp.HasDelegate)
                await SetEventListener("OnKeyUp");
            if (OnMouseDown.HasDelegate)
                await SetEventListener("OnMouseDown");
            if (OnMouseLeave.HasDelegate)
                await SetEventListener("OnMouseLeave");
            if (OnMouseMove.HasDelegate)
                await SetEventListener("OnMouseMove");
            if (OnMouseUp.HasDelegate)
                await SetEventListener("OnMouseUp");
            await base.SetEventListeners();
        }

        [JSInvokable]
        public override async Task EventCallback(string eventName, string eventJson)
        {
            var jsonOptions = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

            switch (eventName)
            {
                case "OnDidCompositionEnd": await OnDidCompositionEnd.InvokeAsync(this); break;
                case "OnDidCompositionStart": await OnDidCompositionStart.InvokeAsync(this); break;
                case "OnContextMenu": if (eventJson != null) await OnContextMenu.InvokeAsync(JsonSerializer.Deserialize<EditorMouseEvent>(eventJson, jsonOptions)); break;
                case "OnDidBlurEditorText": await OnDidBlurEditorText.InvokeAsync(this); break;
                case "OnDidBlurEditorWidget": await OnDidBlurEditorWidget.InvokeAsync(this); break;
                case "OnDidChangeConfiguration": await OnDidChangeConfiguration.InvokeAsync(this); break;
                case "OnDidChangeCursorPosition": await OnDidChangeCursorPosition.InvokeAsync(JsonSerializer.Deserialize<CursorPositionChangedEvent>(eventJson, jsonOptions)); break;
                case "OnDidChangeCursorSelection": await OnDidChangeCursorSelection.InvokeAsync(JsonSerializer.Deserialize<CursorSelectionChangedEvent>(eventJson, jsonOptions)); break;
                case "OnDidChangeModel": await OnDidChangeModel.InvokeAsync(JsonSerializer.Deserialize<ModelChangedEvent>(eventJson, jsonOptions)); break;
                case "OnDidChangeModelContent": await OnDidChangeModelContent.InvokeAsync(JsonSerializer.Deserialize<ModelContentChangedEvent>(eventJson, jsonOptions)); break;
                case "OnDidChangeModelDecorations": await OnDidChangeModelDecorations.InvokeAsync(JsonSerializer.Deserialize<ModelDecorationsChangedEvent>(eventJson, jsonOptions)); break;
                case "OnDidChangeModelLanguage": await OnDidChangeModelLanguage.InvokeAsync(JsonSerializer.Deserialize<ModelLanguageChangedEvent>(eventJson, jsonOptions)); break;
                case "OnDidChangeModelLanguageConfiguration": await OnDidChangeModelLanguageConfiguration.InvokeAsync(JsonSerializer.Deserialize<ModelLanguageConfigurationChangedEvent>(eventJson, jsonOptions)); break;
                case "OnDidChangeModelOptions": await OnDidChangeModelOptions.InvokeAsync(JsonSerializer.Deserialize<ModelOptionsChangedEvent>(eventJson, jsonOptions)); break;
                case "OnDidContentSizeChange": await OnDidContentSizeChange.InvokeAsync(JsonSerializer.Deserialize<ContentSizeChangedEvent>(eventJson, jsonOptions)); break;
                case "OnDidFocusEditorText": await OnDidFocusEditorText.InvokeAsync(this); break;
                case "OnDidFocusEditorWidget": await OnDidFocusEditorWidget.InvokeAsync(this); break;
                case "OnDidLayoutChange": if (eventJson != null) await OnDidLayoutChange.InvokeAsync(JsonSerializer.Deserialize<EditorLayoutInfo>(eventJson, jsonOptions)); break;
                case "OnDidPaste": if (eventJson != null) await OnDidPaste.InvokeAsync(JsonSerializer.Deserialize<PasteEvent>(eventJson, jsonOptions)); break;
                case "OnDidScrollChange": if (eventJson != null) await OnDidScrollChange.InvokeAsync(JsonSerializer.Deserialize<ScrollEvent>(eventJson, jsonOptions)); break;
                case "OnKeyDown": if (eventJson != null) await OnKeyDown.InvokeAsync(JsonSerializer.Deserialize<KeyboardEvent>(eventJson, jsonOptions)); break;
                case "OnKeyUp": if (eventJson != null) await OnKeyUp.InvokeAsync(JsonSerializer.Deserialize<KeyboardEvent>(eventJson, jsonOptions)); break;
                case "OnMouseDown": if (eventJson != null) await OnMouseDown.InvokeAsync(JsonSerializer.Deserialize<EditorMouseEvent>(eventJson, jsonOptions)); break;
                case "OnMouseLeave": if (eventJson != null) await OnMouseLeave.InvokeAsync(JsonSerializer.Deserialize<EditorMouseEvent>(eventJson, jsonOptions)); break;
                case "OnMouseMove": if (eventJson != null) await OnMouseMove.InvokeAsync(JsonSerializer.Deserialize<EditorMouseEvent>(eventJson, jsonOptions)); break;
                case "OnMouseUp": if (eventJson != null) await OnMouseUp.InvokeAsync(JsonSerializer.Deserialize<EditorMouseEvent>(eventJson, jsonOptions)); break;
            }

            await base.EventCallback(eventName, eventJson);
        }

        public static MonacoEditor CreateVirtualEditor(string id, string cssClass = null)
        {
            var virtual_editor = new MonacoEditor
            {
                Id = id,
                CssClass = cssClass,
                jsRuntime = JsRuntimeExt.Shared
            };
            virtual_editor._dotnetObjectRef = DotNetObjectReference.Create(virtual_editor as MonacoEditorBase);
            return virtual_editor;
        }

        #endregion

        #region Instance Methods

        // addContentWidget

        // addOverlayWidget

        // applyFontInfo

        // changeViewZones

        public async Task<string[]> DeltaDecorations(string[] oldDecorationIds, ModelDeltaDecoration[] newDecorations)
        {
            oldDecorationIds = oldDecorationIds ?? new string[] { };

            // Convert the newDecorations object into a JsonElement to get rid of the properties with null values
            var newDecorationsJson = JsonSerializer.Serialize(newDecorations, new JsonSerializerOptions
            {
                IgnoreNullValues = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
            var newDecorationsElement = JsonSerializer.Deserialize<JsonElement>(newDecorationsJson);
            var newDecorationIds = await jsRuntime.SafeInvokeAsync<string[]>("blazorMonaco.editor.deltaDecorations", Id, oldDecorationIds, newDecorationsElement);
            deltaDecorationIds.RemoveAll(d => oldDecorationIds.Any(o => o == d));
            deltaDecorationIds.AddRange(newDecorationIds);
            return newDecorationIds;
        }

        // executeCommand

        // executeCommands

        public Task<bool> ExecuteEdits(string source, List<IdentifiedSingleEditOperation> edits, List<Selection> endCursorState)
            => jsRuntime.SafeInvokeAsync<bool>("blazorMonaco.editor.executeEdits", Id, source, edits, endCursorState);

        public Task<bool> ExecuteEdits(string source, List<IdentifiedSingleEditOperation> edits, Func<List<ValidEditOperation>, List<Selection>> endCursorState)
        {
            ExecuteEditsLambda = endCursorState;
            return jsRuntime.SafeInvokeAsync<bool>("blazorMonaco.editor.executeEdits", Id, source, edits, "function");
        }

        private Func<List<ValidEditOperation>, List<Selection>> ExecuteEditsLambda { get; set; }

        [JSInvokable]
        public List<Selection> ExecuteEditsCallback(List<ValidEditOperation> inverseEditOperations)
        {
            Console.WriteLine("ExecuteEditsCallback is called : " + JsonSerializer.Serialize(inverseEditOperations));
            return ExecuteEditsLambda?.Invoke(inverseEditOperations);
        }

        // getAction

        public Task<string> GetContainerDomNodeId()
            => jsRuntime.SafeInvokeAsync<string>("blazorMonaco.editor.getContainerDomNodeId", Id);

        public Task<double> GetContentHeight()
            => jsRuntime.SafeInvokeAsync<double>("blazorMonaco.editor.getContentHeight", Id);

        public Task<double> GetContentWidth()
            => jsRuntime.SafeInvokeAsync<double>("blazorMonaco.editor.getContentWidth", Id);

        // getContribution

        public Task<EditorLayoutInfo> GetLayoutInfo()
            => jsRuntime.SafeInvokeAsync<EditorLayoutInfo>("blazorMonaco.editor.getLayoutInfo", Id);

        // getLineDecorations

        public Task<TextModel> GetModel()
            => jsRuntime.SafeInvokeAsync<TextModel>("blazorMonaco.editor.getInstanceModel", Id);

        public Task<int> GetOffsetForColumn(int lineNumber, int column)
            => jsRuntime.SafeInvokeAsync<int>("blazorMonaco.editor.getOffsetForColumn", Id, lineNumber, column);

        public Task<string> GetOption(EditorOption option)
            => jsRuntime.SafeInvokeAsync<string>("blazorMonaco.editor.getOption", Id, (int)option);

        public Task<List<string>> GetOptions()
            => jsRuntime.SafeInvokeAsync<List<string>>("blazorMonaco.editor.getOptions", Id);

        public Task<EditorOptions> GetRawOptions()
            => jsRuntime.SafeInvokeAsync<EditorOptions>("blazorMonaco.editor.getRawOptions", Id);

        public Task<double> GetScrollHeight()
            => jsRuntime.SafeInvokeAsync<double>("blazorMonaco.editor.getScrollHeight", Id);

        public Task<double> GetScrollLeft()
            => jsRuntime.SafeInvokeAsync<double>("blazorMonaco.editor.getScrollLeft", Id);

        public Task<double> GetScrollTop()
            => jsRuntime.SafeInvokeAsync<double>("blazorMonaco.editor.getScrollTop", Id);

        public Task<double> GetScrollWidth()
            => jsRuntime.SafeInvokeAsync<double>("blazorMonaco.editor.getScrollWidth", Id);

        public Task<Position> GetScrolledVisiblePosition(Position position)
            => jsRuntime.SafeInvokeAsync<Position>("blazorMonaco.editor.getScrolledVisiblePosition", Id, position);

        public Task<BaseMouseTarget> GetTargetAtClientPoint(int clientX, int clientY)
            => jsRuntime.SafeInvokeAsync<BaseMouseTarget>("blazorMonaco.editor.getTargetAtClientPoint", Id, clientX, clientY);

        public Task<double> GetTopForLineNumber(int lineNumber)
            => jsRuntime.SafeInvokeAsync<double>("blazorMonaco.editor.getTopForLineNumber", Id, lineNumber);

        public Task<double> GetTopForPosition(int lineNumber, int column)
            => jsRuntime.SafeInvokeAsync<double>("blazorMonaco.editor.getTopForPosition", Id, lineNumber, column);

        public Task<string> GetValue()
            => jsRuntime.SafeInvokeAsync<string>("blazorMonaco.editor.getValue", Id);

        public Task<List<Range>> GetVisibleRanges()
            => jsRuntime.SafeInvokeAsync<List<Range>>("blazorMonaco.editor.getVisibleRanges", Id);

        public Task<bool> HasWidgetFocus()
            => jsRuntime.SafeInvokeAsync<bool>("blazorMonaco.editor.hasWidgetFocus", Id);

        // layoutContentWidget

        // layoutOverlayWidget

        // popUndoStop(): boolean;

        public Task<bool> PushUndoStop()
            => jsRuntime.SafeInvokeAsync<bool>("blazorMonaco.editor.pushUndoStop", Id);

        // removeContentWidget

        // removeOverlayWidget

        public Task Render(bool? forceRedraw = null)
            => jsRuntime.SafeInvokeAsync("blazorMonaco.editor.render", Id, forceRedraw);

        public Task ResetDeltaDecorations()
            => DeltaDecorations(deltaDecorationIds.ToArray(), new ModelDeltaDecoration[0]);

        // restoreViewState

        // saveViewState

        public Task SetModel(TextModel model)
            => jsRuntime.SafeInvokeAsync("blazorMonaco.editor.setInstanceModel", Id, model.Uri);

        public Task SetScrollLeft(int newScrollLeft, ScrollType? scrollType = null)
            => jsRuntime.SafeInvokeAsync("blazorMonaco.editor.setScrollLeft", Id, newScrollLeft, scrollType);

        public Task SetScrollPosition(int newScrollLeft, int newScrollTop, ScrollType? scrollType = null)
            => jsRuntime?.SafeInvokeAsync("blazorMonaco.editor.setScrollPosition", Id, newScrollLeft, newScrollTop, scrollType);

        public Task SetScrollTop(int newScrollTop, ScrollType? scrollType = null)
            => jsRuntime.SafeInvokeAsync("blazorMonaco.editor.setScrollTop", Id, newScrollTop, scrollType);

        public Task SetValue(string newValue)
            => jsRuntime.SafeInvokeAsync("blazorMonaco.editor.setValue", Id, newValue);

        public Task UpdateOptions(IGlobalEditorOptions options)
        {
            // Convert the options object into a JsonElement to get rid of the properties with null values
            var optionsJson = JsonSerializer.Serialize(options, new JsonSerializerOptions
            {
                IgnoreNullValues = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
            var optionsDict = JsonSerializer.Deserialize<JsonElement>(optionsJson);
            return jsRuntime.SafeInvokeAsync("blazorMonaco.editor.updateOptions", Id, optionsDict);
        }

        #endregion
    }
}
