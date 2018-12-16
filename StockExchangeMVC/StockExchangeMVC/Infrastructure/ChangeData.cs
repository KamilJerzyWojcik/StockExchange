using StockExchangeMVC.Models;
using StockExchangeMVC.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockExchangeMVC.Infrastructure
{
	public static class ChangeData
	{
		public static void getMonthRange(DateTime dateFrom, DateTime dateTo, IRepository repository, string name)
		{
			List<DayTickWSE> data = repository.dayTickWSE.Where(x => x.Date > dateFrom && x.Date < dateTo && x.ItemName == name).ToList();
			//dodac data do cache (sesji) i obrabiac dopoki name takie samo
			var monthYear = new List<string>();

			DateTime d1 = dateFrom;
			if (d1.Day != 1)
			{
				if (d1.Month == 12)
				{
					d1 = new DateTime(d1.Year + 1, 1, 1);
				}
				else
				{
					d1 = new DateTime(d1.Year + 1, d1.Month + 1, 1);
				}

			}
			DateTime d2 = d1.AddMonths(1);

			do
			{

				List<DayTickWSE> dataMonth = data.Where(x => x.Date > d1 && x.Date < d2).ToList();

				d1.AddMonths(1);
				d2.AddMonths(1);

			}
			while (d1.Year == dateTo.Year && d1.Month > dateTo.Month);
		}
	}
}
