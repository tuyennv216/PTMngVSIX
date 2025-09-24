using Microsoft.VisualStudio.Shell;
using PTMngVSIX.Abstraction;
using PTMngVSIX.Utils.Chat;
using PTMngVSIX.Utils.Doc;
using PTMngVSIX.Utils.Editor;
using System;
using Task = System.Threading.Tasks.Task;

namespace PTMngVSIX.Commands.F6Chat
{
	internal sealed class C6031_AddActiveFunction : CommandBase<C6031_AddActiveFunction>
	{
		public override int CommandId { get; protected set; } = CommandIds.C6031;
		public override string CommandText { get; protected set; } = Resource.Lang.ContextMenu.Button_C6031_AddActiveFunction;

		public C6031_AddActiveFunction(AsyncPackage package, OleMenuCommandService commandService)
			: base(package, commandService)
		{
		}

		protected override async Task ExecuteAsync(object sender, EventArgs e)
		{
			await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

			var item = new EditorItem();
			item.Initial();

			var functionText = await DocView.GetParentTextAsync(EnvDTE.vsCMElement.vsCMElementFunction, item);
			await ChatService.Instance.AddCodeSnippetAsync(functionText).ConfigureAwait(false);
		}
	}
}
