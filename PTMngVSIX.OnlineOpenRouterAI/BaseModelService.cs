using Microsoft.Extensions.AI;
using OpenAI;
using OpenAI.Chat;
using PTMngVSIX.Abstraction;
using PTMngVSIX.Abstraction.RequestModel;
using PTMngVSIX.Abstraction.ResponseModel;
using PTMngVSIX.Setting;
using System;
using System.ClientModel;
using System.Threading.Tasks;

namespace PTMngVSIX.OnlineOpenRouterAI
{
	public abstract class BaseModelService : IModelService
	{
		public abstract Task<ResponseBase> Call(RequestBase request);

		public async Task TryConnectAsync()
		{
			try
			{
				IChatClient client = new ChatClient(
					model: ModelSetting.AssistantModelName,
					credential: new ApiKeyCredential(ModelSetting.ApiKey),
					options: new OpenAIClientOptions()
					{
						Endpoint = new Uri(ModelSetting.Endpoint)
					}
				).AsIChatClient();

				var response = await client.GetResponseAsync("Không cần làm gì. Hãy phản hồi số 1.");

				if (response.Text.Length > 0)
				{
					AppState.IsModelAvailable = true;

					if (AppState.ApiClient == null ||
						AppState.ApiClient.GetType() != typeof(OpenAIClient))
					{
						AppState.ApiClient = client;
					}

				}
				else
				{
					AppState.IsModelAvailable = false;
				}
			}
			catch
			{
				AppState.IsModelAvailable = false;
			}
		}
	}
}
