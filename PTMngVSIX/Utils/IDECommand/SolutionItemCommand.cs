using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using System.Threading.Tasks;

namespace PTMngVSIX.Utils.IDECommand
{
	internal static class SolutionItemCommand
	{
		internal static async Task<string> GetFilePathFromHierarchyAsync(IVsHierarchy hierarchy, uint itemId)
		{
			await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

			try
			{
				// Lấy canonical name (full path) - cách tốt nhất
				if (hierarchy.GetCanonicalName(itemId, out string filePath) == VSConstants.S_OK)
				{
					return filePath;
				}

				// Fallback: lấy qua ExtObject
				if (hierarchy.GetProperty(itemId, (int)__VSHPROPID.VSHPROPID_ExtObject, out object obj) == VSConstants.S_OK)
				{
					if (obj is EnvDTE.ProjectItem projectItem)
					{
						return projectItem.get_FileNames(1);
					}
					if (obj is EnvDTE.Project project)
					{
						return project.FullName;
					}
				}
			}
			catch
			{
				return string.Empty;
			}

			return string.Empty;
		}
	}
}
