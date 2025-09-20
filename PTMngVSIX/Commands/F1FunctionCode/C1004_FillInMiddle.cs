using Microsoft.VisualStudio.Shell;
using PTMngVSIX.Abstraction;
using PTMngVSIX.Utils.Dialog;
using PTMngVSIX.Utils.Doc;
using PTMngVSIX.Utils.DTECommand;
using System;
using Task = System.Threading.Tasks.Task;

namespace PTMngVSIX.Commands.F1FunctionCode
{
	internal sealed class C1004_FillInMiddle : CommandBase<C1004_FillInMiddle>
	{
		public override int CommandId { get; protected set; } = 1004;
		public override string CommandText { get; protected set; } = Resource.Lang.ContextMenu.Button_C1004_FillInMiddle;

		public C1004_FillInMiddle(AsyncPackage package, OleMenuCommandService commandService)
			: base(package, commandService)
		{
		}

		protected override async Task ExecuteAsync(object sender, EventArgs e)
		{
			await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

			// Hiển thị hộp nhập liệu
			string prompt = await InputDialog.ShowInputDialogAsync(
				Resource.Lang.Input.Input_Add,
				Resource.Lang.Input.Input_EnterDescription,
				"");

			var message = new Utils.Chat.Message
			{
				Task = Data.Constant.TaskName.TaskF1.FillInMiddle,
				Prompt = prompt,
			};
			message.Option.IncludeDocumentLanguage = true;
			message.Option.IncludeFillInMiddle = true;

			var response = await Utils.Chat.ChatService.Instance.SendAsync(message);

			if (response.Type == "Unknow" || response.Type == "Error")
			{
				await TextDialog.ShowTextDialogAsync(response.Type, response.Answer).ConfigureAwait(false);
			}
			else
			{
				await DocEdit.Insert_After_Selection_Async(message, response);
				await EditCommand.FormatDocumentAsync();
			}
		}
	}
}
