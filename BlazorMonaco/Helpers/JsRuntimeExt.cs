using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BlazorMonaco.Helpers
{
    internal static class JsRuntimeExt
    {
        public static IJSRuntime Shared;
        public static void SetJSRuntime(IJSRuntime _jsRuntime)
        {
            Shared = _jsRuntime;
        }

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
