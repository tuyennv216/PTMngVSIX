using EnvDTE;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.TextManager.Interop;
using PTMngVSIX.Utils.Cache;
using System;
using System.Threading.Tasks;

namespace PTMngVSIX.Utils.Doc
{
	internal class DocView
	{
		internal static readonly MiniCache<bool> CachedHasSelectedText = new MiniCache<bool>(TimeSpan.FromMilliseconds(1000), HasSelectedTextAsync);

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

		internal static ITrackingPoint GetCurrentTrackingPoint()
		{
			var wpfTextView = GetActiveTextView();

			var result = wpfTextView?.TextBuffer.CurrentSnapshot.CreateTrackingPoint(
				wpfTextView.Caret.Position.BufferPosition.Position,
				PointTrackingMode.Positive);

			return result;
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

			var textDocument = (TextDocument)activeDoc.Object("TextDocument");
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

		internal static async Task<string> GetParentFillIdMiddleTextByKindTrackingPointAsync(vsCMElement kind, ITrackingPoint trackingPoint)
		{
			await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();
			try
			{
				var currentSnapshot = GetActiveTextView().TextSnapshot;
				var position = trackingPoint.GetPosition(currentSnapshot);
				var codeElement = await GetParentByKindPositionAsync(kind, position);
				if (codeElement == null) return string.Empty;

				// Lấy điểm bắt đầu và kết thúc của code element
				var startPoint = codeElement.GetStartPoint().CreateEditPoint();
				var endPoint = codeElement.GetEndPoint();

				// Lấy văn bản từ đầu đến vị trí trackingPoint
				var textBefore = startPoint.GetText(position - startPoint.AbsoluteCharOffset);

				// Lấy văn bản từ vị trí trackingPoint đến cuối
				var editPointAtPosition = codeElement.StartPoint.CreateEditPoint();
				editPointAtPosition.MoveToAbsoluteOffset(position);
				var textAfter = editPointAtPosition.GetText(endPoint.AbsoluteCharOffset - position);

				// Kết hợp với chuỗi chèn ở giữa
				var text = textBefore + " [Thêm code mới ở đây] " + textAfter;
				return text;
			}
			catch (Exception)
			{
				return string.Empty;
			}
		}

		internal static async Task<string> GetParentTextByKindTrackingPointAsync(vsCMElement kind, ITrackingPoint trackingPoint)
		{
			await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();
			try
			{
				var currentSnapshot = GetActiveTextView().TextSnapshot;
				var position = trackingPoint.GetPosition(currentSnapshot);
				var codeElement = await GetParentByKindPositionAsync(kind, position);
				if (codeElement == null) return string.Empty;
				var text = codeElement.GetStartPoint().CreateEditPoint().GetText(codeElement.GetEndPoint());
				return text;
			}
			catch (Exception)
			{
				return string.Empty;
			}
		}

		internal static async Task<CodeElement> GetParentByKindTrackingPointAsync(vsCMElement kind, ITrackingPoint trackingPoint)
		{
			await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();
			try
			{
				var currentSnapshot = GetActiveTextView().TextSnapshot;
				var position = trackingPoint.GetPosition(currentSnapshot);
				return await GetParentByKindPositionAsync(kind, position);
			}
			catch (Exception)
			{
				return null;
			}
		}

		internal static async Task<CodeElement> GetParentByKindPositionAsync(vsCMElement kind, int position)
		{
			await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

			// Lấy DTE service
			var dte = (DTE)Package.GetGlobalService(typeof(DTE));
			var activeDoc = dte.ActiveDocument;

			// Lấy TextDocument và tạo EditPoint từ tracking point
			var textDocument = (TextDocument)activeDoc.Object("TextDocument");
			var editPoint = textDocument.CreateEditPoint();
			editPoint.MoveToAbsoluteOffset(position + 1); // VS uses 1-based indexing

			// Tìm element kiểu kind chứa vị trí
			CodeElement element = null;
			try
			{
				element = editPoint.CodeElement[kind] as CodeElement;
			}
			catch { }
			return element;
		}

		internal static async Task<string> GetDocumentTextAsync()
		{
			await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

			// Lấy document và text document hiện tại
			var dte = (DTE)Package.GetGlobalService(typeof(DTE));
			var textDocument = (TextDocument)dte.ActiveDocument.Object("TextDocument");

			// Lấy toàn bộ nội dung document
			var startPoint = textDocument.StartPoint.CreateEditPoint();
			var endPoint = textDocument.EndPoint.CreateEditPoint();
			var fullText = startPoint.GetText(endPoint);

			return fullText;
		}
	}
}
