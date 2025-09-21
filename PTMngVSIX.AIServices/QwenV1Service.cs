using PTMngVSIX.Abstraction.AI;
using PTMngVSIX.Abstraction.AIServices.RequestModel;
using PTMngVSIX.Abstraction.AIServices.ResponseModel;
using PTMngVSIX.Prompt.Builder;
using PTMngVSIX.Prompt.OutputParser;
using PTMngVSIX.Setting;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PTMngVSIX.AIServices
{
	public class QwenV1Service : BaseAIService
	{
		public static readonly QwenV1Service Instance = new QwenV1Service();
		private QwenV1Service() { }

		public override async Task<ResponseBase> CallAsync(RequestBase request)
		{
			if (request.IsEmpty)
			{
				return ResponseBase.Unknow("Empty request");
			}

			var messages = new List<ChatMessage>();

			// Thêm tổng quan yêu cầu người dùng, và giải pháp trong quá khứ
			for (var i = 0; i < request.History.Count; i += 2)
			{
				messages.Add(new ChatMessage(ChatRoles.User, "Summary: " + request.History[i]));
				messages.Add(new ChatMessage(ChatRoles.Assistant, "Solution: " + request.History[i + 1]));
			}

			// Thêm chat ngay trước đó
			if (request.LastRequest != null && request.LastResponse != null)
			{
				messages.Add(new ChatMessage(ChatRoles.User, request.LastRequest.Information + Environment.NewLine + request.LastRequest.Prompt));
				messages.Add(new ChatMessage(ChatRoles.Assistant, request.LastResponse.Answer));
			}

			// Thêm chat hiện tại
			var systemPrompt = DeepseekSPBuilder.Build(request);
			var userPrompt = UserPromptBuilder.Build(request);

			messages.Add(new ChatMessage(ChatRoles.System, systemPrompt));
			messages.Add(new ChatMessage(ChatRoles.User, userPrompt));

			try
			{
				var options = new ChatOptions
				{
					ModelId = ModelSetting.AssistantModelName,
					AdditionalProperties = APBuilder.Build(request)
				};

				var response = await AppState.ApiClient.GetResponseAsync(messages, options);
				var promptReturn = OutputParser_v1.Parser(response);

				return new ResponseBase
				{
					Type = promptReturn.Type,
					Answer = promptReturn.Text,
				};
			}
			catch (Exception ex)
			{
				return ResponseBase.Error(ex.Message);
			}
		}
	}
}
