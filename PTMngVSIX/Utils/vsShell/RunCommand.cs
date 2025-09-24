using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using System;
using System.Threading.Tasks;

namespace PTMngVSIX.Utils.vsShell
{
	internal static class RunCommand
	{
		internal static Task ExecutePTMngCommandAsync(uint commandId)
		{
			return ExecuteCommandAsync(Utils.Setting.Constant.PTMngCommandSet, commandId);
		}

		internal static async Task ExecuteCommandAsync(Guid commandGroup, uint commandId)
		{
			await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

			var uiShell = Package.GetGlobalService(typeof(SVsUIShell)) as IVsUIShell;
			if (uiShell != null)
			{
				var guidGroup = commandGroup;
				uiShell.PostExecCommand(
					ref guidGroup,
					commandId,
					0, // nCmdExecOpt
					IntPtr.Zero);
			}
		}
	}
}
