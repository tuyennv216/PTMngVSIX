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
	public class GemmaV1Service : BaseAIService
	{
		public static readonly GemmaV1Service Instance = new GemmaV1Service();
		private GemmaV1Service() { }

		private readonly ChatOptions Options = new ChatOptions
		{
			ModelId = ModelSetting.TranslatorModelName,
			AdditionalProperties = new Dictionary<string, object>
			{
				{ "temperature", 0.2 },					// Đặt thấp để dịch chính xác
				{ "top_p", 0.9 },						// Top-p sampling

				{ "reasoning", false },					// Yêu cầu không trả về reasoning
				{ "include_reasoning", false },			// Yêu cầu không trả về reasoning
				{ "intermediary", false }				// Đảm bảo không trả về nội dung trung gian
			}
		};

		public override async Task<ResponseBase> CallAsync(RequestBase request)
		{
			if (request.IsEmpty)
			{
				return ResponseBase.Unknow("Empty request");
			}

			var systemPrompt = GemmaSPBuilder.Build(request);
			var userPrompt = UserPromptBuilder.Build(request);

			var messages = new List<ChatMessage>
			{
				new ChatMessage(ChatRoles.System, systemPrompt),
				new ChatMessage(ChatRoles.User, userPrompt)
			};

			try
			{
				var response = await AppState.Instance.ApiClient.GetResponseAsync(messages, Options);
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
