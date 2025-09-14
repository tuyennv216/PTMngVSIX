using EnvDTE;
using Microsoft.VisualStudio.Shell;
using PTMngVSIX.Utils.Dialog;
using System;
using System.Threading.Tasks;

namespace PTMngVSIX.Utils.Doc
{
	internal class DocValidate
	{
		public static async Task<bool> ValidateActiveDocumentFileAsync(string fileName)
		{
			await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

			var dte = (DTE)Package.GetGlobalService(typeof(DTE));
			var activeDoc = dte.ActiveDocument;
			// Kiểm tra xem active document có phải là file input không
			if (!string.Equals(activeDoc.FullName, fileName, StringComparison.OrdinalIgnoreCase))
			{
				await MsgboxDialog.ShowMessageAsync(
					Resource.Lang.Validate.Incorrect,
					$"{Resource.Lang.Validate.Incorrect_ActiveDocument} {fileName}");

				return false;
			}
			return true;
		}

		public static async Task<bool> ValidateHasSelectionAsync()
		{
			await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

			if (!await DocView.HasSelectedTextAsync())
			{
				await MsgboxDialog.ShowMessageAsync(
					Resource.Lang.Validate.Invalid,
					Resource.Lang.Validate.Invalid_Snippet);
				return true;
			}
			return false;
		}
	}
}
