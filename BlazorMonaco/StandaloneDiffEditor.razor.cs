using BlazorMonaco.Helpers;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

namespace BlazorMonaco.Editor
{
    public partial class StandaloneDiffEditor : DiffEditor
    {
        #region Blazor

        [Parameter]
        public Func<StandaloneDiffEditor, StandaloneDiffEditorConstructionOptions> ConstructionOptions { get; set; }

        protected readonly Dictionary<string, List<ActionDescriptor>> _actions = new Dictionary<string, List<ActionDescriptor>>();
        protected readonly Dictionary<string, List<CommandHandler>> _commands = new Dictionary<string, List<CommandHandler>>();

        [JSInvokable]
        public void ActionCallback(string actionId)
        {
            if (!_actions.TryGetValue(actionId, out var actionDescriptors))
                return;
            actionDescriptors?.ForEach(descriptor => descriptor.Run.Invoke(ModifiedEditor)); // actions of a diff editor run only for its modified editor
        }

        [JSInvokable]
        public void CommandCallback(int keyCode)
        {
            if (!_commands.TryGetValue(keyCode.ToString(), out var commandHandlers))
                return;
            commandHandlers.ForEach(handler => handler.Invoke(this, keyCode));
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

                // Create the bridges for the inner editors
                _originalEditor = StandaloneCodeEditor.CreateVirtualEditor(JsRuntime, Id + "_original");
                _modifiedEditor = StandaloneCodeEditor.CreateVirtualEditor(JsRuntime, Id + "_modified");

                // Create the editor
                await Global.CreateDiffEditor(JsRuntime, Id, options, null, _dotnetObjectRef, OriginalEditor._dotnetObjectRef, ModifiedEditor._dotnetObjectRef);
            }
            await base.OnAfterRenderAsync(firstRender);
        }

        #endregion

        [Obsolete("This method is deprecated. Use AddCommand(int keybinding, CommandHandler handler, string context = null) instead.")]
        public Task AddCommand(int keybinding, Action<StandaloneDiffEditor, int> handler)
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

