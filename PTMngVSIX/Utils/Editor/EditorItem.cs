using EnvDTE;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.TextManager.Interop;
using System;

namespace PTMngVSIX.Utils.Editor
{
	public class EditorItem
	{
		//--- 1 reuse
		public string FilePath { get; set; }
		public ITextView TextView { get; set; }
		public ITrackingPoint TrackingPoint { get; set; }
		//--- 2 reuse
		public ITextDocument TextDocument { get; set; }
		//--- 3 thay đổi mỗi snapshot
		public ITextSnapshot Snapshot { get; set; }
		//--- 4 tự động lấy giá trị
		public int Position => TrackingPoint.GetPosition(Snapshot);
		public int LineNumber => Snapshot.GetLineNumberFromPosition(Position);
		public ITextSnapshotLine Line => Snapshot.GetLineFromPosition(Position);
		public SyntaxTree SyntaxTree => CSharpSyntaxTree.ParseText(Snapshot.GetText());
		public SyntaxNode SyntaxRoot => SyntaxTree.GetRoot();
		public SyntaxNode SyntaxNode
		{
			get
			{
				SyntaxToken token = SyntaxRoot.FindToken(Position);
				return token.Parent;
			}
		}

		public void Initial()
		{
			var textManager = Package.GetGlobalService(typeof(SVsTextManager)) as IVsTextManager;
			textManager.GetActiveView(1, null, out IVsTextView activeView);

			if (activeView is IVsUserData userData)
			{
				Guid guidViewHost = Microsoft.VisualStudio.Editor.DefGuidList.guidIWpfTextViewHost;
				userData.GetData(ref guidViewHost, out object textViewHost);
				TextView = (textViewHost as IWpfTextViewHost)?.TextView;

				TrackingPoint = TextView?.TextBuffer.CurrentSnapshot.CreateTrackingPoint(
					TextView.Caret.Position.BufferPosition.Position,
					PointTrackingMode.Positive
					);

				if (TextView?.TextBuffer.Properties.TryGetProperty(typeof(ITextDocument), out ITextDocument textDocument) ?? false)
				{
					TextDocument = textDocument;
					FilePath = textDocument.FilePath;
				}
			}
		}

		public void UpdateSnapshot()
		{
			Snapshot = TextView.TextSnapshot;
		}

		public SyntaxNode FindParent(vsCMElement vsKind, bool not_found_get_root = false)
		{
			var nodeKind = EditorAction.GetNodeKindFrom(vsKind);
			var parentNode = SyntaxRoot;
			if (nodeKind != SyntaxKind.None)
			{
				parentNode = SyntaxNode.FirstAncestorOrSelf<SyntaxNode>(n => n.IsKind(nodeKind));
			}

			if (parentNode == null && not_found_get_root)
			{
				return SyntaxRoot;
			}

			return parentNode;
		}


		public SyntaxNode FindParent(SyntaxKind nodeKind, bool not_found_get_root = false)
		{
			var parentNode = SyntaxNode.FirstAncestorOrSelf<SyntaxNode>(n => n.IsKind(nodeKind));
			if (parentNode == null && not_found_get_root)
			{
				return SyntaxRoot;
			}

			return parentNode;
		}
	}
}
