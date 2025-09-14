using EnvDTE;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.Shell.TableControl;
using Microsoft.VisualStudio.Shell.TableManager;
using PTMngVSIX.Utils.Model;
using System.Threading.Tasks;

namespace PTMngVSIX.Utils.Doc
{
	internal class ErrorView
	{
		internal static async Task<ErrorViewModel> GetErrorMessageAsync()
		{
			await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

			var dte = (DTE)Package.GetGlobalService(typeof(DTE));

			// Lấy IErrorList service
			var errorList = Package.GetGlobalService(typeof(SVsErrorList)) as IErrorList;
			var tableControl = errorList.TableControl;

			// Lấy thông tin lỗi
			ITableEntryHandle selectedEntry = tableControl.SelectedEntry;
			selectedEntry.TryGetValue(StandardTableKeyNames.ErrorCode, out string errorCode);
			selectedEntry.TryGetValue(StandardTableKeyNames.Text, out string message);
			selectedEntry.TryGetValue(StandardTableKeyNames.DocumentName, out string fileName);
			selectedEntry.TryGetValue(StandardTableKeyNames.Line, out int lineNumber);
			selectedEntry.TryGetValue(StandardTableKeyNames.Column, out int columnNumber);

			var result = new ErrorViewModel
			{
				ErrorCode = errorCode,
				Message = message,
				FileName = fileName,
				LineNumber = lineNumber + 1, // Chuyển từ 0-based sang 1-based
				ColumnNumber = columnNumber + 1 // Chuyển từ 0-based sang 1-based
			};

			return result;
		}
	}
}
