using EnvDTE;
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
	internal sealed class C1002_GenerateClass : CommandBase<C1002_GenerateClass>
	{
		public override int CommandId { get; protected set; } = CommandIds.C1002;
		public override string CommandText { get; protected set; } = Resource.Lang.ContextMenu.Button_C1002_GenerateClass;

		public C1002_GenerateClass(AsyncPackage package, OleMenuCommandService commandService)
			: base(package, commandService)
		{
		}

		protected override async Task ExecuteAsync(object sender, EventArgs e)
		{
			await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

			// Hiển thị hộp nhập liệu
			string prompt = await InputDialog.ShowInputDialogAsync(
				Resource.Lang.Input.Input_Generate_Code,
				Resource.Lang.Input.Input_EnterDescription,
				"");

			if (string.IsNullOrWhiteSpace(prompt))
				return;

			var message = new Utils.Chat.Message
			{
				Task = Data.Constant.TaskName.TaskF1.GenerateClass,
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
				await EditorAction.Insert_To_Parent_Kind_Async(message, response, vsCMElement.vsCMElementClass, EditorModel.InsertNodePosition.BeforeNode);
				await EditCommand.FormatDocumentAsync();
			}
		}
	}
}
