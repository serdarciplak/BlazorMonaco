import { initializeMonacoEditor } from './monaco-editor.js'
import { setYamlDiagnosticsOptions } from './monaco-yaml.js'

initializeMonacoEditor();

export function SetYamlDiagnosticsOptions(schemaUriStr) {
    setYamlDiagnosticsOptions(schemaUriStr);
}