using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using PTMngVSIX.AIServices;
using PTMngVSIX.Setting;
using System;
using System.ComponentModel;

namespace PTMngVSIX
{
	internal class PTMngOptionPage : DialogPage
	{
		private string _localEndpoint = DefaultSetting.LocalEndpoint;
		private string _localAssistantModelName = DefaultSetting.LocalAssistantModelName;
		private string _localTranslatorModelName = DefaultSetting.LocalTranslatorModelName;

		private bool _useInternet = true;
		private string _onlineApiKey = string.Empty;
		private string _onlineEndpoint = DefaultSetting.OnlineEndpoint;
		private string _onlineAssistantModelName = DefaultSetting.OnlineAssistantModelName;
		private string _onlineTranslatorModelName = DefaultSetting.OnlineTranslatorModelName;

		//Localhost
		[Category("I. Localhost")]
		[DisplayName("1. Endpoint.")]
		[Description("The Api endpoint for the model service. http://localhost:11434")]
		public string LocalEndpoint
		{
			get => _localEndpoint;
			set
			{
				_localEndpoint = value;
			}
		}

		[Category("I. Localhost")]
		[DisplayName("2. Assistant model.")]
		[Description("Name of the Assistant model. PTMng-mistral")]
		public string LocalAssistantModelName
		{
			get => _localAssistantModelName;
			set
			{
				_localAssistantModelName = value;
			}
		}

		[Category("I. Localhost")]
		[DisplayName("3. Translator model.")]
		[Description("Name of the Translator model. PTMng-gemma3")]
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
		[Category("II. Internet")]
		[DisplayName("1. Use OpenRouter")]
		[Description("Connect to OpenRouterAI.")]
		public bool UseInternet
		{
			get => _useInternet;
			set
			{
				_useInternet = value;
			}
		}

		[Category("II. Internet")]
		[DisplayName("2. Endpoint.")]
		[Description("The Api endpoint for the model service. https://openrouter.ai/api/v1")]
		public string OnlineEndpoint
		{
			get => _onlineEndpoint;
			set
			{
				_onlineEndpoint = value;
			}
		}


		[Category("II. Internet")]
		[DisplayName("3. Api key.")]
		[Description("The Api key.")]
		public string OnlineApiKey
		{
			get => _onlineApiKey;
			set
			{
				_onlineApiKey = value;
			}
		}

		[Category("II. Internet")]
		[DisplayName("4. Assistant model.")]
		[Description("Name of the Assistant model. deepseek/deepseek-chat-v3.1")]
		public string OnlineAssistantModelName
		{
			get => _onlineAssistantModelName;
			set
			{
				_onlineAssistantModelName = value;
			}
		}

		[Category("II. Internet")]
		[DisplayName("3. Translator model.")]
		[Description("Name of the Translator model. google/gemma-3-12b-it")]
		public string OnlineTranslatorModelName
		{
			get => _onlineTranslatorModelName;
			set
			{
				_onlineTranslatorModelName = value;
			}
		}
		// End Internet - OpenRouterAI

		//IDE setting
		[Browsable(false)]
		[Category("III. IDE")]
		[DisplayName("1. Use Advanced Shortcut.")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[Description("Examples of advanced shortcuts include DD, DF, and more.")]
		public bool UseAdvancedShortcut { get; set; }

		[Browsable(false)]
		[Category("III. IDE")]
		[DisplayName("2. Advanced hotkey.")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[Description("Hotkey to use the Ctrl + Shift + [Key] combo. A -> Z. Default key is M")]
		public System.Windows.Input.Key AdvancedHotkey
		{
			get
			{
				return _advancedHotkey;
			}
			set
			{
				if (value <= System.Windows.Input.Key.A || value >= System.Windows.Input.Key.Z) return;
				_advancedHotkey = value;
			}
		}
		private System.Windows.Input.Key _advancedHotkey = System.Windows.Input.Key.M;
		//End IDE setting

		// Others setting
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public string RoleName { get; set; }

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public string OutputFormat { get; set; }

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public bool TranslateInput { get; set; }

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public bool TranslateOutput { get; set; }

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public string OutputLanguage { get; set; }

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public string ChatEnviroment { get; set; }
		// End Others setting

		private void SetAppSettingModel()
		{
			ModelSetting.UseInternet = _useInternet;

			if (ModelSetting.UseInternet)
			{
				ModelSetting.Endpoint = _onlineEndpoint;
				ModelSetting.AssistantModelName = _onlineAssistantModelName;
				ModelSetting.TranslatorModelName = _onlineTranslatorModelName;
				ModelSetting.ApiKey = _onlineApiKey;

				AppState.Instance.Assistant = AIServices.DeepseekV2Service.Instance;
				AppState.Instance.Translator = AIServices.GemmaV1Service.Instance;
			}
			else
			{
				ModelSetting.Endpoint = _localEndpoint;
				ModelSetting.AssistantModelName = _localAssistantModelName;
				ModelSetting.TranslatorModelName = _localTranslatorModelName;

				AppState.Instance.Assistant = MistralV1Service.Instance;
				AppState.Instance.Translator = GemmaV1Service.Instance;
			}

			IDESetting.UseAdvancedShortcut = UseAdvancedShortcut;
			IDESetting.AdvancedHotkey = ((int)AdvancedHotkey);
		}

		public override void LoadSettingsFromStorage()
		{
			base.LoadSettingsFromStorage(); // Gọi base để đảm bảo xử lý mặc định

			SetAppSettingModel();
		}

		protected override void OnApply(PageApplyEventArgs e)
		{
			base.OnApply(e);

			if (e.ApplyBehavior == ApplyKind.Apply)
			{
				SetAppSettingModel();

				var jtf = PTMngVSIXPackage.JoinableTaskContext.Factory;
				_ = jtf.RunAsync(async () =>
				{
					await AppState.Instance.Assistant.TryConnectAsync();
					if (!AppState.Instance.IsModelAvailable)
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
