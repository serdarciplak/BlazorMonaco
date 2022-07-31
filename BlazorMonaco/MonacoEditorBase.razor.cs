using BlazorMonaco.Bridge;
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
    public partial class MonacoEditorBase
    {
        #region Blazor

        [Parameter]
        public string Id { get; set; }

        [Parameter]
        public string CssClass { get; set; }

        [Parameter]
        public EventCallback<MonacoEditorBase> OnDidDispose { get; set; }

        [Parameter]
        public EventCallback<MonacoEditorBase> OnDidInit { get; set; }

        [Inject]
        protected IJSRuntime jsRuntime { get; set; }
        protected DotNetObjectReference<MonacoEditorBase> jsObjectRef { get; set; }
        private Dictionary<string, Action<MonacoEditorBase, int[]>> actions { get; set; } = new Dictionary<string, Action<MonacoEditorBase, int[]>>();
        private Dictionary<string, Action<MonacoEditorBase, int>> commands { get; set; } = new Dictionary<string, Action<MonacoEditorBase, int>>();

        protected override void OnInitialized()
        {
            base.OnInitialized();
            JsRuntimeExt.Shared = jsRuntime;
            jsObjectRef = DotNetObjectReference.Create(this);
        }

        public virtual void Dispose()
        {
            jsObjectRef?.Dispose();
        }

        protected override void OnParametersSet()
        {
            base.OnParametersSet();
            if (string.IsNullOrWhiteSpace(Id))
                Id = "blazor-monaco-" + Guid.NewGuid().ToString();
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await SetEventListeners();
                await OnDidInit.InvokeAsync(this);
            }
            await base.OnAfterRenderAsync(firstRender);
        }

        internal virtual async Task SetEventListeners()
        {
            if (OnDidDispose.HasDelegate)
                await SetEventListener("OnDidDispose");
        }

        public virtual async Task EventCallback(string eventName, string eventJson)
        {
            switch (eventName)
            {
                case "OnDidDispose": await OnDidDispose.InvokeAsync(this); break;
            }
        }

        [JSInvokable]
        public void ActionCallback(string keyCodesStr)
        {
            var keyCodes = keyCodesStr.Split(';').Select(k => Int32.Parse(k)).ToArray();
            var action = actions[keyCodesStr];
            action?.Invoke(this, keyCodes);
        }

        [JSInvokable]
        public void CommandCallback(int keyCode)
        {
            var command = commands[keyCode.ToString()];
            command?.Invoke(this, keyCode);
        }

        protected Func<int, string> LineNumbersLambda { get; set; }
        [JSInvokable]
        public string LineNumbersCallback(int lineNumber)
        {
            return LineNumbersLambda?.Invoke(lineNumber) ?? lineNumber.ToString();
        }

        #endregion

        #region Static Methods

        public static Task<string> Colorize(string text, string languageId, ColorizerOptions options)
            => JsRuntimeExt.Shared.SafeInvokeAsync<string>("blazorMonaco.editor.colorize", text, languageId, options);

        public static Task ColorizeElement(string elementId, ColorizerElementOptions options)
            => JsRuntimeExt.Shared.SafeInvokeAsync<string>("blazorMonaco.editor.colorizeElement", elementId, options);

        public static Task<string> ColorizeModelLine(TextModel model, int lineNumber, int? tabSize = null)
            => JsRuntimeExt.Shared.SafeInvokeAsync<string>("blazorMonaco.editor.colorizeModelLine", model.Uri, lineNumber, tabSize);

        protected static Task Create(string id, StandaloneEditorConstructionOptions options, DotNetObjectReference<MonacoEditorBase> jsObjectRef)
        {
            options = options ?? new StandaloneEditorConstructionOptions
            {
                Language = "javascript"
            };

            // Convert the options object into a JsonElement to get rid of the properties with null values
            var optionsJson = JsonSerializer.Serialize(options, new JsonSerializerOptions
            {
                IgnoreNullValues = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
            var optionsDict = JsonSerializer.Deserialize<JsonElement>(optionsJson);

            // Create the editor
            return JsRuntimeExt.Shared.SafeInvokeAsync("blazorMonaco.editor.create", id, optionsDict, jsObjectRef);
        }

        protected static Task CreateDiffEditor(string id, StandaloneDiffEditorConstructionOptions options, DotNetObjectReference<MonacoEditorBase> jsObjectRef)
        {
            options = options ?? new StandaloneDiffEditorConstructionOptions();

            // Convert the options object into a JsonElement to get rid of the properties with null values
            var optionsJson = JsonSerializer.Serialize(options, new JsonSerializerOptions
            {
                IgnoreNullValues = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
            var optionsDict = JsonSerializer.Deserialize<JsonElement>(optionsJson);

            // Create the editor
            return JsRuntimeExt.Shared.SafeInvokeAsync("blazorMonaco.editor.createDiffEditor", id, optionsDict, jsObjectRef);
        }

        // createDiffNavigator

        public static Task<TextModel> CreateModel(string value, string language = null, string uri = null)
            => JsRuntimeExt.Shared.SafeInvokeAsync<TextModel>("blazorMonaco.editor.createModel", value, language, uri);

        // createWebWorker

        public static Task DefineTheme(string themeName, StandaloneThemeData themeData)
            => JsRuntimeExt.Shared.SafeInvokeAsync("blazorMonaco.editor.defineTheme", themeName, themeData);

        public static Task<TextModel> GetModel(string uri)
            => JsRuntimeExt.Shared.SafeInvokeAsync<TextModel>("blazorMonaco.editor.getModel", uri);

        // getModelMarkers

        public static Task<List<TextModel>> GetModels()
            => JsRuntimeExt.Shared.SafeInvokeAsync<List<TextModel>>("blazorMonaco.editor.getModels");

        // export function onDidCreateEditor(listener: (codeEditor: ICodeEditor) => void): IDisposable;

        // export function onDidChangeMarkers(listener: (e: readonly Uri[]) => void): IDisposable;

        // export function onDidCreateModel(listener: (model: ITextModel) => void): IDisposable;

        // export function onWillDisposeModel(listener: (model: ITextModel) => void): IDisposable;

        // export function onDidChangeModelLanguage(listener: (e: { readonly model: ITextModel; readonly oldLanguage: string;}) => void): IDisposable;

        // export function registerCommand(id: string, handler: (accessor: any, ...args: any[]) => void): IDisposable;

        public static Task RemeasureFonts()
            => JsRuntimeExt.Shared.SafeInvokeAsync("blazorMonaco.editor.remeasureFonts");

        public static Task SetModelLanguage(TextModel model, string languageId)
            => JsRuntimeExt.Shared.SafeInvokeAsync("blazorMonaco.editor.setModelLanguage", model.Uri, languageId);

        // setModelMarkers

        public static Task SetTheme(string newTheme)
            => JsRuntimeExt.Shared.SafeInvokeAsync("blazorMonaco.editor.setTheme", newTheme);

        // tokenize

        #endregion

        #region Instance Methods

        public Task AddAction(string actionId, string label, int[] keyCodes, string precondition, string keybindingContext, string contextMenuGroupId, double contextMenuOrder, Action<MonacoEditorBase, int[]> action)
        {
            actions[string.Join(";", keyCodes)] = action;
            return jsRuntime.SafeInvokeAsync("blazorMonaco.editor.addAction", Id, actionId, label, keyCodes, precondition, keybindingContext, contextMenuGroupId, contextMenuOrder);
        }

        public Task AddCommand(int keyCode, Action<MonacoEditorBase, int> action)
        {
            commands[keyCode.ToString()] = action;
            return jsRuntime.SafeInvokeAsync("blazorMonaco.editor.addCommand", Id, keyCode);
        }

        // createContextKey

        public Task DisposeEditor()
            => jsRuntime.SafeInvokeAsync("blazorMonaco.editor.dispose", Id);

        public Task Focus()
            => jsRuntime.SafeInvokeAsync("blazorMonaco.editor.focus", Id);

        // getDomNode

        public Task<string> GetEditorType()
            => jsRuntime.SafeInvokeAsync<string>("blazorMonaco.editor.getEditorType", Id);

        // getId

        public Task<Position> GetPosition()
            => jsRuntime.SafeInvokeAsync<Position>("blazorMonaco.editor.getPosition", Id);

        public Task<Selection> GetSelection()
            => jsRuntime.SafeInvokeAsync<Selection>("blazorMonaco.editor.getSelection", Id);

        public Task<List<Selection>> GetSelections()
            => jsRuntime.SafeInvokeAsync<List<Selection>>("blazorMonaco.editor.getSelections", Id);

        // getSupportedActions

        public Task<int> GetVisibleColumnFromPosition(Position position)
            => jsRuntime.SafeInvokeAsync<int>("blazorMonaco.editor.getVisibleColumnFromPosition", Id, position);

        public Task<bool> HasTextFocus()
            => jsRuntime.SafeInvokeAsync<bool>("blazorMonaco.editor.hasTextFocus", Id);

        public Task Layout(Dimension dimension = null)
            => jsRuntime.SafeInvokeAsync<string>("blazorMonaco.editor.layout", Id, dimension);

        public Task RevealLine(int lineNumber, ScrollType? scrollType = null)
            => jsRuntime.SafeInvokeAsync("blazorMonaco.editor.revealLine", Id, lineNumber, scrollType);

        public Task RevealLineInCenter(int lineNumber, ScrollType? scrollType = null)
            => jsRuntime.SafeInvokeAsync("blazorMonaco.editor.revealLineInCenter", Id, lineNumber, scrollType);

        public Task RevealLineInCenterIfOutsideViewport(int lineNumber, ScrollType? scrollType = null)
            => jsRuntime.SafeInvokeAsync("blazorMonaco.editor.revealLineInCenterIfOutsideViewport", Id, lineNumber, scrollType);

        // revealLineNearTop(lineNumber: number, scrollType?: ScrollType) : void;

        public Task RevealLines(int startLineNumber, int endLineNumber, ScrollType? scrollType = null)
            => jsRuntime.SafeInvokeAsync("blazorMonaco.editor.revealLines", Id, startLineNumber, endLineNumber, scrollType);

        public Task RevealLinesInCenter(int startLineNumber, int endLineNumber, ScrollType? scrollType = null)
            => jsRuntime.SafeInvokeAsync("blazorMonaco.editor.revealLinesInCenter", Id, startLineNumber, endLineNumber, scrollType);

        public Task RevealLinesInCenterIfOutsideViewport(int startLineNumber, int endLineNumber, ScrollType? scrollType = null)
            => jsRuntime.SafeInvokeAsync("blazorMonaco.editor.revealLinesInCenterIfOutsideViewport", Id, startLineNumber, endLineNumber, scrollType);

        // revealLinesNearTop(lineNumber: number, endLineNumber: number, scrollType?: ScrollType) : void;

        public Task RevealPosition(Position position, ScrollType? scrollType = null)
            => jsRuntime.SafeInvokeAsync("blazorMonaco.editor.revealPosition", Id, position, scrollType);

        public Task RevealPositionInCenter(Position position, ScrollType? scrollType = null)
            => jsRuntime.SafeInvokeAsync("blazorMonaco.editor.revealPositionInCenter", Id, position, scrollType);

        public Task RevealPositionInCenterIfOutsideViewport(Position position, ScrollType? scrollType = null)
            => jsRuntime.SafeInvokeAsync("blazorMonaco.editor.revealPositionInCenterIfOutsideViewport", Id, position, scrollType);

        // revealPositionNearTop(position: IPosition, scrollType?: ScrollType) : void;

        public Task RevealRange(Range range, ScrollType? scrollType = null)
            => jsRuntime.SafeInvokeAsync("blazorMonaco.editor.revealRange", Id, range, scrollType);

        public Task RevealRangeAtTop(Range range, ScrollType? scrollType = null)
            => jsRuntime.SafeInvokeAsync("blazorMonaco.editor.revealRangeAtTop", Id, range, scrollType);

        public Task RevealRangeInCenter(Range range, ScrollType? scrollType = null)
            => jsRuntime.SafeInvokeAsync("blazorMonaco.editor.revealRangeInCenter", Id, range, scrollType);

        public Task RevealRangeInCenterIfOutsideViewport(Range range, ScrollType? scrollType = null)
            => jsRuntime.SafeInvokeAsync("blazorMonaco.editor.revealRangeInCenterIfOutsideViewport", Id, range, scrollType);

        // revealRangeNearTop(range: IRange, scrollType?: ScrollType) : void;

        // revealRangeNearTopIfOutsideViewport(range: IRange, scrollType?: ScrollType) : void;

        public Task SetPosition(Position position, string source)
            => jsRuntime.SafeInvokeAsync("blazorMonaco.editor.setPosition", Id, position, source);

        internal Task SetEventListener(string eventName)
            => jsRuntime.SafeInvokeAsync("blazorMonaco.editor.setEventListener", Id, eventName);

        public Task SetSelection(Range selection, string source)
            => jsRuntime.SafeInvokeAsync("blazorMonaco.editor.setSelection", Id, selection, source);

        public Task SetSelection(Selection selection, string source)
            => jsRuntime.SafeInvokeAsync("blazorMonaco.editor.setSelection", Id, selection, source);

        public Task SetSelections(List<Selection> selections, string source)
            => jsRuntime.SafeInvokeAsync("blazorMonaco.editor.setSelections", Id, selections, source);

        public Task Trigger(string source, string handlerId, object payload = null)
        {
            var payloadJsonElement = JsonSerializer.Deserialize<JsonElement>(JsonSerializer.Serialize(payload));
            return jsRuntime.SafeInvokeAsync("blazorMonaco.editor.trigger", Id, source, handlerId, payloadJsonElement);
        }

        #endregion
    }
}
