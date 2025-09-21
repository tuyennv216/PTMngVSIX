using PTMngVSIX.Abstraction.AIServices.RequestModel;
using PTMngVSIX.Abstraction.Exceptions;
using PTMngVSIX.Prompt.AdditionalParam;
using System.Collections.Generic;
using System.Diagnostics;

namespace PTMngVSIX.Prompt.Builder
{
	public static class APBuilder
	{
		private static readonly Dictionary<string, Dictionary<string, object>> saved = new Dictionary<string, Dictionary<string, object>>();

		public static Dictionary<string, object> Build(RequestBase request)
		{
			var savedKey = request.Task;
			if (saved.ContainsKey(savedKey)) return saved[savedKey];

			Dictionary<string, object> result;

			switch (request.Task)
			{
				case Data.Constant.TaskName.TaskF1.GenerateCode:
					result = Apf1.AP1000_GenerateCode;
					break;

				case Data.Constant.TaskName.TaskF1.GenerateFunction:
					result = Apf1.AP1001_GenerateFunction;
					break;

				case Data.Constant.TaskName.TaskF1.GenerateClass:
					result = Apf1.AP1002_GenerateClass;
					break;

				case Data.Constant.TaskName.TaskF1.RefelctCode:
					result = Apf1.AP1003_Reflect;
					break;

				case Data.Constant.TaskName.TaskF1.FillInMiddle:
					result = Apf1.AP1004_FillInMiddle;
					break;

				case Data.Constant.TaskName.TaskF1.AddComment:
					result = Apf1.AP1005_AddComment;
					break;

				case Data.Constant.TaskName.TaskF1.OptimizeFunction:
					result = Apf1.AP1050_Optimize;
					break;

				case Data.Constant.TaskName.TaskF1.SummaryFunction:
					result = Apf1.AP1040_Summary;
					break;

				case Data.Constant.TaskName.TaskF1.ExplainFunction:
					result = Apf1.AP1060_Explain;
					break;

				case Data.Constant.TaskName.TaskF2.GenerateDocsSelection:
					result = Apf2.AP2000_DocsSelection;
					break;

				case Data.Constant.TaskName.TaskF2.GenerateDocsFunction:
					result = Apf2.AP2001_DocsFunction;
					break;

				case Data.Constant.TaskName.TaskF2.GenerateDocsClass:
					result = Apf2.AP2002_DocsClass;
					break;

				case Data.Constant.TaskName.TaskF2.GenerateDocsApi:
					result = Apf2.AP2010_DocsApi;
					break;

				case Data.Constant.TaskName.TaskF2.GenerateDocsTechnicalSpecification:
					result = Apf2.AP2020_DocsTechnicalSpecification;
					break;

				case Data.Constant.TaskName.TaskF3.ExplainError:
					result = Apf3.AP3010_ExplainError;
					break;

				case Data.Constant.TaskName.TaskF3.SuggestFixes:
					result = Apf3.AP3020_SuggestFixes;
					break;

				case Data.Constant.TaskName.TaskF4.Whitebox:
					result = Apf4.AP4010_WhiteBox;
					break;

				case Data.Constant.TaskName.TaskF4.Blackbox_UnitTests:
					result = Apf4.AP4020_Blackbox_UnitTests;
					break;

				case Data.Constant.TaskName.TaskF4.Blackbox_IntegrationTests:
					result = Apf4.AP4021_Blackbox_IntegrationTests;
					break;

				case Data.Constant.TaskName.TaskF4.Blackbox_EdgeCaseTesting:
					result = Apf4.AP4022_Blackbox_EdgeCaseTesting;
					break;

				case Data.Constant.TaskName.TaskF4.Blackbox_PerformanceTesting:
					result = Apf4.AP4023_Blackbox_PerformanceTesting;
					break;

				case Data.Constant.TaskName.TaskF5.ReviewCodeFunction:
					result = Apf5.AP5020_ReviewFuncion;
					break;

				case Data.Constant.TaskName.TaskF5.ReviewCodeClass:
					result = Apf5.AP5021_ReviewClass;
					break;

				case Data.Constant.TaskName.TaskF5.SuggestSolution:
					result = Apf5.AP5030_Solution;
					break;

				case Data.Constant.TaskName.TaskF5.SuggestDeploy:
					result = Apf5.AP5040_Deploy;
					break;

				case Data.Constant.TaskName.TaskF5.SuggestArchitecture:
					result = Apf5.AP5050_Architecture;
					break;

				case Data.Constant.TaskName.TaskF5.SuggestTechnologies:
					result = Apf5.AP5060_Technologies;
					break;

				case Data.Constant.TaskName.TaskF5.SuggestFeatures:
					result = Apf5.AP5070_Features;
					break;

				case Data.Constant.TaskName.TaskF6.Chat:
					result = Apf6.AP6000_Chat;
					break;

				default:
					Debugger.Log(0, "Error", $"[AdditinalParam] Unknown task: '{request.Task}'\n");
					throw new UnknowTaskException("[AdditinalParam] Unknown task: " + request.Task);
			}

			//result.Add("reasoning", false);			// Yêu cầu không trả về reasoning
			//result.Add("include_reasoning", false);	// Yêu cầu không trả về reasoning
			//result.Add("intermediary", false);		// Đảm bảo không trả về nội dung trung gian)

			saved.Add(savedKey, result);

			return result;
		}
	}
}
