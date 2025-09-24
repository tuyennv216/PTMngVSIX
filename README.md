# PTMngVSIX - AI-Powered Development Assistant

## Overview

PTMngVSIX is a powerful Visual Studio extension that integrates multiple AI services through Local Ollama and OpenRouter, providing intelligent code assistance and AI-powered development tools directly within the Visual Studio IDE.

## Features

- **Multi-AI Service Support**: Integrated support for local Ollama (Mistral, Llama, etc.) and cloud AI services via OpenRouter
- **Code Generation**: Generate code snippets and complete functions
- **Code Refactoring**: Optimization and bug fixing
- **Documentation**: Generate comprehensive code documentation
- **Test Case Generation**: Create unit tests and test cases
- **Code Review**: Get code review and improvement suggestions
- **Architecture**: Get solution and feature suggestions

## Requirements

- Visual Studio 2022 or later
- .NET Framework 4.7.2 or later
- OpenRouter API key (for cloud AI service access)
- Optional: Local Ollama installation for offline AI capabilities

## Configuration

1. Open Visual Studio and go to **Tools → Options → PTMng AI**
2. For OpenRouter access: Enter your API key in the designated field
3. For local Ollama: Specify the Ollama server URL
4. Choose the assistant model you want to use
5. Save your settings

## Usage

### Common Use
1. Right-click in the code editor and select **menu → sub action**
2. Open **View / PTMng AI Chat → Setting** to configure chat options
3. Right-click in the solution file to attach file(s) to the chat window

### Shortcuts
1. **Ctrl + Shift + M → M**: Open PTMng AI Chat window
2. **Ctrl + Shift + M → A**: Add code snippet
3. **Ctrl + Shift + M → Q**: Add current editor document to code snippet
4. **Ctrl + Shift + M → W**: Add current editor function to code snippet
5. **Ctrl + Shift + M → C**: Generate function
6. **Ctrl + Shift + M → F**: Suggest a fix for the current line error
7. **Ctrl + Shift + M → X**: Clear code snippets and active documents
8. **Ctrl + Shift + M → Z**: Connect to AI server

## Important Notice

**Data Privacy and Security**:
- When using online AI services (via OpenRouter), your code snippets, text, and input data will be sent to external servers for processing.
- Please ensure your project allows this data transmission before using online AI features.
- For sensitive projects, consider using local Ollama for complete data privacy.