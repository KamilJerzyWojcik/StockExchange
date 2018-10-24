using System;
using System.Net;

namespace StockExchange
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Start");
			Console.WriteLine("podaj początek: rrrrmmdd");
			string start = Console.ReadLine();

			Console.WriteLine("podaj koniec: rrrrmmdd");
			string finish = Console.ReadLine();


			string result = "";
			string link = $"https://stooq.pl/q/d/l/?s=wig20&d1={start}&d2={finish}&i=d";

			using (WebClient client = new WebClient())
			{
				client.Headers.Add("User-Agent: Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; WOW64; Trident/5.0)");
				client.Headers.Add("Content-Type", "application/json");
				client.UseDefaultCredentials = true;
				result = client.DownloadString(link);
			}

			string[] table = result.Split('\n');
			string[] day = table[6].Split(',');
			string[] date = day[0].Split('-');

			Console.WriteLine(day);

			Console.WriteLine(date[0]);
			Console.WriteLine(date[1]);
			Console.WriteLine(date[2]);

			Console.WriteLine("koniec");

			Console.ReadKey();
		}
	}
}
