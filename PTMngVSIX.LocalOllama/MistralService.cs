using Microsoft.Extensions.AI;
using PTMngVSIX.Abstraction.RequestModel;
using PTMngVSIX.Abstraction.ResponseModel;
using PTMngVSIX.Prompt.Builder;
using PTMngVSIX.Prompt.OutputParser;
using PTMngVSIX.Setting;
using System;
using System.Text;
using System.Threading.Tasks;

namespace PTMngVSIX.LocalOllama
{
	public class MistralService : BaseModelService
	{
		public static MistralService Instance = new MistralService();
		private MistralService() { }

		public override async Task<ResponseBase> Call(RequestBase request)
		{
			if (request.IsEmpty)
			{
				return ResponseBase.Unknow("Empty request");
			}

			var systemPrompt = MistralSPBuilder.Build(request);
			var userPrompt = UserPromptBuilder.Build(request);
			var prompt = Combine(systemPrompt, userPrompt);

			var message = new ChatMessage(ChatRole.User, prompt);

			try
			{
				var options = new ChatOptions
				{
					ModelId = ModelSetting.AssistantModelName,
					AdditionalProperties = APBuilder.Build(request)
				};

				var response = await AppState.ApiClient.GetResponseAsync(message, options);
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

		public string Combine(string systemPrompt, string userPrompt)
		{
			var sb = new StringBuilder();
			sb.AppendLine("[INST]");

			if (!string.IsNullOrWhiteSpace(systemPrompt))
			{
				sb.AppendLine("<<SYS>>");
				sb.AppendLine(systemPrompt.Trim());
				sb.AppendLine("<</SYS>>");
			}

			if (!string.IsNullOrWhiteSpace(userPrompt))
			{
				sb.AppendLine(userPrompt.Trim());
			}

			sb.AppendLine("[/INST]");

			return sb.ToString().Trim();
		}

	}
}
