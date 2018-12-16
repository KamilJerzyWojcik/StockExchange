using StockExchangeMVC.Infrastructure;
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
		private WSEIndexItemSingleton _wseSingleton;

		public Table()
		{
			_wseSingleton = WSEIndexItemSingleton.Instance();
			Head = _wseSingleton.Head;
		}

		public void GetTable()
		{
			setDefaultData();
			new DayTickWSE().ClearTicks();
			checkExistItem();
			setBody();
		}

		public async Task<Table> GetTableByNameFromDB(string name, IRepository _repository)
		{
			Body = await _repository.getBodyByNameFromDB(name);
			return this;
		}

		private void setBody()
		{
			if (finishDate == DateTime.MinValue || startDate == DateTime.MinValue)
			{
				Body = new List<DayTickWSE>();
				startDate = DateTime.Today.AddYears(-1);
				finishDate = DateTime.Today;
			}
			else
			{
				Body = new DayTickWSE().GetDataFromStooq(startDate, finishDate, Index, Name);
				Head = WSEIndexItemSingleton.Instance().Head;
			}
		}

		internal bool IsCorrectData()
		{
			return (Index == "" || Name == "" || startDate == DateTime.MinValue || finishDate == DateTime.MinValue);
		}

		private void checkExistItem()
		{
			var itemExist = _wseSingleton.Indexes[Index.ToLower()];
			if (itemExist.Count == 0 || !itemExist.ContainsKey(Name)) Name = null;
		}

		private void setDefaultData()
		{
			
			if (Name == null) Name = _wseSingleton.getFirstIndexName;
			if (Index == null) Index = _wseSingleton.getFirstIndexName;
		}
	}
}

