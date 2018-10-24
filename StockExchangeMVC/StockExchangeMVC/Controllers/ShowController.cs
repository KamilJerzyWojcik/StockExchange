using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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

		public IActionResult ShowIndex(string name = "alr")
		{
			ViewBag.Title = name;

			var table = new Table();

			table.Body = _repository.dayTickWSE.Where(t => t.ItemName == name).OrderBy(t => t.Date).ToList();

			return View(table);
		}
	}
}
