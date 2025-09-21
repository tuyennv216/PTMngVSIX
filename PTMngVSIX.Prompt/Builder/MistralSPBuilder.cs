using PTMngVSIX.Abstraction.AIServices.RequestModel;
using PTMngVSIX.Abstraction.Exceptions;
using PTMngVSIX.Prompt.DeepseekSystemPrompt;
using PTMngVSIX.Prompt.MistralSystemPrompt;
using PTMngVSIX.Prompt.OutputPrompt;
using PTMngVSIX.Setting;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace PTMngVSIX.Prompt.Builder
{
	public static class MistralSPBuilder
	{
		private static readonly Dictionary<string, string> saved = new Dictionary<string, string>();

		public static string Build(RequestBase request)
		{
			var savedKey = request.Task;
			if (saved.ContainsKey(savedKey)) return saved[savedKey];

			var sb = new StringBuilder();

			switch (request.Task)
			{
				case Data.Constant.TaskName.TaskF1.GenerateCode:
					sb.AppendLine(MistralSPF1.SP1000_GenerateCode);
					sb.AppendLine();
					sb.AppendLine(MistralSPF0.SP0002_Restrict);
					sb.AppendLine();
					sb.AppendLine("Task: " + request.Task);
					sb.AppendLine(OutputDefined.OCode01);
					break;

				case Data.Constant.TaskName.TaskF1.GenerateFunction:
					sb.AppendLine(MistralSPF1.SP1001_GenerateFunction);
					sb.AppendLine();
					sb.AppendLine(MistralSPF0.SP0002_Restrict);
					sb.AppendLine();
					sb.AppendLine("Task: " + request.Task);
					sb.AppendLine(OutputDefined.OCode01);
					break;

				case Data.Constant.TaskName.TaskF1.GenerateClass:
					sb.AppendLine(MistralSPF1.SP1002_GenerateClass);
					sb.AppendLine();
					sb.AppendLine(MistralSPF0.SP0002_Restrict);
					sb.AppendLine();
					sb.AppendLine("Task: " + request.Task);
					sb.AppendLine(OutputDefined.OCode01);
					break;

				case Data.Constant.TaskName.TaskF1.RefelctCode:
					sb.AppendLine(MistralSPF1.SP1003_ReflectCode);
					sb.AppendLine();
					sb.AppendLine(MistralSPF0.SP0002_Restrict);
					sb.AppendLine();
					sb.AppendLine("Task: " + request.Task);
					sb.AppendLine(OutputDefined.OCode01);
					break;

				case Data.Constant.TaskName.TaskF1.FillInMiddle:
					sb.AppendLine(MistralSPF1.SP1004_FillInMiddle);
					sb.AppendLine();
					sb.AppendLine(MistralSPF0.SP0002_Restrict);
					sb.AppendLine();
					sb.AppendLine("Task: " + request.Task);
					sb.AppendLine(OutputDefined.OCode01);
					break;

				case Data.Constant.TaskName.TaskF1.AddComment:
					sb.AppendLine(MistralSPF1.SP1005_AddComments);
					sb.AppendLine();
					sb.AppendLine(MistralSPF0.SP0002_Restrict);
					sb.AppendLine();
					sb.AppendLine("Task: " + request.Task);
					sb.AppendLine(OutputDefined.OCode01);
					break;

				case Data.Constant.TaskName.TaskF1.CompleteFunction:
					sb.AppendLine(MistralSPF1.SP1006_CompleteFunction);
					sb.AppendLine();
					sb.AppendLine(MistralSPF0.SP0002_Restrict);
					sb.AppendLine();
					sb.AppendLine("Task: " + request.Task);
					sb.AppendLine(OutputDefined.OCode01);
					break;

				case Data.Constant.TaskName.TaskF1.OptimizeFunction:
					sb.AppendLine(MistralSPF1.SP1040_OptimizeFunction);
					sb.AppendLine();
					sb.AppendLine(MistralSPF0.SP0002_Restrict);
					sb.AppendLine();
					sb.AppendLine("Task: " + request.Task);

					sb.AppendLine(OutputDefined.OCode01);
					break;

				case Data.Constant.TaskName.TaskF1.SummaryFunction:
					sb.AppendLine(MistralSPF1.SP1050_SummaryFunction);
					sb.AppendLine();
					sb.AppendLine(MistralSPF0.SP0002_Restrict);
					sb.AppendLine();
					sb.AppendLine("Task: " + request.Task);
					sb.AppendLine(OutputDefined.OText01);
					break;

				case Data.Constant.TaskName.TaskF1.ExplainFunction:
					sb.AppendLine(MistralSPF1.SP1060_ExplainFunction);
					sb.AppendLine();
					sb.AppendLine(MistralSPF0.SP0002_Restrict);
					sb.AppendLine();
					sb.AppendLine("Task: " + request.Task);
					sb.AppendLine(OutputDefined.OText01);
					break;

				case Data.Constant.TaskName.TaskF2.GenerateDocsSelection:
					sb.AppendLine(MistralSPF2.SP2000_DocsSelection);
					sb.AppendLine();
					sb.AppendLine(MistralSPF0.SP0002_Restrict);
					sb.AppendLine();
					sb.AppendLine("Task: " + request.Task);
					sb.AppendLine(OutputDefined.OText01);
					break;

				case Data.Constant.TaskName.TaskF2.GenerateDocsFunction:
					sb.AppendLine(MistralSPF2.SP2001_DocsFunction);
					sb.AppendLine();
					sb.AppendLine(MistralSPF0.SP0002_Restrict);
					sb.AppendLine();
					sb.AppendLine("Task: " + request.Task);
					sb.AppendLine(OutputDefined.OText01);
					break;

				case Data.Constant.TaskName.TaskF2.GenerateDocsClass:
					sb.AppendLine(MistralSPF2.SP2002_DocsClass);
					sb.AppendLine();
					sb.AppendLine(MistralSPF0.SP0002_Restrict);
					sb.AppendLine();
					sb.AppendLine("Task: " + request.Task);
					sb.AppendLine(OutputDefined.OText01);
					break;

				case Data.Constant.TaskName.TaskF2.GenerateDocsApi:
					sb.AppendLine(MistralSPF2.SP2010_DocsApi);
					sb.AppendLine();
					sb.AppendLine(MistralSPF0.SP0002_Restrict);
					sb.AppendLine();
					sb.AppendLine("Task: " + request.Task);
					sb.AppendLine(OutputDefined.OText01);
					break;

				case Data.Constant.TaskName.TaskF2.GenerateDocsTechnicalSpecification:
					sb.AppendLine(MistralSPF2.SP2020_DocsTechnicalSpecifications);
					sb.AppendLine();
					sb.AppendLine(MistralSPF0.SP0002_Restrict);
					sb.AppendLine();
					sb.AppendLine("Task: " + request.Task);
					sb.AppendLine(OutputDefined.OText01);
					break;



				case Data.Constant.TaskName.TaskF3.ExplainError:
					sb.AppendLine(MistralSPF3.SP3010_ExplainError);
					sb.AppendLine();
					sb.AppendLine(MistralSPF0.SP0002_Restrict);
					sb.AppendLine();
					sb.AppendLine("Task: " + request.Task);
					sb.AppendLine(OutputDefined.OText01);
					break;

				case Data.Constant.TaskName.TaskF3.SuggestFixes:
					sb.AppendLine(MistralSPF3.SP3020_SuggestFixes);
					sb.AppendLine();
					sb.AppendLine(MistralSPF0.SP0002_Restrict);
					sb.AppendLine();
					sb.AppendLine("Task: " + request.Task);
					sb.AppendLine(OutputDefined.OText01);
					break;

				case Data.Constant.TaskName.TaskF4.Whitebox:
					sb.AppendLine(MistralSPF4.SP4010_Whitebox);
					sb.AppendLine();
					sb.AppendLine(MistralSPF0.SP0002_Restrict);
					sb.AppendLine();
					sb.AppendLine("Task: " + request.Task);
					sb.AppendLine(OutputDefined.OCode01);
					break;

				case Data.Constant.TaskName.TaskF4.Blackbox_UnitTests:
					sb.AppendLine(MistralSPF4.SP4020_Blackbox_UnitTests);
					sb.AppendLine();
					sb.AppendLine(MistralSPF0.SP0002_Restrict);
					sb.AppendLine();
					sb.AppendLine("Task: " + request.Task);
					sb.AppendLine(OutputDefined.OCode01);
					break;

				case Data.Constant.TaskName.TaskF4.Blackbox_IntegrationTests:
					sb.AppendLine(MistralSPF4.C4021_Blackbox_IntegrationTests);
					sb.AppendLine();
					sb.AppendLine(MistralSPF0.SP0002_Restrict);
					sb.AppendLine();
					sb.AppendLine("Task: " + request.Task);
					sb.AppendLine(OutputDefined.OCode01);
					break;

				case Data.Constant.TaskName.TaskF4.Blackbox_EdgeCaseTesting:
					sb.AppendLine(MistralSPF4.C4022_Blackbox_EdgeCaseTesting);
					sb.AppendLine();
					sb.AppendLine(MistralSPF0.SP0002_Restrict);
					sb.AppendLine();
					sb.AppendLine("Task: " + request.Task);
					sb.AppendLine(OutputDefined.OCode01);
					break;

				case Data.Constant.TaskName.TaskF4.Blackbox_PerformanceTesting:
					sb.AppendLine(MistralSPF4.C4023_Blackbox_PerformanceTesting);
					sb.AppendLine();
					sb.AppendLine(MistralSPF0.SP0002_Restrict);
					sb.AppendLine();
					sb.AppendLine("Task: " + request.Task);
					sb.AppendLine(OutputDefined.OCode01);
					break;

				case Data.Constant.TaskName.TaskF5.ReviewCodeFunction:
					sb.AppendLine(MistralSPF5.SP5020_ReviewCodeFunction);
					sb.AppendLine();
					sb.AppendLine(MistralSPF0.SP0002_Restrict);
					sb.AppendLine();
					sb.AppendLine("Task: " + request.Task);
					sb.AppendLine(OutputDefined.OText01);
					break;

				case Data.Constant.TaskName.TaskF5.ReviewCodeClass:
					sb.AppendLine(MistralSPF5.SP5021_ReviewCodeClass);
					sb.AppendLine();
					sb.AppendLine(MistralSPF0.SP0002_Restrict);
					sb.AppendLine();
					sb.AppendLine("Task: " + request.Task);
					sb.AppendLine(OutputDefined.OText01);
					break;

				case Data.Constant.TaskName.TaskF5.SuggestSolution:
					sb.AppendLine(MistralSPF5.SP5030_SuggestSolution);
					sb.AppendLine();
					sb.AppendLine(MistralSPF0.SP0002_Restrict);
					sb.AppendLine();
					sb.AppendLine("Task: " + request.Task);
					sb.AppendLine(OutputDefined.OText01);
					break;

				case Data.Constant.TaskName.TaskF5.SuggestDeploy:
					sb.AppendLine(MistralSPF5.SP5040_SuggestDeploy);
					sb.AppendLine();
					sb.AppendLine(MistralSPF0.SP0002_Restrict);
					sb.AppendLine();
					sb.AppendLine("Task: " + request.Task);
					sb.AppendLine(OutputDefined.OText01);
					break;

				case Data.Constant.TaskName.TaskF5.SuggestArchitecture:
					sb.AppendLine(MistralSPF5.SP5050_SuggestArchitecture);
					sb.AppendLine();
					sb.AppendLine(MistralSPF0.SP0002_Restrict);
					sb.AppendLine();
					sb.AppendLine("Task: " + request.Task);
					sb.AppendLine(OutputDefined.OText01);
					break;

				case Data.Constant.TaskName.TaskF5.SuggestTechnologies:
					sb.AppendLine(MistralSPF5.SP5060_SuggestTechnologies);
					sb.AppendLine();
					sb.AppendLine(MistralSPF0.SP0002_Restrict);
					sb.AppendLine();
					sb.AppendLine("Task: " + request.Task);
					sb.AppendLine(OutputDefined.OText01);
					break;

				case Data.Constant.TaskName.TaskF5.SuggestFeatures:
					sb.AppendLine(MistralSPF5.SP5070_SuggestFeatures);
					sb.AppendLine();
					sb.AppendLine(MistralSPF0.SP0002_Restrict);
					sb.AppendLine();
					sb.AppendLine("Task: " + request.Task);
					sb.AppendLine(OutputDefined.OText01);
					break;

				case Data.Constant.TaskName.TaskF6.Chat:
					switch (ModelSetting.RoleName)
					{
						case "Developer":
							sb.AppendLine(DeepseekSPF6.SP6001_Developer);
							break;

						case "Designer":
							sb.AppendLine(DeepseekSPF6.SP6002_Designer);
							break;

						case "Tester":
							sb.AppendLine(DeepseekSPF6.SP6003_Tester);
							break;

						case "Writer":
							sb.AppendLine(DeepseekSPF6.SP6004_Writer);
							break;

						case "Architecture":
							sb.AppendLine(DeepseekSPF6.SP6005_Architecture);
							break;

						default:
							sb.AppendLine(DeepseekSPF6.SP6000_Chat);

							break;
					}
					sb.AppendLine();
					sb.AppendLine(MistralSPF0.SP0002_Restrict);
					sb.AppendLine();
					sb.AppendLine("Task: " + request.Task);
					sb.AppendLine(OutputDefined.OChat01);
					break;

				default:
					Debugger.Log(0, "Error", $"[Mistral] Unknown task: '{request.Task}'\n");
					throw new UnknowTaskException("[Mistral] Unknown task: " + request.Task);
			}

			var prompt = sb.ToString();
			saved.Add(savedKey, prompt);

			return prompt;
		}
	}
}