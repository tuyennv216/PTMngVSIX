using Microsoft.VisualStudio.Shell;
using PTMngVSIX.Abstraction;
using PTMngVSIX.Utils.Chat;
using PTMngVSIX.Utils.Editor;
using System;
using Task = System.Threading.Tasks.Task;

namespace PTMngVSIX.Commands.F6Chat
{
	internal sealed class C6032_AddCurrentLine : CommandBase<C6032_AddCurrentLine>
	{
		public override int CommandId { get; protected set; } = CommandIds.C6032;
		public override string CommandText { get; protected set; } = Resource.Lang.ContextMenu.Button_C6032_AddCurrentLine;

		public C6032_AddCurrentLine(AsyncPackage package, OleMenuCommandService commandService)
			: base(package, commandService)
		{
		}

		protected override async Task ExecuteAsync(object sender, EventArgs e)
		{
			await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

			var item = new EditorItem();
			item.Initial();
			var lineText = item.Line.GetText();

			await ChatService.Instance.AddCodeSnippetAsync(lineText).ConfigureAwait(false);
		}
	}
}
