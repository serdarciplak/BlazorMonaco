using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorMonaco.Bridge
{
    public class EditorLayoutInfo
    {
        public double Width { get; set; }
        public double Height { get; set; }
        public double GlyphMarginLeft { get; set; }
        public double GlyphMarginWidth { get; set; }
        public double LineNumbersLeft { get; set; }
        public double LineNumbersWidth { get; set; }
        public double DecorationsLeft { get; set; }
        public double DecorationsWidth { get; set; }
        public double ContentLeft { get; set; }
        public double ContentWidth { get; set; }
        public double MinimapLeft { get; set; }
        public double MinimapWidth { get; set; }
        public RenderMinimap RenderMinimap { get; set; }
        public double ViewportColumn { get; set; }
        public double VerticalScrollbarWidth { get; set; }
        public double HorizontalScrollbarHeight { get; set; }
        public OverviewRulerPosition OverviewRuler { get; set; }
    }

    public class OverviewRulerPosition
    {
        public double Width { get; set; }
        public double Height { get; set; }
        public double Top { get; set; }
        public double Right { get; set; }
    }

    public enum RenderMinimap
    {
        None = 0,
        Text = 1,
        Blocks = 2
    }
}
