import { defineConfig } from 'vite'
import react from '@vitejs/plugin-react'

// https://vite.dev/config/
export default defineConfig({
  plugins: [react()],
  test: {
    globals: true, // This enables global test functions like `test` and `expect`
    environment: 'jsdom', // Ensure the test environment is jsdom for DOM testing
    transformMode: {
      web: [/\.jsx$/, /\.tsx$/], // Ensure Vitest processes JSX/TSX files
    }
  }
})
