using OllamaSharp;
using PTMngVSIX.Abstraction;
using PTMngVSIX.Abstraction.RequestModel;
using PTMngVSIX.Abstraction.ResponseModel;
using PTMngVSIX.Setting;
using System.Threading.Tasks;

namespace PTMngVSIX.LocalOllama
{
	public abstract class BaseModelService : IModelService
	{
		public abstract Task<ResponseBase> Call(RequestBase request);

		public async Task TryConnectAsync()
		{
			try
			{
				var client = new OllamaApiClient(
					ModelSetting.Endpoint
				);

				var version = await client.GetVersionAsync();

				AppState.IsModelAvailable = true;

				if (AppState.ApiClient == null ||
					AppState.ApiClient.GetType() != client.GetType())
				{
					AppState.ApiClient = client;
				}
			}
			catch
			{
				AppState.IsModelAvailable = false;
			}
		}
	}
}
