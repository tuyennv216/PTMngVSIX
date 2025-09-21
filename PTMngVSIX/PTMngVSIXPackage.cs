using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.Threading;
using PTMngVSIX.AIServices;
using PTMngVSIX.Setting;
using PTMngVSIX.Utils.Setting;
using System;
using System.IO.Packaging;
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
	[ProvideAutoLoad(UIContextGuids80.SolutionExists, PackageAutoLoadFlags.BackgroundLoad)]
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

			await this.JoinableTaskFactory.SwitchToMainThreadAsync(cancellationToken);

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

			await PTMngVSIX.Commands.F2FunctionCode.C2000_DocsSelection.InitializeAsync(this);
			await PTMngVSIX.Commands.F2FunctionCode.C2001_DocsFunction.InitializeAsync(this);
			await PTMngVSIX.Commands.F2FunctionCode.C2002_DocsClass.InitializeAsync(this);
			await PTMngVSIX.Commands.F2FunctionCode.C2010_DocsApi.InitializeAsync(this);
			await PTMngVSIX.Commands.F2FunctionCode.C2020_DocsTechnicalSpecifications.InitializeAsync(this);

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
			await PTMngVSIX.Commands.F6Chat.C6011_ResetCodeSnippet.InitializeAsync(this);

			await PTMngVSIX.ToolWindow.PTMngChatCommand.InitializeAsync(this);


			LoadFromRegStorage(this);
		}

		#endregion

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

				AppState.Assistant = DeepseekV2Service.Instance;
				AppState.Translator = GemmaV1Service.Instance;
			}
			else
			{
				ModelSetting.Endpoint = options.LocalEndpoint;
				ModelSetting.AssistantModelName = options.LocalAssistantModelName;
				ModelSetting.TranslatorModelName = options.LocalTranslatorModelName;

				AppState.Assistant = MistralV1Service.Instance;
				AppState.Translator = GemmaV1Service.Instance;
			}

			ModelSetting.RoleName = options.RoleName;
			ModelSetting.OutputFormat = options.OutputFormat;
			ModelSetting.TranslateInput = options.TranslateInput;
			ModelSetting.TranslateOutput = options.TranslateOutput;
			ModelSetting.OutputLanguage = options.OutputLanguage;
		}
	}
}
