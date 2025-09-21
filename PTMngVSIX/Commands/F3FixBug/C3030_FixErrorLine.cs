using Microsoft.VisualStudio.Shell;
using PTMngVSIX.Abstraction;
using PTMngVSIX.Utils.Dialog;
using PTMngVSIX.Utils.Doc;
using System;
using System.ComponentModel.Composition;
using Task = System.Threading.Tasks.Task;

namespace PTMngVSIX.Commands.F3FixBug
{
	[Export(typeof(C3030_FixErrorLine))]
	internal sealed class C3030_FixErrorLine : CommandBase<C3030_FixErrorLine>
	{
		public override int CommandId { get; protected set; } = 3030;
		public override string CommandText { get; protected set; } = Resource.Lang.ContextMenu.Button_C3030_FixErrorLine;

		public C3030_FixErrorLine(AsyncPackage package, OleMenuCommandService commandService)
			: base(package, commandService)
		{
		}

		protected override async Task ExecuteAsync(object sender, EventArgs e)
		{
			await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

			var errorModel = await ErrorView.GetErrorLineAsync();

			if (errorModel == null) return;

			var message = new Utils.Chat.Message
			{
				Task = Data.Constant.TaskName.TaskF3.FixErrorLine,
			};

			message.Error = errorModel;
			message.Option.IncludeParentFunction = true;
			message.Option.IncludeError = true;

			var response = await Utils.Chat.ChatService.Instance.SendAsync(message);

			if (response.Type == "Unknow" || response.Type == "Error")
			{
				await TextDialog.ShowTextDialogAsync(response.Type, response.Answer).ConfigureAwait(false);
			}
			else
			{
				await TextDialog.ShowTextDialogAsync("Gợi ý sửa lỗi", response.Answer);
			}
		}
	}
}
