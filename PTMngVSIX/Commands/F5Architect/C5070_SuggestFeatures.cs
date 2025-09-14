using Microsoft.VisualStudio.Shell;
using PTMngVSIX.Abstraction;
using PTMngVSIX.Utils.Dialog;
using System;
using Task = System.Threading.Tasks.Task;

namespace PTMngVSIX.Commands.F5Architect
{
	internal sealed class C5070_SuggestFeatures : CommandBase<C5070_SuggestFeatures>
	{
		public override int CommandId { get; protected set; } = 5070;
		public override string CommandText { get; protected set; } = Resource.Lang.ContextMenu.Button_C5070_SuggestFeatures;

		public C5070_SuggestFeatures(AsyncPackage package, OleMenuCommandService commandService)
			: base(package, commandService)
		{
		}

		protected override async Task ExecuteAsync(object sender, EventArgs e)
		{
			await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

			// Hiển thị hộp nhập liệu
			string prompt = await InputDialog.ShowInputDialogAsync(
				Resource.Lang.Input.Input_Suggest_Solution,
				Resource.Lang.Input.Input_Feature,
				"");

			var message = new Utils.Chat.Message
			{
				Task = Data.Constant.TaskName.TaskF5.SuggestFeatures,
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
