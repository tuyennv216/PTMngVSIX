using Microsoft.VisualStudio.Shell;
using PTMngVSIX.Abstraction;
using PTMngVSIX.Utils.Dialog;
using PTMngVSIX.Utils.Doc;
using PTMngVSIX.Utils.DTECommand;
using System;
using Task = System.Threading.Tasks.Task;

namespace PTMngVSIX.Commands.F1FunctionCode
{
	internal sealed class C1000_GenerateCode : CommandBase<C1000_GenerateCode>
	{
		public override int CommandId { get; protected set; } = 1000;
		public override string CommandText { get; protected set; } = Resource.Lang.ContextMenu.Button_C1000_GenerateCode;

		public C1000_GenerateCode(AsyncPackage package, OleMenuCommandService commandService)
			: base(package, commandService)
		{
		}

		protected override void BeforeRenderMenu(OleMenuCommand menu)
		{
			var jtf = PTMngVSIXPackage.JoinableTaskContext.Factory;
			var hasSelection = jtf.Run(async () =>
			{
				return await DocView.CachedHasSelectedText.GetAsync();
			});

			menu.Enabled = menu.Enabled && hasSelection;
		}

		protected override async Task ExecuteAsync(object sender, EventArgs e)
		{
			await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

			// Hiển thị hộp nhập liệu
			string prompt = await InputDialog.ShowInputDialogAsync(
				Resource.Lang.Input.Input_Generate,
				Resource.Lang.Input.Input_EnterDescription,
				"");

			var message = new Utils.Chat.Message
			{
				Task = Data.Constant.TaskName.TaskF1.GenerateCode,
				Prompt = prompt,
			};
			message.Option.IncludeDocumentLanguage = true;
			message.Option.IncludeSelection = true;

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
