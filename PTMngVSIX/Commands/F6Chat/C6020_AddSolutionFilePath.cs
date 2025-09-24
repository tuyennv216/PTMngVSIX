using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using PTMngVSIX.Abstraction;
using PTMngVSIX.Utils.Chat;
using PTMngVSIX.Utils.IDECommand;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Task = System.Threading.Tasks.Task;

namespace PTMngVSIX.Commands.F6Chat
{
	internal sealed class C6020_AddSolutionFilePath : CommandBase<C6020_AddSolutionFilePath>
	{
		public override int CommandId { get; protected set; } = CommandIds.C6020;
		public override string CommandText { get; protected set; } = Resource.Lang.ContextMenu.Button_C6020_AddFilePath;

		public C6020_AddSolutionFilePath(AsyncPackage package, OleMenuCommandService commandService)
			: base(package, commandService)
		{
		}

		protected override async Task ExecuteAsync(object sender, EventArgs e)
		{
			await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

			try
			{
				// Lấy IVsMonitorSelection service
				var monitorSelection = Package.GetGlobalService(typeof(SVsShellMonitorSelection)) as IVsMonitorSelection;
				if (monitorSelection == null) return;

				if (monitorSelection.GetCurrentSelection(out IntPtr hierarchyPtr, out uint itemId, out IVsMultiItemSelect multiSelect, out IntPtr containerPtr) == VSConstants.S_OK)
				{
					try
					{
						var filePaths = new List<string>();

						// Single selection
						if (hierarchyPtr != IntPtr.Zero && itemId != (uint)VSConstants.VSITEMID.Nil)
						{
							var hierarchy = Marshal.GetObjectForIUnknown(hierarchyPtr) as IVsHierarchy;
							string itemPath = await SolutionItemCommand.GetFilePathFromHierarchyAsync(hierarchy, itemId);

							if (System.IO.File.Exists(itemPath))
							{
								await ChatService.Instance.AddActiveFileAsync(itemPath);
							}
						}

						// Multi-selection
						if (multiSelect != null)
						{
							multiSelect.GetSelectionInfo(out uint itemCount, out int _);
							VSITEMSELECTION[] items = new VSITEMSELECTION[itemCount];
							multiSelect.GetSelectedItems(0, itemCount, items);

							foreach (var item in items)
							{
								if (item.pHier != null && item.itemid != (uint)VSConstants.VSITEMID.Nil)
								{
									string itemPath = await SolutionItemCommand.GetFilePathFromHierarchyAsync(item.pHier, item.itemid);

									if (System.IO.File.Exists(itemPath))
									{
										await ChatService.Instance.AddActiveFileAsync(itemPath);
									}
								}
							}
						}
					}
					finally
					{
						if (hierarchyPtr != IntPtr.Zero) Marshal.Release(hierarchyPtr);
						if (containerPtr != IntPtr.Zero) Marshal.Release(containerPtr);
					}
				}
			}
			catch { }
		}
	}
}
