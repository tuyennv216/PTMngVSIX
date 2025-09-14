using Microsoft.VisualStudio.Shell;
using PTMngVSIX.Abstraction;
using PTMngVSIX.Utils.Chat;
using PTMngVSIX.Utils.Doc;
using System;
using Task = System.Threading.Tasks.Task;

namespace PTMngVSIX.Commands.F6Chat
{
	internal sealed class C6010_AddCodeSnippet : CommandBase<C6010_AddCodeSnippet>
	{
		public override int CommandId { get; protected set; } = 6010;
		public override string CommandText { get; protected set; } = Resource.Lang.ContextMenu.Button_C6010_AddCodeSnippet;

		public C6010_AddCodeSnippet(AsyncPackage package, OleMenuCommandService commandService)
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

			var selectedText = await DocView.GetSelectedTextAsync();
			await ChatService.Instance.AddCodeSnippetAsync(selectedText);
		}
	}
}
