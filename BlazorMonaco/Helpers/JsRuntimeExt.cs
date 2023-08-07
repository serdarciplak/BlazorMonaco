using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace BlazorMonaco.Helpers
{
    internal static class JsRuntimeExt
    {
        public static IJSRuntime Shared { get; set; }

        public static async Task SafeInvokeAsync(this IJSRuntime jsRuntime, string identifier, params object[] args)
        {
            if (jsRuntime == null)
                return;
            await jsRuntime.InvokeVoidAsync(identifier, args);
        }

        public static async Task<T> SafeInvokeAsync<T>(this IJSRuntime jsRuntime, string identifier, params object[] args)
        {
            if (jsRuntime == null)
                return default;
            return await jsRuntime.InvokeAsync<T>(identifier, args);
        }
    }
}
