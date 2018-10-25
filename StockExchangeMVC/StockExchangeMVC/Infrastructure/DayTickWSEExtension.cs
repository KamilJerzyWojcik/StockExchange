using StockExchangeMVC.Models;
using StockExchangeMVC.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace StockExchangeMVC.Infrastructure
{
	public static class DayTickWSEExtension
	{
		private static List<DayTickWSE> _ticks;
		private static string _link;
		private static string _result;
		private static WebClient _client
		{
			get
			{
				WebClient c = new WebClient();
				c.Headers.Add("User-Agent: Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; WOW64; Trident/5.0)");
				c.Headers.Add("Content-Type", "application/json");
				c.UseDefaultCredentials = true;
				return c;
			}
		}

		public static List<DayTickWSE> GetDataFromStooq(this DayTickWSE dayTickWSE, DateTime startDate, DateTime finishDate, string index = "", string name = "")
		{
			//przekaznie obiektu np. query
			_ticks = new List<DayTickWSE>();
			_link = getLink(startDate, finishDate, name);
			try
			{
				using (WebClient client = _client)
					_result = client.DownloadString(_link);

				addRange(index, name);

				return _ticks;
			}
			catch(Exception)
			{
				return null;
			}
		}

		public static void ClearTicks(this DayTickWSE dayTickWSE)
		{
			_ticks = new List<DayTickWSE>();
		}

		public static void SaveDataFromStooq(this DayTickWSE dayTickWSE, IRepository repository)
		{
			if (_ticks != null && _ticks.Count != 0)
			{
				foreach (var t in _ticks)
				{
					repository.SaveData(t);
				}

			}
		}

		private static string getLink(DateTime startDate, DateTime finishDate, string name)
		{
			string start = GetDate(startDate);
			string finish = GetDate(finishDate);

			return $"https://stooq.pl/q/d/l/?s={name}&d1={start}&d2={finish}&i=d";
		}

		private static void addRange(string index, string name)
		{
			string[] data = _result.Split('\n');
			for (int i = 1; i <= data.Length - 1; i++)
			{
				string[] item = data[i].Split(',');
				if (item.Length == 6)
				{
					_ticks.Add(new DayTickWSE
					{
						ItemName = name,
						IndexName = index,
						Date = DateTime.Parse(item[0]),
						Open = decimal.Parse(item[1]),
						High = decimal.Parse(item[2]),
						Low = decimal.Parse(item[3]),
						Close = decimal.Parse(item[4])
					});
				}
			}
		}

		private static string GetDate(DateTime date)
		{
			string day, month;

			if (date.Day > 9)
				day = $"{date.Day.ToString()}";
			else
				day = $"0{date.Day.ToString()}";

			if (date.Month > 9)
				month = $"{date.Month.ToString()}";
			else
				month = $"0{date.Month.ToString()}";

			return $"{date.Year}{month}{day}";

		}

		
	}
}
