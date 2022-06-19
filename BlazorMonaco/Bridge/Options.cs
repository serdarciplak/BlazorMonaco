using System;
using System.Collections.Generic;

namespace BlazorMonaco
{
    // Options Containers

    public class EditorOptions
    {
        public bool? AcceptSuggestionOnCommitCharacter { get; set; }
        public string AcceptSuggestionOnEnter { get; set; }
        public int? AccessibilityPageSize { get; set; }
        public string AccessibilitySupport { get; set; }
        public string AriaLabel { get; set; }
        public string AutoClosingBrackets { get; set; }
        public string AutoClosingDelete { get; set; }
        public string AutoClosingOvertype { get; set; } //'always' | 'auto' | 'never';
        public string AutoClosingQuotes { get; set; }
        public bool? AutoIndent { get; set; }
        public string AutoSurround { get; set; }
        public bool? AutomaticLayout { get; set; }
        public int? CodeActionsOnSaveTimeout { get; set; }
        public bool? CodeLens { get; set; }
        public string CodeLensFontFamily { get; set; }
        public int? CodeLensFontSize { get; set; }
        public bool? ColorDecorators { get; set; }
        public bool? ColumnSelection { get; set; }
        public EditorCommentsOptions Comments { get; set; }
        public bool? Contextmenu { get; set; }
        public bool? CopyWithSyntaxHighlighting { get; set; }
        public string CursorBlinking { get; set; }
        public bool? CursorSmoothCaretAnimation { get; set; }
        public string CursorStyle { get; set; }
        public int? CursorSurroundingLines { get; set; }
        public string CursorSurroundingLinesStyle { get; set; }
        public int? CursorWidth { get; set; }
        public bool? DefinitionLinkOpensInPeek { get; set; }
        public bool? DisableLayerHinting { get; set; }
        public bool? DisableMonospaceOptimizations { get; set; }
        public bool? DomReadOnly { get; set; }
        public bool? DragAndDrop { get; set; }
        public bool? EmptySelectionClipboard { get; set; }
        public string ExtraEditorClassName { get; set; }
        public int? FastScrollSensitivity { get; set; }
        public EditorFindOptions Find { get; set; }
        public bool? FixedOverflowWidgets { get; set; }
        public bool? Folding { get; set; }
        public bool? FoldingHighlight { get; set; }
        public bool? FoldingImportsByDefault { get; set; }
        public bool? FoldingMaximumRegions { get; set; }
        public string FoldingStrategy { get; set; }
        public string FontFamily { get; set; }
        public bool? FontLigatures { get; set; }
        public int? FontSize { get; set; }
        public string FontWeight { get; set; }
        public bool? FormatOnPaste { get; set; }
        public bool? FormatOnType { get; set; }
        public bool? GlyphMargin { get; set; }
        public GotoLocationOptions GotoLocation { get; set; }
        public GuidesOptions Guides { get; set; }
        public bool? HideCursorInOverviewRuler { get; set; }
        public bool? RenderControlCharacters { get; set; }
        public EditorHoverOptions Hover { get; set; }
        public bool? InDiffEditor { get; set; }
        public EditorInlayHintsOptions InlayHints { get; set; }
        public InlineSuggestOptions InlineSuggest { get; set; }
        public int? LetterSpacing { get; set; }
        public EditorLightbulbOptions Lightbulb { get; set; }
        public string LineDecorationsWidth { get; set; }
        public int? LineHeight { get; set; }
        public string LineNumbers { get; set; }
        public Func<int, string> LineNumbersLambda { get; set; }
        public int? LineNumbersMinChars { get; set; }
        public bool? LinkedEditing { get; set; }
        public bool? Links { get; set; }
        public bool? MatchBrackets { get; set; }
        public EditorMinimapOptions Minimap { get; set; }
        public string MouseStyle { get; set; }
        public int? MouseWheelScrollSensitivity { get; set; }
        public bool? MouseWheelZoom { get; set; }
        public bool? MultiCursorMergeOverlapping { get; set; }
        public string MultiCursorModifier { get; set; }
        public string MultiCursorPaste { get; set; }
        public bool? OccurrencesHighlight { get; set; }
        public bool? OverviewRulerBorder { get; set; }
        public int? OverviewRulerLanes { get; set; }
        public EditorPaddingOptions Padding { get; set; }
        public EditorParameterHintOptions ParameterHints { get; set; }
        public bool? PeekWidgetDefaultFocus { get; set; }
        public QuickSuggestionsOptions QuickSuggestions { get; set; }
        public int? QuickSuggestionsDelay { get; set; }
        public bool? ReadOnly { get; set; }
        public bool? RenameOnType { get; set; }
        public bool? RenderFinalNewline { get; set; }
        public string RenderLineHighlight { get; set; }
        public bool? RenderLineHighlightOnlyWhenFocus { get; set; }
        public string RenderValidationDecorations { get; set; }
        public string RenderWhitespace { get; set; }
        public int? RevealHorizontalRightPadding { get; set; }
        public bool? RoundedSelection { get; set; }
        public int[] Rulers { get; set; }
        public int? ScrollBeyondLastColumn { get; set; }
        public bool? ScrollBeyondLastLine { get; set; }
        public EditorScrollbarOptions Scrollbar { get; set; }
        public bool? ScrollPredominantAxis { get; set; }
        public bool? SelectOnLineNumbers { get; set; }
        public bool? SelectionClipboard { get; set; }
        public bool? SelectionHighlight { get; set; }
        public bool? ShowDeprecated { get; set; }
        public string ShowFoldingControls { get; set; }
        public bool? ShowUnused { get; set; }
        public SmartSelectOptions SmartSelect { get; set; }
        public bool? SmoothScrolling { get; set; }
        public string SnippetSuggestions { get; set; }
        public bool? StickyTabStops { get; set; }
        public int? StopRenderingLineAfter { get; set; }
        public SuggestOptions Suggest { get; set; }
        public int? SuggestFontSize { get; set; }
        public int? SuggestLineHeight { get; set; }
        public bool? SuggestOnTriggerCharacters { get; set; }
        public string SuggestSelection { get; set; }
        public string TabCompletion { get; set; }
        public int? TabIndex { get; set; }
        public bool? UnfoldOnClickAfterEndOfLine { get; set; }
        public string UnusualLineTerminators { get; set; }
        public bool? UseTabStops { get; set; }
        public string WordSeparators { get; set; }
        public string WordWrap { get; set; }
        public string WordWrapBreakAfterCharacters { get; set; }
        public string WordWrapBreakBeforeCharacters { get; set; }
        public int? WordWrapColumn { get; set; }
        public string WordWrapOverride1 { get; set; }
        public string WordWrapOverride2 { get; set; }
        public string WrappingIndent { get; set; }
        public string WrappingStrategy { get; set; }

