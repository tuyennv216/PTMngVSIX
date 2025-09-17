using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.Threading;
using PTMngVSIX.Setting;
using PTMngVSIX.Utils.Setting;
using System;
using System.Runtime.InteropServices;
using System.Threading;
using Task = System.Threading.Tasks.Task;

namespace PTMngVSIX
{
	/// <summary>
	/// This is the class that implements the package exposed by this assembly.
	/// </summary>
	/// <remarks>
	/// <para>
	/// The minimum requirement for a class to be considered a valid package for Visual Studio
	/// is to implement the IVsPackage interface and register itself with the shell.
	/// This package uses the helper classes defined inside the Managed Package Framework (MPF)
	/// to do it: it derives from the Package class that provides the implementation of the
	/// IVsPackage interface and uses the registration attributes defined in the framework to
	/// register itself and its components with the shell. These attributes tell the pkgdef creation
	/// utility what data to put into .pkgdef file.
	/// </para>
	/// <para>
	/// To get loaded into VS, the package must be referred by &lt;Asset Type="Microsoft.VisualStudio.VsPackage" ...&gt; in .vsixmanifest file.
	/// </para>
	/// </remarks>
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
			JoinableTaskContext = this.JoinableTaskFactory.Context;

			LoadFromOptionPage();
			LoadFromRegStorage();
			_ = AppState.Assistant.TryConnectAsync();

			// When initialized asynchronously, the current thread may be a background thread at this point.
			// Do any initialization that requires the UI thread after switching to the UI thread.
			await this.JoinableTaskFactory.SwitchToMainThreadAsync(cancellationToken);

			var commandTasks = new[]
			{
				PTMngVSIX.Commands.F0App.C9000_ConnectServer.InitializeAsync(this),

				PTMngVSIX.Commands.F1FunctionCode.C1000_GenerateCode.InitializeAsync(this),
				PTMngVSIX.Commands.F1FunctionCode.C1001_GenerateFunction.InitializeAsync(this),
				PTMngVSIX.Commands.F1FunctionCode.C1002_GenerateClass.InitializeAsync(this),
				PTMngVSIX.Commands.F1FunctionCode.C1003_ReflectCode.InitializeAsync(this),
				PTMngVSIX.Commands.F1FunctionCode.C1004_FillInMiddle.InitializeAsync(this),
				PTMngVSIX.Commands.F1FunctionCode.C1005_AddComment.InitializeAsync(this),
				PTMngVSIX.Commands.F1FunctionCode.C1006_CompleteFunction.InitializeAsync(this),
				PTMngVSIX.Commands.F1FunctionCode.C1040_SummaryFunction.InitializeAsync(this),
				PTMngVSIX.Commands.F1FunctionCode.C1050_OptimizeFunction.InitializeAsync(this),
				PTMngVSIX.Commands.F1FunctionCode.C1060_ExplainFunction.InitializeAsync(this),

				PTMngVSIX.Commands.F2FunctionCode.C2000_DocsSelection.InitializeAsync(this),
				PTMngVSIX.Commands.F2FunctionCode.C2001_DocsFunction.InitializeAsync(this),
				PTMngVSIX.Commands.F2FunctionCode.C2002_DocsClass.InitializeAsync(this),
				PTMngVSIX.Commands.F2FunctionCode.C2010_DocsApi.InitializeAsync(this),
				PTMngVSIX.Commands.F2FunctionCode.C2020_DocsTechnicalSpecifications.InitializeAsync(this),

				PTMngVSIX.Commands.F3FixBug.C3010_ExplainError.InitializeAsync(this),
				PTMngVSIX.Commands.F3FixBug.C3020_SuggestFixes.InitializeAsync(this),

				PTMngVSIX.Commands.F4Test.C4010_Whitebox.InitializeAsync(this),
				PTMngVSIX.Commands.F4Test.C4020_Blackbox_UnitTests.InitializeAsync(this),
				PTMngVSIX.Commands.F4Test.C4021_Blackbox_IntegrationTests.InitializeAsync(this),
				PTMngVSIX.Commands.F4Test.C4022_Blackbox_EdgeCaseTesting.InitializeAsync(this),
				PTMngVSIX.Commands.F4Test.C4023_Blackbox_PerformanceTesting.InitializeAsync(this),

				PTMngVSIX.Commands.F5Architect.C5020_ReviewCodeFunction.InitializeAsync(this),
				PTMngVSIX.Commands.F5Architect.C5021_ReviewCodeClass.InitializeAsync(this),
				PTMngVSIX.Commands.F5Architect.C5030_SuggestSolution.InitializeAsync(this),
				PTMngVSIX.Commands.F5Architect.C5040_SuggestDeploy.InitializeAsync(this),
				PTMngVSIX.Commands.F5Architect.C5050_SuggestArchitecture.InitializeAsync(this),
				PTMngVSIX.Commands.F5Architect.C5060_SuggestTechnologies.InitializeAsync(this),
				PTMngVSIX.Commands.F5Architect.C5070_SuggestFeatures.InitializeAsync(this),

				PTMngVSIX.Commands.F6Chat.C6010_AddCodeSnippet.InitializeAsync(this),
				PTMngVSIX.Commands.F6Chat.C6011_ResetCodeSnippet.InitializeAsync(this),

				PTMngVSIX.ToolWindow.PTMngChatCommand.InitializeAsync(this),
			};

			await Task.WhenAll(commandTasks);
		}

		#endregion

		private void LoadFromOptionPage()
		{
			var optionPage = (PTMngOptionPage)GetDialogPage(typeof(PTMngOptionPage));
			ModelSetting.UseInternet = optionPage.UseInternet;

			if (optionPage.UseInternet)
			{
				ModelSetting.Endpoint = optionPage.OnlineEndpoint;
				ModelSetting.ApiKey = optionPage.OnlineApiKey;
				ModelSetting.AssistantModelName = optionPage.OnlineAssistantModelName;
				ModelSetting.TranslatorModelName = optionPage.OnlineTranslatorModelName;

				AppState.Assistant = OnlineOpenRouterAI.DeepseekService.Instance;
				AppState.Translator = OnlineOpenRouterAI.GemmaService.Instance;
			}
			else
			{
				ModelSetting.Endpoint = optionPage.LocalEndpoint;
				ModelSetting.AssistantModelName = optionPage.LocalAssistantModelName;
				ModelSetting.TranslatorModelName = optionPage.LocalTranslatorModelName;

				AppState.Assistant = LocalOllama.MistralService.Instance;
				AppState.Translator = LocalOllama.GemmaService.Instance;
			}
		}

		private void LoadFromRegStorage()
		{
			var pageName = Utils.Setting.Constant.OptionPageGeneral;
			ModelSetting.RoleName = RegStorage.GetValue(pageName, nameof(PTMngOptionPage.RoleName));
			ModelSetting.OutputFormat = RegStorage.GetValue(pageName, nameof(PTMngOptionPage.OutputFormat));
			ModelSetting.TranslateInput = RegStorage.GetBool(pageName, nameof(PTMngOptionPage.TranslateInput));
			ModelSetting.TranslateOutput = RegStorage.GetBool(pageName, nameof(PTMngOptionPage.TranslateOutput));
			ModelSetting.OutputLanguage = RegStorage.GetValue(pageName, nameof(PTMngOptionPage.OutputLanguage));
		}
	}
}
