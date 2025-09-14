# Running Ollama on Localhost

## Installation

Step 1: Download and install Ollama from: https://ollama.com/download  
Step 2: Download the files `mistral-7b.config` and `gemma3-1b.translator.config`  
Step 3: Run Ollama server  
Step 4: Create models using the script:

```cmd
ollama create PTMng-mistral -f mistral-7b.config
ollama create PTMng-gemma3 -f gemma3-1b.translator.config
```

## Ollama for AMD GPU

Download and install from: https://github.com/ByronLeeeee/Ollama-For-AMD-Installer/releases