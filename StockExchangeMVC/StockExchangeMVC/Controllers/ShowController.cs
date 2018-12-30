using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StockExchangeMVC.Infrastructure;
using StockExchangeMVC.Models;
using StockExchangeMVC.Models.ViewModels;

namespace StockExchangeMVC.Controllers
{
	public class ShowController : Controller
	{
		private IRepository _repository;

		public ShowController(IRepository repository)
		{
			_repository = repository;
		}

		public async Task<IActionResult> ShowIndex(string name)
		{
			if (name == "" || name == null) name = WSEIndexItemSingleton.Instance().getFirstItemName;
			ViewBag.Title = name;
			var table = await new Table().GetTableByNameFromDB(name, _repository);

			return View(table);
		}

		public IActionResult ShowMonths(string name, bool json = false)
		{
			if (name == "" || name == null) name = WSEIndexItemSingleton.Instance().getFirstItemName;
			ViewBag.Title = name;

			DateTime dateFrom = DateTime.Today.AddYears(-1);
			DateTime dateTo = DateTime.Today;
			

			if (json) return Json(ChangeData.getMonthRange(dateFrom, dateTo, _repository, name.ToLower()).OrderByDescending(x => x.Date).ToList());

			return View(ChangeData.getMonthRange(dateFrom, dateTo, _repository, name).OrderByDescending(x => x.Date).ToList());
		}

		public IActionResult UpdateAllData(string currentItem)
		{
			Table table = new Table();
			WSEIndexItemSingleton WSESingleton = WSEIndexItemSingleton.Instance();


			table.finishDate = DateTime.Today;
			if (table.finishDate.DayOfWeek == DayOfWeek.Sunday) table.finishDate = table.finishDate.AddDays(-2);
			if (table.finishDate.DayOfWeek == DayOfWeek.Saturday) table.finishDate = table.finishDate.AddDays(-1);


			foreach (string index in WSESingleton.GetIndexes())
			{
				try
				{
					foreach (string item in WSESingleton.Indexes[index].Keys)
					{
						var existFinish = _repository.dayTickWSE.Where(x => x.Date == table.finishDate && x.ItemName == item).SingleOrDefault();

						if (existFinish == null)
						{
							var lastTick = _repository.dayTickWSE.Where(x => x.ItemName == item).OrderByDescending(x => x.Date).First();

							if (lastTick == null) table.startDate = DateTime.Today.AddYears(-1);
							else table.startDate = lastTick.Date.AddDays(1);

							table.Index = index;
							table.Name = item;
							table.GetTable();
							new DayTickWSE().SaveDataFromStooq(_repository);
						}
					}
				}
				catch
				{ }
			}

			if(currentItem == "Signal") return RedirectToAction("SignalMonth", "Signal");

			return RedirectToAction("ShowMonths", "Show", new { name = currentItem });
		}

	}
}
