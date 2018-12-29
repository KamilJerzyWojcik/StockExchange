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
			dateTo = dateTo.AddDays(1);

			if (json) return Json(ChangeData.getMonthRange(dateFrom, dateTo, _repository, name.ToLower()).OrderByDescending(x => x.Date).ToList());

			return View(ChangeData.getMonthRange(dateFrom, dateTo, _repository, name).OrderByDescending(x => x.Date).ToList());
		}
	}
}
