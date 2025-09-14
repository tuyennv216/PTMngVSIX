using Microsoft.VisualStudio.Shell;
using PTMngVSIX.Abstraction;
using PTMngVSIX.Utils.Dialog;
using System;
using Task = System.Threading.Tasks.Task;

namespace PTMngVSIX.Commands.F5Architect
{
	internal sealed class C5030_SuggestSolution : CommandBase<C5030_SuggestSolution>
	{
		public override int CommandId { get; protected set; } = 5030;
		public override string CommandText { get; protected set; } = Resource.Lang.ContextMenu.Button_C5030_SuggestSolution;

		public C5030_SuggestSolution(AsyncPackage package, OleMenuCommandService commandService)
			: base(package, commandService)
		{
		}

		protected override async Task ExecuteAsync(object sender, EventArgs e)
		{
			await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

			// Hiển thị hộp nhập liệu
			string prompt = await InputDialog.ShowInputDialogAsync(
				Resource.Lang.Input.Input_Suggest_Solution,
				Resource.Lang.Input.Input_Solution,
				"");

			var message = new Utils.Chat.Message
			{
				Task = Data.Constant.TaskName.TaskF5.SuggestSolution,
				Prompt = prompt,
			};
			message.Option.IncludeSolutionStructure = true;

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
