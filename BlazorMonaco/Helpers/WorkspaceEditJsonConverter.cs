using BlazorMonaco.Languages;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BlazorMonaco.Helpers
{
    internal class WorkspaceEditJsonConverter : JsonConverter<IWorkspaceEdit>
    {
        public override IWorkspaceEdit Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var readerClone = reader;
            if (readerClone.TokenType != JsonTokenType.StartObject)
            {
                reader.Skip();
                return null;
            }

            var type = typeof(CustomEdit);
            while (readerClone.Read() && readerClone.TokenType != JsonTokenType.EndObject)
            {
                if (readerClone.TokenType != JsonTokenType.PropertyName)
                    continue;

                var propertyName = readerClone.GetString();
                switch (propertyName)
                {
                    case "textEdit": type = typeof(WorkspaceTextEdit); break;
                    case "options": type = typeof(WorkspaceFileEdit); break;
                }
            }

            var deserialized = JsonSerializer.Deserialize(ref reader, type, options);
            return (IWorkspaceEdit)deserialized;
        }

        public override void Write(Utf8JsonWriter writer, IWorkspaceEdit value, JsonSerializerOptions options)
        {
            if (value == null)
                writer.WriteNullValue();

            var type = value?.GetType();
            using (var jsonDocument = JsonDocument.Parse(JsonSerializer.Serialize(value, type, options)))
            {
                writer.WriteStartObject();
                foreach (var element in jsonDocument.RootElement.EnumerateObject())
                    element.WriteTo(writer);
                writer.WriteEndObject();
            }
        }
    }
}
