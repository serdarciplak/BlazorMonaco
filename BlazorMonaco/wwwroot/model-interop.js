export const getModel = (uriStr) => {
    const uri = monaco.Uri.parse(uriStr);
    if (!uri)
        return null;
    return monaco.editor.getModel(uri);
};

export const getOptions = (uriStr) => getModel(uriStr)?.getOptions();

export const getVersionId = (uriStr) => getModel(uriStr)?.getVersionId();

export const getAlternativeVersionId = (uriStr) => getModel(uriStr)?.getAlternativeVersionId();

export const setValue = (uriStr, newValue) => getModel(uriStr)?.setValue(newValue);

export const getValue = (uriStr, eol, preserveBOM) => getModel(uriStr)?.getValue(eol, preserveBOM);

export const getValueLength = (uriStr, eol, preserveBOM) => getModel(uriStr)?.getValueLength(eol, preserveBOM);

export const getValueInRange = (uriStr, range, eol) => getModel(uriStr)?.getValueInRange(range, eol);

export const getValueLengthInRange = (uriStr, range) => getModel(uriStr)?.getValueLengthInRange(range);

export const getCharacterCountInRange = (uriStr, range) => getModel(uriStr)?.getCharacterCountInRange(range);

export const getLineCount = (uriStr) => getModel(uriStr)?.getLineCount();

export const getLineContent = (uriStr, lineNumber) => getModel(uriStr)?.getLineContent(lineNumber);

export const getLineLength = (uriStr, lineNumber) => getModel(uriStr)?.getLineLength(lineNumber);

export const getLinesContent = (uriStr) => getModel(uriStr)?.getLinesContent();

export const getEOL = (uriStr) => getModel(uriStr)?.getEOL();

export const getEndOfLineSequence = (uriStr) => getModel(uriStr)?.getEndOfLineSequence();

export const getLineMinColumn = (uriStr, lineNumber) => getModel(uriStr)?.getLineMinColumn(lineNumber);

export const getLineMaxColumn = (uriStr, lineNumber) => getModel(uriStr)?.getLineMaxColumn(lineNumber);

export const getLineFirstNonWhitespaceColumn = (uriStr, lineNumber) => getModel(uriStr)?.getLineFirstNonWhitespaceColumn(lineNumber);

export const getLineLastNonWhitespaceColumn = (uriStr, lineNumber) => getModel(uriStr)?.getLineLastNonWhitespaceColumn(lineNumber);

export const validatePosition = (uriStr, position) => getModel(uriStr)?.validatePosition(position);

export const modifyPosition = (uriStr, position, offset) => getModel(uriStr)?.modifyPosition(position, offset);

export const validateRange = (uriStr, range) => getModel(uriStr)?.validateRange(range);

export const getOffsetAt = (uriStr, position) => getModel(uriStr)?.getOffsetAt(position);

export const getPositionAt = (uriStr, offset) => getModel(uriStr)?.getPositionAt(offset);

export const getFullModelRange = (uriStr) => getModel(uriStr)?.getFullModelRange();

export const isDisposed = (uriStr) => getModel(uriStr)?.isDisposed();

export const findMatches = (uriStr, searchString, searchScope_or_searchOnlyEditableRange, isRegex, matchCase, wordSeparators, captureMatches, limitResultCount) => getModel(uriStr)?.findMatches(searchString, searchScope_or_searchOnlyEditableRange, isRegex, matchCase, wordSeparators, captureMatches, limitResultCount);

export const findNextMatch = (uriStr, searchString, searchStart, isRegex, matchCase, wordSeparators, captureMatches) => getModel(uriStr)?.findNextMatch(searchString, searchStart, isRegex, matchCase, wordSeparators, captureMatches);

export const findPreviousMatch = (uriStr, searchString, searchStart, isRegex, matchCase, wordSeparators, captureMatches) => getModel(uriStr)?.findPreviousMatch(searchString, searchStart, isRegex, matchCase, wordSeparators, captureMatches);

export const getLanguageId = (uriStr) => getModel(uriStr)?.getLanguageId();

export const getWordAtPosition = (uriStr, position) => getModel(uriStr)?.getWordAtPosition(position);

export const getWordUntilPosition = (uriStr, position) => getModel(uriStr)?.getWordUntilPosition(position);

export const deltaDecorations = (uriStr, oldDecorations, newDecorations, ownerId) => getModel(uriStr)?.deltaDecorations(oldDecorations, newDecorations, ownerId);

export const getDecorationOptions = (uriStr, id) => getModel(uriStr)?.getDecorationOptions(id);

export const getDecorationRange = (uriStr, id) => getModel(uriStr)?.getDecorationRange(id);

export const getLineDecorations = (uriStr, lineNumber, ownerId, filterOutValidation) => getModel(uriStr)?.getLineDecorations(lineNumber, ownerId, filterOutValidation);

export const getLinesDecorations = (uriStr, startLineNumber, endLineNumber, ownerId, filterOutValidation) => getModel(uriStr)?.getLinesDecorations(startLineNumber, endLineNumber, ownerId, filterOutValidation);

export const getDecorationsInRange = (uriStr, range, ownerId, filterOutValidation) => getModel(uriStr)?.getDecorationsInRange(range, ownerId, filterOutValidation);

export const getAllDecorations = (uriStr, ownerId, filterOutValidation) => getModel(uriStr)?.getAllDecorations(ownerId, filterOutValidation);

export const getInjectedTextDecorations = (uriStr, ownerId) => getModel(uriStr)?.getInjectedTextDecorations(ownerId);

export const getOverviewRulerDecorations = (uriStr, ownerId, filterOutValidation) => getModel(uriStr)?.getOverviewRulerDecorations(ownerId, filterOutValidation);

export const normalizeIndentation = (uriStr, str) => getModel(uriStr)?.normalizeIndentation(str);

export const updateOptions = (uriStr, newOpts) => getModel(uriStr)?.updateOptions(newOpts);

export const detectIndentation = (uriStr, defaultInsertSpaces, defaultTabSize) => getModel(uriStr)?.detectIndentation(defaultInsertSpaces, defaultTabSize);

export const pushStackElement = (uriStr) => getModel(uriStr)?.pushStackElement();

export const popStackElement = (uriStr) => getModel(uriStr)?.popStackElement();

export const pushEOL = (uriStr, eol) => getModel(uriStr)?.pushEOL(eol);

export const applyEdits = (uriStr, operations, computeUndoEdits) => getModel(uriStr)?.applyEdits(operations, computeUndoEdits);

export const setEOL = (uriStr, eol) => getModel(uriStr)?.setEOL(eol);

export const dispose = (uriStr) => getModel(uriStr)?.dispose();