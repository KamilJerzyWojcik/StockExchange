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
			dateTo = dateTo.AddDays(1);
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

			int iterator = 0;

			while (true)
			{

				MonthTick month = new MonthTick();

				month.DayTicksTable.Body = data.Where(x => x.Date > date1 && x.Date < date2).ToList();
				month.ItemName = name;
				month.Date = date1;
				month.FinishDate = date2;
				iterator++;
				month.ID = iterator;

				monthTicks.Add(month);

				if (date1.Year == dateTo.Year && date1.Month == dateTo.Month) break;

				date1 = date1.AddMonths(1);
				date2 = date2.AddMonths(1);
			}

			return monthTicks;
		}

		public static List<SignalMonth> getSignalMonth(IRepository repository)
		{
			DateTime dateFrom = DateTime.Today.AddMonths(-4);
			DateTime dateTo = DateTime.Today;

			List<SignalMonth> signals = new List<SignalMonth>();
			var WSESingleton = WSEIndexItemSingleton.Instance();
			int iterator = 0;

			foreach (string index in WSESingleton.GetIndexes())
			{
				try
				{
					foreach (string item in WSESingleton.Indexes[index].Keys)
					{

						SignalMonth signalMonth = new SignalMonth();
						signalMonth.StartDate = new DateTime(dateTo.Year, dateTo.Month, 1);

						var months = getMonthRange(dateFrom, dateTo, repository, item);
						if (months.Count == 4)
						{
							signalMonth.NameItem = item;
							signalMonth.DayTicksTable = months[3].DayTicksTable.Body;
							signalMonth.monthTick = months[3];

							iterator++;
							signalMonth.ID = iterator;
							signalMonth.AvarageRange = (months[0].Range + months[1].Range + months[2].Range) / 3;
							decimal min = 0;
							decimal max = 0;

							for (int i = 0; i < months[3].DayTicksTable.Body.Count; i++)
							{
								if (i == 0)
								{
									max = months[3].DayTicksTable.Body[i].High;
									min = months[3].DayTicksTable.Body[i].Low;
								}
								else
								{
									if (months[3].DayTicksTable.Body[i].High > max)
									{
										max = months[3].DayTicksTable.Body[i].High;
									}

									if (months[3].DayTicksTable.Body[i].Low < min)
									{
										min = months[3].DayTicksTable.Body[i].Low;
									}
								}
								if (i < 5) signalMonth.DayPercentRange.Add($"{Math.Round(100 * (max - min) / signalMonth.AvarageRange, 1)}% ({(i + 1)})");
								else signalMonth.DayPercentRange.Add($"-{i + 1}-");
							}

							SignalMonth.CurrentList = signals;

							signals.Add(signalMonth);
						}
					}
				}
				catch { }
			}

			return signals;
		}
	}
}