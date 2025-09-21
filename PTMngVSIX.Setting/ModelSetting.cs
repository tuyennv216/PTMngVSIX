namespace PTMngVSIX.Setting
{
	public static class ModelSetting
	{
		public static string Endpoint { get; set; } = DefaultSetting.LocalEndpoint;
		public static string ApiKey { get; set; } = DefaultSetting.OnlineApiKey;
		public static string AssistantModelName { get; set; } = DefaultSetting.LocalAssistantModelName;
		public static string TranslatorModelName { get; set; } = DefaultSetting.LocalTranslatorModelName;

		public static bool UseInternet { get; set; } = false;

		public static string RoleName { get; set; }		= DefaultSetting.RoleName;
		public static string OutputFormat { get; set; } = DefaultSetting.OutputFormat;
		public static bool TranslateInput { get; set; } = DefaultSetting.TranslateInput;
		public static bool TranslateOutput { get; set; }	= DefaultSetting.TranslateOutput;
		public static string OutputLanguage { get; set; } = DefaultSetting.OutputLanguage;
		public static string Enviroment { get; set; } = DefaultSetting.ChatEnviroment;
	}
}
