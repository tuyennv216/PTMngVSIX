using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.Shell;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTMngVSIX.Utils.Print
{
	internal class PrintStructure
	{
		public static async Task<string> GetSolutionStructureAsync()
		{
			await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

			var dte = (DTE2)Package.GetGlobalService(typeof(DTE));
			if (dte?.Solution == null || !dte.Solution.IsOpen)
			{
				return "No solution is open.";
			}

			var sb = new StringBuilder();
			sb.AppendLine(dte.Solution.FileName);
			foreach (Project project in dte.Solution.Projects)
			{
				// Bỏ qua các project ảo
				if (project.Kind == EnvDTE.Constants.vsProjectKindSolutionItems ||
					project.Kind == EnvDTE.Constants.vsProjectKindMisc)
				{
					continue;
				}
				sb.AppendLine($"├─ {project.Name}");
				// Đệ quy thêm các item của project
				await AddProjectItemsToTreeAsync(project.ProjectItems, sb, "│  ", false);
			}

			return sb.ToString();
		}

		public static async Task<string> GetProjectStructureAsync()
		{
			await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();
			var dte = (DTE)Package.GetGlobalService(typeof(DTE));
			var document = dte.ActiveDocument;

			if (document == null)
				return null;

			var project = document.ProjectItem?.ContainingProject;

			var sb = new StringBuilder();
			sb.AppendLine(project.FileName);
			await AddProjectItemsToTreeAsync(project.ProjectItems, sb, "", true);

			return sb.ToString();
		}

		private static async Task AddProjectItemsToTreeAsync(ProjectItems items, StringBuilder sb, string prefix, bool isLastItem)
		{
			if (items == null) return;

			await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

			var itemList = new List<ProjectItem>();
			foreach (ProjectItem item in items)
			{
				// Lọc item hợp lệ trước khi xử lý
				if (await ShouldIncludeProjectItemAsync(item))
				{
					itemList.Add(item);
				}
			}

			for (int i = 0; i < itemList.Count; i++)
			{
				var item = itemList[i];
				bool isLast = (i == itemList.Count - 1);

				string itemName = item?.Name ?? "Unknow Item";
				sb.AppendLine($"{prefix}{(isLast ? "└─ " : "├─ ")}{itemName}");

				// Nếu có con và là folder vật lý (không phải ảo), duyệt tiếp
				if (item.ProjectItems.Count > 0)
				{
					string childPrefix = prefix + (isLast ? "   " : "│  ");
					await AddProjectItemsToTreeAsync(item.ProjectItems, sb, childPrefix, isLast);
				}
			}
		}

		private static async Task<bool> ShouldIncludeProjectItemAsync(ProjectItem item)
		{
			try
			{
				await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

				// 1. Kiểm tra tên item
				string name = item.Name;
				if (string.IsNullOrWhiteSpace(name)) return false;

				// 2. Bỏ qua các item ảo (Dependencies, References, NuGet, SDK, v.v.)
				if (item.Kind == EnvDTE.Constants.vsProjectItemKindVirtualFolder)
				{
					string virtualKind = item.Kind;
					if (virtualKind == "{66A26720-8FB5-11D2-AA7E-00C04F688DDE}" || // References
						virtualKind == "{6BB5F8EF-4483-11D3-8BCF-00C04F8EC28C}" || // Solution Items
						virtualKind == "{91796F34-3853-47E7-972A-71260847EBAE}")   // NuGet Packages
					{
						return false;
					}

					// Tên folder ảo thường là: Dependencies, References, packages, ...
					if (IsLikelyHiddenVirtualFolder(name))
					{
						return false;
					}
				}

				// 3. Lấy đường dẫn vật lý (nếu có)
				string fullPath = await TryGetFullPathAsync(item);
				if (!string.IsNullOrEmpty(fullPath))
				{
					var fileName = System.IO.Path.GetFileName(fullPath).ToLower();
					var dirName = System.IO.Path.GetDirectoryName(fullPath)?.ToLower();

					// Bỏ file tạm, build
					string[] excludedFiles = { ".user", ".suo", ".cache", ".dll", ".pdb", ".exe", ".config" };
					if (excludedFiles.Any(x => fileName.EndsWith(x)))
					{
						return false;
					}

					// Bỏ thư mục build
					string[] excludedFolders = { "bin", "obj", "debug", "release", "packages", "node_modules" };
					if (excludedFolders.Any(folder =>
						dirName.Contains(System.IO.Path.DirectorySeparatorChar + folder + System.IO.Path.DirectorySeparatorChar) ||
						dirName.EndsWith(System.IO.Path.DirectorySeparatorChar + folder)))
					{
						return false;
					}
				}

				// 4. Bỏ các file ẩn (nếu có thuộc tính)
				try
				{
					var properties = item.Properties;
					if (properties != null)
					{
						// Một số file bị ẩn trong Solution Explorer
						var isVisible = properties.Item("ShowInSolutionExplorer")?.Value?.ToString() != "False";
						if (!isVisible) return false;
					}
				}
				catch { }

				return true;
			}
			catch
			{
				return false;
			}
		}

		private static bool IsLikelyHiddenVirtualFolder(string name)
		{
			string[] hiddenNames = {
				"Dependencies", "References", "NuGet", "SDK", "Assemblies",
				"packages", "node_modules", "npm", "bower_components"
			};
			return hiddenNames.Any(h => name.IndexOf(h, StringComparison.OrdinalIgnoreCase) >= 0);
		}

		private static async Task<string> TryGetFullPathAsync(ProjectItem item)
		{
			try
			{
				await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

				// Một số item có nhiều file (generated), lấy đầu tiên
				var property = item.Properties.Item("FullPath");
				return property?.Value?.ToString() ?? "";
			}
			catch
			{
				return "";
			}
		}
	}
}
