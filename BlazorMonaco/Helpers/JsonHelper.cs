using System.Text.Json;
using System.Text.Json.Serialization;
#if NET8_0_OR_GREATER
using System.Collections.Generic;
using System.Text.Json.Serialization.Metadata;
using BlazorMonaco.Editor;
using BlazorMonaco.Languages;
#endif

namespace BlazorMonaco.Helpers
{
    internal static class JsonHelper
    {
        /// <summary>
        /// Options used to serialize objects passed to js. Properties with null values are omitted.
        /// </summary>
        public static readonly JsonSerializerOptions SerializeOptions = CreateOptions(ignoreNullValues: true);

        /// <summary>
        /// Options used to deserialize json received from js.
        /// </summary>
        public static readonly JsonSerializerOptions DeserializeOptions = CreateOptions(ignoreNullValues: false);

        private static JsonSerializerOptions CreateOptions(bool ignoreNullValues)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
#if NET8_0_OR_GREATER
                TypeInfoResolver = BlazorMonacoJsonContext.Default,
#endif
            };
            if (ignoreNullValues)
            {
#if !NET6_0_OR_GREATER
                options.IgnoreNullValues = true;
#else
                options.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
#endif
            }
            return options;
        }

        public static T Deserialize<T>(string json)
        {
#if NET8_0_OR_GREATER
            return JsonSerializer.Deserialize(json, GetTypeInfo<T>(DeserializeOptions));
#else
            return JsonSerializer.Deserialize<T>(json, DeserializeOptions);
#endif
        }

        public static byte[] SerializeToUtf8Bytes<T>(T value)
        {
#if NET8_0_OR_GREATER
            return JsonSerializer.SerializeToUtf8Bytes(value, GetTypeInfo<T>(SerializeOptions));
#else
            return JsonSerializer.SerializeToUtf8Bytes(value, SerializeOptions);
#endif
        }

#if NET8_0_OR_GREATER
        private static JsonTypeInfo<T> GetTypeInfo<T>(JsonSerializerOptions options)
            => (JsonTypeInfo<T>)options.GetTypeInfo(typeof(T));
#endif
    }

