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
				return CreateObject.GetWebClient();
			}
		}

		public static List<DayTickWSE> GetDataFromStooq(this DayTickWSE dayTickWSE, DateTime startDate, DateTime finishDate, string index = "", string name = "")
		{
			//przekaznie obiektu np. query
			_ticks = new List<DayTickWSE>();
			_link = ConvertData.getLink(startDate, finishDate, name);
			try
			{
				using (WebClient client = _client)
					_result = client.DownloadString(_link);

				addRange(index, name);

				return _ticks;
			}
			catch (Exception)
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

		private static void addRange(string index, string name)
		{
			string[] data = _result.Split('\n');
			for (int i = 1; i <= data.Length - 1; i++)
			{
				string[] item = data[i].Split(',');
				if (item.Length == 6)
				{
					item = ConvertData.SetCorrectSeparator(item, new List<int> { 1, 2, 3, 4 });

					_ticks.Add(ConvertData.CreateTick(item, index, name));
				}
			}
		}

	}
}
