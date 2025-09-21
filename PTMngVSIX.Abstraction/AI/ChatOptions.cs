using System.Collections.Generic;

namespace PTMngVSIX.Abstraction.AI
{
	public class ChatOptions
	{
		public string ModelId { get; set; }
		public Dictionary<string, object> AdditionalProperties { get; set; }

		public ChatOptions()
		{
			AdditionalProperties = new Dictionary<string, object>();
		}

		public ChatOptions(string modelId)
		{
			ModelId = modelId;
			AdditionalProperties = new Dictionary<string, object>();
		}

		public ChatOptions(string modelId, Dictionary<string, object> additionalProperties)
		{
			ModelId = modelId;
			AdditionalProperties = additionalProperties;
		}
	}
}
