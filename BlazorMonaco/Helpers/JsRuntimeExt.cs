using BlazorMonaco.Services;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BlazorMonaco.Helpers
{
    internal static class JsRuntimeExt
    {
        public static BlazorMonacoJsRuntime Shared { get; set; }
    }
}
