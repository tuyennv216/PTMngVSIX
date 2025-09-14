using EnvDTE;
using Microsoft.VisualStudio.Shell;
using PTMngVSIX.Abstraction.ResponseModel;
using PTMngVSIX.Utils.Dialog;
using System;
using System.Threading.Tasks;

namespace PTMngVSIX.Utils.Doc
{
	internal class DocEdit
	{
		public static async Task Insert_Before_Selection_Async(Utils.Chat.Message message, ResponseBase response)
		{
			try
			{
				var textView = DocView.GetActiveTextView();

				var currentSnapshot = textView.TextSnapshot;
				var newPosition = message.TrackingPoint.GetPosition(currentSnapshot);

				if (newPosition >= 0)
				{
					using (var edit = textView.TextBuffer.CreateEdit())
					{
						// Lấy dòng chứa vị trí tracking point
						var line = currentSnapshot.GetLineFromPosition(newPosition);

						// Chèn text vào đầu dòng hiện tại (sẽ đẩy nội dung hiện tại xuống dưới)
						string textToInsert = (response.Answer ?? string.Empty) + Environment.NewLine;
						edit.Insert(line.Start.Position, textToInsert);
						edit.Apply();
					}
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

		public static async Task Insert_After_Selection_Async(Utils.Chat.Message message, ResponseBase response)
		{
			try
			{
				var textView = DocView.GetActiveTextView();
				var currentSnapshot = textView.TextSnapshot;
				var newPosition = message.TrackingPoint.GetPosition(currentSnapshot);
				if (newPosition >= 0)
				{
					using (var edit = textView.TextBuffer.CreateEdit())
					{
						// Lấy dòng chứa vị trí tracking point
						var line = currentSnapshot.GetLineFromPosition(newPosition);
						// Chèn text vào cuối dòng hiện tại
						string textToInsert = Environment.NewLine + (response.Answer ?? string.Empty);
						edit.Insert(line.End.Position, textToInsert);
						edit.Apply();
					}
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

		internal static async Task Insert_Before_Parent_Kind_Async(Utils.Chat.Message message, ResponseBase response, vsCMElement kind, bool kindNotFound_InsertIntoTrackingPoint = true)
		{
			try
			{
				var element = await DocView.GetParentByKindTrackingPointAsync(kind, message.TrackingPoint);
				if (element != null)
				{
					await Insert_Before_CodeElement_Async(element, response.Answer ?? string.Empty);
				}
				else if (kindNotFound_InsertIntoTrackingPoint)
				{
					await Insert_Before_Selection_Async(message, response);
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

		internal static async Task Insert_After_Parent_Kind_Async(Utils.Chat.Message message, ResponseBase response, vsCMElement kind, bool kindNotFound_InsertIntoTrackingPoint = true)
		{
			try
			{
				var element = await DocView.GetParentByKindTrackingPointAsync(kind, message.TrackingPoint);
				if (element != null)
				{
					await Insert_After_CodeElement_Async(element, response.Answer ?? string.Empty);
				}
				else if (kindNotFound_InsertIntoTrackingPoint)
				{
					await Insert_After_Selection_Async(message, response);
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

		internal static async Task Insert_Before_CodeElement_Async(CodeElement codeElement, string text)
		{
			await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

			if (codeElement == null) return;

			var editPoint = codeElement.StartPoint.CreateEditPoint();
			editPoint.StartOfLine();
			editPoint.Insert(text + "\n");
		}

		internal static async Task Insert_After_CodeElement_Async(CodeElement codeElement, string text)
		{
			await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

			if (codeElement == null) return;

			var editPoint = codeElement.EndPoint.CreateEditPoint();
			editPoint.EndOfLine();
			editPoint.Insert("\n" + text);
		}
	}
}
