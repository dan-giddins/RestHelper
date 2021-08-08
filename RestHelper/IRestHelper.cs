using System.Threading.Tasks;

namespace RestHelper
{
	public interface IRestHelper
	{
		Task<T> Get<T>(string requestUri);
		Task<T> GetOData<T>(string requestUri);
		Task<string> GetReturnJson(string requestUri);
		Task Patch<T>(string requestUri, T data);
		Task<T> PatchReturnObject<T>(string requestUri, T data);
		Task<TOut> PatchReturnObject<TIn, TOut>(string requestUri, TIn data);
		Task<string> PatchReturnJson<T>(string requestUri, T data);
		Task Post<T>(string requestUri, T data);
		Task<T> PostReturnObject<T>(string requestUri, T data);
		Task<TOut> PostReturnObject<TIn, TOut>(string requestUri, TIn data);
		Task<string> PostReturnJson<T>(string requestUri, T data);
		Task Put<T>(string requestUri, T data);
		Task<T> PutReturnObject<T>(string requestUri, T data);
		Task<TOut> PutReturnObject<TIn, TOut>(string requestUri, TIn data);
		Task<string> PutReturnJson<T>(string requestUri, T data);
	}
}