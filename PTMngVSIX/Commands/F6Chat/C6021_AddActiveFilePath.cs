using Microsoft.VisualStudio.Shell;
using PTMngVSIX.Abstraction;
using PTMngVSIX.Utils.Chat;
using System;
using Task = System.Threading.Tasks.Task;

namespace PTMngVSIX.Commands.F6Chat
{
	internal sealed class C6021_AddActiveFilePath : CommandBase<C6021_AddActiveFilePath>
	{
		public override int CommandId { get; protected set; } = 6021;
		public override string CommandText { get; protected set; } = Resource.Lang.ContextMenu.Button_C6020_AddFilePath;

		public C6021_AddActiveFilePath(AsyncPackage package, OleMenuCommandService commandService)
			: base(package, commandService)
		{
		}

		protected override async Task ExecuteAsync(object sender, EventArgs e)
		{
			await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

			try
			{
				var dte = Package.GetGlobalService(typeof(EnvDTE.DTE)) as EnvDTE.DTE;
				if (dte == null) return;

				var activeDoc = dte.ActiveDocument;
				if (activeDoc == null) return;

				string filePath = activeDoc.FullName;
				await ChatService.Instance.AddActiveFileAsync(filePath);
			}
			catch { }
		}
	}
}
