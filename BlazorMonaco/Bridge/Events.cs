using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace BlazorMonaco
{
    public class ContentSizeChangedEvent
    {
        public double ContentWidth { get; set; }
        public double ContentHeight { get; set; }
        public bool ContentWidthChanged { get; set; }
        public bool ContentHeightChanged { get; set; }
    }

    public class CursorPositionChangedEvent
    {
        public Position Position { get; set; }
        public List<Position> SecondaryPositions { get; set; }
        public CursorChangeReason Reason { get; set; }
        public string Source { get; set; }
    }

    public class CursorSelectionChangedEvent
    {
        public Selection Selection { get; set; }
        public List<Selection> SecondarySelections { get; set; }
        public int ModelVersionId { get; set; }
        public List<Selection> OldSelections { get; set; }
        public int OldModelVersionId { get; set; }
        public string Source { get; set; }
        public CursorChangeReason Reason { get; set; }
    }

    public class KeyboardEvent
    {
        //readonly _standardKeyboardEventBrand: true;
        public JsonElement? BrowserEvent { get; set; }
        public JsonElement? Target { get; set; }
        public bool CtrlKey { get; set; }
        public bool ShiftKey { get; set; }
        public bool AltKey { get; set; }
        public bool MetaKey { get; set; }
        public KeyCode KeyCode { get; set; }
        public string Code { get; set; }
    }

    public class ModelContentChange
    {
        public Range Range { get; set; }
        public int RangeOffset { get; set; }
        public int RangeLength { get; set; }
        public string Text { get; set; }
    }

    public class ModelChangedEvent
    {
        public string OldModelUri { get; set; }
        public string NewModelUri { get; set; }
    }

    public class ModelContentChangedEvent
    {
        public List<ModelContentChange> Changes { get; set; }
        public string Eol { get; set; }
        public int VersionId { get; set; }
        public bool IsUndoing { get; set; }
        public bool IsRedoing { get; set; }
        public bool IsFlush { get; set; }
    }

    public class ModelDecorationsChangedEvent
    {
        public bool AffectsMinimap { get; set; }
        public bool AffectsOverviewRuler { get; set; }
    }

    public class ModelLanguageChangedEvent
    {
        public string OldLanguage { get; set; }
        public string NewLanguage { get; set; }
    }

    public class ModelLanguageConfigurationChangedEvent
    {
    }

    public class ModelOptionsChangedEvent
    {
        public bool TabSize { get; set; }
        public bool IndentSize { get; set; }
        public bool InsertSpaces { get; set; }
        public bool TrimAutoWhitespace { get; set; }
    }

    public class EditorMouseEvent
    {
        public MouseEvent Event { get; set; }
        public MouseTarget Target { get; set; }
    }

    public class MouseEvent
    {
        public JsonElement? BrowserEvent { get; set; }
        public bool LeftButton { get; set; }
        public bool MiddleButton { get; set; }
        public bool RightButton { get; set; }
        public int Buttons { get; set; }
        public JsonElement? Target { get; set; }
        public int Detail { get; set; }
        public double Posx { get; set; }
        public double Posy { get; set; }
        public bool CtrlKey { get; set; }
        public bool ShiftKey { get; set; }
        public bool AltKey { get; set; }
        public bool MetaKey { get; set; }
        public long Timestamp { get; set; }
    }

    public class MouseTarget
    {
        public JsonElement? Element { get; set; }
        public MouseTargetType Type { get; set; }
        public Position Position { get; set; }
        public int MouseColumn { get; set; }
        public Range Range { get; set; }
        public JsonElement? Detail { get; set; }
    }

    public class PasteEvent
    {
        public Range Range { get; set; }
        public string Mode { get; set; }
    }

    public class ScrollEvent
    {
        public double ScrollTop { get; set; }
        public double ScrollLeft { get; set; }
        public double ScrollWidth { get; set; }
        public double ScrollHeight { get; set; }
        public bool ScrollTopChanged { get; set; }
        public bool ScrollLeftChanged { get; set; }
        public bool ScrollWidthChanged { get; set; }
        public bool ScrollHeightChanged { get; set; }
    }
}
