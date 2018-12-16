using StockExchangeMVC.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockExchangeMVC.Infrastructure
{
	public static class ConvertData
	{
		internal static string GetDate(DateTime date)
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

		internal static string[] SetCorrectSeparator(string[] item, List<int> list)
		{
			var sep = System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;

			foreach(int index in list)
			{
				if (!item[index].Contains(sep))
				{
					if (sep == ",")
						item[index] = item[index].Replace(".", sep);
					else
						item[index] = item[index].Replace(",", sep);
				}
			}

			return item;
		}

		internal static DayTickWSE CreateTick(string[] item, string index, string name)
		{
			try
			{
				return new DayTickWSE()
				{
					ItemName = name,
					IndexName = index,
					Date = DateTime.Parse(item[0]),
					Open = decimal.Parse(item[1]),
					High = decimal.Parse(item[2]),
					Low = decimal.Parse(item[3]),
					Close = decimal.Parse(item[4])
				};
			}
			catch
			{
				return null;
			}
		}

		internal static string getLink(DateTime startDate, DateTime finishDate, string name)
		{
			string start = GetDate(startDate);
			string finish = GetDate(finishDate);

			return $"https://stooq.pl/q/d/l/?s={name}&d1={start}&d2={finish}&i=d";
		}
	}
}
