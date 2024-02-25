using BlazorMonaco.Helpers;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BlazorMonaco.Editor
{
    public partial class StandaloneCodeEditor : CodeEditor
    {
        #region Blazor

        [Parameter]
        public Func<StandaloneCodeEditor, StandaloneEditorConstructionOptions> ConstructionOptions { get; set; }

        protected readonly Dictionary<string, List<ActionDescriptor>> _actions = new Dictionary<string, List<ActionDescriptor>>();
        protected readonly Dictionary<string, List<CommandHandler>> _commands = new Dictionary<string, List<CommandHandler>>();

        [JSInvokable]
        public void ActionCallback(string actionId)
        {
            if (!_actions.TryGetValue(actionId, out var actionDescriptors))
                return;
            actionDescriptors?.ForEach(descriptor => descriptor.Run.Invoke(this));
        }

        [JSInvokable]
        public void CommandCallback(int keyCode)
        {
            if (!_commands.TryGetValue(keyCode.ToString(), out var commandHandlers))
                return;
            commandHandlers.ForEach(handler => handler.Invoke(this, keyCode));
        }

        internal static StandaloneCodeEditor CreateVirtualEditor(IJSRuntime jsRuntime, string id, string cssClass = null)
        {
            var virtual_editor = new StandaloneCodeEditor
            {
                Id = id,
                CssClass = cssClass,
                JsRuntime = jsRuntime
            };
            virtual_editor._dotnetObjectRef = DotNetObjectReference.Create<Editor>(virtual_editor);
            return virtual_editor;
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                // Get options
                var options = ConstructionOptions?.Invoke(this);

                // Prepare the line numbers callback
                LineNumbersLambda = options?.LineNumbersLambda;
                if (LineNumbersLambda != null)
                {
                    options.LineNumbers = "function";
                    options.LineNumbersLambda = null;
                }

                // Create the editor
                await Global.Create(JsRuntime, Id, options, null, _dotnetObjectRef);
            }
            await base.OnAfterRenderAsync(firstRender);
        }

        #endregion

        public Task UpdateOptions(EditorUpdateOptions newOptions)
        {
            // Convert the options object into a JsonElement to get rid of the properties with null values
            var optionsJson = JsonSerializer.Serialize(newOptions, JsonSerializerExt.DefaultOptions);
            var optionsDict = JsonSerializer.Deserialize<JsonElement>(optionsJson);
            return JsRuntime.SafeInvokeAsync("blazorMonaco.editor.updateOptions", Id, optionsDict);
        }
        
        [Obsolete("This method is deprecated. Use AddCommand(int keybinding, CommandHandler handler, string context = null) instead.")]
        public Task AddCommand(int keybinding, Action<StandaloneCodeEditor, int> handler)
        {
            return AddCommand(keybinding, _ =>
            {
                handler?.Invoke(this, keybinding);
            });
        }
        public Task<string> AddCommand(int keybinding, CommandHandler handler, string context = null)
        {
            if (_commands.ContainsKey(keybinding.ToString()))
            {
                _commands[keybinding.ToString()].Add(handler);
                return Task.FromResult("");
            }

            _commands[keybinding.ToString()] = new List<CommandHandler> { handler };
            return JsRuntime.SafeInvokeAsync<string>("blazorMonaco.editor.addCommand", Id, keybinding, context);
        }

        //createContextKey<T extends ContextKeyValue = ContextKeyValue>(key: string, defaultValue: T): IContextKey<T>;

        [Obsolete("This method is deprecated. Use AddAction(ActionDescriptor actionDescriptor) instead.")]
        public Task AddAction(string actionId, string label, int[] keybindings, string precondition, string keybindingContext, string contextMenuGroupId, double contextMenuOrder, Action<CodeEditor, int[]> action)
        {
            var actionDescriptor = new ActionDescriptor
            {
                Id = actionId,
                Label = label,
                Precondition = precondition,
                Keybindings = keybindings,
                KeybindingContext = keybindingContext,
                ContextMenuGroupId = contextMenuGroupId,
                ContextMenuOrder = (float)contextMenuOrder,
                Run = (editor) => action(editor, keybindings)
            };
            return AddAction(actionDescriptor);
        }
        public Task AddAction(ActionDescriptor actionDescriptor)
        {
            if (_actions.ContainsKey(actionDescriptor.Id))
            {
                _actions[actionDescriptor.Id].Add(actionDescriptor);
                return Task.CompletedTask;
            }

            _actions[actionDescriptor.Id] = new List<ActionDescriptor> { actionDescriptor };
            return JsRuntime.SafeInvokeAsync("blazorMonaco.editor.addAction", Id, actionDescriptor);
        }
    }

    /**
     * A rich code editor.
     */
    public class CodeEditor : Editor
    {
        #region Blazor

        private readonly List<string> _deltaDecorationIds = new List<string>();
        public Task ResetDeltaDecorations()
            => DeltaDecorations(_deltaDecorationIds.ToArray(), new ModelDeltaDecoration[0]);

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
            if (OnWillChangeModel.HasDelegate)
                await SetEventListener("OnWillChangeModel");
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
                case "OnDidChangeConfiguration": await OnDidChangeConfiguration.InvokeAsync(new ConfigurationChangedEvent(JsonSerializer.Deserialize<List<bool>>(eventJson, jsonOptions))); break;
                case "OnDidChangeCursorPosition": await OnDidChangeCursorPosition.InvokeAsync(JsonSerializer.Deserialize<CursorPositionChangedEvent>(eventJson, jsonOptions)); break;
                case "OnDidChangeCursorSelection": await OnDidChangeCursorSelection.InvokeAsync(JsonSerializer.Deserialize<CursorSelectionChangedEvent>(eventJson, jsonOptions)); break;
                case "OnWillChangeModel": await OnWillChangeModel.InvokeAsync(JsonSerializer.Deserialize<ModelChangedEvent>(eventJson, jsonOptions)); break;
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
                case "OnMouseLeave": if (eventJson != null) await OnMouseLeave.InvokeAsync(JsonSerializer.Deserialize<PartialEditorMouseEvent>(eventJson, jsonOptions)); break;
                case "OnMouseMove": if (eventJson != null) await OnMouseMove.InvokeAsync(JsonSerializer.Deserialize<EditorMouseEvent>(eventJson, jsonOptions)); break;
                case "OnMouseUp": if (eventJson != null) await OnMouseUp.InvokeAsync(JsonSerializer.Deserialize<EditorMouseEvent>(eventJson, jsonOptions)); break;
            }

            await base.EventCallback(eventName, eventJson);
        }

        private CursorStateComputer ExecuteEditsLambda { get; set; }

        [JSInvokable]
        public List<Selection> ExecuteEditsCallback(List<ValidEditOperation> inverseEditOperations)
        {
            Console.WriteLine("ExecuteEditsCallback is called : " + JsonSerializer.Serialize(inverseEditOperations));
            return ExecuteEditsLambda?.Invoke(inverseEditOperations);
        }

        #endregion

        /**
         * An event emitted when the content of the current model has changed.
         * @event
         */
        [Parameter] public EventCallback<ModelContentChangedEvent> OnDidChangeModelContent { get; set; }
        /**
         * An event emitted when the language of the current model has changed.
         * @event
         */
        [Parameter] public EventCallback<ModelLanguageChangedEvent> OnDidChangeModelLanguage { get; set; }
        /**
         * An event emitted when the language configuration of the current model has changed.
         * @event
         */
        [Parameter] public EventCallback<ModelLanguageConfigurationChangedEvent> OnDidChangeModelLanguageConfiguration { get; set; }
        /**
         * An event emitted when the options of the current model has changed.
         * @event
         */
        [Parameter] public EventCallback<ModelOptionsChangedEvent> OnDidChangeModelOptions { get; set; }
        /**
         * An event emitted when the configuration of the editor has changed. (e.g. `editor.updateOptions()`)
         * @event
         */
        [Parameter] public EventCallback<ConfigurationChangedEvent> OnDidChangeConfiguration { get; set; }
        /**
         * An event emitted when the cursor position has changed.
         * @event
         */
        [Parameter] public EventCallback<CursorPositionChangedEvent> OnDidChangeCursorPosition { get; set; }
        /**
         * An event emitted when the cursor selection has changed.
         * @event
         */
        [Parameter] public EventCallback<CursorSelectionChangedEvent> OnDidChangeCursorSelection { get; set; }
        /**
         * An event emitted when the model of this editor is about to change (e.g. from `editor.setModel()`).
         * @event
         */
        [Parameter] public EventCallback<ModelChangedEvent> OnWillChangeModel { get; set; }
        /**
         * An event emitted when the model of this editor has changed (e.g. `editor.setModel()`).
         * @event
         */
        [Parameter] public EventCallback<ModelChangedEvent> OnDidChangeModel { get; set; }
        /**
         * An event emitted when the decorations of the current model have changed.
         * @event
         */
        [Parameter] public EventCallback<ModelDecorationsChangedEvent> OnDidChangeModelDecorations { get; set; }
        /**
         * An event emitted when the text inside this editor gained focus (i.e. cursor starts blinking).
         * @event
         */
        [Parameter] public EventCallback OnDidFocusEditorText { get; set; }
        /**
         * An event emitted when the text inside this editor lost focus (i.e. cursor stops blinking).
         * @event
         */
        [Parameter] public EventCallback OnDidBlurEditorText { get; set; }
        /**
         * An event emitted when the text inside this editor or an editor widget gained focus.
         * @event
         */
        [Parameter] public EventCallback OnDidFocusEditorWidget { get; set; }
        /**
         * An event emitted when the text inside this editor or an editor widget lost focus.
         * @event
         */
        [Parameter] public EventCallback OnDidBlurEditorWidget { get; set; }
        /**
         * An event emitted after composition has started.
         */
        [Parameter] public EventCallback OnDidCompositionStart { get; set; }
        /**
         * An event emitted after composition has ended.
         */
        [Parameter] public EventCallback OnDidCompositionEnd { get; set; }
        /**
         * An event emitted when editing failed because the editor is read-only.
         * @event
         */
        //readonly onDidAttemptReadOnlyEdit: IEvent<void>;
        /**
         * An event emitted when users paste text in the editor.
         * @event
         */
        [Parameter] public EventCallback<PasteEvent> OnDidPaste { get; set; }
        /**
         * An event emitted on a "mouseup".
         * @event
         */
        [Parameter] public EventCallback<EditorMouseEvent> OnMouseUp { get; set; }
        /**
         * An event emitted on a "mousedown".
         * @event
         */
        [Parameter] public EventCallback<EditorMouseEvent> OnMouseDown { get; set; }
        /**
         * An event emitted on a "contextmenu".
         * @event
         */
        [Parameter] public EventCallback<EditorMouseEvent> OnContextMenu { get; set; }
        /**
         * An event emitted on a "mousemove".
         * @event
         */
        [Parameter] public EventCallback<EditorMouseEvent> OnMouseMove { get; set; }
        /**
         * An event emitted on a "mouseleave".
         * @event
         */
        [Parameter] public EventCallback<PartialEditorMouseEvent> OnMouseLeave { get; set; }
        /**
         * An event emitted on a "keyup".
         * @event
         */
        [Parameter] public EventCallback<KeyboardEvent> OnKeyUp { get; set; }
        /**
         * An event emitted on a "keydown".
         * @event
         */
        [Parameter] public EventCallback<KeyboardEvent> OnKeyDown { get; set; }
        /**
         * An event emitted when the layout of the editor has changed.
         * @event
         */
        [Parameter] public EventCallback<EditorLayoutInfo> OnDidLayoutChange { get; set; }
        /**
         * An event emitted when the content width or content height in the editor has changed.
         * @event
         */
        [Parameter] public EventCallback<ContentSizeChangedEvent> OnDidContentSizeChange { get; set; }
        /**
         * An event emitted when the scroll in the editor has changed.
         * @event
         */
        [Parameter] public EventCallback<ScrollEvent> OnDidScrollChange { get; set; }
        /**
         * An event emitted when hidden areas change in the editor (e.g. due to folding).
         * @event
         */
        //readonly onDidChangeHiddenAreas: IEvent<void>;
        /**
         * Saves current view state of the editor in a serializable object.
         */
        //saveViewState(): ICodeEditorViewState | null;
        /**
         * Restores the view state of the editor from a serializable object generated by `saveViewState`.
         */
        //restoreViewState(state: ICodeEditorViewState | null): void;
        /**
         * Returns true if the text inside this editor or an editor widget has focus.
         */
        public Task<bool> HasWidgetFocus()
            => JsRuntime.SafeInvokeAsync<bool>("blazorMonaco.editor.hasWidgetFocus", Id);
        /**
         * Get a contribution of this editor.
         * @id Unique identifier of the contribution.
         * @return The contribution or null if contribution not found.
         */
        //getContribution<T extends IEditorContribution>(id: string): T | null;
        /**
         * Type the getModel() of IEditor.
         */
        public async Task<TextModel> GetModel()
        {
            var textModel = await JsRuntime.SafeInvokeAsync<TextModel>("blazorMonaco.editor.getInstanceModel", Id);
            if (textModel != null)
                textModel.JsRuntime = JsRuntime;
            return textModel;
        }

        /**
         * Sets the current model attached to this editor.
         * If the previous model was created by the editor via the value key in the options
         * literal object, it will be destroyed. Otherwise, if the previous model was set
         * via setModel, or the model key in the options literal object, the previous model
         * will not be destroyed.
         * It is safe to call setModel(null) to simply detach the current model from the editor.
         */
        public Task SetModel(TextModel model)
            => JsRuntime.SafeInvokeAsync("blazorMonaco.editor.setInstanceModel", Id, model.Uri);
        /**
         * Gets all the editor computed options.
         */
        public async Task<ComputedEditorOptions> GetOptions()
        {
            var strList = await JsRuntime.SafeInvokeAsync<List<string>>("blazorMonaco.editor.getOptions", Id);
            return new ComputedEditorOptions(strList);
        }
        /**
         * Gets a specific editor option.
         */
        public async Task<T> GetOption<T>(EditorOption option)
        {
            var strValue = await JsRuntime.SafeInvokeAsync<string>("blazorMonaco.editor.getOption", Id, (int)option);
            return JsonSerializer.Deserialize<T>(strValue);
        }
        /**
         * Returns the editor's configuration (without any validation or defaults).
         */
        public Task<EditorOptions> GetRawOptions()
            => JsRuntime.SafeInvokeAsync<EditorOptions>("blazorMonaco.editor.getRawOptions", Id);
        /**
         * Get value of the current model attached to this editor.
         * @see {@link ITextModel.getValue}
         */
        public Task<string> GetValue(bool? preserveBOM = null, string lineEnding = null)
            => JsRuntime.SafeInvokeAsync<string>("blazorMonaco.editor.getValue", Id, preserveBOM, lineEnding);
        /**
         * Set the value of the current model attached to this editor.
         * @see {@link ITextModel.setValue}
         */
        public Task SetValue(string newValue)
            => JsRuntime.SafeInvokeAsync("blazorMonaco.editor.setValue", Id, newValue);
        /**
         * Get the width of the editor's content.
         * This is information that is "erased" when computing `scrollWidth = Math.max(contentWidth, width)`
         */
        public Task<double> GetContentWidth()
            => JsRuntime.SafeInvokeAsync<double>("blazorMonaco.editor.getContentWidth", Id);
        /**
         * Get the scrollWidth of the editor's viewport.
         */
        public Task<double> GetScrollWidth()
            => JsRuntime.SafeInvokeAsync<double>("blazorMonaco.editor.getScrollWidth", Id);
        /**
         * Get the scrollLeft of the editor's viewport.
         */
        public Task<double> GetScrollLeft()
            => JsRuntime.SafeInvokeAsync<double>("blazorMonaco.editor.getScrollLeft", Id);
        /**
         * Get the height of the editor's content.
         * This is information that is "erased" when computing `scrollHeight = Math.max(contentHeight, height)`
         */
        public Task<double> GetContentHeight()
            => JsRuntime.SafeInvokeAsync<double>("blazorMonaco.editor.getContentHeight", Id);
        /**
         * Get the scrollHeight of the editor's viewport.
         */
        public Task<double> GetScrollHeight()
            => JsRuntime.SafeInvokeAsync<double>("blazorMonaco.editor.getScrollHeight", Id);
        /**
         * Get the scrollTop of the editor's viewport.
         */
        public Task<double> GetScrollTop()
            => JsRuntime.SafeInvokeAsync<double>("blazorMonaco.editor.getScrollTop", Id);
        /**
         * Change the scrollLeft of the editor's viewport.
         */
        public Task SetScrollLeft(int newScrollLeft, ScrollType? scrollType = null)
            => JsRuntime.SafeInvokeAsync("blazorMonaco.editor.setScrollLeft", Id, newScrollLeft, scrollType);
        /**
         * Change the scrollTop of the editor's viewport.
         */
        public Task SetScrollTop(int newScrollTop, ScrollType? scrollType = null)
            => JsRuntime.SafeInvokeAsync("blazorMonaco.editor.setScrollTop", Id, newScrollTop, scrollType);
        /**
         * Change the scroll position of the editor's viewport.
         */
        public Task SetScrollPosition(NewScrollPosition position, ScrollType? scrollType = null)
            => JsRuntime?.SafeInvokeAsync("blazorMonaco.editor.setScrollPosition", Id, position, scrollType);
        /**
         * Check if the editor is currently scrolling towards a different scroll position.
         */
        public Task<bool> HasPendingScrollAnimation()
            => JsRuntime?.SafeInvokeAsync<bool>("blazorMonaco.editor.hasPendingScrollAnimation", Id);
        /**
         * Get an action that is a contribution to this editor.
         * @id Unique identifier of the contribution.
         * @return The action or null if action not found.
         */
        //getAction(id: string): IEditorAction | null;
        /**
         * Execute a command on the editor.
         * The edits will land on the undo-redo stack, but no "undo stop" will be pushed.
         * @param source The source of the call.
         * @param command The command to execute
         */
        //executeCommand(source: string | null | undefined, command: ICommand): void;
        /**
         * Create an "undo stop" in the undo-redo stack.
         */
        public Task<bool> PushUndoStop()
            => JsRuntime.SafeInvokeAsync<bool>("blazorMonaco.editor.pushUndoStop", Id);
        /**
         * Remove the "undo stop" in the undo-redo stack.
         */
        public Task<bool> PopUndoStop()
            => JsRuntime.SafeInvokeAsync<bool>("blazorMonaco.editor.popUndoStop", Id);
        /**
         * Execute edits on the editor.
         * The edits will land on the undo-redo stack, but no "undo stop" will be pushed.
         * @param source The source of the call.
         * @param edits The edits to execute.
         * @param endCursorState Cursor state after the edits were applied.
         */
        public Task<bool> ExecuteEdits(string source, List<IdentifiedSingleEditOperation> edits, List<Selection> endCursorState)
            => JsRuntime.SafeInvokeAsync<bool>("blazorMonaco.editor.executeEdits", Id, source, edits, endCursorState);
        public Task<bool> ExecuteEdits(string source, List<IdentifiedSingleEditOperation> edits, CursorStateComputer endCursorState)
        {
            ExecuteEditsLambda = endCursorState;
            return JsRuntime.SafeInvokeAsync<bool>("blazorMonaco.editor.executeEdits", Id, source, edits, "function");
        }
        /**
         * Execute multiple (concomitant) commands on the editor.
         * @param source The source of the call.
         * @param command The commands to execute
         */
        //executeCommands(source: string | null | undefined, commands: (ICommand | null)[]): void;
        /**
         * Get all the decorations on a line (filtering out decorations from other editors).
         */
        //getLineDecorations(lineNumber: number): IModelDecoration[] | null;
        /**
         * Get all the decorations for a range (filtering out decorations from other editors).
         */
        //getDecorationsInRange(range: Range): IModelDecoration[] | null;
        /**
         * All decorations added through this call will get the ownerId of this editor.
         * @deprecated Use `createDecorationsCollection`
         * @see createDecorationsCollection
         */
        public async Task<string[]> DeltaDecorations(string[] oldDecorationIds, ModelDeltaDecoration[] newDecorations)
        {
            oldDecorationIds = oldDecorationIds ?? new string[] { };

            // Convert the newDecorations object into a JsonElement to get rid of the properties with null values
            var newDecorationsJson = JsonSerializer.Serialize(newDecorations, JsonSerializerExt.DefaultOptions);
            var newDecorationsElement = JsonSerializer.Deserialize<JsonElement>(newDecorationsJson);
            var newDecorationIds = await JsRuntime.SafeInvokeAsync<string[]>("blazorMonaco.editor.deltaDecorations", Id, oldDecorationIds, newDecorationsElement);
            _deltaDecorationIds.RemoveAll(d => oldDecorationIds.Any(o => o == d));
            _deltaDecorationIds.AddRange(newDecorationIds);
            return newDecorationIds;
        }
        /**
         * Remove previously added decorations.
         */
        //removeDecorations(decorationIds: string[]) : void;
        /**
         * Get the layout info for the editor.
         */
        public Task<EditorLayoutInfo> GetLayoutInfo()
            => JsRuntime.SafeInvokeAsync<EditorLayoutInfo>("blazorMonaco.editor.getLayoutInfo", Id);
        /**
         * Returns the ranges that are currently visible.
         * Does not account for horizontal scrolling.
         */
        public Task<List<Range>> GetVisibleRanges()
            => JsRuntime.SafeInvokeAsync<List<Range>>("blazorMonaco.editor.getVisibleRanges", Id);
        /**
         * Get the vertical position (top offset) for the line's top w.r.t. to the first line.
         */
        public Task<double> GetTopForLineNumber(int lineNumber, bool? includeViewZones = null)
            => JsRuntime.SafeInvokeAsync<double>("blazorMonaco.editor.getTopForLineNumber", Id, lineNumber, includeViewZones);
        /**
         * Get the vertical position (top offset) for the line's bottom w.r.t. to the first line.
         */
        //getBottomForLineNumber(lineNumber: number) : number;
        /**
         * Get the vertical position (top offset) for the position w.r.t. to the first line.
         */
        public Task<double> GetTopForPosition(int lineNumber, int column)
            => JsRuntime.SafeInvokeAsync<double>("blazorMonaco.editor.getTopForPosition", Id, lineNumber, column);
        /**
         * Write the screen reader content to be the current selection
         */
        public Task WriteScreenReaderContent(string reason)
            => JsRuntime.SafeInvokeAsync("blazorMonaco.editor.writeScreenReaderContent", Id, reason);
        /**
         * Returns the editor's container dom node
         */
        public Task<string> GetContainerDomNodeId()
            => JsRuntime.SafeInvokeAsync<string>("blazorMonaco.editor.getContainerDomNodeId", Id);
        /**
         * Returns the editor's dom node
         */
        public Task<string> GetDomNodeId()
            => JsRuntime.SafeInvokeAsync<string>("blazorMonaco.editor.getDomNodeId", Id);
        /**
         * Add a content widget. Widgets must have unique ids, otherwise they will be overwritten.
         */
        //addContentWidget(widget: IContentWidget): void;
        /**
         * Layout/Reposition a content widget. This is a ping to the editor to call widget.getPosition()
         * and update appropriately.
         */
        //layoutContentWidget(widget: IContentWidget): void;
        /**
         * Remove a content widget.
         */
        //removeContentWidget(widget: IContentWidget): void;
        /**
         * Add an overlay widget. Widgets must have unique ids, otherwise they will be overwritten.
         */
        //addOverlayWidget(widget: IOverlayWidget): void;
        /**
         * Layout/Reposition an overlay widget. This is a ping to the editor to call widget.getPosition()
         * and update appropriately.
         */
        //layoutOverlayWidget(widget: IOverlayWidget): void;
        /**
         * Remove an overlay widget.
         */
        //removeOverlayWidget(widget: IOverlayWidget): void;
        /**
         * Add a glyph margin widget. Widgets must have unique ids, otherwise they will be overwritten.
         */
        //addGlyphMarginWidget(widget: IGlyphMarginWidget) : void;
        /**
         * Layout/Reposition a glyph margin widget. This is a ping to the editor to call widget.getPosition()
         * and update appropriately.
         */
        //layoutGlyphMarginWidget(widget: IGlyphMarginWidget) : void;
        /**
         * Remove a glyph margin widget.
         */
        //removeGlyphMarginWidget(widget: IGlyphMarginWidget) : void;
        /**
         * Change the view zones. View zones are lost when a new model is attached to the editor.
         */
        //changeViewZones(callback: (accessor: IViewZoneChangeAccessor) => void): void;
        /**
         * Get the horizontal position (left offset) for the column w.r.t to the beginning of the line.
         * This method works only if the line `lineNumber` is currently rendered (in the editor's viewport).
         * Use this method with caution.
         */
        public Task<int> GetOffsetForColumn(int lineNumber, int column)
            => JsRuntime.SafeInvokeAsync<int>("blazorMonaco.editor.getOffsetForColumn", Id, lineNumber, column);
        /**
         * Force an editor render now.
         */
        public Task Render(bool? forceRedraw = null)
            => JsRuntime.SafeInvokeAsync("blazorMonaco.editor.render", Id, forceRedraw);
        /**
         * Get the hit test target at coordinates `clientX` and `clientY`.
         * The coordinates are relative to the top-left of the viewport.
         *
         * @returns Hit test target or null if the coordinates fall outside the editor or the editor has no model.
         */
        public Task<BaseMouseTarget> GetTargetAtClientPoint(int clientX, int clientY)
            => JsRuntime.SafeInvokeAsync<BaseMouseTarget>("blazorMonaco.editor.getTargetAtClientPoint", Id, clientX, clientY);
        /**
         * Get the visible position for `position`.
         * The result position takes scrolling into account and is relative to the top left corner of the editor.
         * Explanation 1: the results of this method will change for the same `position` if the user scrolls the editor.
         * Explanation 2: the results of this method will not change if the container of the editor gets repositioned.
         * Warning: the results of this method are inaccurate for positions that are outside the current editor viewport.
         */
        public Task<ScrolledVisiblePosition> GetScrolledVisiblePosition(Position position)
            => JsRuntime.SafeInvokeAsync<ScrolledVisiblePosition>("blazorMonaco.editor.getScrolledVisiblePosition", Id, position);
        /**
         * Apply the same font settings as the editor to `target`.
         */
        //applyFontInfo(target: HTMLElement): void;
        //setBanner(bannerDomNode: HTMLElement | null, height: number): void;
        /**
         * Is called when the model has been set, view state was restored and options are updated.
         * This is the best place to compute data for the viewport (such as tokens).
         */
        //handleInitialized? (): void;
    }
}
