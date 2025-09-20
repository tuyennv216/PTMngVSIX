using Microsoft.VisualStudio.Shell;
using PTMngVSIX.Abstraction;
using PTMngVSIX.Utils.Dialog;
using PTMngVSIX.Utils.Doc;
using System;
using System.ComponentModel.Composition;
using Task = System.Threading.Tasks.Task;

namespace PTMngVSIX.Commands.F3FixBug
{
	[Export(typeof(C3010_ExplainError))]
	internal sealed class C3010_ExplainError : CommandBase<C3010_ExplainError>
	{
		public override int CommandId { get; protected set; } = 3010;
		public override string CommandText { get; protected set; } = Resource.Lang.ContextMenu.Button_C3010_ExplainError;

		public C3010_ExplainError(AsyncPackage package, OleMenuCommandService commandService)
			: base(package, commandService)
		{
		}

		protected override async Task ExecuteAsync(object sender, EventArgs e)
		{
			await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

			var errorModel = await ErrorView.GetErrorMessageAsync();
			
			var validate = await DocValidate.ValidateActiveDocumentFileAsync(errorModel.FileName);
			if (!validate) return;


			var message = new Utils.Chat.Message
			{
				Task = Data.Constant.TaskName.TaskF3.ExplainError,
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
				await TextDialog.ShowTextDialogAsync("Giải thích lỗi", response.Answer);
			}
		}
	}
}
