using PTMngVSIX.Abstraction.AI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PTMngVSIX.Setting
{
	public class AIServiceUsage
	{
		public static readonly AIServiceUsage Instance = new();
		private AIServiceUsage() { }

		public DateTime SessionTime { get; set; } = DateTime.Now;
		public Usage TotalUsage { get; set; } = new();

		public string LastSendText { get; set; } = string.Empty;
		public string LastReceiveText { get; set; } = string.Empty;
		public OpenAIResponse LastResponse { get; set; } = new();

		public Task Update(IEnumerable<ChatMessage> messages, OpenAIResponse response)
		{
			var choice = response?.Choices?[0]?.Message?.Content ?? string.Empty;

			LastSendText = string.Join(
				Environment.NewLine,
				messages.Select(m => $"{m.Role}: {m.Content}"));

			LastReceiveText = choice;

			LastResponse = response;

			TotalUsage.PromptTokens += response.Usage.PromptTokens;
			TotalUsage.CompletionTokens += response.Usage.CompletionTokens;
			TotalUsage.TotalTokens += response.Usage.TotalTokens;

			TotalUsage.PromptTokensDetails = response.Usage.PromptTokensDetails;
			TotalUsage.CompletionTokensDetails = response.Usage.CompletionTokensDetails;

			return Task.CompletedTask;
		}

		public Task NewSession()
		{
			SessionTime = DateTime.Now;
			TotalUsage.PromptTokens = 0;
			TotalUsage.CompletionTokens = 0;
			TotalUsage.TotalTokens = 0;

			TotalUsage.PromptTokensDetails = string.Empty;
			TotalUsage.CompletionTokensDetails = string.Empty;

			return Task.CompletedTask;
		}
	}
}
