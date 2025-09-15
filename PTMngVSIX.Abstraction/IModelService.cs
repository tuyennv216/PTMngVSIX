using PTMngVSIX.Abstraction.RequestModel;
using PTMngVSIX.Abstraction.ResponseModel;
using System.Threading.Tasks;

namespace PTMngVSIX.Abstraction
{
	public interface IModelService
	{
		Task TryConnectAsync();
		Task<ResponseBase> Call(RequestBase request);
	}
}
