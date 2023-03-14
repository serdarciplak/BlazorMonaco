using Microsoft.JSInterop;
using System;
using System.Threading.Tasks;

namespace BlazorMonaco.Services 
{ 
    /// <summary>
    /// The service use to load the ESM version of BlazorMonaco
    /// </summary>
    public class BlazorMonacoJsRuntime
        : IAsyncDisposable
    {
        /// <summary>
        /// A reference to the js interop module
        /// </summary>
        private readonly Lazy<Task<IJSObjectReference>> moduleTask;


        /// <summary>
        /// Constructs a new <see cref="BlazorMonacoJsRuntime"/>
        /// </summary>
        /// <param name="jsRuntime">The service used to interop with JS</param>
        public BlazorMonacoJsRuntime(IJSRuntime jsRuntime)
        {
            moduleTask = new(() => jsRuntime.InvokeAsync<IJSObjectReference>("import", "./_content/BlazorMonaco/blazor-monaco-interop.js").AsTask());
        }


        public async Task SafeInvokeAsync(string identifier, params object[] args)
        {
            var module = await this.moduleTask.Value;
            if (module == null)
                return;
            await module.InvokeVoidAsync(identifier, args);
        }

        public async Task<T> SafeInvokeAsync<T>(string identifier, params object[] args)
        {
            var module = await this.moduleTask.Value;
            if (module == null)
                return default;
            return await module.InvokeAsync<T>(identifier, args);
        }

        /// <inheritdoc />
        public async ValueTask DisposeAsync()
        {
            if (moduleTask.IsValueCreated)
            {
                var module = await moduleTask.Value;
                await module.DisposeAsync();
            }
        }
    }
}