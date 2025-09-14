using Microsoft.VisualStudio.Shell;
using PTMngVSIX.Abstraction;
using PTMngVSIX.Utils.Dialog;
using System;
using Task = System.Threading.Tasks.Task;

namespace PTMngVSIX.Commands.F5Architect
{
	internal sealed class C5021_ReviewCodeClass : CommandBase<C5021_ReviewCodeClass>
	{
		public override int CommandId { get; protected set; } = 5021;
		public override string CommandText { get; protected set; } = Resource.Lang.ContextMenu.Button_C5021_ReviewCodeClass;

		public C5021_ReviewCodeClass(AsyncPackage package, OleMenuCommandService commandService)
			: base(package, commandService)
		{
		}

		protected override async Task ExecuteAsync(object sender, EventArgs e)
		{
			await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

			// Hiển thị hộp nhập liệu
			string prompt = await InputDialog.ShowInputDialogAsync(
				Resource.Lang.Input.Input_Request,
				Resource.Lang.Input.Input_EnterDescription,
				"");

			var message = new Utils.Chat.Message
			{
				Task = Data.Constant.TaskName.TaskF5.ReviewCodeClass,
				Prompt = prompt,
			};
			message.Option.IncludeParentClass = true;

			var response = await Utils.Chat.ChatService.Instance.SendAsync(message);

			if (response.Type == "Unknow" || response.Type == "Error")
			{
				await TextDialog.ShowTextDialogAsync(response.Type, response.Answer);
			}
			else
			{
				await TextDialog.ShowTextDialogAsync(Resource.Lang.Dialog.Dialog_Result, response.Answer);
			}
		}
	}
}
