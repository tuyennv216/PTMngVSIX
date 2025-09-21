using PTMngVSIX.Prompt.OutputModel;
using System.IO;

namespace PTMngVSIX.Prompt.OutputParser
{
	public static class OutputParser_v2
	{
		public static ModelReturn_v2 Parser(string rawContent)
		{
			var result = new ModelReturn_v2();

			var content = rawContent.Trim().Trim(' ', '`', '\r', '\n');
			using (var reader = new StringReader(content))
			{
				var done = false;

				while (true || !done)
				{
					string line = reader.ReadLine();
					if (line == null)
						break;
					else if (line.Trim().Length == 0)
						continue;

					var separatorIndex = line.IndexOf(':');
					if (separatorIndex == -1) break;

					var field = line.Substring(0, separatorIndex).Trim();
					var value = line.Substring(separatorIndex + 1).Trim();

					switch (field)
					{
						case "version":
							break;

						case "type":
							result.Type = value;
							break;

						case "summary":
							result.Summary = value;
							break;

						case "solution":
							result.Solution = value;
							break;

						case "answer":
							result.Answer = reader.ReadToEnd();
							done = true;
							break;
					}
				}
			}

			return result;
		}
	}
}
