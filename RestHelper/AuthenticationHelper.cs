using System.Net.Http.Headers;
using System.Text;

namespace RestHelper
{
	public static class AuthenticationHelper
	{
		public static AuthenticationHeaderValue CreateBasicAuthHeader(string username, string password) =>
			new(
				"Basic",
				Convert.ToBase64String(Encoding.ASCII.GetBytes($"{username}:{password}")));

		public static AuthenticationHeaderValue CreateTokenHeader(string token) =>
			new(
				"Bearer",
				token);
	}
}
