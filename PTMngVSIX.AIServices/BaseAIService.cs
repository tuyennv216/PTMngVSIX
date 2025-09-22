using PTMngVSIX.Abstraction.AI;
using PTMngVSIX.Abstraction.AIServices;
using PTMngVSIX.Abstraction.AIServices.RequestModel;
using PTMngVSIX.Abstraction.AIServices.ResponseModel;
using PTMngVSIX.Setting;
using System.Threading.Tasks;

namespace PTMngVSIX.AIServices
{
	public abstract class BaseAIService : IModelService
	{
		public abstract Task<ResponseBase> CallAsync(RequestBase request);

		public async Task TryConnectAsync()
		{
			try
			{
				IChatClient client = new BaseOpenAILikeChatClient(ModelSetting.Endpoint, ModelSetting.ApiKey, ModelSetting.AssistantModelName);

				var response = await client.GetResponseAsync("Không cần làm gì. Hãy phản hồi số 1.");

				if (response.Length > 0)
				{
					AppState.Instance.IsModelAvailable = true;

					if (AppState.Instance.ApiClient == null ||
						AppState.Instance.ApiClient.GetType() != typeof(BaseOpenAILikeChatClient))
					{
						AppState.Instance.ApiClient = client;
					}

				}
				else
				{
					AppState.Instance.IsModelAvailable = false;
				}
			}
			catch
			{
				AppState.Instance.IsModelAvailable = false;
			}
		}
	}
}
