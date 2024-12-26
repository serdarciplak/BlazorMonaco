using BlazorMonaco.Helpers;
using Microsoft.JSInterop;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BlazorMonaco.Editor
{
    /**
     * A model.
     */
    public class TextModel
    {
        [JsonIgnore]
        public IJSRuntime JsRuntime { get; set; }

        /**
         * Gets the resource associated with this editor model.
         */
        public string Uri { get; set; }
        /**
         * A unique identifier associated with this model.
         */
        public string Id { get; set; }
        /**
         * Get the resolved options for this model.
         */
        public Task<TextModelResolvedOptions> GetOptions()
            => JsRuntime.SafeInvokeAsync<TextModelResolvedOptions>("blazorMonaco.editor.model.getOptions", Uri);
        /**
         * Get the current version id of the model.
         * Anytime a change happens to the model (even undo/redo),
         * the version id is incremented.
         */
        public Task<int> GetVersionId()
            => JsRuntime.SafeInvokeAsync<int>("blazorMonaco.editor.model.getVersionId", Uri);
        /**
         * Get the alternative version id of the model.
         * This alternative version id is not always incremented,
         * it will return the same values in the case of undo-redo.
         */
        public Task<int> GetAlternativeVersionId()
            => JsRuntime.SafeInvokeAsync<int>("blazorMonaco.editor.model.getAlternativeVersionId", Uri);
        /**
         * Replace the entire text buffer value contained in this model.
         */
        // TODO setValue(newValue: string | ITextSnapshot) : void;
        public Task SetValue(string newValue)
            => JsRuntime.SafeInvokeAsync("blazorMonaco.editor.model.setValue", Uri, newValue);
        /**
         * Get the text stored in this model.
         * @param eol The end of line character preference. Defaults to `EndOfLinePreference.TextDefined`.
         * @param preserverBOM Preserve a BOM character if it was detected when the model was constructed.
         * @return The text.
         */
        public Task<string> GetValue(EndOfLinePreference? eol, bool? preserveBOM)
            => JsRuntime.SafeInvokeAsync<string>("blazorMonaco.editor.model.getValue", Uri, eol, preserveBOM);
        /**
         * Get the text stored in this model.
         * @param preserverBOM Preserve a BOM character if it was detected when the model was constructed.
         * @return The text snapshot (it is safe to consume it asynchronously).
         */
        //createSnapshot(preserveBOM?: boolean) : ITextSnapshot;
        /**
         * Get the length of the text stored in this model.
         */
        public Task<int> GetValueLength(EndOfLinePreference? eol, bool? preserveBOM)
            => JsRuntime.SafeInvokeAsync<int>("blazorMonaco.editor.model.getValueLength", Uri, eol, preserveBOM);
        /**
         * Get the text in a certain range.
         * @param range The range describing what text to get.
         * @param eol The end of line character preference. This will only be used for multiline ranges. Defaults to `EndOfLinePreference.TextDefined`.
         * @return The text.
         */
        public Task<string> GetValueInRange(Range range, EndOfLinePreference? eol)
            => JsRuntime.SafeInvokeAsync<string>("blazorMonaco.editor.model.getValueInRange", Uri, range, eol);
        /**
         * Get the length of text in a certain range.
         * @param range The range describing what text length to get.
         * @return The text length.
         */
        public Task<int> GetValueLengthInRange(Range range, EndOfLinePreference? eol)
            => JsRuntime.SafeInvokeAsync<int>("blazorMonaco.editor.model.getValueLengthInRange", Uri, range, eol);
        /**
         * Get the character count of text in a certain range.
         * @param range The range describing what text length to get.
         */
        public Task<int> GetCharacterCountInRange(Range range, EndOfLinePreference? eol)
            => JsRuntime.SafeInvokeAsync<int>("blazorMonaco.editor.model.getCharacterCountInRange", Uri, range, eol);
        /**
         * Get the number of lines in the model.
         */
        public Task<int> GetLineCount()
            => JsRuntime.SafeInvokeAsync<int>("blazorMonaco.editor.model.getLineCount", Uri);
        /**
         * Get the text for a certain line.
         */
        public Task<string> GetLineContent(int lineNumber)
            => JsRuntime.SafeInvokeAsync<string>("blazorMonaco.editor.model.getLineContent", Uri, lineNumber);
        /**
         * Get the text length for a certain line.
         */
        public Task<int> GetLineLength(int lineNumber)
            => JsRuntime.SafeInvokeAsync<int>("blazorMonaco.editor.model.getLineLength", Uri, lineNumber);
        /**
         * Get the text for all lines.
         */
        public Task<List<string>> GetLinesContent()
            => JsRuntime.SafeInvokeAsync<List<string>>("blazorMonaco.editor.model.getLinesContent", Uri);
        /**
         * Get the end of line sequence predominantly used in the text buffer.
         * @return EOL char sequence (e.g.: '\n' or '\r\n').
         */
        public Task<string> GetEOL()
            => JsRuntime.SafeInvokeAsync<string>("blazorMonaco.editor.model.getEOL", Uri);
        /**
         * Get the end of line sequence predominantly used in the text buffer.
         */
        public Task<EndOfLineSequence> GetEndOfLineSequence()
            => JsRuntime.SafeInvokeAsync<EndOfLineSequence>("blazorMonaco.editor.model.getEndOfLineSequence", Uri);
        /**
         * Get the minimum legal column for line at `lineNumber`
         */
        public Task<int> GetLineMinColumn(int lineNumber)
            => JsRuntime.SafeInvokeAsync<int>("blazorMonaco.editor.model.getLineMinColumn", Uri, lineNumber);
        /**
         * Get the maximum legal column for line at `lineNumber`
         */
        public Task<int> GetLineMaxColumn(int lineNumber)
            => JsRuntime.SafeInvokeAsync<int>("blazorMonaco.editor.model.getLineMaxColumn", Uri, lineNumber);
        /**
         * Returns the column before the first non whitespace character for line at `lineNumber`.
         * Returns 0 if line is empty or contains only whitespace.
         */
        public Task<int> GetLineFirstNonWhitespaceColumn(int lineNumber)
            => JsRuntime.SafeInvokeAsync<int>("blazorMonaco.editor.model.getLineFirstNonWhitespaceColumn", Uri, lineNumber);
        /**
         * Returns the column after the last non whitespace character for line at `lineNumber`.
         * Returns 0 if line is empty or contains only whitespace.
         */
        public Task<int> GetLineLastNonWhitespaceColumn(int lineNumber)
            => JsRuntime.SafeInvokeAsync<int>("blazorMonaco.editor.model.getLineLastNonWhitespaceColumn", Uri, lineNumber);
        /**
         * Create a valid position.
         */
        public Task<Position> ValidatePosition(Position position)
            => JsRuntime.SafeInvokeAsync<Position>("blazorMonaco.editor.model.validatePosition", Uri, position);
        /**
         * Advances the given position by the given offset (negative offsets are also accepted)
         * and returns it as a new valid position.
         *
         * If the offset and position are such that their combination goes beyond the beginning or
         * end of the model, throws an exception.
         *
         * If the offset is such that the new position would be in the middle of a multi-byte
         * line terminator, throws an exception.
         */
        public Task<Position> ModifyPosition(Position position, int offset)
            => JsRuntime.SafeInvokeAsync<Position>("blazorMonaco.editor.model.modifyPosition", Uri, position, offset);
        /**
         * Create a valid range.
         */
        public Task<Range> ValidateRange(Range range)
            => JsRuntime.SafeInvokeAsync<Range>("blazorMonaco.editor.model.validateRange", Uri, range);
        /**
         * Converts the position to a zero-based offset.
         *
         * The position will be [adjusted](#TextDocument.validatePosition).
         *
         * @param position A position.
         * @return A valid zero-based offset.
         */
        public Task<int> GetOffsetAt(Position position)
            => JsRuntime.SafeInvokeAsync<int>("blazorMonaco.editor.model.getOffsetAt", Uri, position);
        /**
         * Converts a zero-based offset to a position.
         *
         * @param offset A zero-based offset.
         * @return A valid [position](#Position).
         */
        public Task<Position> GetPositionAt(int offset)
            => JsRuntime.SafeInvokeAsync<Position>("blazorMonaco.editor.model.getPositionAt", Uri, offset);
        /**
         * Get a range covering the entire model.
         */
        public Task<Range> GetFullModelRange()
            => JsRuntime.SafeInvokeAsync<Range>("blazorMonaco.editor.model.getFullModelRange", Uri);
        /**
         * Returns if the model was disposed or not.
         */
        public Task<bool> IsDisposed()
            => JsRuntime.SafeInvokeAsync<bool>("blazorMonaco.editor.model.isDisposed", Uri);
        /**
         * Search the model.
         * @param searchString The string used to search. If it is a regular expression, set `isRegex` to true.
         * @param searchOnlyEditableRange Limit the searching to only search inside the editable range of the model.
         * @param isRegex Used to indicate that `searchString` is a regular expression.
         * @param matchCase Force the matching to match lower/upper case exactly.
         * @param wordSeparators Force the matching to match entire words only. Pass null otherwise.
         * @param captureMatches The result will contain the captured groups.
         * @param limitResultCount Limit the number of results
         * @return The ranges where the matches are. It is empty if not matches have been found.
         */
        public Task<List<FindMatch>> FindMatches(string searchString, bool searchOnlyEditableRange, bool isRegex, bool matchCase, string wordSeparators, bool captureMatches, int? limitResultCount)
            => JsRuntime.SafeInvokeAsync<List<FindMatch>>("blazorMonaco.editor.model.findMatches", Uri, searchString, searchOnlyEditableRange, isRegex, matchCase, wordSeparators, captureMatches, limitResultCount);
        /**
         * Search the model.
         * @param searchString The string used to search. If it is a regular expression, set `isRegex` to true.
         * @param searchScope Limit the searching to only search inside these ranges.
         * @param isRegex Used to indicate that `searchString` is a regular expression.
         * @param matchCase Force the matching to match lower/upper case exactly.
         * @param wordSeparators Force the matching to match entire words only. Pass null otherwise.
         * @param captureMatches The result will contain the captured groups.
         * @param limitResultCount Limit the number of results
         * @return The ranges where the matches are. It is empty if no matches have been found.
         */
        public Task<List<FindMatch>> FindMatches(string searchString, Range searchScope, bool isRegex, bool matchCase, string wordSeparators, bool captureMatches, int? limitResultCount)
            => JsRuntime.SafeInvokeAsync<List<FindMatch>>("blazorMonaco.editor.model.findMatches", Uri, searchString, searchScope, isRegex, matchCase, wordSeparators, captureMatches, limitResultCount);
        /**
         * Search the model for the next match. Loops to the beginning of the model if needed.
         * @param searchString The string used to search. If it is a regular expression, set `isRegex` to true.
         * @param searchStart Start the searching at the specified position.
         * @param isRegex Used to indicate that `searchString` is a regular expression.
         * @param matchCase Force the matching to match lower/upper case exactly.
         * @param wordSeparators Force the matching to match entire words only. Pass null otherwise.
         * @param captureMatches The result will contain the captured groups.
         * @return The range where the next match is. It is null if no next match has been found.
         */
        public Task<FindMatch> FindNextMatch(string searchString, Position searchStart, bool isRegex, bool matchCase, string wordSeparators, bool captureMatches)
            => JsRuntime.SafeInvokeAsync<FindMatch>("blazorMonaco.editor.model.findNextMatch", Uri, searchString, searchStart, isRegex, matchCase, wordSeparators, captureMatches);
        /**
         * Search the model for the previous match. Loops to the end of the model if needed.
         * @param searchString The string used to search. If it is a regular expression, set `isRegex` to true.
         * @param searchStart Start the searching at the specified position.
         * @param isRegex Used to indicate that `searchString` is a regular expression.
         * @param matchCase Force the matching to match lower/upper case exactly.
         * @param wordSeparators Force the matching to match entire words only. Pass null otherwise.
         * @param captureMatches The result will contain the captured groups.
         * @return The range where the previous match is. It is null if no previous match has been found.
         */
        public Task<FindMatch> FindPreviousMatch(string searchString, Position searchStart, bool isRegex, bool matchCase, string wordSeparators, bool captureMatches)
            => JsRuntime.SafeInvokeAsync<FindMatch>("blazorMonaco.editor.model.findPreviousMatch", Uri, searchString, searchStart, isRegex, matchCase, wordSeparators, captureMatches);
        /**
         * Get the language associated with this model.
         */
        public Task<string> GetLanguageId()
            => JsRuntime.SafeInvokeAsync<string>("blazorMonaco.editor.model.getLanguageId", Uri);
        /**
         * Get the word under or besides `position`.
         * @param position The position to look for a word.
         * @return The word under or besides `position`. Might be null.
         */
        public Task<WordAtPosition> GetWordAtPosition(Position position)
            => JsRuntime.SafeInvokeAsync<WordAtPosition>("blazorMonaco.editor.model.getWordAtPosition", Uri, position);
        /**
         * Get the word under or besides `position` trimmed to `position`.column
         * @param position The position to look for a word.
         * @return The word under or besides `position`. Will never be null.
         */
        public Task<WordAtPosition> GetWordUntilPosition(Position position)
            => JsRuntime.SafeInvokeAsync<WordAtPosition>("blazorMonaco.editor.model.getWordUntilPosition", Uri, position);
        /**
         * Perform a minimum amount of operations, in order to transform the decorations
         * identified by `oldDecorations` to the decorations described by `newDecorations`
         * and returns the new identifiers associated with the resulting decorations.
         *
         * @param oldDecorations Array containing previous decorations identifiers.
         * @param newDecorations Array describing what decorations should result after the call.
         * @param ownerId Identifies the editor id in which these decorations should appear. If no `ownerId` is provided, the decorations will appear in all editors that attach this model.
         * @return An array containing the new decorations identifiers.
         */
        public Task<List<string>> DeltaDecorations(List<string> oldDecorations, List<ModelDeltaDecoration> newDecorations, int? ownerId)
            => JsRuntime.SafeInvokeAsync<List<string>>("blazorMonaco.editor.model.deltaDecorations", Uri, oldDecorations, newDecorations, ownerId);
        /**
         * Get the options associated with a decoration.
         * @param id The decoration id.
         * @return The decoration options or null if the decoration was not found.
         */
        public Task<ModelDecorationOptions> GetDecorationOptions(string id)
            => JsRuntime.SafeInvokeAsync<ModelDecorationOptions>("blazorMonaco.editor.model.getDecorationOptions", Uri, id);
        /**
         * Get the range associated with a decoration.
         * @param id The decoration id.
         * @return The decoration range or null if the decoration was not found.
         */
        public Task<Range> GetDecorationRange(string id)
            => JsRuntime.SafeInvokeAsync<Range>("blazorMonaco.editor.model.getDecorationRange", Uri, id);
        /**
         * Gets all the decorations for the line `lineNumber` as an array.
         * @param lineNumber The line number
         * @param ownerId If set, it will ignore decorations belonging to other owners.
         * @param filterOutValidation If set, it will ignore decorations specific to validation (i.e. warnings, errors).
         * @return An array with the decorations
         */
        public Task<ModelDecoration> GetLineDecorations(int lineNumber, int? ownerId, bool? filterOutValidation)
            => JsRuntime.SafeInvokeAsync<ModelDecoration>("blazorMonaco.editor.model.getLineDecorations", Uri, lineNumber, ownerId, filterOutValidation);
        /**
         * Gets all the decorations for the lines between `startLineNumber` and `endLineNumber` as an array.
         * @param startLineNumber The start line number
         * @param endLineNumber The end line number
         * @param ownerId If set, it will ignore decorations belonging to other owners.
         * @param filterOutValidation If set, it will ignore decorations specific to validation (i.e. warnings, errors).
         * @return An array with the decorations
         */
        public Task<ModelDecoration> GetLinesDecorations(int startLineNumber, int endLineNumber, int? ownerId, bool? filterOutValidation)
            => JsRuntime.SafeInvokeAsync<ModelDecoration>("blazorMonaco.editor.model.getLinesDecorations", Uri, startLineNumber, endLineNumber, ownerId, filterOutValidation);
        /**
         * Gets all the decorations in a range as an array. Only `startLineNumber` and `endLineNumber` from `range` are used for filtering.
         * So for now it returns all the decorations on the same line as `range`.
         * @param range The range to search in
         * @param ownerId If set, it will ignore decorations belonging to other owners.
         * @param filterOutValidation If set, it will ignore decorations specific to validation (i.e. warnings, errors).
         * @param onlyMinimapDecorations If set, it will return only decorations that render in the minimap.
         * @param onlyMarginDecorations If set, it will return only decorations that render in the glyph margin.
         * @return An array with the decorations
         */
        public Task<ModelDecoration[]> GetDecorationsInRange(Range range, int? ownerId, bool? filterOutValidation, bool? onlyMinimapDecorations, bool? onlyMarginDecorations)
            => JsRuntime.SafeInvokeAsync<ModelDecoration[]>("blazorMonaco.editor.model.getDecorationsInRange", Uri, range, ownerId, filterOutValidation, onlyMinimapDecorations, onlyMarginDecorations);
        /**
         * Gets all the decorations as an array.
         * @param ownerId If set, it will ignore decorations belonging to other owners.
         * @param filterOutValidation If set, it will ignore decorations specific to validation (i.e. warnings, errors).
         */
        public Task<List<ModelDecoration>> GetAllDecorations(int? ownerId, bool? filterOutValidation)
            => JsRuntime.SafeInvokeAsync<List<ModelDecoration>>("blazorMonaco.editor.model.getAllDecorations", Uri, ownerId, filterOutValidation);
        /**
         * Gets all decorations that render in the glyph margin as an array.
         * @param ownerId If set, it will ignore decorations belonging to other owners.
         */
        public Task<List<ModelDecoration>> GetAllMarginDecorations(int? ownerId)
            => JsRuntime.SafeInvokeAsync<List<ModelDecoration>>("blazorMonaco.editor.model.getAllMarginDecorations", Uri, ownerId);
        /**
         * Gets all the decorations that should be rendered in the overview ruler as an array.
         * @param ownerId If set, it will ignore decorations belonging to other owners.
         * @param filterOutValidation If set, it will ignore decorations specific to validation (i.e. warnings, errors).
         */
        public Task<ModelDecoration> GetOverviewRulerDecorations(int? ownerId, bool? filterOutValidation)
            => JsRuntime.SafeInvokeAsync<ModelDecoration>("blazorMonaco.editor.model.getOverviewRulerDecorations", Uri, ownerId, filterOutValidation);
        /**
         * Gets all the decorations that contain injected text.
         * @param ownerId If set, it will ignore decorations belonging to other owners.
         */
        public Task<ModelDecoration[]> GetInjectedTextDecorations(int? ownerId)
            => JsRuntime.SafeInvokeAsync<ModelDecoration[]>("blazorMonaco.editor.model.getInjectedTextDecorations", Uri, ownerId);
        /**
         * Normalize a string containing whitespace according to indentation rules (converts to spaces or to tabs).
         */
        public Task<string> NormalizeIndentation(string str)
            => JsRuntime.SafeInvokeAsync<string>("blazorMonaco.editor.model.normalizeIndentation", Uri, str);
        /**
         * Change the options of this model.
         */
        public Task UpdateOptions(TextModelUpdateOptions newOpts)
            => JsRuntime.SafeInvokeAsync("blazorMonaco.editor.model.updateOptions", Uri, newOpts);
        /**
         * Detect the indentation options for this model from its content.
         */
        public Task DetectIndentation(bool defaultInsertSpaces, int defaultTabSize)
            => JsRuntime.SafeInvokeAsync("blazorMonaco.editor.model.detectIndentation", Uri, defaultInsertSpaces, defaultTabSize);
        /**
         * Close the current undo-redo element.
         * This offers a way to create an undo/redo stop point.
         */
        public Task PushStackElement()
            => JsRuntime.SafeInvokeAsync("blazorMonaco.editor.model.pushStackElement", Uri);
        /**
         * Open the current undo-redo element.
         * This offers a way to remove the current undo/redo stop point.
         */
        public Task PopStackElement()
            => JsRuntime.SafeInvokeAsync("blazorMonaco.editor.model.popStackElement", Uri);
        /**
         * Push edit operations, basically editing the model. This is the preferred way
         * of editing the model. The edit operations will land on the undo stack.
         * @param beforeCursorState The cursor state before the edit operations. This cursor state will be returned when `undo` or `redo` are invoked.
         * @param editOperations The edit operations.
         * @param cursorStateComputer A callback that can compute the resulting cursors state after the edit operations have been executed.
         * @return The cursor state returned by the `cursorStateComputer`.
         */
        //pushEditOperations(beforeCursorState: Selection[] | null, editOperations: IIdentifiedSingleEditOperation[], cursorStateComputer: ICursorStateComputer): Selection[] | null;
        /**
         * Change the end of line sequence. This is the preferred way of
         * changing the eol sequence. This will land on the undo stack.
         */
        public Task PushEOL(EndOfLineSequence eol)
            => JsRuntime.SafeInvokeAsync("blazorMonaco.editor.model.pushEOL", Uri, eol);
        /**
         * Edit the model without adding the edits to the undo stack.
         * This can have dire consequences on the undo stack! See @pushEditOperations for the preferred way.
         * @param operations The edit operations.
         * @return If desired, the inverse edit operations, that, when applied, will bring the model back to the previous state.
         */
        public Task<List<ValidEditOperation>> ApplyEdits(List<IdentifiedSingleEditOperation> operations, bool computeUndoEdits = false)
            => JsRuntime.SafeInvokeAsync<List<ValidEditOperation>>("blazorMonaco.editor.model.applyEdits", Uri, operations, computeUndoEdits);
        /**
         * Change the end of line sequence without recording in the undo stack.
         * This can have dire consequences on the undo stack! See @pushEOL for the preferred way.
         */
        public Task SetEOL(EndOfLineSequence eol)
            => JsRuntime.SafeInvokeAsync("blazorMonaco.editor.model.setEOL", Uri, eol);
        /**
         * An event emitted when the contents of the model have changed.
         * @event
         */
        //onDidChangeContent(listener: (e: IModelContentChangedEvent) => void): IDisposable;
        /**
         * An event emitted when decorations of the model have changed.
         * @event
         */
        //readonly onDidChangeDecorations: IEvent<IModelDecorationsChangedEvent>;
        /**
         * An event emitted when the model options have changed.
         * @event
         */
        //readonly onDidChangeOptions: IEvent<IModelOptionsChangedEvent>;
        /**
         * An event emitted when the language associated with the model has changed.
         * @event
         */
        //readonly onDidChangeLanguage: IEvent<IModelLanguageChangedEvent>;
        /**
         * An event emitted when the language configuration associated with the model has changed.
         * @event
         */
        //readonly onDidChangeLanguageConfiguration: IEvent<IModelLanguageConfigurationChangedEvent>;
        /**
         * An event emitted when the model has been attached to the first editor or detached from the last editor.
         * @event
         */
        //readonly onDidChangeAttached: IEvent<void>;
        /**
         * An event emitted right before disposing the model.
         * @event
         */
        //readonly onWillDispose: IEvent<void>;
        /**
         * Destroy this model.
         */
        public Task DisposeModel()
            => JsRuntime.SafeInvokeAsync("blazorMonaco.editor.model.dispose", Uri);
        /**
         * Returns if this model is attached to an editor or not.
         */
        //isAttachedToEditor(): boolean;
    }
}
