namespace BlazorMonaco.Bridge
{
    public class DiffEditorData
    {
        public string Language { get; set; } = "javascript";
        public TextModel OriginalTextModel { get; set; }
        public TextModel ModifiedTextModel { get; set; }
        public string OriginalValue { get; set; } = string.Empty;
        public string ModifiedValue { get; set; } = string.Empty;
        public string OriginalUri { get; set; } = string.Empty;
        public string ModifiedUri { get; set; } = string.Empty;
    }
}
