using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockExchangeMVC.Infrastructure
{
	public static class SelectListItemExtension
	{
		public static List<SelectListItem> GetDataFromStooq(this List<SelectListItem> listInput, string index)
		{
			var list = new List<SelectListItem>() {
				new SelectListItem(){Text = "Wybierz Index", Value = null},
				new SelectListItem(){Text = "WIG20", Value = "wig20"},
				new SelectListItem(){Text = "mWIG40", Value = "mwig40"},
				new SelectListItem(){Text = "sWig80", Value = "swig80"},
			};
			var listOutput = new List<SelectListItem>();

			foreach (var l in list)
			{
				if (l.Value == "wig20")
				{
					l.Selected = true;
					listOutput.Add(l);
				}
				else
				{
					listOutput.Add(l);
				}
			}
			return listOutput;
		}

		public static List<SelectListItem> GetSelectList(this List<SelectListItem> listInput, string name)
		{
			var list = new List<SelectListItem>()
			{
				new SelectListItem(){Text = "Wybierz walor", Value = null},
				new SelectListItem(){Text = "WIG20", Value = "wig20"},
				new SelectListItem(){Text = "Alior", Value = "alr"},
				new SelectListItem(){Text = "CCC", Value = "ccc"},
				new SelectListItem(){Text = "CDProject", Value = "cdr"},
				new SelectListItem(){Text = "Cyfrowy Polsat", Value = "cps"},
				new SelectListItem(){Text = "Energa", Value = "eng"},
				new SelectListItem(){Text = "Eurocash", Value = "eur"},
				new SelectListItem(){Text = "JSW", Value = "jsw"},
				new SelectListItem(){Text = "KGHM", Value = "kgh"},
				new SelectListItem(){Text = "Lotos", Value = "lts"},
				new SelectListItem(){Text = "LPP", Value = "lpp"},
				new SelectListItem(){Text = "mBank", Value = "mbk"},
				new SelectListItem(){Text = "Orange Polska", Value = "opl"},
				new SelectListItem(){Text = "PEKAO", Value = "peo"},
				new SelectListItem(){Text = "PGE", Value = "pge"},
				new SelectListItem(){Text = "PGNIG", Value = "pgn"},
				new SelectListItem(){Text = "PKN Orlen", Value = "pkn"},
				new SelectListItem(){Text = "PKO BP", Value = "pko"},
				new SelectListItem(){Text = "PZU", Value = "pzu"},
				new SelectListItem(){Text = "Santander", Value = "SPL"},
				new SelectListItem(){Text = "Tauron", Value = "tpe"}
			};
			var listOutput = new List<SelectListItem>();

			foreach (var l in list)
			{
				if (l.Value == name)
				{
					l.Selected = true;
					listOutput.Add(l);
				}
				else
				{
					listOutput.Add(l);
				}
			}

			listOutput = listOutput.OrderBy(l => l.Value).ToList();

			return listOutput;
		}
	}
}
