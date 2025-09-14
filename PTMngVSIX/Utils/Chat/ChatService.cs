using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using PTMngVSIX.Abstraction.RequestModel;
using PTMngVSIX.Abstraction.ResponseModel;
using PTMngVSIX.Setting;
using PTMngVSIX.ToolWindow;
using PTMngVSIX.Utils.Doc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTMngVSIX.Utils.Chat
{
	internal class ChatService
	{
		public static ChatService Instance = new ChatService();
		private ChatService() { }

		public List<string> ActiveSnippets { get; private set; } = new List<string>();
		private List<string> ChatHistory = new List<string>();

		private RequestBase LastRequest = null;
		private ResponseBase LastResponse = null;

		public async Task<ResponseBase> SendAsync(Message message)
		{
			message.TrackingPoint = DocView.GetCurrentTrackingPoint();

			await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

			var request = new RequestBase
			{
				Task = message.Task,
				Information = await GetMessageInformationAsync(message),
				Prompt = message.Prompt,

				History = ChatHistory,
				LastRequest = LastRequest,
				LastResponse = LastResponse
			};

			await ClearCodeSnippetAsync();

			var response = await AppState.Assistant.Call(request);

			LastRequest = request;
			LastResponse = response;

			ChatHistory.Add(response.Summary);
			ChatHistory.Add(response.Solution);

			return response;
		}

		public async Task AddCodeSnippetAsync(string rawCode)
		{
			var code = rawCode.Trim().Trim('\r', '\n', ' ');
			if (string.IsNullOrEmpty(code) || ActiveSnippets.Contains(code)) return;

			ActiveSnippets.Add(code);

			var chatControl = await GetChatControlAsync();
			chatControl?.AddCodeSnippet(code);
		}

		public async Task ClearChatTextAsync()
		{
			ActiveSnippets.Clear();
			var chatControl = await GetChatControlAsync();
			chatControl?.ResetChatText();
		}

		public async Task ClearCodeSnippetAsync()
		{
			ActiveSnippets.Clear();
			var chatControl = await GetChatControlAsync();
			chatControl?.ResetCodeSnippet();
		}

		public async Task ResetChatAsync()
		{
			await ClearChatTextAsync();
			await ClearCodeSnippetAsync();

			ChatHistory.Clear();
			LastRequest = null;
			LastResponse = null;
		}

		public async Task<PTMngChatControl> GetChatControlAsync()
		{
			await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

			var uiShell = PTMngVSIXPackage.GetGlobalService(typeof(SVsUIShell)) as IVsUIShell;
			var PTMngChatGuid = typeof(PTMngChat).GUID;
			IVsWindowFrame chatWindowFrame = null;
			int result = uiShell.FindToolWindow((uint)0, ref PTMngChatGuid, out chatWindowFrame);
			if (ErrorHandler.Succeeded(result) && chatWindowFrame != null)
			{
				object chatFrameContent;
				chatWindowFrame.GetProperty((int)__VSFPROPID.VSFPROPID_DocView, out chatFrameContent);
				if (chatFrameContent is PTMngChat pane)
				{
					var chatControl = pane.Content as PTMngChatControl;
					return chatControl;
				}
			}

			return null;
		}

		private async Task<string> GetMessageInformationAsync(Message message)
		{
			var allSnippets = message.Snippets.Concat(ActiveSnippets).ToList();
			ActiveSnippets.Clear();

			// Khởi chạy các task đồng thời (nếu điều kiện đúng)
			var languageTask = DocView.GetDocumentLanguageAsync();
			var solutionStructureTask = message.Option.IncludeSolutionStructure ?
				Print.PrintStructure.GetSolutionStructureAsync() :
				Task.FromResult(string.Empty);

			var projectStructureTask = message.Option.IncludeProjectStructure ?
				Print.PrintStructure.GetProjectStructureAsync() :
				Task.FromResult(string.Empty);

			var documentTask = message.Option.IncludeActiveDocument ?
				DocView.GetDocumentTextAsync() :
				Task.FromResult(string.Empty);

			var classTask = message.Option.IncludeParentClass ?
				DocView.GetParentTextByKindTrackingPointAsync(EnvDTE.vsCMElement.vsCMElementClass, message.TrackingPoint) :
				Task.FromResult(string.Empty);

			var functionTask = message.Option.IncludeParentFunction ?
				DocView.GetParentTextByKindTrackingPointAsync(EnvDTE.vsCMElement.vsCMElementFunction, message.TrackingPoint) :
				Task.FromResult(string.Empty);

			var selectionTask = message.Option.IncludeSelection ?
				DocView.GetSelectedTextAsync() :
				Task.FromResult(string.Empty);

			var errorLineTask = message.Option.IncludeError ? DocView.GetLineTextAtAsync(message.Error.LineNumber) : Task.FromResult(string.Empty);

			// Đợi tất cả hoàn thành đồng thời
			await Task.WhenAll(languageTask, solutionStructureTask, projectStructureTask, documentTask, classTask, functionTask, errorLineTask);

			var sb = new StringBuilder();

			var lang = await languageTask;
			if ((message.Option.IncludeDocumentLanguage || message.Option.HasInclude)
				&& lang.Length > 0)
			{
				sb.AppendLine("Document programming language is: " + lang);
			}

			if (message.Option.IncludeSolutionStructure)
			{
				sb.AppendLine("Here is the structure of the solution:");
				sb.AppendLine("```");
				sb.AppendLine(await solutionStructureTask);
				sb.AppendLine("```");
			}

			if (message.Option.IncludeProjectStructure)
			{
				sb.AppendLine("Here is the structure of the project:");
				sb.AppendLine("```");
				sb.AppendLine(await projectStructureTask);
				sb.AppendLine("```");
			}

			if (message.Option.IncludeActiveDocument)
			{
				sb.AppendLine("Here is some relevant document:");
				sb.AppendLine("```");
				sb.AppendLine(await documentTask);
				sb.AppendLine("```");
			}

			if (message.Option.IncludeParentClass)
			{
				sb.AppendLine("Here is the class code:");
				sb.AppendLine("```");
				sb.AppendLine(await classTask);
				sb.AppendLine("```");
			}

			if (message.Option.IncludeParentFunction)
			{
				sb.AppendLine("Here is the function code:");
				sb.AppendLine("```");
				sb.AppendLine(await functionTask);
				sb.AppendLine("```");
			}

			if (message.Option.IncludeError)
			{
				sb.AppendLine("Here is the error line:");
				sb.AppendLine("```");
				sb.AppendLine(await errorLineTask);
				sb.AppendLine("```");

				sb.AppendLine("Here is the error message:");
				sb.AppendLine("```");
				sb.AppendLine(message.Error.Message);
				sb.AppendLine("```");
			}

			if (message.Option.IncludeSelection)
			{
				allSnippets.Add(await selectionTask);
			}

			if (allSnippets.Count > 0)
			{
				sb.AppendLine("Here are some code snippets:");
				sb.AppendLine("```");
				sb.AppendLine(string.Join(Environment.NewLine + "---" + Environment.NewLine, allSnippets));
				sb.AppendLine("```");
			}

			var result = sb.ToString();
			return result;
		}
	}
}
