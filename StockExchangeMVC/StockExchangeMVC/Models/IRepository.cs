using StockExchangeMVC.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockExchangeMVC.Models
{
	public interface IRepository
	{
		IQueryable<DayTickWSE> dayTickWSE { get; }
		void SaveData(DayTickWSE dayTickWSE);
		Task<List<DayTickWSE>> GetDataFromDB(Table table);
		Task<List<DayTickWSE>> getBodyByNameFromDB(string name);
	}
}
