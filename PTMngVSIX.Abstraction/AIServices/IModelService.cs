using PTMngVSIX.Abstraction.AIServices.RequestModel;
using PTMngVSIX.Abstraction.AIServices.ResponseModel;
using System.Threading.Tasks;

namespace PTMngVSIX.Abstraction.AIServices
{
	public interface IModelService
	{
		public Task TryConnectAsync();
		public Task<ResponseBase> CallAsync(RequestBase request);
	}
}