        public bool? UseShadowDOM { get; set; }
        public object UnicodeHighlight { get; set; }
        public BracketPairColorizationOptions BracketPairColorization { get; set; }
    }

    public class DiffEditorOptions : EditorOptions
    {
        public bool? EnableSplitViewResizing { get; set; }
        public bool? RenderSideBySide { get; set; }
        public int? MaxComputationTime { get; set; }
        public bool? IgnoreTrimWhitespace { get; set; }
        public bool? RenderIndicators { get; set; }
        public bool? OriginalEditable { get; set; }
        public bool? DiffCodeLens { get; set; }
        public bool? IsInEmbeddedEditor { get; set; }
        public bool? RenderOverviewRuler { get; set; }
        public string DiffWordWrap { get; set; }

        public int? MaxFileSize { get; set; }
    }

    public class DiffEditorConstructionOptions : DiffEditorOptions
    {
        public string Theme { get; set; }
        public bool? AutoDetectHighContrast { get; set; }

        public Dimension Dimension { get; set; }
        // overflowWidgetsDomNode?: HTMLElement;
        public string OriginalAriaLabel { get; set; }
        public string ModifiedAriaLabel { get; set; }
        public bool? IsInEmbeddedEditor { get; set; }

    }

    public class StandaloneDiffEditorConstructionOptions : DiffEditorConstructionOptions
    {
        public string Theme { get; set; }
        public bool AutoDetectHighContrast { get; set; }
    }

    public class GlobalEditorOptions : EditorOptions
    {
        public int? TabSize { get; set; }
        public bool? InsertSpaces { get; set; }
        public bool? DetectIndentation { get; set; }
        public bool? TrimAutoWhitespace { get; set; }
        public bool? LargeFileOptimizations { get; set; }
        public bool? WordBasedSuggestions { get; set; }
        public bool? WordBasedSuggestionsOnlySameLanguage { get; set; }
        // TODO 'semanticHighlighting.enabled'?: true | false | 'configuredByTheme';
        public bool? StablePeek { get; set; }
        public int? MaxTokenizationLineLength { get; set; }
        public string Theme { get; set; }
        public bool? AutoDetectHighContrast { get; set; }
    }

    public class EditorConstructionOptions : GlobalEditorOptions
    {
        public Dimension Dimension { get; set; }
        // overflowWidgetsDomNode?: HTMLElement;
    }

