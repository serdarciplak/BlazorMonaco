using Microsoft.JSInterop;
#if NET5_0_OR_GREATER
using System.Diagnostics.CodeAnalysis;
#endif
using System.Threading.Tasks;

namespace BlazorMonaco.Helpers
{
    internal static class JsRuntimeExt
    {
#if NET5_0_OR_GREATER
        // Same as the annotation on IJSRuntime.InvokeAsync<TValue>
        private const DynamicallyAccessedMemberTypes JsonSerialized = DynamicallyAccessedMemberTypes.PublicConstructors | DynamicallyAccessedMemberTypes.PublicFields | DynamicallyAccessedMemberTypes.PublicProperties;

#endif
        private static IJSRuntime Shared { get; set; }

        public static IJSRuntime UpdateRuntime(IJSRuntime jsRuntime)
        {
            if (Shared is IJSInProcessRuntime || jsRuntime is IJSInProcessRuntime)
            {
                // Allow jsRuntime to be null in WASM apps. If it's null, use the static instance instead.
                return Shared = jsRuntime ?? Shared;
            }
            else
            {
                // The static instance is not used in server-side apps. jsRuntime has to be non-null there.
                return jsRuntime;
            }
        }

        public static async Task SafeInvokeAsync(this IJSRuntime jsRuntime, string identifier, params object[] args)
        {
            if (jsRuntime == null)
                return;
            await jsRuntime.InvokeVoidAsync(identifier, args);
        }

#if NET5_0_OR_GREATER
        public static async Task<T> SafeInvokeAsync<[DynamicallyAccessedMembers(JsonSerialized)] T>(this IJSRuntime jsRuntime, string identifier, params object[] args)
#else
        public static async Task<T> SafeInvokeAsync<T>(this IJSRuntime jsRuntime, string identifier, params object[] args)
#endif
        {
            if (jsRuntime == null)
                return default;
            return await jsRuntime.InvokeAsync<T>(identifier, args);
        }
    }
}
