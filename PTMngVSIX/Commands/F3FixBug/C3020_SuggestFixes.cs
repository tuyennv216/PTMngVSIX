using Microsoft.VisualStudio.Shell;
using PTMngVSIX.Abstraction;
using PTMngVSIX.Utils.Dialog;
using PTMngVSIX.Utils.Doc;
using System;
using System.ComponentModel.Composition;
using Task = System.Threading.Tasks.Task;

namespace PTMngVSIX.Commands.F3FixBug
{
	[Export(typeof(C3020_SuggestFixes))]
	internal sealed class C3020_SuggestFixes : CommandBase<C3020_SuggestFixes>
	{
		public override int CommandId { get; protected set; } = CommandIds.C3020;
		public override string CommandText { get; protected set; } = Resource.Lang.ContextMenu.Button_C3020_SuggestFixes;

		public C3020_SuggestFixes(AsyncPackage package, OleMenuCommandService commandService)
			: base(package, commandService)
		{
		}

		protected override async Task ExecuteAsync(object sender, EventArgs e)
		{
			await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

			var errorModel = await ErrorView.GetErrorPanelSelectedAsync();

			var validate = await DocValidate.ValidateActiveDocumentFileAsync(errorModel.FileName);
			if (!validate) return;


			var message = new Utils.Chat.Message
			{
				Task = Data.Constant.TaskName.TaskF3.SuggestFixes,
			};
			message.Error = errorModel;
			message.Option.IncludeError = true;
			message.Option.IncludeParentFunction = true;

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
