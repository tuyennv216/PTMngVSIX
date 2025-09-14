using Microsoft.VisualStudio.Shell;
using PTMngVSIX.Abstraction;
using PTMngVSIX.Utils.Dialog;
using PTMngVSIX.Utils.Doc;
using PTMngVSIX.Utils.DTECommand;
using System;
using System.ComponentModel.Composition;
using Task = System.Threading.Tasks.Task;

namespace PTMngVSIX.Commands.F1FunctionCode
{
	[Export(typeof(C1040_SummaryFunction))]
	internal sealed class C1040_SummaryFunction : CommandBase<C1040_SummaryFunction>
	{
		public override int CommandId { get; protected set; } = 1040;
		public override string CommandText { get; protected set; } = Resource.Lang.ContextMenu.Button_C1040_SummaryFunction;

		public C1040_SummaryFunction(AsyncPackage package, OleMenuCommandService commandService)
			: base(package, commandService)
		{
		}

		protected override async Task ExecuteAsync(object sender, EventArgs e)
		{
			await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

			var message = new Utils.Chat.Message
			{
				Task = Data.Constant.TaskName.TaskF1.SummaryFunction,
			};

			message.Option.IncludeParentFunction = true;

			var response = await Utils.Chat.ChatService.Instance.SendAsync(message);

			if (response.Type == "Unknow" || response.Type == "Error")
			{
				await TextDialog.ShowTextDialogAsync(response.Type, response.Answer);
			}
			else
			{
				await DocEdit.Insert_Before_Parent_Kind_Async(message, response, EnvDTE.vsCMElement.vsCMElementFunction);
				await EditCommand.FormatDocumentAsync();
			}
		}
	}
}
