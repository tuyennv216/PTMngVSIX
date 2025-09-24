using Microsoft.VisualStudio.Shell;
using PTMngVSIX.Abstraction;
using PTMngVSIX.Utils.Dialog;
using System;
using Task = System.Threading.Tasks.Task;

namespace PTMngVSIX.Commands.F1FunctionCode
{
	internal sealed class C1060_ExplainFunction : CommandBase<C1060_ExplainFunction>
	{
		public override int CommandId { get; protected set; } = CommandIds.C1060;
		public override string CommandText { get; protected set; } = Resource.Lang.ContextMenu.Button_C1060_ExplainFunction;

		public C1060_ExplainFunction(AsyncPackage package, OleMenuCommandService commandService)
			: base(package, commandService)
		{
		}

		protected override async Task ExecuteAsync(object sender, EventArgs e)
		{
			await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

			var message = new Utils.Chat.Message
			{
				Task = Data.Constant.TaskName.TaskF1.ExplainFunction,
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
