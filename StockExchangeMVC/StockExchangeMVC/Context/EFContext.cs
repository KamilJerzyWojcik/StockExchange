using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using StockExchangeMVC.Models.ViewModels;

namespace StockExchangeMVC.Models
{
	public class EFContext : IdentityDbContext<User, IdentityRole<int>, int>
	{
		public EFContext(DbContextOptions options) : base(options)
		{ }

		public DbSet<DayTickWSE> DayTickWSE { get; set; }
		public DbSet<User> Users { get; set; }
	}
}
