# Migration Guide

## Migrating from ***v2.x*** to ***v3.x***

- Edit your `index.html`

    - To have a cleaner file, you can remove the stylesheet link to Monaco Editor's css file (`editor.main.css`) in your html head. The new Monaco Editor version will automatically add it if it's not there.

    - The script tags you need to add to your html body are now cleaner. You need to update them.

  ```html
  <script src="_content/BlazorMonaco/jsInterop.js"></script>
  <script src="_content/BlazorMonaco/lib/monaco-editor/min/vs/loader.js"></script>
  <script src="_content/BlazorMonaco/lib/monaco-editor/min/vs/editor/editor.main.js"></script>
  ```

- Everything resides in two namespaces now. You need to update your using directives in your root `_Imports.razor` file or any other places you use them.

  ```
  @using BlazorMonaco
  @using BlazorMonaco.Editor
  ```

- Class, property and object names are now better aligned with the original Monaco Editor JavaScript library. You need to update your usages with the new names. You'll notice right away that `MonacoEditor` is renamed as `StandaloneCodeEditor` and `MonacoDiffEditor` is renamed as `StandaloneDiffEditor`. Based on what you use, you'll notice more changes. Just check the namespace and class definitions. If Microsoft did not remove that thing in the new Monaco Editor version, it'll be there with a new name similar to the old one.

- Static methods in the `MonacoEditor` and `MonacoEditorBase` classes (e.g. `SetTheme`, `Colorize`, etc) are removed from the editor class and grouped together under a new class named `BlazorMonaco.Editor.Global`.

- Event callback methods used to have a parameter for the source editor instance that raised the event. It's removed to follow the original Monaco Editor implementation.

- The embedded Monaco Editor JavaScript library is updated from `v0.22.3` to `v0.34.1`, and Microsoft has removed some things or changed the way they work. If you happen to use those things, please follow Microsoft's [suggestions](https://github.com/microsoft/monaco-editor) on how to update your usage.

## Migrating from ***v1.x*** to ***v2.x***

- Merged the `BlazorMonaco.Bridge` namespace into the `BlazorMonaco` namespace
- Renamed the `CommentOptions` class as `EditorCommentsOptions`
- Renamed the `FindOptions` class as `EditorFindOptions`
- Renamed the `HoverOptions` class as `EditorHoverOptions`
- Renamed the `LightbulbOptions` class as `EditorLightbulbOptions`
- Renamed the `MinimapOptions` class as `EditorMinimapOptions`
- Renamed the `ParameterHintOptions` class as `EditorParameterHintOptions`
- Renamed the `ScrollbarOptions` class as `EditorScrollbarOptions`
- Restructured `EditorLayoutInfo`
- Removed some options and properties that are removed from Monaco Editor itself
