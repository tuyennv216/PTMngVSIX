using PTMngVSIX.Prompt.OutputPrompt;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace PTMngVSIX.Prompt.Builder
{
	public class GemmaSPBuilder
	{
		private static readonly Dictionary<string, string> saved = new Dictionary<string, string>();

		public static string Build(Abstraction.RequestModel.RequestBase request)
		{
			var savedKey = request.Task;
			if (saved.ContainsKey(savedKey)) return saved[savedKey];

			var sb = new StringBuilder();

			switch (request.Task)
			{
				case Data.Constant.TaskName.Translator.Translate:
					sb.AppendLine(GemmaSystemPrompt.SPTranslator.SP0001_Translator);
					sb.AppendLine();
					sb.AppendLine("Task: " + request.Task);
					sb.AppendLine(OutputDefined.OText01);
					break;

				default:
					Debugger.Log(0, "Error", $"[Gemma] Unknown task: '{request.Task}'\n");
					throw new System.Exception("[Gemma] Unknown task: " + request.Task);
			}

			var prompt = sb.ToString();
			saved.Add(savedKey, prompt);

			return prompt;
		}
	}
}