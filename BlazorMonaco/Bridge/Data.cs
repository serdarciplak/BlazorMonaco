using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorMonaco.Bridge
{
    public class Position
    {
        public int LineNumber { get; set; }
        public int Column { get; set; }
    }

    public class Range
    {
        public int StartLineNumber { get; set; }
        public int StartColumn { get; set; }
        public int EndLineNumber { get; set; }
        public int EndColumn { get; set; }
    }

    public class Selection : Range
    {
        public int SelectionStartLineNumber { get; set; }
        public int SelectionStartColumn { get; set; }
        public int PositionLineNumber { get; set; }
        public int PositionColumn { get; set; }
    }

    public class Dimension
    {
        public int Width { get; set; }
        public int Height { get; set; }
    }

    public class DecorationOptions
    {
        public string AfterContentClassName { get; set; }
        public string BeforeContentClassName { get; set; }
        public string ClassName { get; set; }
        public string GlyphMarginClassName { get; set; }
        public MarkdownString GlyphMarginHoverMessage { get; set; }
        public MarkdownString HoverMessage { get; set; }
        public string InlineClassName { get; set; }
        public bool? InlineClassNameAffectsLetterSpacing { get; set; }
        public bool? IsWholeLine { get; set; }
        public string LinesDecorationsClassName { get; set; }
        public string MarginClassName { get; set; }
        public DecorationMinimapOptions Minimap { get; set; }
        public DecorationOverviewRulerOptions OverviewRuler { get; set; }
        public TrackedRangeStickiness? Stickiness { get; set; }
        public int? ZIndex { get; set; }
    }

    public class MarkdownString
    {
        public bool? IsTrusted { get; set; }
        public bool? SupportThemeIcons { get; set; }
        public string Uris { get; set; }
        public string Value { get; set; }
    }

    public class DecorationMinimapOptions
    {
        public string Color { get; set; }
        public string DarkColor { get; set; }
        public MinimapPosition? Position { get; set; }
    }

    public class DecorationOverviewRulerOptions
    {
        public string Color { get; set; }
        public string DarkColor { get; set; }
        public MinimapPosition? Position { get; set; }
    }
}
