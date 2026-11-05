using System.Text.Json;

namespace BlazorMonaco.Helpers
{
    internal static class JsonElementExt
    {
        public static string AsString(this JsonElement jsonElement)
        {
            return jsonElement.ValueKind == JsonValueKind.String ? jsonElement.GetString() : null;
        }

        public static T AsObject<T>(this JsonElement jsonElement)
            where T : class
        {
            return jsonElement.ValueKind == JsonValueKind.Object ? JsonHelper.Deserialize<T>(jsonElement.GetRawText()) : null;
        }

        public static JsonElement? FromObject<T>(T obj) where T : class
        {
            try
            {
                if (obj == null)
                    return null;

                var bytes = JsonHelper.SerializeToUtf8Bytes(obj);
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
