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
	public class GemmaService : BaseModelService
	{
		public static GemmaService Instance = new GemmaService();
		private GemmaService() { }

		private readonly ChatOptions Options = new ChatOptions
		{
			ModelId = ModelSetting.TranslatorModelName,
			AdditionalProperties = new AdditionalPropertiesDictionary
			{
				{ "temperature", 0.2 },					// Đặt thấp để dịch chính xác
				{ "top_p", 0.9 },						// Top-p sampling

				{ "reasoning", false },					// Yêu cầu không trả về reasoning
				{ "include_reasoning", false },			// Yêu cầu không trả về reasoning
				{ "intermediary", false }				// Đảm bảo không trả về nội dung trung gian
			}
		};

		public override async Task<ResponseBase> Call(RequestBase request)
		{
			if (request.IsEmpty)
			{
				return ResponseBase.Unknow("Empty request");
			}

			var systemPrompt = GemmaSPBuilder.Build(request);
			var userPrompt = UserPromptBuilder.Build(request);

			var messages = new List<ChatMessage>
			{
				new ChatMessage(ChatRole.System, systemPrompt),
				new ChatMessage(ChatRole.User, userPrompt)
			};

			try
			{
				var response = await AppState.ApiClient.GetResponseAsync(messages, Options);
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
