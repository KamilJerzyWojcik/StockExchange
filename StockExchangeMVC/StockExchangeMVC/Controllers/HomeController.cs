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
			if (table.Name == null) table.Name = "alr";
			if (table.Index == null)  table.Index = "Wig20";

			new DayTickWSE().ClearTicks();
			

			var itemExist = WSEIndexItemSingleton.Instance().Indexes[table.Index.ToLower()];

			if(itemExist.Count == 0 || !itemExist.ContainsKey(table.Name)) table.Name = null;

			if ( table.finishDate == DateTime.MinValue  || table.startDate == DateTime.MinValue)
			{
				table.Body = new List<DayTickWSE>();
				table.startDate = DateTime.Today.AddYears(-1);
				table.finishDate = DateTime.Today;
			}
			else
			{
				table.Body = new DayTickWSE().GetDataFromStooq(table.startDate, table.finishDate, table.Index, table.Name);
				table.Head = WSEIndexItemSingleton.Instance().Head;
			}

			ViewBag.ListItem = new List<SelectListItem>().GetSelectList(table.Index, table.Name);
			ViewBag.ListIndex = new List<SelectListItem>().GetIndexList(table.Index);

			return View(table);
		}

		public IActionResult AddData(Table table)
		{
			if (table.Index == "" || table.Name == "" || table.startDate == DateTime.MinValue || table.finishDate == DateTime.MinValue)
				return RedirectToAction("Index", "Home");

			new DayTickWSE().SaveDataFromStooq(_repository);

			table.Body = _repository.dayTickWSE.Where(d => d.Date > table.startDate && d.Date < table.finishDate && d.ItemName == table.Name && d.IndexName == table.Index).OrderBy(x => x.Date).ToList();

			return View(table);
		}

	}
}
