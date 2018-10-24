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

		public IActionResult Index(DateTime startDate, DateTime finishDate, string index, string name)
		{
			new DayTickWSE().ClearTicks();
			Table table;
			ViewBag.CurrentDate = DateTime.Today.ToShortDateString();

			if (finishDate == DateTime.MinValue)
			{
				ViewBag.StartDate = DateTime.Today.AddYears(-1).ToShortDateString();
				table = null;
			}
			else
			{
				ViewBag.StartDate = startDate.ToShortDateString();
				ViewBag.Name = name;
				ViewBag.Index = index;
				table = new Table();
				table.Body = new DayTickWSE().GetDataFromStooq(startDate, finishDate, index, name);
			}
			
			List<SelectListItem> listIndex = new List<SelectListItem>();
			listIndex = listIndex.GetDataFromStooq(index);
			List<SelectListItem> listItem = listIndex.GetSelectList(name);

			ViewBag.ListItem = listItem;
			ViewBag.ListIndex = listIndex;

			return View(table);
		}

		public IActionResult AddData(DateTime startDate, DateTime finishDate, string index, string name)
		{
			if (index == "" || name == "" || startDate == DateTime.MinValue || finishDate == DateTime.MinValue)
				return RedirectToAction("Index", "Home");

			new DayTickWSE().SaveDataFromStooq(_repository);
			Table table = new Table();
			table.Body = _repository.dayTickWSE.Where(d => d.Date > startDate && d.Date < finishDate && d.ItemName == name && d.IndexName == index)
				.OrderBy(x => x.Date).ToList();

			return View(table);
		}

	}
}
