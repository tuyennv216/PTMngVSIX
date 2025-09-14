using EnvDTE;
using Microsoft.VisualStudio.Shell;
using System.Threading.Tasks;

namespace PTMngVSIX.Utils.Selector
{
	internal class ElementSelector1
	{
		internal static async Task<CodeElement> FindParentByKindAsync(vsCMElement parentKind)
		{
			await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

			var dte = (DTE)Package.GetGlobalService(typeof(DTE));
			var activeLine = ((TextSelection)dte.ActiveDocument.Selection).ActivePoint.Line;

			var projectItem = dte.ActiveDocument.ProjectItem;
			var fileCodeModel = projectItem.FileCodeModel;

			if (fileCodeModel == null) return null;

			var elements = fileCodeModel.CodeElements;

			var find = await FindParentByKindAsync(elements, activeLine, parentKind);

			return find;
		}

		internal static async Task<CodeElement> FindParentByKindAsync(CodeElements elements, int line, vsCMElement parentKind)
		{
			await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

			foreach (CodeElement element in elements)
			{
				if (element.StartPoint.Line <= line && element.EndPoint.Line >= line)
				{
					if (element.Kind == parentKind)
						return element;

					var child = await FindParentByKindAsync(element.Children, line, parentKind);
					if (child != null) return child;
				}
			}

			return null;
		}

	}
}
