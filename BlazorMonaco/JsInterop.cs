using BlazorMonaco.Bridge;
using Microsoft.JSInterop;
using System.Text.Json;
using System.Threading.Tasks;

namespace BlazorMonaco
{
    public static class JsInterop
    {
        #region Static Methods

        public static ValueTask ColorizeElement(this IJSRuntime jsRuntime, string id)
            => jsRuntime.InvokeVoidAsync("blazorMonaco.editor.colorizeElement", id);

        public static ValueTask Create(this IJSRuntime jsRuntime, string id, EditorOptions options)
        {
            // Convert the options object into a dictionary to get rid of the properties with null values
            var optionsJson = JsonSerializer.Serialize(options, new JsonSerializerOptions
            {
                IgnoreNullValues = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
            var optionsDict = JsonSerializer.Deserialize<JsonElement>(optionsJson);
            return jsRuntime.InvokeVoidAsync("blazorMonaco.editor.create", id, optionsDict);
        }

        public static ValueTask SetTheme(this IJSRuntime jsRuntime, string theme)
            => jsRuntime.InvokeVoidAsync("blazorMonaco.editor.setTheme", theme);

        #endregion

        #region Instance Methods

        public static ValueTask AddAction(this IJSRuntime jsRuntime, string id, string actionId, string label, int[] keybindings, string precondition, string keybindingContext, string contextMenuGroupId, double contextMenuOrder, DotNetObjectReference<MonacoEditor> objectReference)
            => jsRuntime.InvokeVoidAsync("blazorMonaco.editor.addAction", id, actionId, label, keybindings, precondition, keybindingContext, contextMenuGroupId, contextMenuOrder, objectReference);

        public static ValueTask AddCommand(this IJSRuntime jsRuntime, string id, int keyCode, DotNetObjectReference<MonacoEditor> objectReference)
            => jsRuntime.InvokeVoidAsync("blazorMonaco.editor.addCommand", id, keyCode, objectReference);

        public static ValueTask<string> GetValue(this IJSRuntime jsRuntime, string id)
            => jsRuntime.InvokeAsync<string>("blazorMonaco.editor.getValue", id);

        public static ValueTask SetValue(this IJSRuntime jsRuntime, string id, string value)
            => jsRuntime.InvokeVoidAsync("blazorMonaco.editor.setValue", id, value);

        public static ValueTask UpdateOptions(this IJSRuntime jsRuntime, string id, EditorOptions options)
        {
            // Convert the options object into a dictionary to get rid of the properties with null values
            var optionsJson = JsonSerializer.Serialize(options, new JsonSerializerOptions
            {
                IgnoreNullValues = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
            var optionsDict = JsonSerializer.Deserialize<JsonElement>(optionsJson);
            return jsRuntime.InvokeVoidAsync("blazorMonaco.editor.updateOptions", id, optionsDict);
        }

        #endregion
    }
}
