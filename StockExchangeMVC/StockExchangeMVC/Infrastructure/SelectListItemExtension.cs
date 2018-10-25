using Microsoft.AspNetCore.Mvc.Rendering;
using StockExchangeMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockExchangeMVC.Infrastructure
{
	public static class SelectListItemExtension
	{
		public static List<SelectListItem> GetIndexList(this List<SelectListItem> listInput, string index = "wig20")
		{
			List<SelectListItem> indexes = new List<SelectListItem>();

			foreach (var item in WSEIndexItemSingleton.Instance().Indexes.Keys)
			{
				if (item != index)
					indexes.Add(new SelectListItem() { Text = item.ToUpper(), Value = item });
				else
					indexes.Add(new SelectListItem() { Text = item.ToUpper(), Value = item, Selected = true });
			}

			return indexes;
		}

		public static List<SelectListItem> GetSelectList(this List<SelectListItem> listInput, string index, string name)
		{
			var items = new List<SelectListItem>();

			foreach (var item in WSEIndexItemSingleton.Instance().Indexes[index.ToLower()])
			{
				if (name != item.Value)
					items.Add(new SelectListItem() { Text = item.Value, Value = item.Key });
				else
					items.Add(new SelectListItem() { Text = item.Value, Value = item.Key, Selected = true });
			}

			if (items.Count != 0 && name == null) items[0].Selected = true;

			items = items.OrderBy(l => l.Value).ToList();

			return items;
		}
	}
}
