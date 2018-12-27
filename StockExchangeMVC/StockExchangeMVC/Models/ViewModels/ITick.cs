using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockExchangeMVC.Models.ViewModels
{
	public interface ITick
	{
		int ID { get; set; }
		string ItemName { get; set; }
		string IndexName { get; set; }
		DateTime Date { get; set; }
		decimal Open { get; }
		decimal High { get; }
		decimal Low { get; }
		decimal Close { get; }
		decimal Range { get; }
	}
}
