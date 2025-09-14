using Microsoft.VisualStudio.Shell;
using PTMngVSIX.Abstraction;
using PTMngVSIX.Utils.Dialog;
using PTMngVSIX.Utils.Doc;
using PTMngVSIX.Utils.DTECommand;
using System;
using Task = System.Threading.Tasks.Task;

namespace PTMngVSIX.Commands.F1FunctionCode
{
	internal sealed class C1005_AddComment : CommandBase<C1005_AddComment>
	{
		public override int CommandId { get; protected set; } = 1005;
		public override string CommandText { get; protected set; } = Resource.Lang.ContextMenu.Button_C1005_AddComment;

		public C1005_AddComment(AsyncPackage package, OleMenuCommandService commandService)
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
				Task = Data.Constant.TaskName.TaskF1.AddComment,
				Prompt = prompt,
			};

			if (await DocView.HasSelectedTextAsync())
			{
				message.Option.IncludeSelection = true;
			}
			else
			{
				message.Option.IncludeParentFunction = true;
			}

			var response = await Utils.Chat.ChatService.Instance.SendAsync(message);

			if (response.Type == "Unknow" || response.Type == "Error")
			{
				await TextDialog.ShowTextDialogAsync(response.Type, response.Answer);
			}
			else
			{
				await DocEdit.Insert_After_Parent_Kind_Async(message, response, EnvDTE.vsCMElement.vsCMElementFunction);
				await EditCommand.FormatDocumentAsync();
			}
		}
	}
}
