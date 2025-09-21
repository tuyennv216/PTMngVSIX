using EnvDTE;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.VisualStudio.Shell;
using PTMngVSIX.Abstraction.AIServices.ResponseModel;
using PTMngVSIX.Utils.Dialog;
using System;
using System.Threading.Tasks;
using static PTMngVSIX.Utils.Editor.EditorModel;

namespace PTMngVSIX.Utils.Editor
{
	internal static class EditorAction
	{
		public static async Task Insert_To_Selection_Async(Utils.Chat.Message message, ResponseBase response, InsertSelectionPosition insertPosition)
		{
			try
			{
				var item = message.EditorItem;
				item.UpdateSnapshot();

				if (item.Position > 0)
				{
					using var edit = item.TextView.TextBuffer.CreateEdit();

					string textToInsert = string.IsNullOrWhiteSpace(response.Answer)
						? string.Empty
						: Environment.NewLine + response.Answer + Environment.NewLine;

					var positionToInsert = item.Position;
					switch (insertPosition)
					{
						case InsertSelectionPosition.BeforeLine:
							positionToInsert = item.Line.Start;
							break;
						case InsertSelectionPosition.Current:
							positionToInsert = item.Position;
							break;
						case InsertSelectionPosition.AfterLine:
							positionToInsert = item.Line.End;
							break;
					}

					edit.Insert(positionToInsert, textToInsert);
					edit.Apply();
				}
				else
				{
					var content = message.ToString() + Environment.NewLine + (response.Answer ?? string.Empty);
					await TextDialog.ShowTextDialogAsync(Resource.Lang.Dialog.Dialog_Cant_Insert_Result, content);
				}
			}
			catch
			{
				var content = message.ToString() + Environment.NewLine + (response.Answer ?? string.Empty);
				await TextDialog.ShowTextDialogAsync(Resource.Lang.Dialog.Dialog_Cant_Insert_Result, content);
			}
		}

		internal static async Task Insert_To_Parent_Kind_Async(Utils.Chat.Message message, ResponseBase response, vsCMElement vsKind, InsertNodePosition insertPosition)
		{
			var nodeKind = GetNodeKindFrom(vsKind);

			if (nodeKind == SyntaxKind.None)
			{
				var content = message.ToString() + Environment.NewLine + (response.Answer ?? string.Empty);
				await TextDialog.ShowTextDialogAsync(Resource.Lang.Dialog.Dialog_Cant_Insert_Result, content);
				return;
			}

			await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();
			try
			{
				var item = message.EditorItem;
				item.UpdateSnapshot();

				if (item.Position > 0)
				{
					using var edit = item.TextView.TextBuffer.CreateEdit();

					string textToInsert = string.IsNullOrWhiteSpace(response.Answer)
						? string.Empty
						: Environment.NewLine + response.Answer + Environment.NewLine;

					var parentNode = item.FindParent(nodeKind);

					var positionToInsert = item.Position;
					if (parentNode != null)
					{
						switch (insertPosition)
						{
							case InsertNodePosition.BeforeNode:
								positionToInsert = parentNode.SpanStart;
								break;
							case InsertNodePosition.AfterNode:
								positionToInsert = parentNode.Span.End;
								break;
							case InsertNodePosition.BeforeContent:
								positionToInsert = parentNode.SpanStart + parentNode.GetLeadingTrivia().FullSpan.Length;
								break;
							case InsertNodePosition.AfterContent:
								positionToInsert = parentNode.Span.End - parentNode.GetTrailingTrivia().FullSpan.Length;
								break;
						}
					}

					edit.Insert(positionToInsert, textToInsert);
					edit.Apply();
				}
				else
				{
					var content = message.ToString() + Environment.NewLine + (response.Answer ?? string.Empty);
					await TextDialog.ShowTextDialogAsync(Resource.Lang.Dialog.Dialog_Cant_Insert_Result, content);
				}
			}
			catch
			{
				var content = message.ToString() + Environment.NewLine + (response.Answer ?? string.Empty);
				await TextDialog.ShowTextDialogAsync(Resource.Lang.Dialog.Dialog_Cant_Insert_Result, content);
			}
		}

		internal static SyntaxKind GetNodeKindFrom(vsCMElement vsKind)
		{
			var nodeKind = vsKind switch
			{
				vsCMElement.vsCMElementFunction => SyntaxKind.MethodDeclaration,
				vsCMElement.vsCMElementClass => SyntaxKind.ClassDeclaration,
				vsCMElement.vsCMElementNamespace => SyntaxKind.NamespaceDeclaration,
				_ => SyntaxKind.None
			};

			return nodeKind;
		}
	}
}
