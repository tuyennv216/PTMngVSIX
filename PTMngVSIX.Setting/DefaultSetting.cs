namespace PTMngVSIX.Setting
{
	public static class DefaultSetting
	{
		public static string LocalEndpoint = "http://localhost:11434";
		public static string LocalAssistantModelName = "PTMng-mistral";
		public static string LocalTranslatorModelName = "PTMng-gemma3";

		public static bool UseInternet = false;
		public static string OnlineEndpoint = "https://openrouter.ai/api/v1";
		public static string OnlineApiKey = "";
		public static string OnlineAssistantModelName = "deepseek/deepseek-chat-v3.1";
		public static string OnlineTranslatorModelName = "google/gemma-3-12b-it";
	}
}
