using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockExchangeMVC.Models.ViewModels
{
	public class MonthTick : ITick
	{
		public int ID { get; set; }

		public DateTime Date { get; set; }
		public DateTime FinishDate { get; set; }

		public string ItemName { get; set; }
		public string IndexName { get; set; }

		public decimal Open => DayTicksTable.Body.OrderBy(x => x.Date).First().Open;
		public decimal High => DayTicksTable.Body.OrderBy(x => x.High).Last().High;
		public decimal Low => DayTicksTable.Body.OrderBy(x => x.Low).First().Low;
		public decimal Close => DayTicksTable.Body.OrderBy(x => x.Date).Last().Close;
		public decimal Range => High - Low;

		public Dictionary<DateTime, decimal> DailyChangeRange
		{
			get
			{
				Dictionary<DateTime, decimal> result = new Dictionary<DateTime, decimal>();
				List<DayTickWSE> dt = DayTicksTable.Body.OrderBy(x => x.Date).ToList();

				for (int i = 0; i < dt.Count - 1; i++)
				{
					decimal min = dt[0].Low;
					decimal max = dt[0].High;

					for (int j = 1; j <= i - 1; j++)
					{
						if (min > dt[j].Low) min = dt[j].Low;
						
						if (max < dt[j].High) max = dt[j].High;
					}
					result.Add(DayTicksTable.Body[i].Date, max - min);
				}

				return result;
			}
		}

		public Table DayTicksTable { get; set; }

		public MonthTick()
		{
			DayTicksTable = new Table();
		}


	}
}
