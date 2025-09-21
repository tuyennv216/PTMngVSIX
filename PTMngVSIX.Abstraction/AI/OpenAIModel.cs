using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace PTMngVSIX.Abstraction.AI
{
	public class OpenAIResponse
	{
		public string Id { get; set; } = string.Empty;
		public object Provider { get; set; }
		public string Model { get; set; } = string.Empty;
		public string Object { get; set; } = string.Empty;
		public long Created { get; set; }
		public List<Choice> Choices { get; set; } = new();
		public Usage Usage { get; set; } = new();
	}

	public class Choice
	{
		[JsonPropertyName("logprobs")]
		public object Logprobs { get; set; }  // Null OK

		[JsonPropertyName("finish_reason")]
		public string FinishReason { get; set; }  // Nullable

		[JsonPropertyName("native_finish_reason")]
		public string NativeFinishReason { get; set; }  // Nullable

		public int Index { get; set; }
		public ChatMessage Message { get; set; } = new();
	}

	public class Delta
	{
		public string Content { get; set; }
	}

	public class Logprobs
	{
		public List<object> Content { get; set; } = new();
		public List<object> Refusal { get; set; } = new();
	}

	public class ChatMessage
	{
		public ChatMessage() { }

		public ChatMessage(string role, string content)
		{
			Role = role;
			Content = content;
		}

		public string Role { get; set; } = string.Empty;
		public string Content { get; set; } = string.Empty;
		[JsonPropertyName("refusal")]
		public object Refusal { get; set; }  // Null

		[JsonPropertyName("reasoning")]
		public object Reasoning { get; set; }  // Null
	}

	public class Usage
	{
		[JsonPropertyName("prompt_tokens")]
		public int PromptTokens { get; set; }

		[JsonPropertyName("completion_tokens")]
		public int CompletionTokens { get; set; }

		[JsonPropertyName("total_tokens")]
		public int TotalTokens { get; set; }

		[JsonPropertyName("prompt_tokens_details")]
		public object PromptTokensDetails { get; set; }

		[JsonPropertyName("completion_tokens_details")]
		public object CompletionTokensDetails { get; set; }
	}
}
