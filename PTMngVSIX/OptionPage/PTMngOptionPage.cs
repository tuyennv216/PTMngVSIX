using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using PTMngVSIX.Setting;
using System.ComponentModel;

namespace PTMngVSIX
{
	internal class PTMngOptionPage : DialogPage
	{
		private string _localEndpoint = DefaultSetting.LocalEndpoint;
		private string _localAssistantModelName = DefaultSetting.LocalAssistantModelName;
		private string _localTranslatorModelName = DefaultSetting.LocalTranslatorModelName;

		private bool _useInternet = true;
		private string _onlineApiKey;
		private string _onlineEndpoint = DefaultSetting.OnlineEndpoint;
		private string _onlineAssistantModelName = DefaultSetting.OnlineAssistantModelName;
		private string _onlineTranslatorModelName = DefaultSetting.OnlineTranslatorModelName;

		//Localhost
		[Category("Localhost")]
		[DisplayName("1. Endpoint")]
		[Description("The Api endpoint for the model service. http://localhost:11434")]
		public string LocalEndpoint
		{
			get => _localEndpoint;
			set
			{
				_localEndpoint = value;
			}
		}

		[Category("Localhost")]
		[DisplayName("2. Assistant model")]
		[Description("Name of the Assistant model.")]
		public string LocalAssistantModelName
		{
			get => _localAssistantModelName;
			set
			{
				_localAssistantModelName = value;
			}
		}

		[Category("Localhost")]
		[DisplayName("3. Translator model")]
		[Description("Name of the Translator model.")]
		public string LocalTranslatorModelName
		{
			get => _localTranslatorModelName;
			set
			{
				_localTranslatorModelName = value;
			}
		}
		// End Localhost

		// Internet - OpenRouterAI
		[Category("UseInternet")]
		[DisplayName("1. Use OpenRouter")]
		[Description("Connect to OpenRouterAI. https://openrouter.ai/api/v1")]
		public bool UseInternet
		{
			get => _useInternet;
			set
			{
				_useInternet = value;
			}
		}

		[Category("UseInternet")]
		[DisplayName("2. Endpoint")]
		[Description("The Api endpoint for the model service.")]
		public string OnlineEndpoint
		{
			get => _onlineEndpoint;
			set
			{
				_onlineEndpoint = value;
			}
		}


		[Category("UseInternet")]
		[DisplayName("3. Api key")]
		[Description("The Api key.")]
		public string OnlineApiKey
		{
			get => _onlineApiKey;
			set
			{
				_onlineApiKey = value;
			}
		}

		[Category("UseInternet")]
		[DisplayName("4. Assistant model")]
		[Description("Name of the Assistant model.")]
		public string OnlineAssistantModelName
		{
			get => _onlineAssistantModelName;
			set
			{
				_onlineAssistantModelName = value;
			}
		}

		[Category("UseInternet")]
		[DisplayName("3. Translator model")]
		[Description("Name of the Translator model.")]
		public string OnlineTranslatorModelName
		{
			get => _onlineTranslatorModelName;
			set
			{
				_onlineTranslatorModelName = value;
			}
		}
		// End Internet - OpenRouterAI

		private void SetAppStateModel()
		{
			if (ModelSetting.UseInternet)
			{
				AppState.Assistant = OnlineOpenRouterAI.DeepseekService.Instance;
				AppState.Translator = OnlineOpenRouterAI.GemmaService.Instance;
			}
			else
			{
				AppState.Assistant = LocalOllama.MistralService.Instance;
				AppState.Translator = LocalOllama.GemmaService.Instance;
			}
		}

		public override void LoadSettingsFromStorage()
		{
			base.LoadSettingsFromStorage(); // Gọi base để đảm bảo xử lý mặc định

			SetAppStateModel();
		}

		protected override void OnApply(PageApplyEventArgs e)
		{
			base.OnApply(e);

			if (e.ApplyBehavior == ApplyKind.Apply)
			{
				ModelSetting.UseInternet = _useInternet;

				if (ModelSetting.UseInternet)
				{
					ModelSetting.Endpoint = _onlineEndpoint;
					ModelSetting.AssistantModelName = _onlineAssistantModelName;
					ModelSetting.TranslatorModelName = _onlineTranslatorModelName;
					ModelSetting.ApiKey = _onlineApiKey;
				}
				else
				{
					ModelSetting.Endpoint = _localEndpoint;
					ModelSetting.AssistantModelName = _localAssistantModelName;
					ModelSetting.TranslatorModelName = _localTranslatorModelName;
				}

				SetAppStateModel();

				var jtf = PTMngVSIXPackage.JoinableTaskContext.Factory;
				_ = jtf.RunAsync(async () =>
				{
					await AppState.Assistant.TryConnectAsync();
					if (!AppState.IsModelAvailable)
					{
						await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

						VsShellUtilities.ShowMessageBox(
							this.Site,
							$"Cannot connect to the endpoint server.",
							"Connection Failed",
							OLEMSGICON.OLEMSGICON_WARNING,
							OLEMSGBUTTON.OLEMSGBUTTON_OK,
							OLEMSGDEFBUTTON.OLEMSGDEFBUTTON_FIRST);
					}
				});
			}
		}
	}
}