    public class StandaloneEditorConstructionOptions : EditorConstructionOptions
    {
        public TextModel Model { get; set; }
        public string Value { get; set; }
        public string Language { get; set; }
        public string AccessibilityHelpUrl { get; set; }
        // ariaContainerElement?: HTMLElement;
    }

    // Individual Options

    public class ColorizerElementOptions : ColorizerOptions
    {
        public string MimeType { get; set; }
        public string Theme { get; set; }
    }

    public class ColorizerOptions
    {
        public int? TabSize { get; set; }
    }

    public class EditorCommentsOptions
    {
        public bool? InsertSpace { get; set; }
    }

    public class EditorFindOptions
    {
        public bool? AddExtraSpaceOnTop { get; set; }
        public bool? AutoFindInSelection { get; set; }
        public string SeedSearchStringFromSelection { get; set; } //'never' | 'always' | 'selection';
    }

    public class EditorHoverOptions
    {
        public int? Delay { get; set; } = 300;
        public bool? Enabled { get; set; } = true;
        public bool? Sticky { get; set; } = true;
        public bool? Above { get; set; }
    }

    public class EditorInlayHintsOptions
    {
        public bool? Enabled { get; set; }
        public int? FontSize { get; set; }
        public string FontFamily { get; set; }
    }

    public class EditorLightbulbOptions
    {
        public bool? Enabled { get; set; }
    }

    public class EditorMinimapOptions
    {
        public bool? Enabled { get; set; }
        public int? MaxColumn { get; set; }
        public bool? RenderCharacters { get; set; }
        public int? Scale { get; set; }
        public string ShowSlider { get; set; }
        public string Side { get; set; }
        public string Size { get; set; }
    }

    public class EditorPaddingOptions
    {
        public int? Top { get; set; }
        public int? Bottom { get; set; }
    }

    public class EditorParameterHintOptions
    {
        public bool? Cycle { get; set; }
        public bool? Enabled { get; set; } = true;
    }

    public class EditorScrollbarOptions
    {
        public bool? AlwaysConsumeMouseWheel { get; set; }
        public int? ArrowSize { get; set; }
        public bool? HandleMouseWheel { get; set; }
        public string Horizontal { get; set; }
        public bool? HorizontalHasArrows { get; set; }
        public int? HorizontalScrollbarSize { get; set; }
        public int? HorizontalSliderSize { get; set; }
        public bool? ScrollByPage { get; set; }
        public bool? UseShadows { get; set; }
        public string Vertical { get; set; }
        public bool? VerticalHasArrows { get; set; }
        public int? VerticalScrollbarSize { get; set; }
        public int? VerticalSliderSize { get; set; }
    }

    public class GotoLocationOptions
    {
        public string AlternativeDeclarationCommand { get; set; }
        public string AlternativeDefinitionCommand { get; set; }
        public string AlternativeImplementationCommand { get; set; }
        public string AlternativeReferenceCommand { get; set; }
        public string AlternativeTypeDefinitionCommand { get; set; }
        public string Multiple { get; set; }
        public string MultipleDeclarations { get; set; }
        public string MultipleDefinitions { get; set; }
        public string MultipleImplementations { get; set; }
        public string MultipleReferences { get; set; }
        public string MultipleTypeDefinitions { get; set; }
    }

    public class QuickSuggestionsOptions
    {
        public bool? Comments { get; set; }
        public bool? Other { get; set; }
        public bool? Strings { get; set; }
    }

    public class SmartSelectOptions
    {
        public bool? SelectLeadingAndTrailingWhitespace { get; set; }
    }

    public class SuggestOptions
    {
        public bool? FilterGraceful { get; set; }
        public string InsertMode { get; set; }
        public bool? LocalityBonus { get; set; }
        public bool? ShareSuggestSelections { get; set; }
        public bool? ShowClasses { get; set; }
        public bool? ShowColors { get; set; }
        public bool? ShowConstants { get; set; }
        public bool? ShowConstructors { get; set; }
        public bool? ShowEnumMembers { get; set; }
        public bool? ShowEnums { get; set; }
        public bool? ShowEvents { get; set; }
        public bool? ShowFields { get; set; }
        public bool? ShowFiles { get; set; }
        public bool? ShowFolders { get; set; }
        public bool? ShowFunctions { get; set; }
        public bool? ShowIcons { get; set; }
        public bool? ShowInlineDetails { get; set; }
        public bool? ShowInterfaces { get; set; }
        public bool? ShowIssues { get; set; }
        public bool? ShowKeywords { get; set; }
        public bool? ShowMethods { get; set; }
        public bool? ShowModules { get; set; }
        public bool? ShowOperators { get; set; }
        public bool? ShowProperties { get; set; }
        public bool? ShowReferences { get; set; }
        public bool? ShowSnippets { get; set; }
        public bool? ShowStatusBar { get; set; }
        public bool? ShowStructs { get; set; }
        public bool? ShowTypeParameters { get; set; }
        public bool? ShowUnits { get; set; }
        public bool? ShowUsers { get; set; }
        public bool? ShowValues { get; set; }
        public bool? ShowVariables { get; set; }
        public bool? ShowWords { get; set; }
        public bool? SnippetsPreventQuickSuggestions { get; set; }
        public  bool? Preview { get; set; }
        public string PreviewModel { get; set; } //'prefix' | 'subword' | 'subwordSmart';
        public bool? ShowDeprecated { get; set; }
    }