#if NET8_0_OR_GREATER
    /// <summary>
    /// Source generated serialization metadata for the types serialized by BlazorMonaco.
    /// Registering a type here also preserves its members when the app is trimmed,
    /// which is required for the types Blazor's JS interop serializes via reflection.
    /// </summary>
    [JsonSourceGenerationOptions(GenerationMode = JsonSourceGenerationMode.Metadata)]
    // Primitives & collections
    [JsonSerializable(typeof(string))]
    [JsonSerializable(typeof(string[]))]
    [JsonSerializable(typeof(int[]))]
    [JsonSerializable(typeof(List<bool>))]
    [JsonSerializable(typeof(List<string>))]
    // BlazorMonaco
    [JsonSerializable(typeof(KeyboardEvent))]
    [JsonSerializable(typeof(MarkdownString))]
    [JsonSerializable(typeof(Position))]
    [JsonSerializable(typeof(Range))]
    [JsonSerializable(typeof(List<Range>))]
    [JsonSerializable(typeof(Selection))]
    [JsonSerializable(typeof(List<Selection>))]
    // BlazorMonaco.Editor
    [JsonSerializable(typeof(ActionDescriptor))]
    [JsonSerializable(typeof(BaseMouseTarget))]
    [JsonSerializable(typeof(ColorizerElementOptions))]
    [JsonSerializable(typeof(ColorizerOptions))]
    [JsonSerializable(typeof(ContentSizeChangedEvent))]
    [JsonSerializable(typeof(CursorPositionChangedEvent))]
    [JsonSerializable(typeof(CursorSelectionChangedEvent))]
    [JsonSerializable(typeof(DiffEditorModel))]
    [JsonSerializable(typeof(DiffEditorOptions))]
    [JsonSerializable(typeof(Dimension))]
    [JsonSerializable(typeof(EditorLayoutInfo))]
    [JsonSerializable(typeof(EditorMouseEvent))]
    [JsonSerializable(typeof(EditorOptions))]
    [JsonSerializable(typeof(EditorUpdateOptions))]
    [JsonSerializable(typeof(EndOfLinePreference))]
    [JsonSerializable(typeof(EndOfLineSequence))]
    [JsonSerializable(typeof(FindMatch))]
    [JsonSerializable(typeof(List<FindMatch>))]
    [JsonSerializable(typeof(BlazorMonaco.Editor.Global.GetModelMarkersFilter))]
    [JsonSerializable(typeof(List<IdentifiedSingleEditOperation>))]
    [JsonSerializable(typeof(List<Marker>))]
    [JsonSerializable(typeof(MarkerCode))]
    [JsonSerializable(typeof(List<MarkerData>))]
    [JsonSerializable(typeof(ModelChangedEvent))]
    [JsonSerializable(typeof(ModelContentChangedEvent))]
    [JsonSerializable(typeof(ModelDecoration))]
    [JsonSerializable(typeof(ModelDecoration[]))]
    [JsonSerializable(typeof(List<ModelDecoration>))]
    [JsonSerializable(typeof(ModelDecorationOptions))]
    [JsonSerializable(typeof(ModelDecorationsChangedEvent))]
    [JsonSerializable(typeof(ModelDeltaDecoration[]))]
    [JsonSerializable(typeof(List<ModelDeltaDecoration>))]
    [JsonSerializable(typeof(ModelLanguageChangedEvent))]
    [JsonSerializable(typeof(ModelLanguageConfigurationChangedEvent))]
    [JsonSerializable(typeof(ModelOptionsChangedEvent))]
    [JsonSerializable(typeof(NewScrollPosition))]
    [JsonSerializable(typeof(PartialEditorMouseEvent))]
    [JsonSerializable(typeof(PasteEvent))]
    [JsonSerializable(typeof(ScrolledVisiblePosition))]
    [JsonSerializable(typeof(ScrollEvent))]
    [JsonSerializable(typeof(ScrollType))]
    [JsonSerializable(typeof(StandaloneDiffEditorConstructionOptions))]
    [JsonSerializable(typeof(StandaloneEditorConstructionOptions))]
    [JsonSerializable(typeof(StandaloneThemeData))]
    [JsonSerializable(typeof(TextModel))]
    [JsonSerializable(typeof(List<TextModel>))]
    [JsonSerializable(typeof(TextModelResolvedOptions))]
    [JsonSerializable(typeof(TextModelUpdateOptions))]
    [JsonSerializable(typeof(List<ValidEditOperation>))]
    [JsonSerializable(typeof(WordAtPosition))]
    // BlazorMonaco.Languages
    [JsonSerializable(typeof(CodeAction))]
    [JsonSerializable(typeof(CodeActionContext))]
    [JsonSerializable(typeof(CodeActionList))]
    [JsonSerializable(typeof(CodeActionProviderMetadata))]
    [JsonSerializable(typeof(CompletionContext))]
    [JsonSerializable(typeof(CompletionItem))]
    [JsonSerializable(typeof(CompletionItemLabel))]
    [JsonSerializable(typeof(CompletionItemRanges))]
    [JsonSerializable(typeof(CompletionList))]
    [JsonSerializable(typeof(CustomEdit))]
    [JsonSerializable(typeof(FormattingOptions))]
    [JsonSerializable(typeof(Hover))]
    [JsonSerializable(typeof(HoverContext))]
    [JsonSerializable(typeof(LanguageSelector))]
    [JsonSerializable(typeof(TextEdit[]))]
    [JsonSerializable(typeof(WorkspaceFileEdit))]
    [JsonSerializable(typeof(WorkspaceTextEdit))]
    internal partial class BlazorMonacoJsonContext : JsonSerializerContext
    {
    }
#endif
}
