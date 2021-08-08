using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace RestHelper
{
	public class RestHelper : IRestHelper
	{
		private readonly HttpClient _client;
		private readonly string _mediaType;

		public RestHelper(
			string baseAddress,
			AuthenticationHeaderValue authorization = null,
			string mediaType = "application/json")
		{
			// set content media type 
			_mediaType = mediaType;
			// create client
			_client = new HttpClient
			{
				BaseAddress = new Uri(baseAddress),
				Timeout = TimeSpan.FromMinutes(10)
			};
			// only accept json
			_client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			// auth type
			_client.DefaultRequestHeaders.Authorization = authorization;
		}

		public async Task<T> Get<T>(string requestUri) =>
			await GetDeserializedResponse<T>(await _client.GetAsync(requestUri));

		public async Task<T> GetOData<T>(string requestUri) =>
			await GetDeserializedODataResponse<T>(await _client.GetAsync(requestUri));

		public async Task<string> GetReturnJson(string requestUri) =>
			await GetStringResponse(await _client.GetAsync(requestUri));

		public async Task Post<T>(string requestUri, T data) =>
			CheckResponse(await _client.PostAsync(requestUri, CreateStringContent(data)));

		public async Task<T> PostReturnObject<T>(string requestUri, T data) =>
			await GetDeserializedResponse<T>(await _client.PostAsync(requestUri, CreateStringContent(data)));

		public async Task<TOut> PostReturnObject<Tin, TOut>(string requestUri, Tin data) =>
			await GetDeserializedResponse<TOut>(await _client.PostAsync(requestUri, CreateStringContent(data)));

		public async Task<string> PostReturnJson<T>(string requestUri, T data) =>
			await GetStringResponse(await _client.PostAsync(requestUri, CreateStringContent(data)));

		public async Task Put<T>(string requestUri, T data) =>
			CheckResponse(await _client.PutAsync(requestUri, CreateStringContent(data)));

		public async Task<T> PutReturnObject<T>(string requestUri, T data) =>
			await GetDeserializedResponse<T>(await _client.PutAsync(requestUri, CreateStringContent(data)));

		public async Task<TOut> PutReturnObject<TIn, TOut>(string requestUri, TIn data) =>
			await GetDeserializedResponse<TOut>(await _client.PutAsync(requestUri, CreateStringContent(data)));

		public async Task<string> PutReturnJson<T>(string requestUri, T data) =>
			await GetStringResponse(await _client.PutAsync(requestUri, CreateStringContent(data)));

		public async Task Patch<T>(string requestUri, T data) =>
			CheckResponse(await _client.PatchAsync(requestUri, CreateStringContent(data)));

		public async Task<T> PatchReturnObject<T>(string requestUri, T data) =>
			await GetDeserializedResponse<T>(await _client.PatchAsync(requestUri, CreateStringContent(data)));

		public async Task<TOut> PatchReturnObject<TIn, TOut>(string requestUri, TIn data) =>
			await GetDeserializedResponse<TOut>(await _client.PatchAsync(requestUri, CreateStringContent(data)));

		public async Task<string> PatchReturnJson<T>(string requestUri, T data) =>
			await GetStringResponse(await _client.PatchAsync(requestUri, CreateStringContent(data)));

		private void CheckResponse(HttpResponseMessage response)
		{
			if (!response.IsSuccessStatusCode)
			{
				throw CreateException(response);
			}
		}

		private static async Task<T> GetDeserializedResponse<T>(HttpResponseMessage response) =>
			response.IsSuccessStatusCode
				? await Deserialize<T>(response)
				: throw CreateException(response);

		private static async Task<T> GetDeserializedODataResponse<T>(HttpResponseMessage response) =>
			response.IsSuccessStatusCode
				? (await Deserialize<ODataObject<T>>(response)).Value
				: throw CreateException(response);

		private static async Task<string> GetStringResponse(HttpResponseMessage response) =>
			response.IsSuccessStatusCode
				? await GetContent(response)
				: throw CreateException(response);

		private static async Task<T> Deserialize<T>(HttpResponseMessage response) =>
			JsonConvert.DeserializeObject<T>(await GetContent(response));

		private StringContent CreateStringContent<T>(T data)
		{
			var stringContent = new StringContent(
				JsonConvert.SerializeObject(
					data,
					// ignore null values
					new JsonSerializerSettings
					{
						NullValueHandling = NullValueHandling.Ignore
					}),
				Encoding.UTF8,
				_mediaType);
			// the worktribe API needs this to be unset
			// this could be passed in to the RestHelper constructer if other APIs need something diffrent
			stringContent.Headers.ContentType.CharSet = "";
			return stringContent;
		}

		private static async Task<string> GetContent(HttpResponseMessage response) =>
			await response.Content.ReadAsStringAsync();

		private static UnsuccessfulRequestException CreateException(HttpResponseMessage response) =>
			new UnsuccessfulRequestException(response);
	}
}
