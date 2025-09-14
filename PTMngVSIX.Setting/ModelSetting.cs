namespace PTMngVSIX.Setting
{
	public static class ModelSetting
	{
		public static string Endpoint = DefaultSetting.LocalEndpoint;
		public static string ApiKey = DefaultSetting.OnlineApiKey;
		public static string AssistantModelName = DefaultSetting.LocalAssistantModelName;
		public static string TranslatorModelName = DefaultSetting.LocalTranslatorModelName;

		public static bool UseInternet = false;
	}
}
