using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using EthMLM.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using EthMLM.Models;
//using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication.Cookies;




namespace EthMLM
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			//services.AddDbContext<ApplicationDbContext>(options =>
			//	options.UseSqlServer(
			//		Configuration.GetConnectionString("DefaultConnection")));
			services.AddDbContext<ApplicationDbContext>(options => options.UseInMemoryDatabase(databaseName: "EthMLM"));
			services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
				.AddEntityFrameworkStores<ApplicationDbContext>();
			services.AddControllersWithViews().AddRazorRuntimeCompilation();
			//services.AddControllersWithViews();
			services.AddRazorPages();

			services.ConfigureApplicationCookie(options =>
			{
				options.AccessDeniedPath = new PathString("/Account/AccessDenied");
				options.Cookie.Name = "Cookie";
				options.Cookie.HttpOnly = true;
				options.ExpireTimeSpan = TimeSpan.FromMinutes(720);
				options.LoginPath = new PathString("/Account/Login");
				options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
				options.SlidingExpiration = true;
				//options.LogoutPath = $"/Identity/Account/Logout";
				//options.AccessDeniedPath = $"/Identity/Account/AccessDenied";
			});
			//services.AddNodeServices();

		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseDatabaseErrorPage();
			}
			else
			{
				app.UseExceptionHandler("/Home/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}
			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthentication();
			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllerRoute(
					name: "default",
					pattern: "{controller=Lend}/{action=Index}/{id?}");
				endpoints.MapRazorPages();
			});

			//Bot Configurations
			//Bot.GetBotClientAsync().Wait();
		}
	}
}
