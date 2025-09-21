using System.Collections.Generic;

namespace PTMngVSIX.Prompt.AdditionalParam
{
	public static class Apf6
	{
		public static readonly Dictionary<string, object> AP6000_Chat = new Dictionary<string, object>
		{
			{ "temperature", 0.65 },
			{ "top_p", 0.9 },
			{ "frequency_penalty", 0.15 },
			{ "presence_penalty", 0.1 },
			{ "top_k", 50 }
		};
	}
}
