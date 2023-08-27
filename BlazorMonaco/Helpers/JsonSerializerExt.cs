using BlazorMonaco.Languages;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BlazorMonaco.Helpers
{
    internal static partial class JsonSerializerExt
    {
        public static JsonSerializerOptions DefaultOptions => new JsonSerializerOptions
        {
#if !NET6_0_OR_GREATER
            IgnoreNullValues = true,
#else
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
#endif
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
    }

    internal static class JsonElementExt
    {
        public static JsonElement NullElement {
            get
            {
                using (var doc = JsonDocument.Parse("null"))
                {
                    return doc.RootElement;
                }
            }
        }
        
        public static string AsString(this JsonElement jsonElement)
        {
            return jsonElement.ValueKind == JsonValueKind.String ? jsonElement.GetString() : null;
        }

        public static T AsObject<T>(this JsonElement jsonElement)
            where T : class
        {
            return jsonElement.ValueKind == JsonValueKind.Object ? JsonSerializer.Deserialize<T>(jsonElement.GetRawText()) : null;
        }

        public static JsonElement FromObject<TValue>(TValue value, JsonSerializerOptions options = null)
        {
            try
            {
                if (value == null)
                    return NullElement;

                var bytes = JsonSerializer.SerializeToUtf8Bytes(value, options ?? JsonSerializerExt.DefaultOptions);
                using (var doc = JsonDocument.Parse(bytes))
                {
                    return doc.RootElement.Clone();
                }
            }
            catch
            {
                return NullElement;
            }
        }
    }

    internal class ListJsonConverter<T, TConverter> : JsonConverter<List<T>>
        where TConverter : JsonConverter<T>, new()
    {
        private readonly JsonConverter<T> itemConverter = new TConverter();

        public override List<T> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.StartArray)
            {
                reader.Skip();
                return null;
            }

            var list = new List<T>();

            while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
            {
                var item = itemConverter.Read(ref reader, typeof(T), options);
                if (item != null)
                    list.Add(item);
            }

            return list;
        }

        public override void Write(Utf8JsonWriter writer, List<T> value, JsonSerializerOptions options)
        {
            writer.WriteStartArray();

            foreach (T item in value)
                itemConverter.Write(writer, item, options);

            writer.WriteEndArray();
        }
    }

    internal class WorkspaceEditJsonConverter : JsonConverter<IWorkspaceEdit>
    {
        public override IWorkspaceEdit Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            Utf8JsonReader readerClone = reader;
            if (readerClone.TokenType != JsonTokenType.StartObject)
            {
                reader.Skip();
                return null;
            }

            var isTextEdit = false;
            while (readerClone.Read() && readerClone.TokenType != JsonTokenType.EndObject)
            {
                if (readerClone.TokenType == JsonTokenType.PropertyName)
                {
                    string propertyName = readerClone.GetString();
                    if (propertyName == "textEdit")
                        isTextEdit = true;
                }
            }

            var type = isTextEdit ? typeof(WorkspaceTextEdit) : typeof(WorkspaceFileEdit);
            var deserialized = JsonSerializer.Deserialize(ref reader, type, options);
            return (IWorkspaceEdit)deserialized;
        }

        public override void Write(Utf8JsonWriter writer, IWorkspaceEdit value, JsonSerializerOptions options)
        {
            if (value == null)
                writer.WriteNullValue();

            var type = value.GetType();
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
