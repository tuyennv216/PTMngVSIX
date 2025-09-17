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

		public static object[] OptionRoleList = new object[]
		{
			"AI Assistant",
			"Developer",
			"Designer",
			"Tester",
			"Writer",
			"Architecture",
		};

		public static object[] OptionOutputLanguageList = new object[]
		{
			"English",
			"Vietnamese",
			"Chinese",
			"Russian",
			"Korean",
			"Japanese",
			"French",
			"German",
		};

		public static string RoleName = "AI Assistant";
		public static string OutputFormat = string.Empty;
		public static bool TranslateInput = false;
		public static bool TranslateOutput = false;
		public static string OutputLanguage = "English";
	}
}
