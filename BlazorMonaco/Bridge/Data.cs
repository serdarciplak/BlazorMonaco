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

    public class Marker
    {
        public object Code { get; set; }

        public string Message { get; set; }

        public MarkerSeverity Severity { get; set; }

        public int StartLineNumber { get; set; }

        public int StartColumn { get; set; }

        public int EndLineNumber { get; set; }

        public int EndColumn { get; set; }

        public string Source { get; set; }

        public MarkerTag[] Tags { get; set; } = Array.Empty<MarkerTag>();
    }

    public class MarkerCode
    {
        public Uri Target { get; set; }

        public string Value { get; set; }
    }

    public class CodeAction
    {
        public string Title { get; set; }

        public string Kind { get; set; }

        public Marker[] Diagnostics { get; set; } = Array.Empty<Marker>();

        public WorkspaceEdit Edit { get; } = new WorkspaceEdit();

        public bool IsPreferred { get; set; }
    }

    public class WorkspaceEdit
    {
        public WorkspaceTextEdit[] Edits { get; set; } = Array.Empty<WorkspaceTextEdit>();
    }

    public class WorkspaceTextEdit
    {
        public string Resource { get; set; }

        public TextEdit Edit { get; } = new TextEdit();
    }

    public class TextEdit
    {
        public string Text { get; set; }

        public Range Range { get; set; }

        public EndOfLineSequence? Eol { get; set; }
    }

    public class CompletionItem
    {
        public string Label { get; set; }

        public Range Range { get; set; }

        public string Detail { get; set; }

        public CompletionItemKind Kind { get; set; }

        public string InsertText { get; set; }

        public bool Preselect { get; set; }
    }
}
