using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockExchangeMVC.Models
{
	public class WSEIndexItemSingleton
	{
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

		public static Dictionary<string, string> Wig20 = new Dictionary<string, string>
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

		public static Dictionary<string, string> mWig40 = new Dictionary<string, string>();
		
		public static Dictionary<string, string> sWig80 = new Dictionary<string, string>();

		public static Dictionary<string, Dictionary<string, string>> Indexes = new Dictionary<string, Dictionary<string, string>>
		{
			["Wig20"] = Wig20,
			["mWig40"] = mWig40,
			["sWig80"] = sWig80
		};
	}
}