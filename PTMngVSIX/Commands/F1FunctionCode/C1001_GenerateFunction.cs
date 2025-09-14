using EnvDTE;
using Microsoft.VisualStudio.Shell;
using PTMngVSIX.Abstraction;
using PTMngVSIX.Utils.Dialog;
using PTMngVSIX.Utils.Doc;
using PTMngVSIX.Utils.DTECommand;
using System;
using Task = System.Threading.Tasks.Task;

namespace PTMngVSIX.Commands.F1FunctionCode
{
	internal sealed class C1001_GenerateFunction : CommandBase<C1001_GenerateFunction>
	{
		public override int CommandId { get; protected set; } = 1001;
		public override string CommandText { get; protected set; } = Resource.Lang.ContextMenu.Button_C1001_GenerateFunction;

		public C1001_GenerateFunction(AsyncPackage package, OleMenuCommandService commandService)
			: base(package, commandService)
		{
		}

		protected override async Task ExecuteAsync(object sender, EventArgs e)
		{
			await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

			// Hiển thị hộp nhập liệu
			string prompt = await InputDialog.ShowInputDialogAsync(
				Resource.Lang.Input.Input_Generate_Function,
				Resource.Lang.Input.Input_EnterDescription,
				"");

			if (string.IsNullOrWhiteSpace(prompt))
				return;

			var message = new Utils.Chat.Message
			{
				Task = Data.Constant.TaskName.TaskF1.GenerateFunction,
				Prompt = prompt,
			};
			message.Option.IncludeDocumentLanguage = true;
			message.Option.IncludeSelection = true;

			var response = await Utils.Chat.ChatService.Instance.SendAsync(message);

			if (response.Type == "Unknow" || response.Type == "Error")
			{
				await TextDialog.ShowTextDialogAsync(response.Type, response.Answer);
			}
			else
			{
				await DocEdit.Insert_After_Parent_Kind_Async(message, response, vsCMElement.vsCMElementFunction);
				await EditCommand.FormatDocumentAsync();
			}
		}
	}
}
