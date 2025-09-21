using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.Shell.TableControl;
using Microsoft.VisualStudio.Shell.TableManager;
using PTMngVSIX.Utils.Editor;
using PTMngVSIX.Utils.Model;
using System.Linq;
using System.Threading.Tasks;

namespace PTMngVSIX.Utils.Doc
{
	internal class ErrorView
	{
		internal static async Task<ErrorViewModel> GetErrorPanelSelectedAsync()
		{
			await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

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

		internal static async Task<ErrorViewModel> GetErrorLineAsync()
		{
			await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

			var item = new EditorItem();
			item.Initial();
			item.UpdateSnapshot();

			// Lấy IErrorList service
			var errorList = Package.GetGlobalService(typeof(SVsErrorList)) as IErrorList;
			var tableControl = errorList.TableControl;

			// Lấy thông tin lỗi
			var errorFilePath = item.FilePath;
			var errorLineNumber = item.LineNumber;
			var entries = tableControl.Entries.Where(e =>
			{
				e.TryGetValue(StandardTableKeyNames.DocumentName, out string fileName);
				e.TryGetValue(StandardTableKeyNames.Line, out int lineNumber);

				var take = fileName == errorFilePath && lineNumber == errorLineNumber;
				return take;
			});

			if (entries.Any())
			{
				var entry = entries.First();
				entry.TryGetValue(StandardTableKeyNames.ErrorCode, out string errorCode);
				entry.TryGetValue(StandardTableKeyNames.Text, out string message);
				entry.TryGetValue(StandardTableKeyNames.DocumentName, out string fileName);
				entry.TryGetValue(StandardTableKeyNames.Line, out int lineNumber);
				entry.TryGetValue(StandardTableKeyNames.Column, out int columnNumber);

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

			return null;
		}
	}
}
