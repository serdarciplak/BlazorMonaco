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
        AutoClosingDelete = 6,
        AutoClosingOvertype = 7,
        AutoClosingQuotes = 8,
        AutoIndent = 9,
        AutomaticLayout = 10,
        AutoSurround = 11,
        BracketPairColorization = 12,
        Guides = 13,
        CodeLens = 14,
        CodeLensFontFamily = 15,
        CodeLensFontSize = 16,
        ColorDecorators = 17,
        ColumnSelection = 18,
        Comments = 19,
        Contextmenu = 20,
        CopyWithSyntaxHighlighting = 21,
        CursorBlinking = 22,
        CursorSmoothCaretAnimation = 23,
        CursorStyle = 24,
        CursorSurroundingLines = 25,
        CursorSurroundingLinesStyle = 26,
        CursorWidth = 27,
        DisableLayerHinting = 28,
        DisableMonospaceOptimizations = 29,
        DomReadOnly = 30,
        DragAndDrop = 31,
        EmptySelectionClipboard = 32,
        ExtraEditorClassName = 33,
        FastScrollSensitivity = 34,
        Find = 35,
        FixedOverflowWidgets = 36,
        Folding = 37,
        FoldingStrategy = 38,
        FoldingHighlight = 39,
        FoldingImportsByDefault = 40,
        FoldingMaximumRegions = 41,
        UnfoldOnClickAfterEndOfLine = 42,
        FontFamily = 43,
        FontInfo = 44,
        FontLigatures = 45,
        FontSize = 46,
        FontWeight = 47,
        FormatOnPaste = 48,
        FormatOnType = 49,
        GlyphMargin = 50,
        GotoLocation = 51,
        HideCursorInOverviewRuler = 52,
        Hover = 53,
        InDiffEditor = 54,
        InlineSuggest = 55,
        LetterSpacing = 56,
        Lightbulb = 57,
        LineDecorationsWidth = 58,
        LineHeight = 59,
        LineNumbers = 60,
        LineNumbersMinChars = 61,
        LinkedEditing = 62,
        Links = 63,
        MatchBrackets = 64,
        Minimap = 65,
        MouseStyle = 66,
        MouseWheelScrollSensitivity = 67,
        MouseWheelZoom = 68,
        MultiCursorMergeOverlapping = 69,
        MultiCursorModifier = 70,
        MultiCursorPaste = 71,
        OccurrencesHighlight = 72,
        OverviewRulerBorder = 73,
        OverviewRulerLanes = 74,
        Padding = 75,
        ParameterHints = 76,
        PeekWidgetDefaultFocus = 77,
        DefinitionLinkOpensInPeek = 78,
        QuickSuggestions = 79,
        QuickSuggestionsDelay = 80,
        ReadOnly = 81,
        RenameOnType = 82,
        RenderControlCharacters = 83,
        RenderFinalNewline = 84,
        RenderLineHighlight = 85,
        RenderLineHighlightOnlyWhenFocus = 86,
        RenderValidationDecorations = 87,
        RenderWhitespace = 88,
        RevealHorizontalRightPadding = 89,
        RoundedSelection = 90,
        Sulers = 91,
        Scrollbar = 92,
        ScrollBeyondLastColumn = 93,
        ScrollBeyondLastLine = 94,
        ScrollPredominantAxis = 95,
        SelectionClipboard = 96,
        SelectionHighlight = 97,
        SelectOnLineNumbers = 98,
        ShowFoldingControls = 99,
        ShowUnused = 100,
        SnippetSuggestions = 101,
        SmartSelect = 102,
        SmoothScrolling = 103,
        StickyTabStops = 104,
        StopRenderingLineAfter = 105,
        Suggest = 106,
        SuggestFontSize = 107,
        SuggestLineHeight = 108,
        SuggestOnTriggerCharacters = 109,
        SuggestSelection = 110,
        SabCompletion = 111,
        TabIndex = 112,
        UnicodeHighlighting = 113,
        UnusualLineTerminators = 114,
        UseShadowDOM = 115,
        UseTabStops = 116,
        WordSeparators = 117,
        WordWrap = 118,
        WordWrapBreakAfterCharacters = 119,
        WordWrapBreakBeforeCharacters = 120,
        WordWrapColumn = 121,
        WordWrapOverride1 = 122,
        WordWrapOverride2 = 123,
        WrappingIndent = 124,
        WrappingStrategy = 125,
        ShowDeprecated = 126,
        InlayHints = 127,
        EditorClassName = 128,
        PixelRatio = 129,
        TabFocusMode = 130,
        LayoutInfo = 131,
        WrappingInfo = 132
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
        DependsOnKbLayout = -1,
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
        Digit0 = 21,
        Digit1 = 22,
        Digit2 = 23,
        Digit3 = 24,
        Digit4 = 25,
        Digit5 = 26,
        Digit6 = 27,
        Digit7 = 28,
        Digit8 = 29,
        Digit9 = 30,
        KeyA = 31,
        KeyB = 32,
        KeyC = 33,
        KeyD = 34,
        KeyE = 35,
        KeyF = 36,
        KeyG = 37,
        KeyH = 38,
        KeyI = 39,
        KeyJ = 40,
        KeyK = 41,
        KeyL = 42,
        KeyM = 43,
        KeyN = 44,
        KeyO = 45,
        KeyP = 46,
        KeyQ = 47,
        KeyR = 48,
        KeyS = 49,
        KeyT = 50,
        KeyU = 51,
        KeyV = 52,
        KeyW = 53,
        KeyX = 54,
        KeyY = 55,
        KeyZ = 56,
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
         Semicolon = 80,
        /**
         * For any country/region, the '+' key
         * For the US standard keyboard, the '=+' key
         */
        Equal = 81,
        /**
         * For any country/region, the ',' key
         * For the US standard keyboard, the ',<' key
         */
        Comma = 82,
        /**
         * For any country/region, the '-' key
         * For the US standard keyboard, the '-_' key
         */
        Minus = 83,
        /**
         * For any country/region, the '.' key
         * For the US standard keyboard, the '.>' key
         */
        Period = 84,
        /**
         * Used for miscellaneous characters; it can vary by keyboard.
         * For the US standard keyboard, the '/?' key
         */
        Slash = 85,
        /**
         * Used for miscellaneous characters; it can vary by keyboard.
         * For the US standard keyboard, the '`~' key
         */
        Backquote = 86,
        /**
         * Used for miscellaneous characters; it can vary by keyboard.
         * For the US standard keyboard, the '[{' key
         */
        BracketLeft = 87,
        /**
         * Used for miscellaneous characters; it can vary by keyboard.
         * For the US standard keyboard, the '\|' key
         */
        Backslash = 88,
        /**
         * Used for miscellaneous characters; it can vary by keyboard.
         * For the US standard keyboard, the ']}' key
         */
        BracketRight = 89,
        /**
         * Used for miscellaneous characters; it can vary by keyboard.
         * For the US standard keyboard, the ''"' key
         */
        Quote = 90,
        /**
         * Used for miscellaneous characters; it can vary by keyboard.
         */
        OEM_8 = 91,
        /**
         * Either the angle bracket key or the backslash key on the RT 102-key keyboard.
         */
        IntlBackslash = 92,
        Numpad0 = 93,
        Numpad1 = 94,
        Numpad2 = 95,
        Numpad3 = 96,
        Numpad4 = 97,
        Numpad5 = 98,
        Numpad6 = 99,
        Numpad7 = 100,
        Numpad8 = 101,
        Numpad9 = 102,
        NumpadMultiply = 103,
        NumpadAdd = 104,
        NUMPAD_SEPARATOR = 105,
        NumpadSubtract = 106,
        NumpadDecimal = 107,
        NumpadDivide = 108,
        /**
         * Cover all key codes when IME is processing input.
         */
        KEY_IN_COMPOSITION = 109,
        ABNT_C1 = 110,
        ABNT_C2 = 111,
        AudioVolumeMute = 112,
        AudioVolumeUp = 113,
        AudioVolumeDown = 114,
        BrowserSearch = 115,
        BrowserHome = 116,
        BrowserBack = 117,
        BrowserForward = 118,
        MediaTrackNext = 119,
        MediaTrackPrevious = 120,
        MediaStop = 121,
        MediaPlayPause = 122,
        LaunchMediaPlayer = 123,
        LaunchMail = 124,
        LaunchApp2 = 125,
        /**
         * Placed last to cover the length of the enum.
         * Please do not depend on this value!
         */
        MAX_VALUE = 126
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
