using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BlazorMonaco.Helpers
{
    internal class ListJsonConverter<T, TConverter> : JsonConverter<List<T>>
        where TConverter : JsonConverter<T>, new()
    {
        private readonly JsonConverter<T> _itemConverter = new TConverter();

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
                var item = _itemConverter.Read(ref reader, typeof(T), options);
                if (item != null)
                    list.Add(item);
            }

            return list;
        }

        public override void Write(Utf8JsonWriter writer, List<T> value, JsonSerializerOptions options)
        {
            writer.WriteStartArray();

            foreach (T item in value)
                _itemConverter.Write(writer, item, options);

            writer.WriteEndArray();
        }
    }
}
