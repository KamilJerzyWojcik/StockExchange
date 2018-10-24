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
		public DateTime startDate;
		public DateTime finishDate;
		public string index;
		public string name;

		public Table()
		{
			Body = new List<DayTickWSE>();
			Head = new string[] { "Walor", "Indeks", "Data", "Otwarcie", "Najwyzszy", "Najnizszy", "Zamkniecie", "Zakres" };
		}
	}
}

