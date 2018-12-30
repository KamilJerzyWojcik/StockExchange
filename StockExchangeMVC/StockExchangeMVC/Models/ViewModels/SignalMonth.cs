using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockExchangeMVC.Models.ViewModels
{
	public class SignalMonth
	{
		public int ID { get; set; }
		public static List<SignalMonth> CurrentList { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime FinishDate
		{
			get
			{
				return new DateTime(StartDate.Year, StartDate.Month, DateTime.DaysInMonth(StartDate.Year, StartDate.Month));
			}
		}
		public string NameItem { get; set; }
		public decimal AvarageRange { get; set; }
		public List<string> DayPercentRange { get; set; }
		public List<DayTickWSE> DayTicksTable { get; set; }
		public MonthTick monthTick { get; set; }
		public MonthTick monthTickBefore { get; set; }


		public SignalMonth()
		{
			DayTicksTable = new List<DayTickWSE>();
			DayPercentRange = new List<string>();
		}
	}
}
