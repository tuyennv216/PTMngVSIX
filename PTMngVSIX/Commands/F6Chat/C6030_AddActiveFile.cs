using Microsoft.VisualStudio.Shell;
using PTMngVSIX.Abstraction;
using PTMngVSIX.Utils.Chat;
using PTMngVSIX.Utils.Doc;
using System;
using Task = System.Threading.Tasks.Task;

namespace PTMngVSIX.Commands.F6Chat
{
	internal sealed class C6030_AddActiveFile : CommandBase<C6030_AddActiveFile>
	{
		public override int CommandId { get; protected set; } = CommandIds.C6030;
		public override string CommandText { get; protected set; } = Resource.Lang.ContextMenu.Button_C6030_AddActiveFile;

		public C6030_AddActiveFile(AsyncPackage package, OleMenuCommandService commandService)
			: base(package, commandService)
		{
		}

		protected override async Task ExecuteAsync(object sender, EventArgs e)
		{
			await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

			var documentText = await DocView.GetDocumentTextAsync();
			await ChatService.Instance.AddCodeSnippetAsync(documentText).ConfigureAwait(false);
		}
	}
}
