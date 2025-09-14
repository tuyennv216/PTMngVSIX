using PTMngVSIX.Prompt.DeepseekSystemPrompt;
using PTMngVSIX.Prompt.OutputPrompt;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace PTMngVSIX.Prompt.Builder
{
	public class DeepseekSPBuilder
	{
		private static readonly Dictionary<string, string> saved = new Dictionary<string, string>();

		public static string Build(Abstraction.RequestModel.RequestBase request)
		{
			var savedKey = request.Task;
			if (saved.ContainsKey(savedKey)) return saved[savedKey];

			var sb = new StringBuilder();

			switch (request.Task)
			{
				case Data.Constant.TaskName.TaskF1.GenerateCode:
					sb.AppendLine(DeepseekSPF1.SP1000_GenerateCode);
					sb.AppendLine();
					sb.AppendLine(DeepseekSPF0.SP0002_Restrict);
					sb.AppendLine();
					sb.AppendLine("Task: " + request.Task);
					sb.AppendLine();
					sb.AppendLine(OutputDefined.OCode02);
					break;

				case Data.Constant.TaskName.TaskF1.GenerateFunction:
					sb.AppendLine(DeepseekSPF1.SP1001_GenerateFunction);
					sb.AppendLine();
					sb.AppendLine(DeepseekSPF0.SP0002_Restrict);
					sb.AppendLine();
					sb.AppendLine("Task: " + request.Task);
					sb.AppendLine();
					sb.AppendLine(OutputDefined.OCode02);
					break;

				case Data.Constant.TaskName.TaskF1.GenerateClass:
					sb.AppendLine(DeepseekSPF1.SP1002_GenerateClass);
					sb.AppendLine();
					sb.AppendLine(DeepseekSPF0.SP0002_Restrict);
					sb.AppendLine();
					sb.AppendLine("Task: " + request.Task);
					sb.AppendLine();
					sb.AppendLine(OutputDefined.OCode02);
					break;

				case Data.Constant.TaskName.TaskF1.RefelctCode:
					sb.AppendLine(DeepseekSPF1.SP1003_ReflectCode);
					sb.AppendLine();
					sb.AppendLine(DeepseekSPF0.SP0002_Restrict);
					sb.AppendLine();
					sb.AppendLine("Task: " + request.Task);
					sb.AppendLine();
					sb.AppendLine(OutputDefined.OCode02);
					break;

				case Data.Constant.TaskName.TaskF1.FillInMiddle:
					sb.AppendLine(DeepseekSPF1.SP1004_FillInMiddle);
					sb.AppendLine();
					sb.AppendLine(DeepseekSPF0.SP0002_Restrict);
					sb.AppendLine();
					sb.AppendLine("Task: " + request.Task);
					sb.AppendLine();
					sb.AppendLine(OutputDefined.OText02);
					break;

				case Data.Constant.TaskName.TaskF1.AddComment:
					sb.AppendLine(DeepseekSPF1.SP1005_AddComments);
					sb.AppendLine();
					sb.AppendLine(DeepseekSPF0.SP0002_Restrict);
					sb.AppendLine();
					sb.AppendLine("Task: " + request.Task);
					sb.AppendLine();
					sb.AppendLine(OutputDefined.OText02);
					break;

				case Data.Constant.TaskName.TaskF1.CompleteFunction:
					sb.AppendLine(DeepseekSPF1.SP1006_CompleteFunction);
					sb.AppendLine();
					sb.AppendLine(DeepseekSPF0.SP0002_Restrict);
					sb.AppendLine();
					sb.AppendLine("Task: " + request.Task);
					sb.AppendLine();
					sb.AppendLine(OutputDefined.OCode02);
					break;

				case Data.Constant.TaskName.TaskF1.OptimizeFunction:
					sb.AppendLine(DeepseekSPF1.SP1040_OptimizeFunction);
					sb.AppendLine();
					sb.AppendLine(DeepseekSPF0.SP0002_Restrict);
					sb.AppendLine();
					sb.AppendLine("Task: " + request.Task);
					sb.AppendLine();
					sb.AppendLine(OutputDefined.OCode02);
					break;

				case Data.Constant.TaskName.TaskF1.SummaryFunction:
					sb.AppendLine(DeepseekSPF1.SP1050_SummaryFunction);
					sb.AppendLine();
					sb.AppendLine(DeepseekSPF0.SP0002_Restrict);
					sb.AppendLine();
					sb.AppendLine("Task: " + request.Task);
					sb.AppendLine();
					sb.AppendLine(OutputDefined.OText02);
					break;

				case Data.Constant.TaskName.TaskF1.ExplainFunction:
					sb.AppendLine(DeepseekSPF1.SP1060_ExplainFunction);
					sb.AppendLine();
					sb.AppendLine(DeepseekSPF0.SP0002_Restrict);
					sb.AppendLine();
					sb.AppendLine("Task: " + request.Task);
					sb.AppendLine();
					sb.AppendLine(OutputDefined.OText02);
					break;

				case Data.Constant.TaskName.TaskF2.GenerateDocsSelection:
					sb.AppendLine(DeepseekSPF2.SP2000_DocsSelection);
					sb.AppendLine();
					sb.AppendLine(DeepseekSPF0.SP0002_Restrict);
					sb.AppendLine();
					sb.AppendLine("Task: " + request.Task);
					sb.AppendLine();
					sb.AppendLine(OutputDefined.OText02);
					break;

				case Data.Constant.TaskName.TaskF2.GenerateDocsFunction:
					sb.AppendLine(DeepseekSPF2.SP2001_DocsFunction);
					sb.AppendLine();
					sb.AppendLine(DeepseekSPF0.SP0002_Restrict);
					sb.AppendLine();
					sb.AppendLine("Task: " + request.Task);
					sb.AppendLine();
					sb.AppendLine(OutputDefined.OText02);
					break;

				case Data.Constant.TaskName.TaskF2.GenerateDocsClass:
					sb.AppendLine(DeepseekSPF2.SP2002_DocsClass);
					sb.AppendLine();
					sb.AppendLine(DeepseekSPF0.SP0002_Restrict);
					sb.AppendLine();
					sb.AppendLine("Task: " + request.Task);
					sb.AppendLine();
					sb.AppendLine(OutputDefined.OText02);
					break;

				case Data.Constant.TaskName.TaskF2.GenerateDocsApi:
					sb.AppendLine(DeepseekSPF2.SP2010_DocsApi);
					sb.AppendLine();
					sb.AppendLine(DeepseekSPF0.SP0002_Restrict);
					sb.AppendLine();
					sb.AppendLine("Task: " + request.Task);
					sb.AppendLine();
					sb.AppendLine(OutputDefined.OText02);
					break;

				case Data.Constant.TaskName.TaskF2.GenerateDocsTechnicalSpecification:
					sb.AppendLine(DeepseekSPF2.SP2020_DocsTechnicalSpecifications);
					sb.AppendLine();
					sb.AppendLine(DeepseekSPF0.SP0002_Restrict);
					sb.AppendLine();
					sb.AppendLine("Task: " + request.Task);
					sb.AppendLine();
					sb.AppendLine(OutputDefined.OText02);
					break;



				case Data.Constant.TaskName.TaskF3.ExplainError:
					sb.AppendLine(DeepseekSPF3.SP3010_ExplainError);
					sb.AppendLine();
					sb.AppendLine(DeepseekSPF0.SP0002_Restrict);
					sb.AppendLine();
					sb.AppendLine("Task: " + request.Task);
					sb.AppendLine();
					sb.AppendLine(OutputDefined.OText02);
					break;

				case Data.Constant.TaskName.TaskF3.SuggestFixes:
					sb.AppendLine(DeepseekSPF3.SP3020_SuggestFixes);
					sb.AppendLine();
					sb.AppendLine(DeepseekSPF0.SP0002_Restrict);
					sb.AppendLine();
					sb.AppendLine("Task: " + request.Task);
					sb.AppendLine();
					sb.AppendLine(OutputDefined.OText02);
					break;

				case Data.Constant.TaskName.TaskF4.Whitebox:
					sb.AppendLine(DeepseekSPF4.SP4010_Whitebox);
					sb.AppendLine();
					sb.AppendLine(DeepseekSPF0.SP0002_Restrict);
					sb.AppendLine();
					sb.AppendLine("Task: " + request.Task);
					sb.AppendLine();
					sb.AppendLine(OutputDefined.OCode02);
					break;

				case Data.Constant.TaskName.TaskF4.Blackbox_UnitTests:
					sb.AppendLine(DeepseekSPF4.SP4020_Blackbox_UnitTests);
					sb.AppendLine();
					sb.AppendLine(DeepseekSPF0.SP0002_Restrict);
					sb.AppendLine();
					sb.AppendLine("Task: " + request.Task);
					sb.AppendLine();
					sb.AppendLine(OutputDefined.OCode02);
					break;

				case Data.Constant.TaskName.TaskF4.Blackbox_IntegrationTests:
					sb.AppendLine(DeepseekSPF4.C4021_Blackbox_IntegrationTests);
					sb.AppendLine();
					sb.AppendLine(DeepseekSPF0.SP0002_Restrict);
					sb.AppendLine();
					sb.AppendLine("Task: " + request.Task);
					sb.AppendLine();
					sb.AppendLine(OutputDefined.OCode02);
					break;

				case Data.Constant.TaskName.TaskF4.Blackbox_EdgeCaseTesting:
					sb.AppendLine(DeepseekSPF4.C4022_Blackbox_EdgeCaseTesting);
					sb.AppendLine();
					sb.AppendLine(DeepseekSPF0.SP0002_Restrict);
					sb.AppendLine();
					sb.AppendLine("Task: " + request.Task);
					sb.AppendLine();
					sb.AppendLine(OutputDefined.OCode02);
					break;

				case Data.Constant.TaskName.TaskF4.Blackbox_PerformanceTesting:
					sb.AppendLine(DeepseekSPF4.C4023_Blackbox_PerformanceTesting);
					sb.AppendLine();
					sb.AppendLine(DeepseekSPF0.SP0002_Restrict);
					sb.AppendLine();
					sb.AppendLine("Task: " + request.Task);
					sb.AppendLine();
					sb.AppendLine(OutputDefined.OCode02);
					break;

				case Data.Constant.TaskName.TaskF5.ReviewCodeFunction:
					sb.AppendLine(DeepseekSPF5.SP5020_ReviewCodeFunction);
					sb.AppendLine();
					sb.AppendLine(DeepseekSPF0.SP0002_Restrict);
					sb.AppendLine();
					sb.AppendLine("Task: " + request.Task);
					sb.AppendLine();
					sb.AppendLine(OutputDefined.OText02);
					break;

				case Data.Constant.TaskName.TaskF5.ReviewCodeClass:
					sb.AppendLine(DeepseekSPF5.SP5021_ReviewCodeClass);
					sb.AppendLine();
					sb.AppendLine(DeepseekSPF0.SP0002_Restrict);
					sb.AppendLine();
					sb.AppendLine("Task: " + request.Task);
					sb.AppendLine();
					sb.AppendLine(OutputDefined.OText02);
					break;

				case Data.Constant.TaskName.TaskF5.SuggestSolution:
					sb.AppendLine(DeepseekSPF5.SP5030_SuggestSolution);
					sb.AppendLine();
					sb.AppendLine(DeepseekSPF0.SP0002_Restrict);
					sb.AppendLine();
					sb.AppendLine("Task: " + request.Task);
					sb.AppendLine();
					sb.AppendLine(OutputDefined.OText02);
					break;

				case Data.Constant.TaskName.TaskF5.SuggestDeploy:
					sb.AppendLine(DeepseekSPF5.SP5040_SuggestDeploy);
					sb.AppendLine();
					sb.AppendLine(DeepseekSPF0.SP0002_Restrict);
					sb.AppendLine();
					sb.AppendLine("Task: " + request.Task);
					sb.AppendLine();
					sb.AppendLine(OutputDefined.OText02);
					break;

				case Data.Constant.TaskName.TaskF5.SuggestArchitecture:
					sb.AppendLine(DeepseekSPF5.SP5050_SuggestArchitecture);
					sb.AppendLine();
					sb.AppendLine(DeepseekSPF0.SP0002_Restrict);
					sb.AppendLine();
					sb.AppendLine("Task: " + request.Task);
					sb.AppendLine();
					sb.AppendLine(OutputDefined.OText02);
					break;

				case Data.Constant.TaskName.TaskF5.SuggestTechnologies:
					sb.AppendLine(DeepseekSPF5.SP5060_SuggestTechnologies);
					sb.AppendLine();
					sb.AppendLine(DeepseekSPF0.SP0002_Restrict);
					sb.AppendLine();
					sb.AppendLine("Task: " + request.Task);
					sb.AppendLine();
					sb.AppendLine(OutputDefined.OText02);
					break;

				case Data.Constant.TaskName.TaskF5.SuggestFeatures:
					sb.AppendLine(DeepseekSPF5.SP5070_SuggestFeatures);
					sb.AppendLine();
					sb.AppendLine(DeepseekSPF0.SP0002_Restrict);
					sb.AppendLine();
					sb.AppendLine("Task: " + request.Task);
					sb.AppendLine();
					sb.AppendLine(OutputDefined.OText02);
					break;

				case Data.Constant.TaskName.TaskF6.Chat:
					sb.AppendLine(DeepseekSPF6.SP6000_Chat);
					sb.AppendLine();
					sb.AppendLine(DeepseekSPF0.SP0002_Restrict);
					sb.AppendLine();
					sb.AppendLine(OutputDefined.OChat02);
					break;

				default:
					Debugger.Log(0, "Error", $"[Deepseek] Unknown task: '{request.Task}'\n");
					throw new System.Exception("[Deepseek] Unknown task: " + request.Task);
			}

			var prompt = sb.ToString();
			saved.Add(savedKey, prompt);

			return prompt;
		}
	}
}