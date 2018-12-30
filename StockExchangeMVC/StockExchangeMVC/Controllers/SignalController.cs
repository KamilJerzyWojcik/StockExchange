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
	public class SignalController : Controller
	{
		private IRepository _repository;

		public SignalController(IRepository repository)
		{
			_repository = repository;
		}

		public IActionResult SignalsMonth(DateTime time, bool all, bool json = false)
		{

			if (json) return Json(SignalMonth.CurrentList);

			return View(ChangeData.getSignalMonth(_repository));
		}
	}
}
