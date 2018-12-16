using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace StockExchangeMVC.Infrastructure
{
	public static class CreateObject
	{
		internal static WebClient GetWebClient()
		{
			WebClient client = new WebClient();
			client.Headers.Add("User-Agent: Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; WOW64; Trident/5.0)");
			client.Headers.Add("Content-Type", "application/json");
			client.UseDefaultCredentials = true;

			return client;
		}
	}
}
