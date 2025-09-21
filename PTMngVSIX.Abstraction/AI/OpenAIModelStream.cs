using System.Collections.Generic;

namespace PTMngVSIX.Abstraction.AI
{
	public class OpenAIStreamResponse
	{
		public string Id { get; set; } = string.Empty;
		public string Object { get; set; } = string.Empty;
		public long Created { get; set; }
		public string Model { get; set; } = string.Empty;
		public List<StreamChoice> Choices { get; set; } = new();
	}

	public class StreamChoice
	{
		public Delta Delta { get; set; }
	}

}
