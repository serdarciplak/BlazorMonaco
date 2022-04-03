using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorMonaco
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

        public Range() { }

        public Range(int startLineNumber, int startColumn, int endLineNumber, int endColumn)
        {
            StartLineNumber = startLineNumber;
            StartColumn = startColumn;
            EndLineNumber = endLineNumber;
            EndColumn = endColumn;
        }
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

    public class StandaloneThemeData
    {
        public string Base { get; set; }
        public bool? Inherit { get; set; }
        public List<TokenThemeRule> Rules { get; set; }
        public List<string> EncodedTokensColors { get; set; }
        public Dictionary<string, string> Colors { get; set; }
    }

    public class TokenThemeRule
    {
        public string Token { get; set; }
        public string Foreground { get; set; }
        public string Background { get; set; }
        public string FontStyle { get; set; }
    }

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
        public EditorMinimapLayoutInfo Minimap { get; set; }
        public double ViewportColumn { get; set; }
        public bool IsWordWrapMinified { get; set; }
        public bool IsViewportWrapping { get; set; }
        public double WrappingColumn { get; set; }
        public double VerticalScrollbarWidth { get; set; }
        public double HorizontalScrollbarHeight { get; set; }
        public OverviewRulerPosition OverviewRuler { get; set; }
    }

    public class EditorMinimapLayoutInfo
    {
        public RenderMinimap RenderMinimap { get; set; }
        public double MinimapLeft { get; set; }
        public double MinimapWidth { get; set; }
        public bool MinimapHeightIsEditorHeight { get; set; }
        public bool MinimapIsSampling { get; set; }
        public double MinimapScale { get; set; }
        public double MinimapLineHeight { get; set; }
        public double MinimapCanvasInnerWidth { get; set; }
        public double MinimapCanvasInnerHeight { get; set; }
        public double MinimapCanvasOuterWidth { get; set; }
        public double MinimapCanvasOuterHeight { get; set; }
    }

    public class OverviewRulerPosition
    {
        public double Width { get; set; }
        public double Height { get; set; }
        public double Top { get; set; }
        public double Right { get; set; }
    }

    public class UriComponents
    {
        public string Scheme { get; set; }
        public string Authority { get; set; }
        public string Path { get; set; }
        public string Query { get; set; }
        public string Fragment { get; set; }
    }
    

    public class InjectedTextOptions {

        public string Content { get; set; }
        public string InlineClassName { get; set; }
        public bool InlineClassNameAffectsLetterSpacing { get; set; }
        // readonly attachedData?: unknown;
        public  InjectedTextCursorStops? CursorStops { get; set; }
    }

    public enum InjectedTextCursorStops {
        Both = 0,
        Right = 1,
        Left = 2,
        None = 3
    }
}