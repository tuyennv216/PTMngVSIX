using Microsoft.Extensions.AI;
using PTMngVSIX.Abstraction;

namespace PTMngVSIX.Setting
{
	public static class AppState
	{
		public static bool IsModelAvailable { get; set; } = false;

		public static IChatClient ApiClient { get; set; }
		public static IModelService Assistant { get; set; }
		public static IModelService Translator { get; set; }
	}
}
