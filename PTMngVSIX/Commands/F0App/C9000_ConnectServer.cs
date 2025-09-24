using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using PTMngVSIX.Abstraction;
using PTMngVSIX.Setting;
using System;
using Task = System.Threading.Tasks.Task;

namespace PTMngVSIX.Commands.F0App
{
	internal sealed class C9000_ConnectServer : CommandBase<C9000_ConnectServer>
	{
		public override int CommandId { get; protected set; } = CommandIds.C9000;
		public override string CommandText { get; protected set; } = Resource.Lang.ContextMenu.Button_C0000_Connect_Server;

		public C9000_ConnectServer(AsyncPackage package, OleMenuCommandService commandService)
			: base(package, commandService)
		{
		}

		protected override void BeforeRenderMenu(OleMenuCommand menu)
		{
			menu.Visible = !AppState.Instance.IsModelAvailable;
			menu.Enabled = !AppState.Instance.IsModelAvailable;
		}

		protected override async Task ExecuteAsync(object sender, EventArgs e)
		{
			await AppState.Instance.Assistant.TryConnectAsync();

			if (AppState.Instance.IsModelAvailable)
			{
				VsShellUtilities.ShowMessageBox(
					this.package,
					"Connected to endpoint service.",
					"Connection succeeded",
					OLEMSGICON.OLEMSGICON_INFO,
					OLEMSGBUTTON.OLEMSGBUTTON_OK,
					OLEMSGDEFBUTTON.OLEMSGDEFBUTTON_FIRST);
			}
			else
			{
				VsShellUtilities.ShowMessageBox(
					this.package,
					"Cannot connect to endpoint service.\nPlease check the configuration at:\nTools → Options → PTMng AI.",
					"Connection Failed",
					OLEMSGICON.OLEMSGICON_WARNING,
					OLEMSGBUTTON.OLEMSGBUTTON_OK,
					OLEMSGDEFBUTTON.OLEMSGDEFBUTTON_FIRST);
			}
		}
	}
}
