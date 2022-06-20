using System;
using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace BlazorMonaco
{
    internal class CodeActionProvider
    {
        private readonly CodeActionProviderFunc _func;
        private readonly CodeActionProviderAsyncFunc _asyncFunc;

        public CodeActionProvider(CodeActionProviderFunc func)
        {
            _func = func;
        }

        public CodeActionProvider(CodeActionProviderAsyncFunc func)
        {
            _asyncFunc = func;
        }

        [JSInvokable]
        public async Task<object> Invoke(string uri, Range range, Marker[] markers, string only)
        {
            return new CodeActionResult
            {
                Actions = _asyncFunc == null ? _func(uri, range, markers, only) : await _asyncFunc(uri, range, markers, only)
            };
        }

        private class CodeActionResult
        {
            public CodeAction[] Actions { get; set; } = Array.Empty<CodeAction>();
        }
    }

    internal class CompletionItemProvider
    {
        private readonly CompletionItemProviderFunc _func;
        private readonly CompletionItemProviderAsyncFunc _asyncFunc;

        public CompletionItemProvider(CompletionItemProviderFunc func)
        {
            _func = func;
        }

        public CompletionItemProvider(CompletionItemProviderAsyncFunc func)
        {
            _asyncFunc = func;
        }

        [JSInvokable]
        public async Task<object> Invoke(string uri, Position position)
        {
            return new CompletionItemResult
            {
                Suggestions = _asyncFunc == null ? _func(uri, position) : await _asyncFunc(uri, position)
            };
        }

        private class CompletionItemResult
        {
            public CompletionItem[] Suggestions { get; set; } = Array.Empty<CompletionItem>();
        }
    }
}
