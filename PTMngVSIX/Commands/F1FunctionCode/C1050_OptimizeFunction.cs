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
	[Export(typeof(C1050_OptimizeFunction))]
	internal sealed class C1050_OptimizeFunction : CommandBase<C1050_OptimizeFunction>
	{
		public override int CommandId { get; protected set; } = 1050;
		public override string CommandText { get; protected set; } = Resource.Lang.ContextMenu.Button_C1050_OptimizeFunction;

		public C1050_OptimizeFunction(AsyncPackage package, OleMenuCommandService commandService)
			: base(package, commandService)
		{
		}

		protected override async Task ExecuteAsync(object sender, EventArgs e)
		{
			await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

			var message = new Utils.Chat.Message
			{
				Task = Data.Constant.TaskName.TaskF1.OptimizeFunction,
			};

			message.Option.IncludeDocumentLanguage = true;
			message.Option.IncludeParentFunction = true;

			var response = await Utils.Chat.ChatService.Instance.SendAsync(message);

			if (response.Type == "Unknow" || response.Type == "Error")
			{
				await TextDialog.ShowTextDialogAsync(response.Type, response.Answer);
			}
			else
			{
				await DocEdit.Insert_After_Parent_Kind_Async(message, response, EnvDTE.vsCMElement.vsCMElementFunction);
				await EditCommand.FormatDocumentAsync();
			}
		}
	}
}
