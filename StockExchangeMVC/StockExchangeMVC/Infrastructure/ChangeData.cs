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
		public static List<MonthTick> getMonthRange(DateTime dateFrom, DateTime dateTo, IRepository repository, string name)
		{
			List<DayTickWSE> data = repository.dayTickWSE.Where(x => x.Date > dateFrom && x.Date < dateTo && x.ItemName == name).ToList();
			//dodac data do cache (sesji) i obrabiac dopoki name takie samo

			List<MonthTick> monthTicks = new List<MonthTick>();

			DateTime date1 = dateFrom;
			if (date1.Day != 1)
			{
				if (date1.Month == 12)
				{
					date1 = new DateTime(date1.Year + 1, 1, 1);
				}
				else
				{
					date1 = new DateTime(date1.Year, date1.Month + 1, 1);
				}

			}
			DateTime date2 = date1.AddMonths(1);

			while (true)
			{

				MonthTick month = new MonthTick();

				month.DayTicksTable.Body = data.Where(x => x.Date > date1 && x.Date < date2).ToList();
				month.ItemName = name;
				month.Date = date1;
				month.FinishDate = date2;

				monthTicks.Add(month);

				if (date1.Year == dateTo.Year && date1.Month == dateTo.Month) break;

				date1 = date1.AddMonths(1);
				date2 =  date2.AddMonths(1);
			}

			return monthTicks;
		}
	}
}
