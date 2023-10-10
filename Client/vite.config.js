import fs from 'fs/promises';
import { defineConfig } from 'vite';
import react from '@vitejs/plugin-react';

export default defineConfig(() => ({
    esbuild: {
        loader: "jsx",
        jsxInject: `import React from 'react'`
        // jsxFactory: 'h',
        // jsxFragment: 'Fragment'
    },
    resolve: {
        extensions: ['.js', '.jsx'],
        alias: '.jsx'
    }
}))