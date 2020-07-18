using System;

namespace BlazorMonaco.Bridge
{
    public class TextModel
    {
        public string Id { get; set; }
        public string Uri { get; set; }
    }

    public class DiffEditorModel
    {
        public TextModel Original { get; set; }
        public TextModel Modified { get; set; }
    }
}
