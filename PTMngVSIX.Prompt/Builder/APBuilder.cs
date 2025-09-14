using Microsoft.Extensions.AI;
using PTMngVSIX.Prompt.AdditionalParam;
using System.Collections.Generic;
using System.Diagnostics;

namespace PTMngVSIX.Prompt.Builder
{
	public class APBuilder
	{
		private static readonly Dictionary<string, AdditionalPropertiesDictionary> saved = new Dictionary<string, AdditionalPropertiesDictionary>();

		public static AdditionalPropertiesDictionary Build(Abstraction.RequestModel.RequestBase request)
		{
			var savedKey = request.Task;
			if (saved.ContainsKey(savedKey)) return saved[savedKey];

			AdditionalPropertiesDictionary result = new AdditionalPropertiesDictionary();

			switch (request.Task)
			{
				case Data.Constant.TaskName.TaskF1.GenerateCode:
					result = APF1.AP1000_GenerateCode;
					break;

				case Data.Constant.TaskName.TaskF1.GenerateFunction:
					result = APF1.AP1001_GenerateFunction;
					break;

				case Data.Constant.TaskName.TaskF1.GenerateClass:
					result = APF1.AP1002_GenerateClass;
					break;

				case Data.Constant.TaskName.TaskF1.RefelctCode:
					result = APF1.AP1003_Reflect;
					break;

				case Data.Constant.TaskName.TaskF1.FillInMiddle:
					result = APF1.AP1004_FillInMiddle;
					break;

				case Data.Constant.TaskName.TaskF1.AddComment:
					result = APF1.AP1005_AddComment;
					break;

				case Data.Constant.TaskName.TaskF1.OptimizeFunction:
					result = APF1.AP1050_Optimize;
					break;

				case Data.Constant.TaskName.TaskF1.SummaryFunction:
					result = APF1.AP1040_Summary;
					break;

				case Data.Constant.TaskName.TaskF1.ExplainFunction:
					result = APF1.AP1060_Explain;
					break;

				case Data.Constant.TaskName.TaskF2.GenerateDocsSelection:
					result = APF2.AP2000_DocsSelection;
					break;

				case Data.Constant.TaskName.TaskF2.GenerateDocsFunction:
					result = APF2.AP2001_DocsFunction;
					break;

				case Data.Constant.TaskName.TaskF2.GenerateDocsClass:
					result = APF2.AP2002_DocsClass;
					break;

				case Data.Constant.TaskName.TaskF2.GenerateDocsApi:
					result = APF2.AP2010_DocsApi;
					break;

				case Data.Constant.TaskName.TaskF2.GenerateDocsTechnicalSpecification:
					result = APF2.AP2020_DocsTechnicalSpecification;
					break;

				case Data.Constant.TaskName.TaskF3.ExplainError:
					result = APF3.AP3010_ExplainError;
					break;

				case Data.Constant.TaskName.TaskF3.SuggestFixes:
					result = APF3.AP3020_SuggestFixes;
					break;

				case Data.Constant.TaskName.TaskF4.Whitebox:
					result = APF4.AP4010_WhiteBox;
					break;

				case Data.Constant.TaskName.TaskF4.Blackbox_UnitTests:
					result = APF4.AP4020_Blackbox_UnitTests;
					break;

				case Data.Constant.TaskName.TaskF4.Blackbox_IntegrationTests:
					result = APF4.AP4021_Blackbox_IntegrationTests;
					break;

				case Data.Constant.TaskName.TaskF4.Blackbox_EdgeCaseTesting:
					result = APF4.AP4022_Blackbox_EdgeCaseTesting;
					break;

				case Data.Constant.TaskName.TaskF4.Blackbox_PerformanceTesting:
					result = APF4.AP4023_Blackbox_PerformanceTesting;
					break;

				case Data.Constant.TaskName.TaskF5.ReviewCodeFunction:
					result = APF5.AP5020_ReviewFuncion;
					break;

				case Data.Constant.TaskName.TaskF5.ReviewCodeClass:
					result = APF5.AP5021_ReviewClass;
					break;

				case Data.Constant.TaskName.TaskF5.SuggestSolution:
					result = APF5.AP5030_Solution;
					break;

				case Data.Constant.TaskName.TaskF5.SuggestDeploy:
					result = APF5.AP5040_Deploy;
					break;

				case Data.Constant.TaskName.TaskF5.SuggestArchitecture:
					result = APF5.AP5050_Architecture;
					break;

				case Data.Constant.TaskName.TaskF5.SuggestTechnologies:
					result = APF5.AP5060_Technologies;
					break;

				case Data.Constant.TaskName.TaskF5.SuggestFeatures:
					result = APF5.AP5070_Features;
					break;

				case Data.Constant.TaskName.TaskF6.Chat:
					result = APF6.AP6000_Chat;
					break;

				default:
					Debugger.Log(0, "Error", $"[AdditinalParam] Unknown task: '{request.Task}'\n");
					throw new System.Exception("[AdditinalParam] Unknown task: " + request.Task);
			}

			result.Add("reasoning", false);			// Yêu cầu không trả về reasoning
			result.Add("include_reasoning", false);	// Yêu cầu không trả về reasoning
			result.Add("intermediary", false);		// Đảm bảo không trả về nội dung trung gian)

			saved.Add(savedKey, result);

			return result;
		}
	}
}
