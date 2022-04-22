import { setDiagnosticsOptions } from 'monaco-yaml';

export function setYamlDiagnosticsOptions(schemaUriStr) {
    window.MonacoEnvironment = {};
    window.MonacoEnvironment = {
        getWorker(moduleId, label) {
            switch (label) {
                case 'editorWorkerService':
                    return new Worker(new URL('monaco-editor/esm/vs/editor/editor.worker', import.meta.url));
                case 'yaml':
                    return new Worker(new URL('monaco-yaml/yaml.worker', import.meta.url));
                default:
                    throw new Error(`Unknown label ${label}`);
            }
        },
    };

    setDiagnosticsOptions({
        enableSchemaRequest: true,
        hover: true,
        completion: true,
        validate: true,
        format: true,
        schemas: [
            {
                uri: schemaUriStr,
                fileMatch: ['*.yml']
            }
        ],
    });
}