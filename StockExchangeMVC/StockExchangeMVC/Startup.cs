using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StockExchangeMVC.Models;
using StockExchangeMVC.Models.ViewModels;

namespace StockExchangeMVC
{
	public class Startup
	{
		protected IConfigurationRoot Configuration;

		public Startup()
		{
			var configurationBuilder = new ConfigurationBuilder();
			configurationBuilder.AddXmlFile("appsettings.xml");
			Configuration = configurationBuilder.Build();
		}

		public void ConfigureServices(IServiceCollection services)
		{
			services.AddDbContext<EFContext>( builer => builer.UseSqlServer(Configuration["connectionString"]));
			services.AddIdentity<User, IdentityRole<int>>().
				AddEntityFrameworkStores<EFContext>().AddDefaultTokenProviders();
			services.AddTransient<IRepository, EFRepository>();
			services.AddMvc();
		}

		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseStaticFiles();
			app.UseAuthentication();
			app.UseMvc(routes =>
			{
				routes.MapRoute("default", "{controller=Signal}/{action=SignalsMonth}");
			});
		}
	}
}
