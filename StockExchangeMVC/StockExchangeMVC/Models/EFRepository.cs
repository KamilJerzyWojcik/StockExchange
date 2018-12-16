using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StockExchangeMVC.Models.ViewModels;

namespace StockExchangeMVC.Models
{
	public class EFRepository : IRepository
	{
		private EFContext _DbContext;

		public EFRepository(EFContext DbContext)
		{
			_DbContext = DbContext;
		}

		public IQueryable<DayTickWSE> dayTickWSE => _DbContext.DayTickWSE;

		public void SaveData(DayTickWSE dayTickWSE)
		{
			if (_DbContext.DayTickWSE.Where(i => i.Date == dayTickWSE.Date && i.ItemName == dayTickWSE.ItemName).ToList().Count == 0)
			{
				_DbContext.DayTickWSE.Add(dayTickWSE);
				_DbContext.SaveChanges();
			}
		}

		public List<DayTickWSE> GetDataFromDB(Table table)
		{
			return dayTickWSE
				.Where(d => d.Date > table.startDate && d.Date < table.finishDate && d.ItemName == table.Name && d.IndexName == table.Index)
				.OrderBy(x => x.Date)
				.ToList();
		}

		public List<DayTickWSE> getBodyByNameFromDB(string name)
		{
			return dayTickWSE.Where(t => t.ItemName == name).OrderBy(t => t.Date).ToList();
		}
	}
}
