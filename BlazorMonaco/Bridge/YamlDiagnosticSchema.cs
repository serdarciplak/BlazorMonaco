namespace BlazorMonaco
{
    public class YamlDiagnosticSchema
    {
        public YamlDiagnosticSchema(string uri, params string[] fileMatch)
        {
            Uri = uri;
            FileMatch = fileMatch;
        }

        public string Uri { get; set; }

        public string[] FileMatch { get; set; } 
    }
}
