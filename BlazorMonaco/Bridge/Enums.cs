using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorMonaco
{
    public enum CursorChangeReason
    {
        /**
         * Unknown or not set.
         */
        NotSet = 0,
        /**
         * A `model.setValue()` was called.
         */
        ContentFlush = 1,
        /**
         * The `model` has been changed outside of this cursor and the cursor recovers its position from associated markers.
         */
        RecoverFromMarkers = 2,
        /**
         * There was an explicit user gesture.
         */
        Explicit = 3,
        /**
         * There was a Paste.
         */
        Paste = 4,
        /**
         * There was an Undo.
         */
        Undo = 5,
        /**
         * There was a Redo.
         */
        Redo = 6
    }

    public enum EditorOption
    {
        AcceptSuggestionOnCommitCharacter = 0,
        AcceptSuggestionOnEnter = 1,
        AccessibilitySupport = 2,
        AccessibilityPageSize = 3,
        AriaLabel = 4,
        AutoClosingBrackets = 5,
        AutoClosingOvertype = 6,
        AutoClosingQuotes = 7,
        AutoIndent = 8,
        AutomaticLayout = 9,
        AutoSurround = 10,
        CodeLens = 11,
        CodeLensFontFamily = 12,
        CodeLensFontSize = 13,
        ColorDecorators = 14,
        ColumnSelection = 15,
        Comments = 16,
        Contextmenu = 17,
        CopyWithSyntaxHighlighting = 18,
        CursorBlinking = 19,
        CursorSmoothCaretAnimation = 20,
        CursorStyle = 21,
        CursorSurroundingLines = 22,
        CursorSurroundingLinesStyle = 23,
        CursorWidth = 24,
        DisableLayerHinting = 25,
        DisableMonospaceOptimizations = 26,
        DragAndDrop = 27,
        EmptySelectionClipboard = 28,
        ExtraEditorClassName = 29,
        FastScrollSensitivity = 30,
        Find = 31,
        FixedOverflowWidgets = 32,
        Folding = 33,
        FoldingStrategy = 34,
        FoldingHighlight = 35,
        UnfoldOnClickAfterEndOfLine = 36,
        FontFamily = 37,
        FontInfo = 38,
        FontLigatures = 39,
        FontSize = 40,
        FontWeight = 41,
        FormatOnPaste = 42,
        FormatOnType = 43,
        GlyphMargin = 44,
        GotoLocation = 45,
        HideCursorInOverviewRuler = 46,
        HighlightActiveIndentGuide = 47,
        Hover = 48,
        InDiffEditor = 49,
        LetterSpacing = 50,
        Lightbulb = 51,
        LineDecorationsWidth = 52,
        LineHeight = 53,
        LineNumbers = 54,
        LineNumbersMinChars = 55,
        LinkedEditing = 56,
        Links = 57,
        MatchBrackets = 58,
        Minimap = 59,
        MouseStyle = 60,
        MouseWheelScrollSensitivity = 61,
        MouseWheelZoom = 62,
        MultiCursorMergeOverlapping = 63,
        MultiCursorModifier = 64,
        MultiCursorPaste = 65,
        OccurrencesHighlight = 66,
        OverviewRulerBorder = 67,
        OverviewRulerLanes = 68,
        Padding = 69,
        ParameterHints = 70,
        PeekWidgetDefaultFocus = 71,
        DefinitionLinkOpensInPeek = 72,
        QuickSuggestions = 73,
        QuickSuggestionsDelay = 74,
        ReadOnly = 75,
        RenameOnType = 76,
        RenderControlCharacters = 77,
        RenderIndentGuides = 78,
        RenderFinalNewline = 79,
        RenderLineHighlight = 80,
        RenderLineHighlightOnlyWhenFocus = 81,
        RenderValidationDecorations = 82,
        RenderWhitespace = 83,
        RevealHorizontalRightPadding = 84,
        RoundedSelection = 85,
        Rulers = 86,
        scrollbar = 87,
        ScrollBeyondLastColumn = 88,
        ScrollBeyondLastLine = 89,
        ScrollPredominantAxis = 90,
        SelectionClipboard = 91,
        SelectionHighlight = 92,
        SelectOnLineNumbers = 93,
        ShowFoldingControls = 94,
        ShowUnused = 95,
        SnippetSuggestions = 96,
        SmartSelect = 97,
        SmoothScrolling = 98,
        StickyTabStops = 99,
        StopRenderingLineAfter = 100,
        Suggest = 101,
        SuggestFontSize = 102,
        SuggestLineHeight = 103,
        SuggestOnTriggerCharacters = 104,
        SuggestSelection = 105,
        TabCompletion = 106,
        TabIndex = 107,
        UnusualLineTerminators = 108,
        UseTabStops = 109,
        WordSeparators = 110,
        WordWrap = 111,
        WordWrapBreakAfterCharacters = 112,
        WordWrapBreakBeforeCharacters = 113,
        WordWrapColumn = 114,
        WordWrapOverride1 = 115,
        WordWrapOverride2 = 116,
        WrappingIndent = 117,
        WrappingStrategy = 118,
        ShowDeprecated = 119,
        InlineHints = 120,
        EditorClassName = 121,
        PixelRatio = 122,
        TabFocusMode = 123,
        LayoutInfo = 124,
        WrappingInfo = 125
    }

    public enum KeyMode
    {
        CtrlCmd = 2048,
        Shift = 1024,
        Alt = 512,
        WinCtrl = 256,
    }

    public enum KeyCode
    {
        /**
         * Placed first to cover the 0 value of the enum.
         */
        Unknown = 0,
        Backspace = 1,
        Tab = 2,
        Enter = 3,
        Shift = 4,
        Ctrl = 5,
        Alt = 6,
        PauseBreak = 7,
        CapsLock = 8,
        Escape = 9,
        Space = 10,
        PageUp = 11,
        PageDown = 12,
        End = 13,
        Home = 14,
        LeftArrow = 15,
        UpArrow = 16,
        RightArrow = 17,
        DownArrow = 18,
        Insert = 19,
        Delete = 20,
        KEY_0 = 21,
        KEY_1 = 22,
        KEY_2 = 23,
        KEY_3 = 24,
        KEY_4 = 25,
        KEY_5 = 26,
        KEY_6 = 27,
        KEY_7 = 28,
        KEY_8 = 29,
        KEY_9 = 30,
        KEY_A = 31,
        KEY_B = 32,
        KEY_C = 33,
        KEY_D = 34,
        KEY_E = 35,
        KEY_F = 36,
        KEY_G = 37,
        KEY_H = 38,
        KEY_I = 39,
        KEY_J = 40,
        KEY_K = 41,
        KEY_L = 42,
        KEY_M = 43,
        KEY_N = 44,
        KEY_O = 45,
        KEY_P = 46,
        KEY_Q = 47,
        KEY_R = 48,
        KEY_S = 49,
        KEY_T = 50,
        KEY_U = 51,
        KEY_V = 52,
        KEY_W = 53,
        KEY_X = 54,
        KEY_Y = 55,
        KEY_Z = 56,
        Meta = 57,
        ContextMenu = 58,
        F1 = 59,
        F2 = 60,
        F3 = 61,
        F4 = 62,
        F5 = 63,
        F6 = 64,
        F7 = 65,
        F8 = 66,
        F9 = 67,
        F10 = 68,
        F11 = 69,
        F12 = 70,
        F13 = 71,
        F14 = 72,
        F15 = 73,
        F16 = 74,
        F17 = 75,
        F18 = 76,
        F19 = 77,
        NumLock = 78,
        ScrollLock = 79,
        /**
         * Used for miscellaneous characters; it can vary by keyboard.
         * For the US standard keyboard, the ';:' key
         */
        US_SEMICOLON = 80,
        /**
         * For any country/region, the '+' key
         * For the US standard keyboard, the '=+' key
         */
        US_EQUAL = 81,
        /**
         * For any country/region, the ',' key
         * For the US standard keyboard, the ',<' key
         */
        US_COMMA = 82,
        /**
         * For any country/region, the '-' key
         * For the US standard keyboard, the '-_' key
         */
        US_MINUS = 83,
        /**
         * For any country/region, the '.' key
         * For the US standard keyboard, the '.>' key
         */
        US_DOT = 84,
        /**
         * Used for miscellaneous characters; it can vary by keyboard.
         * For the US standard keyboard, the '/?' key
         */
        US_SLASH = 85,
        /**
         * Used for miscellaneous characters; it can vary by keyboard.
         * For the US standard keyboard, the '`~' key
         */
        US_BACKTICK = 86,
        /**
         * Used for miscellaneous characters; it can vary by keyboard.
         * For the US standard keyboard, the '[{' key
         */
        US_OPEN_SQUARE_BRACKET = 87,
        /**
         * Used for miscellaneous characters; it can vary by keyboard.
         * For the US standard keyboard, the '\|' key
         */
        US_BACKSLASH = 88,
        /**
         * Used for miscellaneous characters; it can vary by keyboard.
         * For the US standard keyboard, the ']}' key
         */
        US_CLOSE_SQUARE_BRACKET = 89,
        /**
         * Used for miscellaneous characters; it can vary by keyboard.
         * For the US standard keyboard, the ''"' key
         */
        US_QUOTE = 90,
        /**
         * Used for miscellaneous characters; it can vary by keyboard.
         */
        OEM_8 = 91,
        /**
         * Either the angle bracket key or the backslash key on the RT 102-key keyboard.
         */
        OEM_102 = 92,
        NUMPAD_0 = 93,
        NUMPAD_1 = 94,
        NUMPAD_2 = 95,
        NUMPAD_3 = 96,
        NUMPAD_4 = 97,
        NUMPAD_5 = 98,
        NUMPAD_6 = 99,
        NUMPAD_7 = 100,
        NUMPAD_8 = 101,
        NUMPAD_9 = 102,
        NUMPAD_MULTIPLY = 103,
        NUMPAD_ADD = 104,
        NUMPAD_SEPARATOR = 105,
        NUMPAD_SUBTRACT = 106,
        NUMPAD_DECIMAL = 107,
        NUMPAD_DIVIDE = 108,
        /**
         * Cover all key codes when IME is processing input.
         */
        KEY_IN_COMPOSITION = 109,
        ABNT_C1 = 110,
        ABNT_C2 = 111,
        /**
         * Placed last to cover the length of the enum.
         * Please do not depend on this value!
         */
        MAX_VALUE = 112
    }

    public enum MouseTargetType
    {
        /**
         * Mouse is on top of an unknown element.
         */
        UNKNOWN = 0,
        /**
         * Mouse is on top of the textarea used for input.
         */
        TEXTAREA = 1,
        /**
         * Mouse is on top of the glyph margin
         */
        GUTTER_GLYPH_MARGIN = 2,
        /**
         * Mouse is on top of the line numbers
         */
        GUTTER_LINE_NUMBERS = 3,
        /**
         * Mouse is on top of the line decorations
         */
        GUTTER_LINE_DECORATIONS = 4,
        /**
         * Mouse is on top of the whitespace left in the gutter by a view zone.
         */
        GUTTER_VIEW_ZONE = 5,
        /**
         * Mouse is on top of text in the content.
         */
        CONTENT_TEXT = 6,
        /**
         * Mouse is on top of empty space in the content (e.g. after line text or below last line)
         */
        CONTENT_EMPTY = 7,
        /**
         * Mouse is on top of a view zone in the content.
         */
        CONTENT_VIEW_ZONE = 8,
        /**
         * Mouse is on top of a content widget.
         */
        CONTENT_WIDGET = 9,
        /**
         * Mouse is on top of the decorations overview ruler.
         */
        OVERVIEW_RULER = 10,
        /**
         * Mouse is on top of a scrollbar.
         */
        SCROLLBAR = 11,
        /**
         * Mouse is on top of an overlay widget.
         */
        OVERLAY_WIDGET = 12,
        /**
         * Mouse is outside of the editor.
         */
        OUTSIDE_EDITOR = 13
    }

    public enum ScrollType
    {
        Smooth = 0,
        Immediate = 1
    }

    public enum MinimapPosition
    {
        Inline = 1,
        Gutter = 2
    }

    public enum RenderMinimap
    {
        None = 0,
        Text = 1,
        Blocks = 2
    }

    public enum TrackedRangeStickiness
    {
        AlwaysGrowsWhenTypingAtEdges = 0,
        NeverGrowsWhenTypingAtEdges = 1,
        GrowsOnlyWhenTypingBefore = 2,
        GrowsOnlyWhenTypingAfter = 3
    }

    public enum DefaultEndOfLine
    {
        LF = 1,
        CRLF = 2
    }

    public enum EndOfLinePreference
    {
        TextDefined = 0,
        LF = 1,
        CRLF = 2
    }

    public enum EndOfLineSequence
    {
        LF = 0,
        CRLF = 1
    }
}
