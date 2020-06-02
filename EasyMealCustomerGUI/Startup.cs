using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyMealCore.DomainModel;
using EasyMealCore.DomainServices;
using EF_SQLServer_Identity_EasyMeal;
using EF_SQLServer_OrderImpl;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EasyMealCustomerGUI
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
			services.Configure<CookiePolicyOptions>(options =>
			{
				// This lambda determines whether user consent for non-essential cookies is needed for a given request.
				options.CheckConsentNeeded = context => true;
				options.MinimumSameSitePolicy = SameSiteMode.None;
			});

			services.AddDbContext<OrderDbContext>(options =>
			options.UseSqlServer(
				Configuration["Data:EasyMealOrder:ConnectionString"]));

			services.AddDbContext<AppIdentityDbContext>(options =>
			options.UseSqlServer(
				Configuration["Data:EasyMealAppIdentity:ConnectionString"]));

			services.AddIdentity<User, IdentityRole>()
				.AddEntityFrameworkStores<AppIdentityDbContext>()
				.AddDefaultTokenProviders();

			services.AddTransient<IOrderRepository, EFOrderRepository>();
			services.AddTransient<IUserContext, EFUserRepository>();
			services.AddMvc();
			services.AddMemoryCache();
			services.AddSession();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			app.UseDeveloperExceptionPage();
			app.UseStatusCodePages();
			app.UseHttpsRedirection();
			app.UseStaticFiles();
			app.UseCookiePolicy();
			app.UseAuthentication();
			app.UseSession();
			app.UseMvc(routes =>
			{
				routes.MapRoute(
					name: "default",
					template: "{controller=Home}/{action=Index}");
			});
		}
	}
}
