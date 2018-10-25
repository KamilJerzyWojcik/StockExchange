using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockExchangeMVC.Models.ViewModels
{
	public class Table
	{
		public string[] Head { get; set; }
		public List<DayTickWSE> Body { get; set; }
		public string[] Footer { get; set; }
		public DateTime startDate { get; set; }
		public DateTime finishDate { get; set; }
		public string Index { get; set; }
		public string Name { get; set; }
	}
}

