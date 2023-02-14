namespace RestHelper
{
	[Serializable]
	public class UnsuccessfulRequestException : Exception
	{
		public HttpResponseMessage Response { get; }

		public UnsuccessfulRequestException(HttpResponseMessage response) =>
			Response = response;
	}
}