        public new StandaloneCodeEditor OriginalEditor => _originalEditor as StandaloneCodeEditor;
        public new StandaloneCodeEditor ModifiedEditor => _modifiedEditor as StandaloneCodeEditor;
    }

    /**
     * A rich diff editor.
     */
    public class DiffEditor : Editor
    {
        #region Blazor

        protected CodeEditor _originalEditor;
        protected CodeEditor _modifiedEditor;

        #region Events for the original editor (left)
        [Parameter] public EventCallback OnDidDisposeOriginal { get; set; }
        [Parameter] public EventCallback OnDidInitOriginal { get; set; }
        [Parameter] public EventCallback<ModelContentChangedEvent> OnDidChangeModelContentOriginal { get; set; }
        [Parameter] public EventCallback<ModelLanguageChangedEvent> OnDidChangeModelLanguageOriginal { get; set; }
        [Parameter] public EventCallback<ModelLanguageConfigurationChangedEvent> OnDidChangeModelLanguageConfigurationOriginal { get; set; }
        [Parameter] public EventCallback<ModelOptionsChangedEvent> OnDidChangeModelOptionsOriginal { get; set; }
        [Parameter] public EventCallback<ConfigurationChangedEvent> OnDidChangeConfigurationOriginal { get; set; }
        [Parameter] public EventCallback<CursorPositionChangedEvent> OnDidChangeCursorPositionOriginal { get; set; }
        [Parameter] public EventCallback<CursorSelectionChangedEvent> OnDidChangeCursorSelectionOriginal { get; set; }
        [Parameter] public EventCallback<ModelChangedEvent> OnWillChangeModelOriginal { get; set; }
        [Parameter] public EventCallback<ModelChangedEvent> OnDidChangeModelOriginal { get; set; }
        [Parameter] public EventCallback<ModelDecorationsChangedEvent> OnDidChangeModelDecorationsOriginal { get; set; }
        [Parameter] public EventCallback OnDidFocusEditorTextOriginal { get; set; }
        [Parameter] public EventCallback OnDidBlurEditorTextOriginal { get; set; }
        [Parameter] public EventCallback OnDidFocusEditorWidgetOriginal { get; set; }
        [Parameter] public EventCallback OnDidBlurEditorWidgetOriginal { get; set; }
        [Parameter] public EventCallback OnDidCompositionStartOriginal { get; set; }
        [Parameter] public EventCallback OnDidCompositionEndOriginal { get; set; }
        //readonly onDidAttemptReadOnlyEdit: IEvent<void>;
        [Parameter] public EventCallback<PasteEvent> OnDidPasteOriginal { get; set; }
        [Parameter] public EventCallback<EditorMouseEvent> OnMouseUpOriginal { get; set; }
        [Parameter] public EventCallback<EditorMouseEvent> OnMouseDownOriginal { get; set; }
        [Parameter] public EventCallback<EditorMouseEvent> OnContextMenuOriginal { get; set; }
        [Parameter] public EventCallback<EditorMouseEvent> OnMouseMoveOriginal { get; set; }
        [Parameter] public EventCallback<PartialEditorMouseEvent> OnMouseLeaveOriginal { get; set; }
        [Parameter] public EventCallback<KeyboardEvent> OnKeyUpOriginal { get; set; }
        [Parameter] public EventCallback<KeyboardEvent> OnKeyDownOriginal { get; set; }
        [Parameter] public EventCallback<EditorLayoutInfo> OnDidLayoutChangeOriginal { get; set; }
        [Parameter] public EventCallback<ContentSizeChangedEvent> OnDidContentSizeChangeOriginal { get; set; }
        [Parameter] public EventCallback<ScrollEvent> OnDidScrollChangeOriginal { get; set; }
        //readonly onDidChangeHiddenAreas: IEvent<void>;

        #endregion

        #region Events for the modified editor (right)
        [Parameter] public EventCallback OnDidDisposeModified { get; set; }
        [Parameter] public EventCallback OnDidInitModified { get; set; }
        [Parameter] public EventCallback<ModelContentChangedEvent> OnDidChangeModelContentModified { get; set; }
        [Parameter] public EventCallback<ModelLanguageChangedEvent> OnDidChangeModelLanguageModified { get; set; }
        [Parameter] public EventCallback<ModelLanguageConfigurationChangedEvent> OnDidChangeModelLanguageConfigurationModified { get; set; }
        [Parameter] public EventCallback<ModelOptionsChangedEvent> OnDidChangeModelOptionsModified { get; set; }
        [Parameter] public EventCallback<ConfigurationChangedEvent> OnDidChangeConfigurationModified { get; set; }
        [Parameter] public EventCallback<CursorPositionChangedEvent> OnDidChangeCursorPositionModified { get; set; }
        [Parameter] public EventCallback<CursorSelectionChangedEvent> OnDidChangeCursorSelectionModified { get; set; }
        [Parameter] public EventCallback<ModelChangedEvent> OnWillChangeModelModified { get; set; }
        [Parameter] public EventCallback<ModelChangedEvent> OnDidChangeModelModified { get; set; }
        [Parameter] public EventCallback<ModelDecorationsChangedEvent> OnDidChangeModelDecorationsModified { get; set; }
        [Parameter] public EventCallback OnDidFocusEditorTextModified { get; set; }
        [Parameter] public EventCallback OnDidBlurEditorTextModified { get; set; }
        [Parameter] public EventCallback OnDidFocusEditorWidgetModified { get; set; }
        [Parameter] public EventCallback OnDidBlurEditorWidgetModified { get; set; }
        [Parameter] public EventCallback OnDidCompositionStartModified { get; set; }
        [Parameter] public EventCallback OnDidCompositionEndModified { get; set; }
        //readonly onDidAttemptReadOnlyEdit: IEvent<void>;
        [Parameter] public EventCallback<PasteEvent> OnDidPasteModified { get; set; }
        [Parameter] public EventCallback<EditorMouseEvent> OnMouseUpModified { get; set; }
        [Parameter] public EventCallback<EditorMouseEvent> OnMouseDownModified { get; set; }
        [Parameter] public EventCallback<EditorMouseEvent> OnContextMenuModified { get; set; }
        [Parameter] public EventCallback<EditorMouseEvent> OnMouseMoveModified { get; set; }
        [Parameter] public EventCallback<PartialEditorMouseEvent> OnMouseLeaveModified { get; set; }
        [Parameter] public EventCallback<KeyboardEvent> OnKeyUpModified { get; set; }
        [Parameter] public EventCallback<KeyboardEvent> OnKeyDownModified { get; set; }
        [Parameter] public EventCallback<EditorLayoutInfo> OnDidLayoutChangeModified { get; set; }
        [Parameter] public EventCallback<ContentSizeChangedEvent> OnDidContentSizeChangeModified { get; set; }
        [Parameter] public EventCallback<ScrollEvent> OnDidScrollChangeModified { get; set; }
        //readonly onDidChangeHiddenAreas: IEvent<void>;

        #endregion

        public override void Dispose()
        {
            OriginalEditor?.Dispose();
            ModifiedEditor?.Dispose();
            base.Dispose();
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
#pragma warning disable BL0005

                #region Initialize the original editor

                _originalEditor = _originalEditor ?? new CodeEditor();
                OriginalEditor.OnDidDispose = OnDidDisposeOriginal;
                OriginalEditor.OnDidInit = OnDidInitOriginal;
                OriginalEditor.OnDidChangeModelContent = OnDidChangeModelContentOriginal;
                OriginalEditor.OnDidChangeModelLanguage = OnDidChangeModelLanguageOriginal;
                OriginalEditor.OnDidChangeModelLanguageConfiguration = OnDidChangeModelLanguageConfigurationOriginal;
                OriginalEditor.OnDidChangeModelOptions = OnDidChangeModelOptionsOriginal;
                OriginalEditor.OnDidChangeConfiguration = OnDidChangeConfigurationOriginal;
                OriginalEditor.OnDidChangeCursorPosition = OnDidChangeCursorPositionOriginal;
                OriginalEditor.OnDidChangeCursorSelection = OnDidChangeCursorSelectionOriginal;
                OriginalEditor.OnWillChangeModel = OnWillChangeModelOriginal;
                OriginalEditor.OnDidChangeModel = OnDidChangeModelOriginal;
                OriginalEditor.OnDidChangeModelDecorations = OnDidChangeModelDecorationsOriginal;
                OriginalEditor.OnDidFocusEditorText = OnDidFocusEditorTextOriginal;
                OriginalEditor.OnDidBlurEditorText = OnDidBlurEditorTextOriginal;
                OriginalEditor.OnDidFocusEditorWidget = OnDidFocusEditorWidgetOriginal;
                OriginalEditor.OnDidBlurEditorWidget = OnDidBlurEditorWidgetOriginal;
                OriginalEditor.OnDidCompositionStart = OnDidCompositionStartOriginal;
                OriginalEditor.OnDidCompositionEnd = OnDidCompositionEndOriginal;
                //readonly onDidAttemptReadOnlyEdit: IEvent<void>;
                OriginalEditor.OnDidPaste = OnDidPasteOriginal;
                OriginalEditor.OnMouseUp = OnMouseUpOriginal;
                OriginalEditor.OnMouseDown = OnMouseDownOriginal;
                OriginalEditor.OnContextMenu = OnContextMenuOriginal;
                OriginalEditor.OnMouseMove = OnMouseMoveOriginal;
                OriginalEditor.OnMouseLeave = OnMouseLeaveOriginal;
                OriginalEditor.OnKeyUp = OnKeyUpOriginal;
                OriginalEditor.OnKeyDown = OnKeyDownOriginal;
                OriginalEditor.OnDidLayoutChange = OnDidLayoutChangeOriginal;
                OriginalEditor.OnDidContentSizeChange = OnDidContentSizeChangeOriginal;
                OriginalEditor.OnDidScrollChange = OnDidScrollChangeOriginal;
                //readonly onDidChangeHiddenAreas: IEvent<void>;
                await OriginalEditor.SetEventListeners();
                await OriginalEditor.OnDidInit.InvokeAsync(OriginalEditor);

                #endregion

                #region Initialize the modified editor

                _modifiedEditor = _modifiedEditor ?? new CodeEditor();
                ModifiedEditor.OnDidCompositionEnd = OnDidCompositionEndModified;
                ModifiedEditor.OnDidDispose = OnDidDisposeModified;
                ModifiedEditor.OnDidInit = OnDidInitModified;
                ModifiedEditor.OnDidChangeModelContent = OnDidChangeModelContentModified;
                ModifiedEditor.OnDidChangeModelLanguage = OnDidChangeModelLanguageModified;
                ModifiedEditor.OnDidChangeModelLanguageConfiguration = OnDidChangeModelLanguageConfigurationModified;
                ModifiedEditor.OnDidChangeModelOptions = OnDidChangeModelOptionsModified;
                ModifiedEditor.OnDidChangeConfiguration = OnDidChangeConfigurationModified;
                ModifiedEditor.OnDidChangeCursorPosition = OnDidChangeCursorPositionModified;
                ModifiedEditor.OnDidChangeCursorSelection = OnDidChangeCursorSelectionModified;
                ModifiedEditor.OnWillChangeModel = OnWillChangeModelModified;
                ModifiedEditor.OnDidChangeModel = OnDidChangeModelModified;
                ModifiedEditor.OnDidChangeModelDecorations = OnDidChangeModelDecorationsModified;
                ModifiedEditor.OnDidFocusEditorText = OnDidFocusEditorTextModified;
                ModifiedEditor.OnDidBlurEditorText = OnDidBlurEditorTextModified;
                ModifiedEditor.OnDidFocusEditorWidget = OnDidFocusEditorWidgetModified;
                ModifiedEditor.OnDidBlurEditorWidget = OnDidBlurEditorWidgetModified;
                ModifiedEditor.OnDidCompositionStart = OnDidCompositionStartModified;
                ModifiedEditor.OnDidCompositionEnd = OnDidCompositionEndModified;
                //readonly onDidAttemptReadOnlyEdit: IEvent<void>;
                ModifiedEditor.OnDidPaste = OnDidPasteModified;
                ModifiedEditor.OnMouseUp = OnMouseUpModified;
                ModifiedEditor.OnMouseDown = OnMouseDownModified;
                ModifiedEditor.OnContextMenu = OnContextMenuModified;
                ModifiedEditor.OnMouseMove = OnMouseMoveModified;
                ModifiedEditor.OnMouseLeave = OnMouseLeaveModified;
                ModifiedEditor.OnKeyUp = OnKeyUpModified;
                ModifiedEditor.OnKeyDown = OnKeyDownModified;
                ModifiedEditor.OnDidLayoutChange = OnDidLayoutChangeModified;
                ModifiedEditor.OnDidContentSizeChange = OnDidContentSizeChangeModified;
                ModifiedEditor.OnDidScrollChange = OnDidScrollChangeModified;
                //readonly onDidChangeHiddenAreas: IEvent<void>;
                await ModifiedEditor.SetEventListeners();
                await ModifiedEditor.OnDidInit.InvokeAsync(ModifiedEditor);

                #endregion

#pragma warning restore BL0005
            }
            await base.OnAfterRenderAsync(firstRender);
        }

        internal override async Task SetEventListeners()
        {
            if (OnDidUpdateDiff.HasDelegate)
                await SetEventListener("OnDidUpdateDiff");
            if (OnDidChangeModel.HasDelegate)
                await SetEventListener("OnDidChangeModel");
            await base.SetEventListeners();
        }

        [JSInvokable]
        public override async Task EventCallback(string eventName, string eventJson)
        {
            switch (eventName)
            {
                case "OnDidUpdateDiff": await OnDidUpdateDiff.InvokeAsync(this); break;
                case "OnDidChangeModel": await OnDidChangeModel.InvokeAsync(this); break;
            }
            await base.EventCallback(eventName, eventJson);
        }

        #endregion

        /**
         * @see {@link ICodeEditor.getContainerDomNode}
         */
        public Task<string> GetContainerDomNodeId()
            => JsRuntime.SafeInvokeAsync<string>("blazorMonaco.editor.getContainerDomNodeId", Id);
        /**
         * An event emitted when the diff information computed by this diff editor has been updated.
         * @event
         */
        [Parameter]
        public EventCallback<DiffEditor> OnDidUpdateDiff { get; set; }
        /**
         * An event emitted when the diff model is changed (i.e. the diff editor shows new content).
         * @event
         */
        [Parameter]
        public EventCallback<DiffEditor> OnDidChangeModel { get; set; }
        /**
         * Saves current view state of the editor in a serializable object.
         */
        //saveViewState(): IDiffEditorViewState | null;
        /**
         * Restores the view state of the editor from a serializable object generated by `saveViewState`.
         */
        //restoreViewState(state: IDiffEditorViewState | null): void;
        /**
         * Type the getModel() of IEditor.
         */
        public Task<DiffEditorModel> GetModel()
            => JsRuntime.SafeInvokeAsync<DiffEditorModel>("blazorMonaco.editor.getInstanceDiffModel", Id);
        //createViewModel(model: IDiffEditorModel) : IDiffEditorViewModel;
        /**
         * Sets the current model attached to this editor.
         * If the previous model was created by the editor via the value key in the options
         * literal object, it will be destroyed. Otherwise, if the previous model was set
         * via setModel, or the model key in the options literal object, the previous model
         * will not be destroyed.
         * It is safe to call setModel(null) to simply detach the current model from the editor.
         */
        public Task SetModel(DiffEditorModel model)
            => JsRuntime.SafeInvokeAsync("blazorMonaco.editor.setInstanceDiffModel", Id, model);
        //setModel(model: IDiffEditorModel | IDiffEditorViewModel | null) : void;
        /**
         * Get the `original` editor.
         */
        public CodeEditor OriginalEditor => _originalEditor;
        /**
         * Get the `modified` editor.
         */
        public CodeEditor ModifiedEditor => _modifiedEditor;
        /**
         * Get the computed diff information.
         */
        //getLineChanges(): ILineChange[] | null;
        /**
         * Update the editor's options after the editor has been created.
         */
        public Task UpdateOptions(DiffEditorOptions newOptions)
        {
            // Convert the options object into a JsonElement to get rid of the properties with null values
            var optionsJson = JsonSerializer.Serialize(newOptions, JsonSerializerExt.DefaultOptions);
            var optionsDict = JsonSerializer.Deserialize<JsonElement>(optionsJson);
            return JsRuntime.SafeInvokeAsync("blazorMonaco.editor.updateOptions", Id, optionsDict);
        }
        /**
         * Jumps to the next or previous diff.
         */
        //goToDiff(target: 'next' | 'previous') : void;
        /**
         * Scrolls to the first diff.
         * (Waits until the diff computation finished.)
         */
        //revealFirstDiff() : unknown;
        //accessibleDiffViewerNext() : void;
        //accessibleDiffViewerPrev() : void;
        //handleInitialized() : void;
    }
}
