using System;
using System.Net.Http.Headers;
using System.Text;

namespace UoN.Library.Api.RestHelper
{
	public static class AuthenticationHelper
	{
		public static AuthenticationHeaderValue CreateBasicAuthHeader(string username, string password) =>
			new AuthenticationHeaderValue(
				"Basic",
				Convert.ToBase64String(Encoding.ASCII.GetBytes($"{username}:{password}")));

		public static AuthenticationHeaderValue CreateTokenHeader(string token) =>
			new AuthenticationHeaderValue("Bearer", token);
	}
}
