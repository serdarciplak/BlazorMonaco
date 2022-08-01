using BlazorMonaco.Editor;
using BlazorMonaco.Helpers;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorMonaco
{
    public class TextModel
    {
        public string Id { get; set; }
        public string Uri { get; set; }

        public Task<TextModelResolvedOptions> GetOptions()
            => JsRuntimeExt.Shared.SafeInvokeAsync<TextModelResolvedOptions>("blazorMonaco.editor.model.getOptions", Uri);

        public Task<int> GetVersionId()
            => JsRuntimeExt.Shared.SafeInvokeAsync<int>("blazorMonaco.editor.model.getVersionId", Uri);

        public Task<int> GetAlternativeVersionId()
            => JsRuntimeExt.Shared.SafeInvokeAsync<int>("blazorMonaco.editor.model.getAlternativeVersionId", Uri);

        public Task SetValue(string newValue)
            => JsRuntimeExt.Shared.SafeInvokeAsync("blazorMonaco.editor.model.setValue", Uri, newValue);

        public Task<string> GetValue(EndOfLinePreference? eol, bool? preserveBOM)
            => JsRuntimeExt.Shared.SafeInvokeAsync<string>("blazorMonaco.editor.model.getValue", Uri, eol, preserveBOM);

        public Task<int> GetValueLength(EndOfLinePreference? eol, bool? preserveBOM)
            => JsRuntimeExt.Shared.SafeInvokeAsync<int>("blazorMonaco.editor.model.getValueLength", Uri, eol, preserveBOM);

        public Task<string> GetValueInRange(Range range, EndOfLinePreference? eol)
            => JsRuntimeExt.Shared.SafeInvokeAsync<string>("blazorMonaco.editor.model.getValueInRange", Uri, range, eol);

        public Task<int> GetValueLengthInRange(Range range)
            => JsRuntimeExt.Shared.SafeInvokeAsync<int>("blazorMonaco.editor.model.getValueLengthInRange", Uri, range);

        public Task<int> GetCharacterCountInRange(Range range)
            => JsRuntimeExt.Shared.SafeInvokeAsync<int>("blazorMonaco.editor.model.getCharacterCountInRange", Uri, range);

        public Task<int> GetLineCount()
            => JsRuntimeExt.Shared.SafeInvokeAsync<int>("blazorMonaco.editor.model.getLineCount", Uri);

        public Task<string> GetLineContent(int lineNumber)
            => JsRuntimeExt.Shared.SafeInvokeAsync<string>("blazorMonaco.editor.model.getLineContent", Uri, lineNumber);

        public Task<int> GetLineLength(int lineNumber)
            => JsRuntimeExt.Shared.SafeInvokeAsync<int>("blazorMonaco.editor.model.getLineLength", Uri, lineNumber);

        public Task<List<string>> GetLinesContent()
            => JsRuntimeExt.Shared.SafeInvokeAsync<List<string>>("blazorMonaco.editor.model.getLinesContent", Uri);

        public Task<string> GetEOL()
            => JsRuntimeExt.Shared.SafeInvokeAsync<string>("blazorMonaco.editor.model.getEOL", Uri);

        public Task<EndOfLineSequence> GetEndOfLineSequence()
            => JsRuntimeExt.Shared.SafeInvokeAsync<EndOfLineSequence>("blazorMonaco.editor.model.getEndOfLineSequence", Uri);

        public Task<int> GetLineMinColumn(int lineNumber)
            => JsRuntimeExt.Shared.SafeInvokeAsync<int>("blazorMonaco.editor.model.getLineMinColumn", Uri, lineNumber);

        public Task<int> GetLineMaxColumn(int lineNumber)
            => JsRuntimeExt.Shared.SafeInvokeAsync<int>("blazorMonaco.editor.model.getLineMaxColumn", Uri, lineNumber);

        public Task<int> GetLineFirstNonWhitespaceColumn(int lineNumber)
            => JsRuntimeExt.Shared.SafeInvokeAsync<int>("blazorMonaco.editor.model.getLineFirstNonWhitespaceColumn", Uri, lineNumber);

        public Task<int> GetLineLastNonWhitespaceColumn(int lineNumber)
            => JsRuntimeExt.Shared.SafeInvokeAsync<int>("blazorMonaco.editor.model.getLineLastNonWhitespaceColumn", Uri, lineNumber);

        public Task<Position> ValidatePosition(Position position)
            => JsRuntimeExt.Shared.SafeInvokeAsync<Position>("blazorMonaco.editor.model.validatePosition", Uri, position);

        public Task<Position> ModifyPosition(Position position, int offset)
            => JsRuntimeExt.Shared.SafeInvokeAsync<Position>("blazorMonaco.editor.model.modifyPosition", Uri, position, offset);

        public Task<Range> ValidateRange(Range range)
            => JsRuntimeExt.Shared.SafeInvokeAsync<Range>("blazorMonaco.editor.model.validateRange", Uri, range);

        public Task<int> GetOffsetAt(Position position)
            => JsRuntimeExt.Shared.SafeInvokeAsync<int>("blazorMonaco.editor.model.getOffsetAt", Uri, position);

        public Task<Position> GetPositionAt(int offset)
            => JsRuntimeExt.Shared.SafeInvokeAsync<Position>("blazorMonaco.editor.model.getPositionAt", Uri, offset);

        public Task<Range> GetFullModelRange()
            => JsRuntimeExt.Shared.SafeInvokeAsync<Range>("blazorMonaco.editor.model.getFullModelRange", Uri);

        public Task<bool> IsDisposed()
            => JsRuntimeExt.Shared.SafeInvokeAsync<bool>("blazorMonaco.editor.model.isDisposed", Uri);

        public Task<List<FindMatch>> FindMatches(string searchString, bool searchOnlyEditableRange, bool isRegex, bool matchCase, string wordSeparators, bool captureMatches, int? limitResultCount)
            => JsRuntimeExt.Shared.SafeInvokeAsync<List<FindMatch>>("blazorMonaco.editor.model.findMatches", Uri, searchString, searchOnlyEditableRange, isRegex, matchCase, wordSeparators, captureMatches, limitResultCount);

        public Task<List<FindMatch>> FindMatches(string searchString, Range searchScope, bool isRegex, bool matchCase, string wordSeparators, bool captureMatches, int? limitResultCount)
            => JsRuntimeExt.Shared.SafeInvokeAsync<List<FindMatch>>("blazorMonaco.editor.model.findMatches", Uri, searchString, searchScope, isRegex, matchCase, wordSeparators, captureMatches, limitResultCount);

        public Task<List<FindMatch>> FindMatches(string searchString, List<Range> searchScope, bool isRegex, bool matchCase, string wordSeparators, bool captureMatches, int? limitResultCount)
            => JsRuntimeExt.Shared.SafeInvokeAsync<List<FindMatch>>("blazorMonaco.editor.model.findMatches", Uri, searchString, searchScope, isRegex, matchCase, wordSeparators, captureMatches, limitResultCount);

        public Task<FindMatch> FindNextMatch(string searchString, Position searchStart, bool isRegex, bool matchCase, string wordSeparators, bool captureMatches)
            => JsRuntimeExt.Shared.SafeInvokeAsync<FindMatch>("blazorMonaco.editor.model.findNextMatch", Uri, searchString, searchStart, isRegex, matchCase, wordSeparators, captureMatches);

        public Task<FindMatch> FindPreviousMatch(string searchString, Position searchStart, bool isRegex, bool matchCase, string wordSeparators, bool captureMatches)
            => JsRuntimeExt.Shared.SafeInvokeAsync<FindMatch>("blazorMonaco.editor.model.findPreviousMatch", Uri, searchString, searchStart, isRegex, matchCase, wordSeparators, captureMatches);

        public Task<string> GetLanguageId()
            => JsRuntimeExt.Shared.SafeInvokeAsync<string>("blazorMonaco.editor.model.getLanguageId", Uri);

        public Task<WordAtPosition> GetWordAtPosition(Position position)
            => JsRuntimeExt.Shared.SafeInvokeAsync<WordAtPosition>("blazorMonaco.editor.model.getWordAtPosition", Uri, position);

        public Task<WordAtPosition> GetWordUntilPosition(Position position)
            => JsRuntimeExt.Shared.SafeInvokeAsync<WordAtPosition>("blazorMonaco.editor.model.getWordUntilPosition", Uri, position);

        public Task<List<string>> DeltaDecorations(List<string> oldDecorations, List<ModelDeltaDecoration> newDecorations, int? ownerId)
            => JsRuntimeExt.Shared.SafeInvokeAsync<List<string>>("blazorMonaco.editor.model.deltaDecorations", Uri, oldDecorations, newDecorations, ownerId);

        public Task<ModelDecorationOptions> GetDecorationOptions(string id)
            => JsRuntimeExt.Shared.SafeInvokeAsync<ModelDecorationOptions>("blazorMonaco.editor.model.getDecorationOptions", Uri, id);

        public Task<Range> GetDecorationRange(string id)
            => JsRuntimeExt.Shared.SafeInvokeAsync<Range>("blazorMonaco.editor.model.getDecorationRange", Uri, id);

        public Task<ModelDecoration> GetLineDecorations(int lineNumber, int? ownerId, bool? filterOutValidation)
            => JsRuntimeExt.Shared.SafeInvokeAsync<ModelDecoration>("blazorMonaco.editor.model.getLineDecorations", Uri, lineNumber, ownerId, filterOutValidation);

        public Task<ModelDecoration> GetLinesDecorations(int startLineNumber, int endLineNumber, int? ownerId, bool? filterOutValidation)
            => JsRuntimeExt.Shared.SafeInvokeAsync<ModelDecoration>("blazorMonaco.editor.model.getLinesDecorations", Uri, startLineNumber, endLineNumber, ownerId, filterOutValidation);

        public Task<ModelDecoration[]> GetDecorationsInRange(Range range, int? ownerId, bool? filterOutValidation)
            => JsRuntimeExt.Shared.SafeInvokeAsync<ModelDecoration[]>("blazorMonaco.editor.model.getDecorationsInRange", Uri, range, ownerId, filterOutValidation);

        public Task<List<ModelDecoration>> GetAllDecorations(int? ownerId, bool? filterOutValidation)
            => JsRuntimeExt.Shared.SafeInvokeAsync<List<ModelDecoration>>("blazorMonaco.editor.model.getAllDecorations", Uri, ownerId, filterOutValidation);

        public Task<ModelDecoration> GetOverviewRulerDecorations(int? ownerId, bool? filterOutValidation)
            => JsRuntimeExt.Shared.SafeInvokeAsync<ModelDecoration>("blazorMonaco.editor.model.getOverviewRulerDecorations", Uri, ownerId, filterOutValidation);

        public Task<ModelDecoration[]> GetInjectedTextDecorations(int? ownerId)
            => JsRuntimeExt.Shared.SafeInvokeAsync<ModelDecoration[]>("blazorMonaco.editor.model.getInjectedTextDecorations", Uri, ownerId);

        public Task<string> NormalizeIndentation(string str)
            => JsRuntimeExt.Shared.SafeInvokeAsync<string>("blazorMonaco.editor.model.normalizeIndentation", Uri, str);

        public Task UpdateOptions(TextModelUpdateOptions newOpts)
            => JsRuntimeExt.Shared.SafeInvokeAsync("blazorMonaco.editor.model.updateOptions", Uri, newOpts);

        public Task DetectIndentation(bool defaultInsertSpaces, int defaultTabSize)
            => JsRuntimeExt.Shared.SafeInvokeAsync("blazorMonaco.editor.model.detectIndentation", Uri, defaultInsertSpaces, defaultTabSize);

        public Task PushStackElement()
            => JsRuntimeExt.Shared.SafeInvokeAsync("blazorMonaco.editor.model.pushStackElement", Uri);

        public Task PopStackElement()
            => JsRuntimeExt.Shared.SafeInvokeAsync("blazorMonaco.editor.model.popStackElement", Uri);

        // pushEditOperations(beforeCursorState: Selection[] | null, editOperations: IIdentifiedSingleEditOperation[], cursorStateComputer: ICursorStateComputer): Selection[] | null;

        public Task PushEOL(EndOfLineSequence eol)
            => JsRuntimeExt.Shared.SafeInvokeAsync("blazorMonaco.editor.model.pushEOL", Uri, eol);

        public Task ApplyEdits(List<IdentifiedSingleEditOperation> operations)
            => JsRuntimeExt.Shared.SafeInvokeAsync("blazorMonaco.editor.model.applyEdits", Uri, operations);

        public Task<List<ValidEditOperation>> ApplyEdits(List<IdentifiedSingleEditOperation> operations, bool computeUndoEdits)
            => JsRuntimeExt.Shared.SafeInvokeAsync<List<ValidEditOperation>>("blazorMonaco.editor.model.applyEdits", Uri, operations, computeUndoEdits);

        public Task SetEOL(EndOfLineSequence eol)
            => JsRuntimeExt.Shared.SafeInvokeAsync("blazorMonaco.editor.model.setEOL", Uri, eol);

        // onDidChangeContent(listener: (e: IModelContentChangedEvent) => void): IDisposable;

        // onDidChangeDecorations(listener: (e: IModelDecorationsChangedEvent) => void): IDisposable;

        // onDidChangeOptions(listener: (e: IModelOptionsChangedEvent) => void): IDisposable;

        // onDidChangeLanguage(listener: (e: IModelLanguageChangedEvent) => void): IDisposable;

        // onDidChangeLanguageConfiguration(listener: (e: IModelLanguageConfigurationChangedEvent) => void): IDisposable;

        // onWillDispose(listener: () => void): IDisposable;

        public Task DisposeModel()
            => JsRuntimeExt.Shared.SafeInvokeAsync("blazorMonaco.editor.model.dispose", Uri);
    }
}
