using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.Threading;
using PTMngVSIX.Abstraction;
using PTMngVSIX.AIServices;
using PTMngVSIX.Setting;
using PTMngVSIX.Shortcut.KeyboardService;
using System;
using System.Runtime.InteropServices;
using System.Threading;
using Task = System.Threading.Tasks.Task;

namespace PTMngVSIX
{
	[PackageRegistration(UseManagedResourcesOnly = true, AllowsBackgroundLoading = true)]
	[ProvideOptionPage(typeof(PTMngOptionPage),
		Utils.Setting.Constant.OptionCategoryPTMngAI,
		Utils.Setting.Constant.OptionPageGeneral,
		0, 0, true)]
	[Guid(PTMngVSIXPackage.PackageGuidString)]
	[ProvideMenuResource("Menus.PTMngMenu", 1)]
	[ProvideToolWindow(typeof(PTMngVSIX.ToolWindow.PTMngChat))]
	//[ProvideService(typeof(IKeyboardSequenceService))]
	[ProvideAutoLoad(UIContextGuids80.SolutionExists, PackageAutoLoadFlags.BackgroundLoad)]
	[ProvideAutoLoad(UIContextGuids80.NoSolution, PackageAutoLoadFlags.BackgroundLoad)]
	public sealed class PTMngVSIXPackage : AsyncPackage
	{
		/// <summary>
		/// PTMngVSIXPackage GUID string.
		/// </summary>
		public const string PackageGuidString = "bd4318e4-1155-4cf7-95ca-e30cfcc156a3";

		/// <summary>
		/// Gets the <see cref="Microsoft.VisualStudio.Threading.JoinableTaskContext"/> instance  associated with the current application.
		/// </summary>
		public static JoinableTaskContext JoinableTaskContext { get; private set; }
		public static PTMngVSIXPackage Instance { get; private set; }
		private KeyboardSequenceService _keyboardService;

		#region Package Members

		/// <summary>
		/// Initialization of the package; this method is called right after the package is sited, so this is the place
		/// where you can put all the initialization code that rely on services provided by VisualStudio.
		/// </summary>
		/// <param name="cancellationToken">A cancellation token to monitor for initialization cancellation, which can occur when VS is shutting down.</param>
		/// <param name="progress">A provider for progress updates.</param>
		/// <returns>A task representing the async work of package initialization, or an already completed task if there is none. Do not return null from this method.</returns>
		protected override async Task InitializeAsync(CancellationToken cancellationToken, IProgress<ServiceProgressData> progress)
		{
			await base.InitializeAsync(cancellationToken, progress);
			JoinableTaskContext = this.JoinableTaskFactory.Context;
			Instance = this;

			//_keyboardService = new KeyboardSequenceService();
			//AddService(typeof(IKeyboardSequenceService), (container, type, services) => Task.FromResult<object?>(_keyboardService), true);

			await this.JoinableTaskFactory.SwitchToMainThreadAsync(cancellationToken);

			//_keyboardService.Initialize();

			await RegistCommandsAsync();
			LoadFromRegStorage(this);
			//RegistShortkey();
		}

		#endregion

