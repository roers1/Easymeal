using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using EasyMealCore.DomainServices;
using EF_SQLServer_DishDataImpl;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using EF_SQLServer_Identity_EasyMeal;
using EasyMealCore.DomainModel;

namespace EasyMealCookGUI
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
			services.AddDbContext<ApplicationDbContext>(options =>
			options.UseSqlServer(
				Configuration["Data:EasyMealDishes:ConnectionString"])
			.EnableSensitiveDataLogging());

			services.AddDbContext<AppIdentityDbContext>(options =>
			options.UseSqlServer(
				Configuration["Data:EasyMealAppIdentity:ConnectionString"]));

			services.AddIdentity<User, IdentityRole>()
				.AddEntityFrameworkStores<AppIdentityDbContext>()
				.AddDefaultTokenProviders();

			services.AddTransient<IPlanningRepository, EFPlanningRepository>();
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