    public class GuidesOptions
    {
        public string BracketPairs { get; set; } //?: boolean | 'active';
        public string BracketPairsHorizontal { get; set; } = "active"; //?: boolean | 'active';
        public bool? HighlightActiveBracketPair { get; set; } = true;
        public bool? Indentation { get; set; } = true;
        public bool? HighlightActiveIndentation { get; set; } = true;
    }

    public class ModelDeltaDecoration
    {
        public Range Range { get; set; }
        public ModelDecorationOptions Options { get; set; }
    }

    public class ModelDecoration
    {
        public string Id { get; set; }
        public int OwnerId { get; set; }
        public Range Range { get; set; }
        public ModelDecorationOptions Options { get; set; }
    }

    public class ModelDecorationOptions
    {
        public string AfterContentClassName { get; set; }
        public string BeforeContentClassName { get; set; }
        public string ClassName { get; set; }
        public string GlyphMarginClassName { get; set; }
        public MarkdownString[] GlyphMarginHoverMessage { get; set; }
        public MarkdownString[] HoverMessage { get; set; }
        public string InlineClassName { get; set; }
        public bool? InlineClassNameAffectsLetterSpacing { get; set; }
        public bool? IsWholeLine { get; set; }
        public string LinesDecorationsClassName { get; set; }
        public string FirstLineDecorationClassName { get; set; }
        public string MarginClassName { get; set; }
        public ModelDecorationMinimapOptions Minimap { get; set; }
        public ModelDecorationOverviewRulerOptions OverviewRuler { get; set; }
        public TrackedRangeStickiness? Stickiness { get; set; }
        public int? ZIndex { get; set; }
    }

    public class MarkdownString
    {
        public bool? IsTrusted { get; set; }
        public bool? SupportThemeIcons { get; set; }
        public bool? SupportHtml { get; set; }
        public UriComponents BaseUri { get; set; }
        public string Uris { get; set; }
        public string Value { get; set; }
    }

    public class ModelDecorationMinimapOptions : DecorationOptions
    {
        public MinimapPosition? Position { get; set; }
    }

    public class ModelDecorationOverviewRulerOptions : DecorationOptions
    {
        public MinimapPosition? Position { get; set; }
    }

    public class DecorationOptions
    {
        public string Color { get; set; }
        public string DarkColor { get; set; }
    }

    // Model Options

    public class TextModelResolvedOptions
    {
        public int? TabSize { get; set; }
        public int? IndentSize { get; set; }
        public bool? InsertSpaces { get; set; }
        public DefaultEndOfLine? DefaultEOL { get; set; }
        public bool? TrimAutoWhitespace { get; set; }
    }

    public class BracketPairColorizationOptions
    {
        public bool Enabled { get; set; }
    }

    public class TextModelUpdateOptions
    {
        public int? TabSize { get; set; }
        public int? IndentSize { get; set; }
        public bool? InsertSpaces { get; set; }
        public bool? TrimAutoWhitespace { get; set; }
        public BracketPairColorizationOptions BracketColorizationOptions { get; set; }
    }
    
    public class WordAtPosition
    {
        public string Word { get; set; }
        public int StartColumn { get; set; }
        public int EndColumn { get; set; }
    }

    public class InlineSuggestOptions
    {
        public bool? Enabled { get; set; }
        public string Mode { get; set; } = "prefix"; //'prefix' | 'subword' | 'subwordSmart'
    }



    public class FindMatch
    {
        public Range Range { get; set; }
        public List<string> Matches { get; set; }
    }

    public class IdentifiedSingleEditOperation : SingleEditOperation
    {
    }

    public class SingleEditOperation
    {
        public Range Range { get; set; }
        public string Text { get; set; }
        public bool? ForceMoveMarkers { get; set; }
    }

    public class ValidEditOperation
    {
        public Range Range { get; set; }
        public string Text { get; set; }
    }

    /*public class CursorStateComputer
    {
        public Func<List<ValidEditOperation>, List<Selection>> InverseEditOperations { get; set; }
    }*/
}