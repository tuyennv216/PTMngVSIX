using Microsoft.VisualStudio.Shell;
using PTMngVSIX.Abstraction;
using PTMngVSIX.Utils.Dialog;
using System;
using Task = System.Threading.Tasks.Task;

namespace PTMngVSIX.Commands.F5Architect
{
	internal sealed class C5020_ReviewCodeFunction : CommandBase<C5020_ReviewCodeFunction>
	{
		public override int CommandId { get; protected set; } = CommandIds.C5020;
		public override string CommandText { get; protected set; } = Resource.Lang.ContextMenu.Button_C5020_ReviewCodeFunction;

		public C5020_ReviewCodeFunction(AsyncPackage package, OleMenuCommandService commandService)
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
				Task = Data.Constant.TaskName.TaskF5.ReviewCodeFunction,
				Prompt = prompt,
			};
			message.Option.IncludeParentFunction = true;

			var response = await Utils.Chat.ChatService.Instance.SendAsync(message);

			if (response.Type == "Unknow" || response.Type == "Error")
			{
				await TextDialog.ShowTextDialogAsync(response.Type, response.Answer).ConfigureAwait(false);
			}
			else
			{
				await TextDialog.ShowTextDialogAsync(response.Summary, response.Answer).ConfigureAwait(false);
			}
		}
	}
}
