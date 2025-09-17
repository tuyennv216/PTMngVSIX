namespace PTMngVSIX.Setting
{
	public static class ModelSetting
	{
		public static string Endpoint = DefaultSetting.LocalEndpoint;
		public static string ApiKey = DefaultSetting.OnlineApiKey;
		public static string AssistantModelName = DefaultSetting.LocalAssistantModelName;
		public static string TranslatorModelName = DefaultSetting.LocalTranslatorModelName;

		public static bool UseInternet = false;

		public static string RoleName = DefaultSetting.RoleName;
		public static string OutputFormat = DefaultSetting.OutputFormat;
		public static bool TranslateInput = DefaultSetting.TranslateInput;
		public static bool TranslateOutput = DefaultSetting.TranslateOutput;
		public static string OutputLanguage = DefaultSetting.OutputLanguage;
	}
}
