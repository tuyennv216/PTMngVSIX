using EnvDTE;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.TextManager.Interop;
using PTMngVSIX.Utils.Cache;
using PTMngVSIX.Utils.Editor;
using System;
using System.Threading.Tasks;

namespace PTMngVSIX.Utils.Doc
{
	internal class DocView
	{
		internal static readonly MiniCache<bool> CachedHasSelectedText = new(TimeSpan.FromMilliseconds(1000), HasSelectedTextAsync);

		internal static ITextView GetActiveTextView()
		{
			var textManager = Package.GetGlobalService(typeof(SVsTextManager)) as IVsTextManager;
			textManager.GetActiveView(1, null, out IVsTextView activeView);

			if (activeView is IVsUserData userData)
			{
				Guid guidViewHost = Microsoft.VisualStudio.Editor.DefGuidList.guidIWpfTextViewHost;
				userData.GetData(ref guidViewHost, out object textViewHost);
				return (textViewHost as IWpfTextViewHost)?.TextView;
			}
			return null;
		}

		internal static async Task<string> GetDocumentLanguageAsync()
		{
			await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

			var dte = (DTE)Package.GetGlobalService(typeof(DTE));
			return dte?.ActiveDocument?.Language ?? string.Empty;
		}

		internal static async Task<string> GetLineTextAtAsync(int lineNumber)
		{
			await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

			var dte = (DTE)Package.GetGlobalService(typeof(DTE));
			var activeDoc = dte.ActiveDocument;
			if (activeDoc == null) return string.Empty;

			var textDocument = (EnvDTE.TextDocument)activeDoc.Object("TextDocument");
			var editPoint = textDocument.CreateEditPoint();
			editPoint.MoveToLineAndOffset(lineNumber, 1);
			string lineText = editPoint.GetLines(lineNumber, lineNumber + 1);
			return lineText;
		}

		internal static async Task<string> GetSelectedTextAsync()
		{
			await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

			var dte = (DTE)Package.GetGlobalService(typeof(DTE));
			var selection = dte?.ActiveDocument?.Selection as TextSelection;

			return selection?.Text ?? string.Empty;
		}

		internal static async Task<bool> HasSelectedTextAsync()
		{
			await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

			var dte = (DTE)Package.GetGlobalService(typeof(DTE));
			var selection = dte?.ActiveDocument?.Selection as TextSelection;
			return selection != null && !selection.IsEmpty;
		}

		internal static async Task<string> GetParentFIMTextAsync(vsCMElement kind, EditorItem item)
		{
			await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

			try
			{
				item.Initial();
				item.UpdateSnapshot();

				var parentNode = item.FindParent(kind, true);

				// Lấy điểm bắt đầu và kết thúc của syntax node
				var startPosition = parentNode.SpanStart;
				var endPosition = parentNode.Span.End;

				// Lấy văn bản từ đầu syntax node đến vị trí trackingPoint
				var textBefore = item.Snapshot.GetText(startPosition, item.Position - startPosition);

				// Lấy văn bản từ vị trí trackingPoint đến cuối syntax node
				var textAfter = item.Snapshot.GetText(item.Position, endPosition - item.Position);

				// Kết hợp với chuỗi chèn ở giữa
				var text = textBefore + " [Add new code here] " + textAfter;
				return text;
			}
			catch (Exception)
			{
				return string.Empty;
			}
		}

		internal static async Task<string> GetParentTextAsync(vsCMElement kind, EditorItem item)
		{
			await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();
			try
			{
				item.UpdateSnapshot();
				var parentNode = item.FindParent(kind, true);
				return parentNode.ToFullString();
			}
			catch (Exception)
			{
				return string.Empty;
			}
		}

		internal static async Task<string> GetDocumentTextAsync()
		{
			await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

			// Lấy document và text document hiện tại
			var dte = (DTE)Package.GetGlobalService(typeof(DTE));
			var textDocument = (EnvDTE.TextDocument)dte.ActiveDocument.Object("TextDocument");

			// Lấy toàn bộ nội dung document
			var startPoint = textDocument.StartPoint.CreateEditPoint();
			var endPoint = textDocument.EndPoint.CreateEditPoint();
			var fullText = startPoint.GetText(endPoint);

			return fullText;
		}
	}
}
