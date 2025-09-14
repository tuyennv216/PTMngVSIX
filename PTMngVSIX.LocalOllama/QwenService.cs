using Microsoft.Extensions.AI;
using PTMngVSIX.Abstraction.RequestModel;
using PTMngVSIX.Abstraction.ResponseModel;
using PTMngVSIX.Prompt.Builder;
using PTMngVSIX.Prompt.OutputParser;
using PTMngVSIX.Setting;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PTMngVSIX.LocalOllama
{
	public class QwenService : BaseModelService
	{
		public static QwenService Instance = new QwenService();
		private QwenService() { }

		public override async Task<ResponseBase> Call(RequestBase request)
		{
			if (request.IsEmpty)
			{
				return ResponseBase.Unknow("Empty request");
			}

			var messages = new List<ChatMessage>();

			// Thêm tổng quan yêu cầu người dùng, và giải pháp trong quá khứ
			for (var i = 0; i < request.History.Count; i += 2)
			{
				messages.Add(new ChatMessage(ChatRole.User, "Summary: " + request.History[i]));
				messages.Add(new ChatMessage(ChatRole.Assistant, "Solution: " + request.History[i + 1]));
			}

			// Thêm chat ngay trước đó
			if (request.LastRequest != null && request.LastResponse != null)
			{
				messages.Add(new ChatMessage(ChatRole.User, request.LastRequest.Information + Environment.NewLine + request.LastRequest.Prompt));
				messages.Add(new ChatMessage(ChatRole.Assistant, request.LastResponse.Answer));
			}

			// Thêm chat hiện tại
			var systemPrompt = DeepseekSPBuilder.Build(request);
			var userPrompt = UserPromptBuilder.Build(request);

			messages.Add(new ChatMessage(ChatRole.System, systemPrompt));
			messages.Add(new ChatMessage(ChatRole.User, userPrompt));

			try
			{
				var options = new ChatOptions
				{
					ModelId = ModelSetting.AssistantModelName,
					AdditionalProperties = APBuilder.Build(request)
				};

				var response = await AppState.ApiClient.GetResponseAsync(messages, options);
				var promptReturn = OutputParser_v1.Parser(response.Text);

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
