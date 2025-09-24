using Microsoft.VisualStudio.Shell;
using PTMngVSIX.Abstraction;
using PTMngVSIX.Utils.Chat;
using PTMngVSIX.Utils.Doc;
using System;
using Task = System.Threading.Tasks.Task;

namespace PTMngVSIX.Commands.F6Chat
{
	internal sealed class C6011_ResetChat : CommandBase<C6011_ResetChat>
	{
		public override int CommandId { get; protected set; } = CommandIds.C6011;
		public override string CommandText { get; protected set; } = Resource.Lang.ContextMenu.Button_C6011_ResetChat;

		public C6011_ResetChat(AsyncPackage package, OleMenuCommandService commandService)
			: base(package, commandService)
		{
		}

		protected override void BeforeRenderMenu(OleMenuCommand menu)
		{
			menu.Visible = menu.Visible && ChatService.Instance.ActiveSnippets.Count > 0;
		}

		protected override async Task ExecuteAsync(object sender, EventArgs e)
		{
			await ChatService.Instance.ResetChatAsync().ConfigureAwait(false);
		}
	}
}
