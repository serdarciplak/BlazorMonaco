import HtmlWebPackPlugin from 'html-webpack-plugin';
import MonacoWebpackPlugin from 'monaco-editor-webpack-plugin';
import * as path from 'path';
import { fileURLToPath } from 'url';

const __filename = fileURLToPath(import.meta.url);
const __dirname = path.dirname(__filename);

export default {
    output: {
        path: path.resolve(__dirname, '../wwwroot/lib'),
        filename: 'index.js',
        library: "MonacoEditor",
    },
    module: {
        rules: [
            {
                test: /\.css$/,
                use: ['style-loader', 'css-loader'],
            },
            {
                test: /\.ttf$/,
                type: 'asset',
            }
        ]
    },
    plugins: [
        new HtmlWebPackPlugin(),
        new MonacoWebpackPlugin({
            languages: ['yaml'],
            customLanguages: [
                {
                    label: 'yaml',
                    entry: 'monaco-yaml',
                    worker: {
                        id: 'monaco-yaml/yamlWorker',
                        entry: 'monaco-yaml/yaml.worker',
                    },
                },
            ],
        }),
    ],
};