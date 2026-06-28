using System.Text.Json;
using System.Text.Json.Serialization;

namespace BlazorMonaco.Helpers
{
    internal static class JsonElementExt
    {
        private static JsonSerializerOptions DefaultOptions => new JsonSerializerOptions
        {
#if !NET6_0_OR_GREATER
            IgnoreNullValues = true,
#else
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
#endif
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        public static string AsString(this JsonElement jsonElement)
        {
            return jsonElement.ValueKind == JsonValueKind.String ? jsonElement.GetString() : null;
        }

        public static T AsObject<T>(this JsonElement jsonElement)
            where T : class
        {
            return jsonElement.ValueKind == JsonValueKind.Object ? JsonSerializer.Deserialize<T>(jsonElement.GetRawText()) : null;
        }

        public static JsonElement? FromObject<T>(T obj) where T : class
        {
            try
            {
                if (obj == null)
                    return null;

                var bytes = JsonSerializer.SerializeToUtf8Bytes(obj, DefaultOptions);
                using (var doc = JsonDocument.Parse(bytes))
                {
                    // Clones the element backing buffer safely for disposal
                    return doc.RootElement.Clone();
                }
            }
            catch
            {
                return null;
            }
        }
    }
}
