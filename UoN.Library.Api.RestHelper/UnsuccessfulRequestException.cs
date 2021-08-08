using System;
using System.Net.Http;
using System.Runtime.Serialization;

namespace UoN.Library.Api.RestHelper
{
	[Serializable]
	public class UnsuccessfulRequestException : Exception
	{
		public HttpResponseMessage Response { get; }

		public UnsuccessfulRequestException()
		{
		}

		public UnsuccessfulRequestException(HttpResponseMessage response) =>
			Response = response;

		public UnsuccessfulRequestException(string message) : base(message)
		{
		}

		public UnsuccessfulRequestException(string message, Exception innerException) : base(message, innerException)
		{
		}

		protected UnsuccessfulRequestException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}