using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockExchangeMVC.Models.ViewModels
{
	public class DayTickWSE
	{
		public int ID { get; set; }
		public string ItemName { get; set; }
		public string IndexName { get; set; }
		public DateTime Date { get; set; }
		public decimal Open { get; set; }
		public decimal High { get; set; }
		public decimal Low { get; set; }
		public decimal Close { get; set; }
		public decimal Range
		{
			get
			{
				return High - Low;
			}

		}

		public DayTickWSE() { }
	}
}
