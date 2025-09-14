using Microsoft.VisualStudio.Shell;
using System.Threading.Tasks;

namespace PTMngVSIX.Utils.DTECommand
{
	internal class EditCommand
	{
		internal static async Task FormatDocumentAsync()
		{
			await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

			var dte = (EnvDTE.DTE)Package.GetGlobalService(typeof(EnvDTE.DTE));
			if (dte?.ActiveDocument != null)
			{
				if (dte.ActiveDocument.Type == "Text")
				{
					await Task.Delay(100);
					dte.ExecuteCommand("Edit.FormatDocument");
				}
			}
		}
	}
}
