using System;
using System.Collections.Generic;
using System.Text;
using EasyMealCore.DomainModel;
using EasyMealCore.DomainModel.CookRepo;
using Microsoft.EntityFrameworkCore;

namespace EF_SQLServer_DishDataImpl
{
	public class ApplicationDbContext :DbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options) { }


		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<WeekplanningDay>().HasKey(wd => new { wd.WeekplanningId, wd.DayId });

			modelBuilder.Entity<WeekplanningDay>()
				.HasOne<Weekplanning>(wd => wd.Weekplanning)
				.WithMany(d => d.WeekplanningDays)
				.HasForeignKey(wd => wd.WeekplanningId);

			modelBuilder.Entity<WeekplanningDay>()
				.HasOne<Day>(wd => wd.Day)
				.WithMany(w => w.WeekplanningDays)
				.HasForeignKey(wd => wd.DayId);

			modelBuilder.Entity<DayDish>().HasKey(dd => new { dd.DayId, dd.DishId });

			modelBuilder.Entity<DayDish>()
				.HasOne<Day>(dd => dd.Day)
				.WithMany(d => d.DayDishes)
				.HasForeignKey(dd => dd.DayId);

			modelBuilder.Entity<DayDish>()
				.HasOne<Dish>(dd => dd.Dish)
				.WithMany(d => d.DayDishes)
				.HasForeignKey(dd => dd.DishId);
		}

		public DbSet<Dish> Dishes { get; set; }
		public DbSet<Day> Days { get; set; }
		public DbSet<DayDish> DayDishes { get; set; }
		public DbSet<Weekplanning> Weekplannings { get; set; }
		public DbSet<WeekplanningDay> WeekplanningDays { get; set; }

		internal void AttachRange(IEnumerable<Dish> enumerable)
		{
			throw new NotImplementedException();
		}
	}
}
