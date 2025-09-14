using Microsoft.VisualStudio.Shell;
using PTMngVSIX.Setting;
using System;
using System.ComponentModel.Design;
using System.Threading.Tasks;

namespace PTMngVSIX.Abstraction
{
	internal abstract class CommandBase<T> where T : CommandBase<T>
	{
		public readonly Guid CommandSet = Utils.Setting.Constant.PTMngCommandSet;
		public abstract int CommandId { get; protected set; }
		public abstract string CommandText { get; protected set; }

		protected readonly AsyncPackage package; // ServiceProvider

		protected CommandBase(AsyncPackage package, OleMenuCommandService commandService)
		{
			this.package = package;

			var menuCommandID = new CommandID(CommandSet, CommandId);
			var menuItem = new OleMenuCommand(OnExecute, menuCommandID);
			menuItem.BeforeQueryStatus += BeforeQueryStatus;
			menuItem.Text = CommandText;
			commandService.AddCommand(menuItem);
		}

		public static T Instance { get; private set; }

		protected virtual void BeforeRenderMenu(OleMenuCommand menu) { }

		private void OnExecute(object sender, EventArgs e)
		{
			_ = ExecuteAsync(sender, e);
		}

		private void BeforeQueryStatus(object sender, EventArgs e)
		{
			var menu = sender as OleMenuCommand;
			menu.Visible = AppState.IsModelAvailable;
			menu.Enabled = true;

			BeforeRenderMenu(menu);
		}

		public static async Task InitializeAsync(AsyncPackage package)
		{
			await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync(package.DisposalToken);
			var commandService = await package.GetServiceAsync(typeof(IMenuCommandService)) as OleMenuCommandService;

			Instance = (T)Activator.CreateInstance(typeof(T), package, commandService);
		}

		protected abstract Task ExecuteAsync(object sender, EventArgs e);
	}
}
