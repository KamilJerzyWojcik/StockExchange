using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using StockExchangeMVC.Infrastructure;
using StockExchangeMVC.Models;
using StockExchangeMVC.Models.ViewModels;

namespace StockExchangeMVC.Controllers
{
	public class HomeController : Controller
	{
		private IRepository _repository;

		public HomeController(IRepository repository)
		{
			_repository = repository;
		}

		public IActionResult Index(Table table)
		{
			table.GetTable();
			ViewBag.ListItem = new List<SelectListItem>().GetListItem(table.Index, table.Name);
			ViewBag.ListIndex = new List<SelectListItem>().GetIndexList(table.Index);

			return View(table);
		}

		public async Task<IActionResult> AddData(Table table)
		{
			if(table.IsCorrectData())
				return RedirectToAction("Index", "Home");

			new DayTickWSE().SaveDataFromStooq(_repository);
			table.Body = await _repository.GetDataFromDB(table);

			return View(table);
		}

		public IActionResult Opis()
		{
			return View();
		}

	}
}
