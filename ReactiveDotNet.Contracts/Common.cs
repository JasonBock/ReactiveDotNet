using System;

namespace ReactiveDotNet.Contracts
{
	public static class Common
	{
		public static readonly Uri BaseUri = new Uri("http://localhost:56012");
		public static readonly string KeyWatcherPartialUri = "/kwh";
		public static readonly Uri KeyWatcherHubApiUri = new Uri(Common.BaseUri, Common.KeyWatcherPartialUri);
		public static readonly Uri KeyWatcherApiUri = new Uri(Common.BaseUri, "/api/keywatcher");
	}
}
