using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StockExchangeMVC.Models;
using StockExchangeMVC.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockExchangeMVC.Components
{
	public class TableViewComponent : ViewComponent
	{
		private IRepository _repository;

		public TableViewComponent(IRepository repository)
		{
			_repository = repository;
		}

		public async Task<IViewComponentResult> InvokeAsync(dynamic data)
		{
			try
			{
				string name = data;

				Table table = await new Table().GetTableByNameFromDB(name, _repository);
				return View(table);
			}
			catch (Exception)
			{
				return View(data);
			}

		}
	}
}