		#region Regist Commands
		private async Task RegistCommandsAsync()
		{
			await PTMngVSIX.Commands.F0App.C9000_ConnectServer.InitializeAsync(this);

			await PTMngVSIX.Commands.F1FunctionCode.C1000_GenerateCode.InitializeAsync(this);
			await PTMngVSIX.Commands.F1FunctionCode.C1001_GenerateFunction.InitializeAsync(this);
			await PTMngVSIX.Commands.F1FunctionCode.C1002_GenerateClass.InitializeAsync(this);
			await PTMngVSIX.Commands.F1FunctionCode.C1004_FillInMiddle.InitializeAsync(this);
			await PTMngVSIX.Commands.F1FunctionCode.C1005_AddComment.InitializeAsync(this);
			await PTMngVSIX.Commands.F1FunctionCode.C1006_CompleteFunction.InitializeAsync(this);

			await PTMngVSIX.Commands.F1FunctionCode.C1010_ReflectCode.InitializeAsync(this);
			await PTMngVSIX.Commands.F1FunctionCode.C1011_ReflectFunction.InitializeAsync(this);
			await PTMngVSIX.Commands.F1FunctionCode.C1012_ReflectClass.InitializeAsync(this);
			await PTMngVSIX.Commands.F1FunctionCode.C1040_SummaryFunction.InitializeAsync(this);
			await PTMngVSIX.Commands.F1FunctionCode.C1050_OptimizeFunction.InitializeAsync(this);
			await PTMngVSIX.Commands.F1FunctionCode.C1060_ExplainFunction.InitializeAsync(this);

			await PTMngVSIX.Commands.F2Docs.C2000_DocsSelection.InitializeAsync(this);
			await PTMngVSIX.Commands.F2Docs.C2001_DocsFunction.InitializeAsync(this);
			await PTMngVSIX.Commands.F2Docs.C2002_DocsClass.InitializeAsync(this);
			await PTMngVSIX.Commands.F2Docs.C2010_DocsApi.InitializeAsync(this);
			await PTMngVSIX.Commands.F2Docs.C2020_DocsTechnicalSpecifications.InitializeAsync(this);

			await PTMngVSIX.Commands.F3FixBug.C3010_ExplainError.InitializeAsync(this);
			await PTMngVSIX.Commands.F3FixBug.C3020_SuggestFixes.InitializeAsync(this);
			await PTMngVSIX.Commands.F3FixBug.C3030_FixErrorLine.InitializeAsync(this);

			await PTMngVSIX.Commands.F4Test.C4010_Whitebox.InitializeAsync(this);
			await PTMngVSIX.Commands.F4Test.C4020_Blackbox_UnitTests.InitializeAsync(this);
			await PTMngVSIX.Commands.F4Test.C4021_Blackbox_IntegrationTests.InitializeAsync(this);
			await PTMngVSIX.Commands.F4Test.C4022_Blackbox_EdgeCaseTesting.InitializeAsync(this);
			await PTMngVSIX.Commands.F4Test.C4023_Blackbox_PerformanceTesting.InitializeAsync(this);

			await PTMngVSIX.Commands.F5Architect.C5020_ReviewCodeFunction.InitializeAsync(this);
			await PTMngVSIX.Commands.F5Architect.C5021_ReviewCodeClass.InitializeAsync(this);
			await PTMngVSIX.Commands.F5Architect.C5030_SuggestSolution.InitializeAsync(this);
			await PTMngVSIX.Commands.F5Architect.C5040_SuggestDeploy.InitializeAsync(this);
			await PTMngVSIX.Commands.F5Architect.C5050_SuggestArchitecture.InitializeAsync(this);
			await PTMngVSIX.Commands.F5Architect.C5060_SuggestTechnologies.InitializeAsync(this);
			await PTMngVSIX.Commands.F5Architect.C5070_SuggestFeatures.InitializeAsync(this);

			await PTMngVSIX.Commands.F6Chat.C6010_AddCodeSnippet.InitializeAsync(this);
			await PTMngVSIX.Commands.F6Chat.C6011_ResetChat.InitializeAsync(this);
			await PTMngVSIX.Commands.F6Chat.C6020_AddSolutionFilePath.InitializeAsync(this);
			await PTMngVSIX.Commands.F6Chat.C6021_AddActiveFilePath.InitializeAsync(this);
			await PTMngVSIX.Commands.F6Chat.C6022_AddSolutionFolderFilesPath.InitializeAsync(this);
			await PTMngVSIX.Commands.F6Chat.C6030_AddActiveFile.InitializeAsync(this);
			await PTMngVSIX.Commands.F6Chat.C6031_AddActiveFunction.InitializeAsync(this);
			await PTMngVSIX.Commands.F6Chat.C6032_AddCurrentLine.InitializeAsync(this);

			await PTMngVSIX.ToolWindow.PTMngChatCommand.InitializeAsync(this);
		}
		#endregion

