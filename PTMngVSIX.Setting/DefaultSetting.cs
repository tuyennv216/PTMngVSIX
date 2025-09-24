namespace PTMngVSIX.Setting
{
	public static class DefaultSetting
	{
		public static readonly string LocalEndpoint = "http://localhost:11434";
		public static readonly string LocalAssistantModelName = "PTMng-mistral";
		public static readonly string LocalTranslatorModelName = "PTMng-gemma3";

		public static readonly bool UseInternet = false;
		public static readonly string OnlineEndpoint = "https://openrouter.ai/api/v1";
		public static readonly string OnlineApiKey = "";
		public static readonly string OnlineAssistantModelName = "deepseek/deepseek-chat-v3.1";
		public static readonly string OnlineTranslatorModelName = "google/gemma-3-12b-it";

		public static readonly  object[] OptionRoleList = new object[]
		{
			"AI Assistant",
			"Developer",
			"Designer",
			"Tester",
			"Writer",
			"Architecture",
		};

		public static readonly object[] OptionOutputLanguageList = new object[]
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

		public static readonly string RoleName = "AI Assistant";
		public static readonly string OutputFormat = string.Empty;
		public static readonly bool TranslateInput = false;
		public static readonly bool TranslateOutput = false;
		public static readonly string OutputLanguage = "English";
		public static readonly string ChatEnviroment = string.Empty;

		public static bool UseAdvancedShortcut = false;
		public static int AdvancedHotkey = 56; // M
	}
}
