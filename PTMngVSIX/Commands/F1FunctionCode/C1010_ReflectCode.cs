using Microsoft.VisualStudio.Shell;
using PTMngVSIX.Abstraction;
using PTMngVSIX.Utils.Dialog;
using PTMngVSIX.Utils.Doc;
using PTMngVSIX.Utils.IDECommand;
using PTMngVSIX.Utils.Editor;
using System;
using Task = System.Threading.Tasks.Task;

namespace PTMngVSIX.Commands.F1FunctionCode
{
	internal sealed class C1010_ReflectCode : CommandBase<C1010_ReflectCode>
	{
		public override int CommandId { get; protected set; } = CommandIds.C1010;
		public override string CommandText { get; protected set; } = Resource.Lang.ContextMenu.Button_C1003_ReflectCode;

		public C1010_ReflectCode(AsyncPackage package, OleMenuCommandService commandService)
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
				Resource.Lang.Input.Input_Modify_Code,
				Resource.Lang.Input.Input_EnterDescription,
				"");

			if (string.IsNullOrWhiteSpace(prompt))
				return;

			var message = new Utils.Chat.Message
			{
				Task = Data.Constant.TaskName.TaskF1.ReflectCode,
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
				await EditorAction.Insert_To_Selection_Async(message, response, EditorModel.InsertSelectionPosition.AfterLine);
				await EditCommand.FormatDocumentAsync();
			}
		}
	}
}
