using Microsoft.VisualStudio.Shell;
using PTMngVSIX.Abstraction;
using PTMngVSIX.Utils.Dialog;
using PTMngVSIX.Utils.Doc;
using System;
using Task = System.Threading.Tasks.Task;

namespace PTMngVSIX.Commands.F2FunctionCode
{
	internal sealed class C2000_DocsSelection : CommandBase<C2000_DocsSelection>
	{
		public override int CommandId { get; protected set; } = 2000;
		public override string CommandText { get; protected set; } = Resource.Lang.ContextMenu.Button_C2000_DocsSelection;

		public C2000_DocsSelection(AsyncPackage package, OleMenuCommandService commandService)
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

			menu.Visible = menu.Visible && hasSelection;
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
				Task = Data.Constant.TaskName.TaskF2.GenerateDocsSelection,
				Prompt = prompt,
			};
			message.Option.IncludeSelection = true;

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
