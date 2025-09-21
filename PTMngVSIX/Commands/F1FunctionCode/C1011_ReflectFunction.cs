using Microsoft.VisualStudio.Shell;
using PTMngVSIX.Abstraction;
using PTMngVSIX.Utils.Dialog;
using PTMngVSIX.Utils.Doc;
using PTMngVSIX.Utils.DTECommand;
using PTMngVSIX.Utils.Editor;
using System;
using Task = System.Threading.Tasks.Task;

namespace PTMngVSIX.Commands.F1FunctionCode
{
	internal sealed class C1011_ReflectFunction : CommandBase<C1011_ReflectFunction>
	{
		public override int CommandId { get; protected set; } = 1011;
		public override string CommandText { get; protected set; } = Resource.Lang.ContextMenu.Button_C1003_ReflectCode;

		public C1011_ReflectFunction(AsyncPackage package, OleMenuCommandService commandService)
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
				Task = Data.Constant.TaskName.TaskF1.ReflectFunction,
				Prompt = prompt,
			};
			message.Option.IncludeDocumentLanguage = true;
			message.Option.IncludeParentFunction = true;

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