		#region Regist Shortkey
		private void RegistShortkey()
		{
			_keyboardService.RegisterSequence("DD", () => Utils.vsShell.RunCommand.ExecutePTMngCommandAsync(CommandIds.C2002).FileAndForget("Hotkey", CommandIds.C2002.ToString()));
			_keyboardService.RegisterSequence("DF", () => Utils.vsShell.RunCommand.ExecutePTMngCommandAsync(CommandIds.C2001).FileAndForget("Hotkey", CommandIds.C2001.ToString()));
			_keyboardService.RegisterSequence("DA", () => Utils.vsShell.RunCommand.ExecutePTMngCommandAsync(CommandIds.C2010).FileAndForget("Hotkey", CommandIds.C2010.ToString()));
			_keyboardService.RegisterSequence("DT", () => Utils.vsShell.RunCommand.ExecutePTMngCommandAsync(CommandIds.C2020).FileAndForget("Hotkey", CommandIds.C2020.ToString()));

			_keyboardService.RegisterSequence("TW", () => Utils.vsShell.RunCommand.ExecutePTMngCommandAsync(CommandIds.C4010).FileAndForget("Hotkey", CommandIds.C4010.ToString()));
			_keyboardService.RegisterSequence("TU", () => Utils.vsShell.RunCommand.ExecutePTMngCommandAsync(CommandIds.C4020).FileAndForget("Hotkey", CommandIds.C4020.ToString()));
			_keyboardService.RegisterSequence("TI", () => Utils.vsShell.RunCommand.ExecutePTMngCommandAsync(CommandIds.C4021).FileAndForget("Hotkey", CommandIds.C4021.ToString()));
			_keyboardService.RegisterSequence("TE", () => Utils.vsShell.RunCommand.ExecutePTMngCommandAsync(CommandIds.C4022).FileAndForget("Hotkey", CommandIds.C4022.ToString()));
			_keyboardService.RegisterSequence("TP", () => Utils.vsShell.RunCommand.ExecutePTMngCommandAsync(CommandIds.C4023).FileAndForget("Hotkey", CommandIds.C4023.ToString()));

			_keyboardService.RegisterSequence("RR", () => Utils.vsShell.RunCommand.ExecutePTMngCommandAsync(CommandIds.C5021).FileAndForget("Hotkey", CommandIds.C5021.ToString()));
			_keyboardService.RegisterSequence("RF", () => Utils.vsShell.RunCommand.ExecutePTMngCommandAsync(CommandIds.C5020).FileAndForget("Hotkey", CommandIds.C5020.ToString()));

			_keyboardService.RegisterSequence("SS", () => Utils.vsShell.RunCommand.ExecutePTMngCommandAsync(CommandIds.C5030).FileAndForget("Hotkey", CommandIds.C5030.ToString()));
			_keyboardService.RegisterSequence("SD", () => Utils.vsShell.RunCommand.ExecutePTMngCommandAsync(CommandIds.C5040).FileAndForget("Hotkey", CommandIds.C5040.ToString()));
			_keyboardService.RegisterSequence("SA", () => Utils.vsShell.RunCommand.ExecutePTMngCommandAsync(CommandIds.C5050).FileAndForget("Hotkey", CommandIds.C5050.ToString()));
			_keyboardService.RegisterSequence("ST", () => Utils.vsShell.RunCommand.ExecutePTMngCommandAsync(CommandIds.C5060).FileAndForget("Hotkey", CommandIds.C5060.ToString()));
			_keyboardService.RegisterSequence("SF", () => Utils.vsShell.RunCommand.ExecutePTMngCommandAsync(CommandIds.C5070).FileAndForget("Hotkey", CommandIds.C5070.ToString()));
		}
		#endregion

		#region Load Storage
		private static void LoadFromRegStorage(PTMngVSIXPackage package)
		{
			var options = (PTMngOptionPage)package.GetDialogPage(typeof(PTMngOptionPage));

			ModelSetting.UseInternet = options.UseInternet;

			if (ModelSetting.UseInternet)
			{
				ModelSetting.Endpoint = options.OnlineEndpoint;
				ModelSetting.ApiKey = options.OnlineApiKey;
				ModelSetting.AssistantModelName = options.OnlineAssistantModelName;
				ModelSetting.TranslatorModelName = options.OnlineTranslatorModelName;

				AppState.Instance.Assistant = DeepseekV2Service.Instance;
				AppState.Instance.Translator = GemmaV1Service.Instance;
			}
			else
			{
				ModelSetting.Endpoint = options.LocalEndpoint;
				ModelSetting.AssistantModelName = options.LocalAssistantModelName;
				ModelSetting.TranslatorModelName = options.LocalTranslatorModelName;

				AppState.Instance.Assistant = MistralV1Service.Instance;
				AppState.Instance.Translator = GemmaV1Service.Instance;
			}

			ModelSetting.RoleName = options.RoleName;
			ModelSetting.OutputFormat = options.OutputFormat;
			ModelSetting.TranslateInput = options.TranslateInput;
			ModelSetting.TranslateOutput = options.TranslateOutput;
			ModelSetting.OutputLanguage = options.OutputLanguage;

			//IDESetting.UseAdvancedShortcut = options.UseAdvancedShortcut;
			//IDESetting.AdvancedHotkey = ((int)options.AdvancedHotkey);
		}
		#endregion

	}
}
