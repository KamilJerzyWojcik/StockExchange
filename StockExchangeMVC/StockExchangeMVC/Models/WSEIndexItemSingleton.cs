using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockExchangeMVC.Models
{
	public class WSEIndexItemSingleton
	{
		private static WSEIndexItemSingleton _wSEIndexItemSingleton;

		private static readonly object _lock = new object();

		private Dictionary<string, string> Wig20 = new Dictionary<string, string>
		{
			["alr"] = "Alior",
			["ccc"] = "CCC",
			["cdr"] = "CDProject",
			["cps"] = "Cyfrowy Polsat",
			["eng"] = "Energa",
			["eur"] = "Eurocash",
			["jsw"] = "JSW",
			["kgh"] = "KGHM",
			["lts"] = "Lotos",
			["lpp"] = "LPP",
			["mbk"] = "mBank",
			["opl"] = "Orange Polska",
			["peo"] = "PEKAO",
			["pge"] = "PGE",
			["pgn"] = "PGNIG",
			["pkn"] = "PKN Orlen",
			["pko"] = "PKO BP",
			["pzu"] = "PZU",
			["pgn"] = "Santander",
			["spl"] = "PGNIG",
			["tpe"] = "Tauron",
		};

		private Dictionary<string, string> mWig40 = new Dictionary<string, string>();

		private Dictionary<string, string> sWig80 = new Dictionary<string, string>();

		public string getFirstItemName
		{
			get
			{
				return Wig20.FirstOrDefault().Key;
			}
		}

		public string getFirstIndexName
		{
			get
			{
				return "wig20";
			}
		}

		public string[] Head = new string[]
		{
			"Walor",
			"Indeks",
			"Data",
			"Otwarcie",
			"Najwyzszy",
			"Najnizszy",
			"Zamkniecie",
			"Zakres"
		};

		public Dictionary<string, Dictionary<string, string>> Indexes
		{
			get
			{
				return new Dictionary<string, Dictionary<string, string>>()
				{

					["wig20"] = Wig20,
					["mwig40"] = mWig40,
					["swig80"] = sWig80
				};

			}
		}

		protected WSEIndexItemSingleton() { }

		public static WSEIndexItemSingleton Instance()
		{
			lock (_lock)
			{
				if (_wSEIndexItemSingleton == null)
				{
					_wSEIndexItemSingleton = new WSEIndexItemSingleton();
				}
				return _wSEIndexItemSingleton;
			}
		}
	}
}