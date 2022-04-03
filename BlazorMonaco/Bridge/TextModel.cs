using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorMonaco
{
    public class DiffEditorModel
    {
        public TextModel Original { get; set; }
        public TextModel Modified { get; set; }
    }

    public class TextModel
    {
        public IJSRuntime jsRuntime { get; set; }
        public string Id { get; set; }
        public string Uri { get; set; }

        public async Task<TextModelResolvedOptions> GetOptions()
        {
            if (jsRuntime == null)
                return null;
            return await jsRuntime.InvokeAsync<TextModelResolvedOptions>("blazorMonaco.editor.model.getOptions", Uri);
        }

        public async Task<int> GetVersionId()
        {
            if (jsRuntime == null)
                return 0;
            return await jsRuntime.InvokeAsync<int>("blazorMonaco.editor.model.getVersionId", Uri);
        }

        public async Task<int> GetAlternativeVersionId()
        {
            if (jsRuntime == null)
                return 0;
            return await jsRuntime.InvokeAsync<int>("blazorMonaco.editor.model.getAlternativeVersionId", Uri);
        }

        public async Task SetValue(string newValue)
        {
            if (jsRuntime == null)
                return;
            await jsRuntime.InvokeVoidAsync("blazorMonaco.editor.model.setValue", Uri, newValue);
        }

        public async Task<string> GetValue(EndOfLinePreference? eol, bool? preserveBOM)
        {
            if (jsRuntime == null)
                return null;
            return await jsRuntime.InvokeAsync<string>("blazorMonaco.editor.model.getValue", Uri, eol, preserveBOM);
        }

        public async Task<int> GetValueLength(EndOfLinePreference? eol, bool? preserveBOM)
        {
            if (jsRuntime == null)
                return 0;
            return await jsRuntime.InvokeAsync<int>("blazorMonaco.editor.model.getValueLength", Uri, eol, preserveBOM);
        }

        public async Task<string> GetValueInRange(Range range, EndOfLinePreference? eol)
        {
            if (jsRuntime == null)
                return null;
            return await jsRuntime.InvokeAsync<string>("blazorMonaco.editor.model.getValueInRange", Uri, range, eol);
        }

        public async Task<int> GetValueLengthInRange(Range range)
        {
            if (jsRuntime == null)
                return 0;
            return await jsRuntime.InvokeAsync<int>("blazorMonaco.editor.model.getValueLengthInRange", Uri, range);
        }

        public async Task<int> GetCharacterCountInRange(Range range)
        {
            if (jsRuntime == null)
                return 0;
            return await jsRuntime.InvokeAsync<int>("blazorMonaco.editor.model.getCharacterCountInRange", Uri, range);
        }

        public async Task<int> GetLineCount()
        {
            if (jsRuntime == null)
                return 0;
            return await jsRuntime.InvokeAsync<int>("blazorMonaco.editor.model.getLineCount", Uri);
        }

        public async Task<string> GetLineContent(int lineNumber)
        {
            if (jsRuntime == null)
                return null;
            return await jsRuntime.InvokeAsync<string>("blazorMonaco.editor.model.getLineContent", Uri, lineNumber);
        }

        public async Task<int> GetLineLength(int lineNumber)
        {
            if (jsRuntime == null)
                return 0;
            return await jsRuntime.InvokeAsync<int>("blazorMonaco.editor.model.getLineLength", Uri, lineNumber);
        }

        public async Task<List<string>> GetLinesContent()
        {
            if (jsRuntime == null)
                return null;
            return await jsRuntime.InvokeAsync<List<string>>("blazorMonaco.editor.model.getLinesContent", Uri);
        }

        public async Task<string> GetEOL()
        {
            if (jsRuntime == null)
                return null;
            return await jsRuntime.InvokeAsync<string>("blazorMonaco.editor.model.getEOL", Uri);
        }

        public async Task<EndOfLineSequence> GetEndOfLineSequence()
        {
            if (jsRuntime == null)
                return EndOfLineSequence.CRLF;
            return await jsRuntime.InvokeAsync<EndOfLineSequence>("blazorMonaco.editor.model.getEndOfLineSequence", Uri);
        }

        public async Task<int> GetLineMinColumn(int lineNumber)
        {
            if (jsRuntime == null)
                return 0;
            return await jsRuntime.InvokeAsync<int>("blazorMonaco.editor.model.getLineMinColumn", Uri, lineNumber);
        }

        public async Task<int> GetLineMaxColumn(int lineNumber)
        {
            if (jsRuntime == null)
                return 0;
            return await jsRuntime.InvokeAsync<int>("blazorMonaco.editor.model.getLineMaxColumn", Uri, lineNumber);
        }

        public async Task<int> GetLineFirstNonWhitespaceColumn(int lineNumber)
        {
            if (jsRuntime == null)
                return 0;
            return await jsRuntime.InvokeAsync<int>("blazorMonaco.editor.model.getLineFirstNonWhitespaceColumn", Uri, lineNumber);
        }

        public async Task<int> GetLineLastNonWhitespaceColumn(int lineNumber)
        {
            if (jsRuntime == null)
                return 0;
            return await jsRuntime.InvokeAsync<int>("blazorMonaco.editor.model.getLineLastNonWhitespaceColumn", Uri, lineNumber);
        }

        public async Task<Position> ValidatePosition(Position position)
        {
            if (jsRuntime == null)
                return null;
            return await jsRuntime.InvokeAsync<Position>("blazorMonaco.editor.model.validatePosition", Uri, position);
        }

        public async Task<Position> ModifyPosition(Position position, int offset)
        {
            if (jsRuntime == null)
                return null;
            return await jsRuntime.InvokeAsync<Position>("blazorMonaco.editor.model.modifyPosition", Uri, position, offset);
        }

        public async Task<Range> ValidateRange(Range range)
        {
            if (jsRuntime == null)
                return null;
            return await jsRuntime.InvokeAsync<Range>("blazorMonaco.editor.model.validateRange", Uri, range);
        }

        public async Task<int> GetOffsetAt(Position position)
        {
            if (jsRuntime == null)
                return 0;
            return await jsRuntime.InvokeAsync<int>("blazorMonaco.editor.model.getOffsetAt", Uri, position);
        }

        public async Task<Position> GetPositionAt(int offset)
        {
            if (jsRuntime == null)
                return null;
            return await jsRuntime.InvokeAsync<Position>("blazorMonaco.editor.model.getPositionAt", Uri, offset);
        }

        public async Task<Range> GetFullModelRange()
        {
            if (jsRuntime == null)
                return null;
            return await jsRuntime.InvokeAsync<Range>("blazorMonaco.editor.model.getFullModelRange", Uri);
        }

        public async Task<bool> IsDisposed()
        {
            if (jsRuntime == null)
                return false;
            return await jsRuntime.InvokeAsync<bool>("blazorMonaco.editor.model.isDisposed", Uri);
        }

        public async Task<List<FindMatch>> FindMatches(string searchString, bool searchOnlyEditableRange, bool isRegex, bool matchCase, string wordSeparators, bool captureMatches, int? limitResultCount)
        {
            if (jsRuntime == null)
                return null;
            return await jsRuntime.InvokeAsync<List<FindMatch>>("blazorMonaco.editor.model.findMatches", Uri, searchString, searchOnlyEditableRange, isRegex, matchCase, wordSeparators, captureMatches, limitResultCount);
        }

        public async Task<List<FindMatch>> FindMatches(string searchString, Range searchScope, bool isRegex, bool matchCase, string wordSeparators, bool captureMatches, int? limitResultCount)
        {
            if (jsRuntime == null)
                return null;
            return await jsRuntime.InvokeAsync<List<FindMatch>>("blazorMonaco.editor.model.findMatches", Uri, searchString, searchScope, isRegex, matchCase, wordSeparators, captureMatches, limitResultCount);
        }

        public async Task<List<FindMatch>> FindMatches(string searchString, List<Range> searchScope, bool isRegex, bool matchCase, string wordSeparators, bool captureMatches, int? limitResultCount)
        {
            if (jsRuntime == null)
                return null;
            return await jsRuntime.InvokeAsync<List<FindMatch>>("blazorMonaco.editor.model.findMatches", Uri, searchString, searchScope, isRegex, matchCase, wordSeparators, captureMatches, limitResultCount);
        }

        public async Task<FindMatch> FindNextMatch(string searchString, Position searchStart, bool isRegex, bool matchCase, string wordSeparators, bool captureMatches)
        {
            if (jsRuntime == null)
                return null;
            return await jsRuntime.InvokeAsync<FindMatch>("blazorMonaco.editor.model.findNextMatch", Uri, searchString, searchStart, isRegex, matchCase, wordSeparators, captureMatches);
        }

        public async Task<FindMatch> FindPreviousMatch(string searchString, Position searchStart, bool isRegex, bool matchCase, string wordSeparators, bool captureMatches)
        {
            if (jsRuntime == null)
                return null;
            return await jsRuntime.InvokeAsync<FindMatch>("blazorMonaco.editor.model.findPreviousMatch", Uri, searchString, searchStart, isRegex, matchCase, wordSeparators, captureMatches);
        }

        public async Task<string> GetLanguageId()
        {
            if (jsRuntime == null)
                return null;
            return await jsRuntime.InvokeAsync<string>("blazorMonaco.editor.model.getLanguageId", Uri);
        }

        public async Task<WordAtPosition> GetWordAtPosition(Position position)
        {
            if (jsRuntime == null)
                return null;
            return await jsRuntime.InvokeAsync<WordAtPosition>("blazorMonaco.editor.model.getWordAtPosition", Uri, position);
        }

        public async Task<WordAtPosition> GetWordUntilPosition(Position position)
        {
            if (jsRuntime == null)
                return null;
            return await jsRuntime.InvokeAsync<WordAtPosition>("blazorMonaco.editor.model.getWordUntilPosition", Uri, position);
        }

        public async Task<List<string>> DeltaDecorations(List<string> oldDecorations, List<ModelDeltaDecoration> newDecorations, int? ownerId)
        {
            if (jsRuntime == null)
                return null;
            return await jsRuntime.InvokeAsync<List<string>>("blazorMonaco.editor.model.deltaDecorations", Uri, oldDecorations, newDecorations, ownerId);
        }

        public async Task<ModelDecorationOptions> GetDecorationOptions(string id)
        {
            if (jsRuntime == null)
                return null;
            return await jsRuntime.InvokeAsync<ModelDecorationOptions>("blazorMonaco.editor.model.getDecorationOptions", Uri, id);
        }

        public async Task<Range> GetDecorationRange(string id)
        {
            if (jsRuntime == null)
                return null;
            return await jsRuntime.InvokeAsync<Range>("blazorMonaco.editor.model.getDecorationRange", Uri, id);
        }

        public async Task<ModelDecoration> GetLineDecorations(int lineNumber, int? ownerId, bool? filterOutValidation)
        {
            if (jsRuntime == null)
                return null;
            return await jsRuntime.InvokeAsync<ModelDecoration>("blazorMonaco.editor.model.getLineDecorations", Uri, lineNumber, ownerId, filterOutValidation);
        }

        public async Task<ModelDecoration> GetLinesDecorations(int startLineNumber, int endLineNumber, int? ownerId, bool? filterOutValidation)
        {
            if (jsRuntime == null)
                return null;
            return await jsRuntime.InvokeAsync<ModelDecoration>("blazorMonaco.editor.model.getLinesDecorations", Uri, startLineNumber, endLineNumber, ownerId, filterOutValidation);
        }

        public async Task<ModelDecoration[]> GetDecorationsInRange(Range range, int? ownerId, bool? filterOutValidation)
        {
            if (jsRuntime == null)
                return null;
            return await jsRuntime.InvokeAsync<ModelDecoration[]>("blazorMonaco.editor.model.getDecorationsInRange", Uri, range, ownerId, filterOutValidation);
        }

        public async Task<ModelDecoration> GetDecorationsInRange(Range range)
        {
            if (jsRuntime == null)
                return null;
            return await jsRuntime.InvokeAsync<ModelDecoration>("blazorMonaco.editor.model.getDecorationsInRange2", Uri, range);
        }

        public async Task<List<ModelDecoration>> GetAllDecorations(int? ownerId, bool? filterOutValidation)
        {
            if (jsRuntime == null)
                return null;
            return await jsRuntime.InvokeAsync<List<ModelDecoration>>("blazorMonaco.editor.model.getAllDecorations", Uri, ownerId, filterOutValidation);
        }

        public async Task<ModelDecoration> GetOverviewRulerDecorations(int? ownerId, bool? filterOutValidation)
        {
            if (jsRuntime == null)
                return null;
            return await jsRuntime.InvokeAsync<ModelDecoration>("blazorMonaco.editor.model.getOverviewRulerDecorations", Uri, ownerId, filterOutValidation);
        }

        public async Task<ModelDecoration[]> GetInjectedTextDecorations(int? ownerId)
        {
            if (jsRuntime == null)
                return null;
            return await jsRuntime.InvokeAsync<ModelDecoration[]>("blazorMonaco.editor.model.getInjectedTextDecorations", Uri, ownerId);
        }

        

        public async Task<string> NormalizeIndentation(string str)
        {
            if (jsRuntime == null)
                return null;
            return await jsRuntime.InvokeAsync<string>("blazorMonaco.editor.model.normalizeIndentation", Uri, str);
        }

        public async Task UpdateOptions(TextModelUpdateOptions newOpts)
        {
            if (jsRuntime == null)
                return;
            await jsRuntime.InvokeVoidAsync("blazorMonaco.editor.model.updateOptions", Uri, newOpts);
        }

        public async Task DetectIndentation(bool defaultInsertSpaces, int defaultTabSize)
        {
            if (jsRuntime == null)
                return;
            await jsRuntime.InvokeVoidAsync("blazorMonaco.editor.model.detectIndentation", Uri, defaultInsertSpaces, defaultTabSize);
        }

        public async Task PushStackElement()
        {
            if (jsRuntime == null)
                return;
            await jsRuntime.InvokeVoidAsync("blazorMonaco.editor.model.pushStackElement", Uri);
        }

        public async Task PopStackElement()
        {
            if (jsRuntime == null)
                return;
            await jsRuntime.InvokeVoidAsync("blazorMonaco.editor.model.popStackElement", Uri);
        }

        // pushEditOperations(beforeCursorState: Selection[] | null, editOperations: IIdentifiedSingleEditOperation[], cursorStateComputer: ICursorStateComputer): Selection[] | null;

        public async Task PushEOL(EndOfLineSequence eol)
        {
            if (jsRuntime == null)
                return;
            await jsRuntime.InvokeVoidAsync("blazorMonaco.editor.model.pushEOL", Uri, eol);
        }

        public async Task ApplyEdits(List<IdentifiedSingleEditOperation> operations)
        {
            if (jsRuntime == null)
                return;
            await jsRuntime.InvokeVoidAsync("blazorMonaco.editor.model.applyEdits", Uri, operations);
        }

        public async Task<List<ValidEditOperation>> ApplyEdits(List<IdentifiedSingleEditOperation> operations, bool computeUndoEdits)
        {
            if (jsRuntime == null)
                return null;
            return await jsRuntime.InvokeAsync<List<ValidEditOperation>>("blazorMonaco.editor.model.applyEdits", Uri, operations, computeUndoEdits);
        }

        public async Task SetEOL(EndOfLineSequence eol)
        {
            if (jsRuntime == null)
                return;
            await jsRuntime.InvokeVoidAsync("blazorMonaco.editor.model.setEOL", Uri, eol);
        }

        // onDidChangeContent(listener: (e: IModelContentChangedEvent) => void): IDisposable;

        // onDidChangeDecorations(listener: (e: IModelDecorationsChangedEvent) => void): IDisposable;

        // onDidChangeOptions(listener: (e: IModelOptionsChangedEvent) => void): IDisposable;

        // onDidChangeLanguage(listener: (e: IModelLanguageChangedEvent) => void): IDisposable;

        // onDidChangeLanguageConfiguration(listener: (e: IModelLanguageConfigurationChangedEvent) => void): IDisposable;

        // onWillDispose(listener: () => void): IDisposable;

        public async Task DisposeModel()
        {
            if (jsRuntime == null)
                return;
            await jsRuntime.InvokeVoidAsync("blazorMonaco.editor.model.dispose", Uri);
        }
    }
}
