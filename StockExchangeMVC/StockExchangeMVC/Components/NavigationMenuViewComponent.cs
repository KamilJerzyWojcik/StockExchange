using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StockExchangeMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockExchangeMVC.Components
{
	public class NavigationMenuViewComponent : ViewComponent
	{
		private IRepository _repository;

		public NavigationMenuViewComponent(IRepository repository)
		{
			_repository = repository;
		}

		public async Task<IViewComponentResult> InvokeAsync()
		{
			
			ViewBag.SelectedCategory = ViewData.Values.ToList()[0];

			var menuList = await _repository.dayTickWSE.Select(d => d.ItemName).Distinct().OrderBy(d => d).ToListAsync();

			return View(menuList);
		}
	}
}
