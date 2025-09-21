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
	internal sealed class C1006_CompleteFunction : CommandBase<C1006_CompleteFunction>
	{
		public override int CommandId { get; protected set; } = 1006;
		public override string CommandText { get; protected set; } = Resource.Lang.ContextMenu.Button_C1006_CompleteFunction;

		public C1006_CompleteFunction(AsyncPackage package, OleMenuCommandService commandService)
			: base(package, commandService)
		{
		}

		protected override async Task ExecuteAsync(object sender, EventArgs e)
		{
			await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

			var message = new Utils.Chat.Message
			{
				Task = Data.Constant.TaskName.TaskF1.CompleteFunction,
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
				await EditorAction.Insert_To_Parent_Kind_Async(message, response, EnvDTE.vsCMElement.vsCMElementFunction, EditorModel.InsertNodePosition.AfterNode);
				await EditCommand.FormatDocumentAsync();
			}
		}
	}
}
