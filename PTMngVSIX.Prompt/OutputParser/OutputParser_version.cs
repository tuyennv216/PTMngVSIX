using PTMngVSIX.Prompt.OutputModel;
using System.Diagnostics;

namespace PTMngVSIX.Prompt.OutputParser
{
	public static class OutputParser_version
	{
		public static T Parser<T>(string rawContent) where T : IReturnModel
		{
			var content = rawContent.Trim().Trim(' ', '`', '\r', '\n');
			var versionLine = content.Substring(0, content.IndexOf('\n') + 1);
			if (versionLine.StartsWith("version:"))
			{
				var version = versionLine.Substring(9).Trim();
				switch (version)
				{
					case "2":
						if (OutputParser_v2.Parser(content) is T result)
						{
							return result;
						}
						break;

					default:
						Debugger.Log(0, "OutputParser", " Can't convert type version " + version + " to " + typeof(T).Name);
						break;
				}
			}

			return default;
		}
	}
}
