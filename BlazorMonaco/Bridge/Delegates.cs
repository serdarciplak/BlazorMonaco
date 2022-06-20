using System.Threading.Tasks;

namespace BlazorMonaco
{
    public delegate CodeAction[] CodeActionProviderFunc(string uri, Range range, Marker[] markers, string kind);

    public delegate Task<CodeAction[]> CodeActionProviderAsyncFunc(string uri, Range range, Marker[] markers, string kind);

    public delegate CompletionItem[] CompletionItemProviderFunc(string uri, Position position);

    public delegate Task<CompletionItem[]> CompletionItemProviderAsyncFunc(string uri, Position position);
}
