using Microsoft.Extensions.AI;

namespace PTMngVSIX.Prompt.AdditionalParam
{
	public class APF6
	{
		public static readonly AdditionalPropertiesDictionary AP6000_Chat = new AdditionalPropertiesDictionary
		{
			{ "temperature", 0.65 },
			{ "top_p", 0.9 },
			{ "frequency_penalty", 0.15 },
			{ "presence_penalty", 0.1 },
			{ "top_k", 50 }
		};
	}
}
