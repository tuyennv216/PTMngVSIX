using System.Collections.Generic;
using System.Threading.Tasks;

namespace PTMngVSIX.Abstraction.AI
{
	public interface IChatClient
	{
		public Task<string> GetResponseAsync(string prompt);
		public Task<string> GetResponseAsync(ChatMessage message, ChatOptions options);
		public Task<string> GetResponseAsync(IEnumerable<ChatMessage> messages, ChatOptions options);
		public IAsyncEnumerable<string> GetStreamResponseAsync(IEnumerable<ChatMessage> messages, ChatOptions options);
	}
}
