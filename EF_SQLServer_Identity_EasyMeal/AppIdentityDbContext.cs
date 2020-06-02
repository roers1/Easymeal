using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using EasyMealCore.DomainModel;
using EasyMealCore.DomainModel.CookModels;
using EasyMealCore.DomainModel.CustomerModels;

namespace EF_SQLServer_Identity_EasyMeal
{
	public class AppIdentityDbContext : IdentityDbContext<User>
	{
		public AppIdentityDbContext(DbContextOptions<AppIdentityDbContext> options)
			: base(options)
		{

		}

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

			builder.Entity<User>()
				.HasOne(a => a.Customer)
				.WithOne(b => b.User)
				.HasForeignKey<Customer>(b => b.Id)
				.OnDelete(DeleteBehavior.Cascade);


			builder.Entity<User>()
				.HasOne(a => a.Cook)
				.WithOne(b => b.User)
				.HasForeignKey<Cook>(b => b.Id)
				.OnDelete(DeleteBehavior.Cascade);
		}

		public DbSet<User> Users { get; set; }
		public DbSet<Cook> Cooks { get; set; }
		public DbSet<Customer> Customers { get; set; }
	}
}
