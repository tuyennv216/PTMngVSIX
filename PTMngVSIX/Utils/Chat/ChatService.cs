using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using PTMngVSIX.Abstraction.AIServices.RequestModel;
using PTMngVSIX.Abstraction.AIServices.ResponseModel;
using PTMngVSIX.Setting;
using PTMngVSIX.ToolWindow;
using PTMngVSIX.Utils.Doc;
using PTMngVSIX.Utils.SystemIO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PTMngVSIX.Utils.Chat
{
	internal class ChatService : INotifyPropertyChanged
	{
		public static ChatService Instance = new();
		private ChatService() { }

		//public List<string> ActiveSnippets { get; private set; } = new();
		//public List<string> ActiveFiles { get; private set; } = new();

		private ObservableCollection<string> _activeSnippets;
		public ObservableCollection<string> ActiveSnippets
		{
			get => _activeSnippets ??= new ObservableCollection<string>();
			set
			{
				_activeSnippets = value;
				OnPropertyChanged(nameof(ActiveSnippets));
			}
		}

		private ObservableCollection<string> _activeFiles;
		public ObservableCollection<string> ActiveFiles
		{
			get => _activeFiles ??= new ObservableCollection<string>();
			set
			{
				_activeFiles = value;
				OnPropertyChanged(nameof(ActiveFiles));
			}
		}

		private List<string> ChatHistory = new();

		private RequestBase LastRequest = null;
		private ResponseBase LastResponse = null;

		public async Task<ResponseBase> SendAsync(Message message)
		{
			await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

			message.EditorItem.Initial();

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
			await ClearActiveFileAsync();

			var response = await AppState.Instance.Assistant.CallAsync(request);

			request.LastRequest = null;
			request.LastResponse = null;

			LastRequest = request;
			LastResponse = response;

			ChatHistory.Add(response.Summary);
			ChatHistory.Add(response.Solution);

			return response;
		}

		public async Task AddExecLogAsync(string log)
		{
			var chatControl = await GetChatControlAsync();
			chatControl?.AddExecLog(log);
		}

		public async Task AddCommentAsync(string comment)
		{
			var chatControl = await GetChatControlAsync();
			chatControl?.AddComment(comment);
		}

		// Code Snippet
		public async Task AddCodeSnippetAsync(string rawCode)
		{
			var code = rawCode.Trim().Trim('\r', '\n', ' ');
			if (string.IsNullOrEmpty(code) || ActiveSnippets.Contains(code)) return;

			ActiveSnippets.Add(code);

			var chatControl = await GetChatControlAsync();
			chatControl?.AddCodeSnippet(code);
		}

		public async Task ClearCodeSnippetAsync()
		{
			ActiveSnippets.Clear();
			var chatControl = await GetChatControlAsync();
			chatControl?.ResetCodeSnippet();
		}
		// End Code Snippet

		// Active File
		public async Task AddActiveFileAsync(string filePath)
		{
			if (string.IsNullOrEmpty(filePath) || ActiveFiles.Contains(filePath)) return;

			ActiveFiles.Add(filePath);

			var chatControl = await GetChatControlAsync();
			chatControl?.AddActiveFile(filePath);
		}

		public async Task ClearActiveFileAsync()
		{
			ActiveFiles.Clear();
			var chatControl = await GetChatControlAsync();
			chatControl?.ResetActiveFile();
		}
		// End Active File

		public async Task ClearChatTextAsync()
		{
			ActiveSnippets.Clear();
			var chatControl = await GetChatControlAsync();
			chatControl?.ResetChatText();
		}

		public async Task ResetChatAsync()
		{
			await ClearChatTextAsync();
			await ClearCodeSnippetAsync();
			await ClearActiveFileAsync();

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
				DocView.GetParentTextAsync(EnvDTE.vsCMElement.vsCMElementClass, message.EditorItem) :
				Task.FromResult(string.Empty);

			var functionTask = message.Option.IncludeParentFunction ?
				DocView.GetParentTextAsync(EnvDTE.vsCMElement.vsCMElementFunction, message.EditorItem) :
				Task.FromResult(string.Empty);

			var selectionTask = message.Option.IncludeSelection ?
				DocView.GetSelectedTextAsync() :
				Task.FromResult(string.Empty);

			var fimTask = message.Option.IncludeSelection ?
				DocView.GetParentFIMTextAsync(EnvDTE.vsCMElement.vsCMElementFunction, message.EditorItem) :
				Task.FromResult(string.Empty);

			var errorLineTask = message.Option.IncludeError ? DocView.GetLineTextAtAsync(message.Error.LineNumber) : Task.FromResult(string.Empty);

			var activeFilesTask = IOCommand.GetFileContentsAsync(ActiveFiles, true);

			// Đợi tất cả hoàn thành đồng thời
			await Task.WhenAll(
				languageTask, solutionStructureTask, projectStructureTask,
				documentTask, classTask, functionTask, errorLineTask, activeFilesTask);

			var sb = new StringBuilder();

			var lang = await languageTask;
			if (lang.Length > 0 &&
				(message.Option.IncludeDocumentLanguage || message.Option.HasInclude))
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

			if (ActiveFiles.Count > 0)
			{
				var activeFilesResult = await activeFilesTask;
				foreach (var activeFile in activeFilesResult)
				{
					sb.AppendLine($"Here is the content of the file: {activeFile.Name}");
					sb.AppendLine("```");
					sb.AppendLine(activeFile.Content);
					sb.AppendLine("```");
				}
			}

			if (message.Option.IncludeActiveDocument)
			{
				sb.AppendLine("Here is the active document:");
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

			if (message.Option.IncludeFIM)
			{
				sb.AppendLine("This is the missing function code:");
				sb.AppendLine("```");
				sb.AppendLine(await fimTask);
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

		public event PropertyChangedEventHandler PropertyChanged;
		protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
