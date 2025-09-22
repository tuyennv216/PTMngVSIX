using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTMngVSIX.Utils.SystemIO
{
	internal class IOCommand
	{
		public static async Task<List<FileContent>> GetFileContentsAsync(IEnumerable<string> filePaths, bool includeFolderName = false)
		{
			var readTasks = filePaths.Select(async (path) =>
			{
				if (System.IO.File.Exists(path))
				{
					var finalName = "";
					if (includeFolderName)
					{
						finalName = Path.Combine(
							Path.GetFileName(Path.GetDirectoryName(path)),
							Path.GetFileName(path)
							);
					}
					else
					{
						finalName = Path.GetFileName(path);
					}

					var content = await ReadAllTextAsync(path);
					return new FileContent
					{
						Path = path,
						Name = finalName,
						Content = content,
					};
				}

				return null;
			});

			var results = await Task.WhenAll(readTasks);
			return results.Where(fc => fc != null).ToList();
		}

		public static async Task<string> ReadAllTextAsync(string filePath)
		{
			using (var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read, 4096, useAsync: true))
			using (var reader = new StreamReader(stream, Encoding.UTF8))
			{
				return await reader.ReadToEndAsync();
			}
		}
	}
}
