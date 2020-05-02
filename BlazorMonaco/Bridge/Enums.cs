using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorMonaco.Bridge
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
        ColorDecorators = 12,
        Comments = 13,
        Contextmenu = 14,
        CopyWithSyntaxHighlighting = 15,
        CursorBlinking = 16,
        CursorSmoothCaretAnimation = 17,
        CursorStyle = 18,
        CursorSurroundingLines = 19,
        CursorSurroundingLinesStyle = 20,
        CursorWidth = 21,
        DisableLayerHinting = 22,
        DisableMonospaceOptimizations = 23,
        DragAndDrop = 24,
        EmptySelectionClipboard = 25,
        ExtraEditorClassName = 26,
        FastScrollSensitivity = 27,
        Find = 28,
        FixedOverflowWidgets = 29,
        Folding = 30,
        FoldingStrategy = 31,
        FoldingHighlight = 32,
        FontFamily = 33,
        FontInfo = 34,
        FontLigatures = 35,
        FontSize = 36,
        FontWeight = 37,
        FormatOnPaste = 38,
        FormatOnType = 39,
        GlyphMargin = 40,
        GotoLocation = 41,
        HideCursorInOverviewRuler = 42,
        HighlightActiveIndentGuide = 43,
        Hover = 44,
        InDiffEditor = 45,
        LetterSpacing = 46,
        Lightbulb = 47,
        LineDecorationsWidth = 48,
        LineHeight = 49,
        LineNumbers = 50,
        LineNumbersMinChars = 51,
        Links = 52,
        MatchBrackets = 53,
        Minimap = 54,
        MouseStyle = 55,
        MouseWheelScrollSensitivity = 56,
        MouseWheelZoom = 57,
        MultiCursorMergeOverlapping = 58,
        MultiCursorModifier = 59,
        MultiCursorPaste = 60,
        OccurrencesHighlight = 61,
        OverviewRulerBorder = 62,
        OverviewRulerLanes = 63,
        ParameterHints = 64,
        PeekWidgetDefaultFocus = 65,
        QuickSuggestions = 66,
        QuickSuggestionsDelay = 67,
        ReadOnly = 68,
        RenderControlCharacters = 69,
        RenderIndentGuides = 70,
        RenderFinalNewline = 71,
        RenderLineHighlight = 72,
        RenderValidationDecorations = 73,
        RenderWhitespace = 74,
        RevealHorizontalRightPadding = 75,
        RoundedSelection = 76,
        Rulers = 77,
        Scrollbar = 78,
        ScrollBeyondLastColumn = 79,
        ScrollBeyondLastLine = 80,
        SelectionClipboard = 81,
        SelectionHighlight = 82,
        SelectOnLineNumbers = 83,
        ShowFoldingControls = 84,
        ShowUnused = 85,
        SnippetSuggestions = 86,
        SmoothScrolling = 87,
        StopRenderingLineAfter = 88,
        Suggest = 89,
        SuggestFontSize = 90,
        SuggestLineHeight = 91,
        SuggestOnTriggerCharacters = 92,
        SuggestSelection = 93,
        TabCompletion = 94,
        UseTabStops = 95,
        WordSeparators = 96,
        WordWrap = 97,
        WordWrapBreakAfterCharacters = 98,
        WordWrapBreakBeforeCharacters = 99,
        WordWrapColumn = 100,
        WordWrapMinified = 101,
        WrappingIndent = 102,
        WrappingStrategy = 103,
        EditorClassName = 104,
        PixelRatio = 105,
        TabFocusMode = 106,
        LayoutInfo = 107,
        WrappingInfo = 108
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
}
