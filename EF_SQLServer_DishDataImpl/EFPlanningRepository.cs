using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EasyMealCore.DomainModel;
using EasyMealCore.DomainModel.CookModels;
using EasyMealCore.DomainModel.CookRepo;
using EasyMealCore.DomainServices;

namespace EF_SQLServer_DishDataImpl
{
	public class EFPlanningRepository : IPlanningRepository{

		private readonly ApplicationDbContext _context;

		public EFPlanningRepository(ApplicationDbContext ctx)
		{
			_context = ctx;
		}

		public IEnumerable<Dish> Dishes => _context.Dishes;

		public IEnumerable<Weekplanning> Weekplannings => _context.Weekplannings;

		public IEnumerable<Day> Days => _context.Days;

		public IEnumerable<WeekplanningDay> weekplanningDays => _context.WeekplanningDays;

		public IEnumerable<DayDish> dayDishes => _context.DayDishes;

		public void SaveDish(Dish dish)
		{
			if (dish.DishId == 0)
			{
				_context.Dishes.Add(dish);
			}
			else
			{
				Dish dbEntry = _context.Dishes
					.FirstOrDefault(d => d.DishId == dish.DishId);
				if(dbEntry != null)
				{
					dbEntry.Name = dish.Name;
					dbEntry.Description = dish.Description;
					dbEntry.Starter = dish.Starter;
					dbEntry.MainDish = dish.MainDish;
					dbEntry.Dessert = dish.Dessert;
					dbEntry.Glutes = dish.Glutes;
					dbEntry.Salt = dish.Salt;
					dbEntry.Diabetes = dish.Diabetes;
					dbEntry.Price = dish.Price;
				}
			}
			_context.SaveChanges();
		}

		public Dish DeleteDish(int dishId)
		{
			Dish dbEntry = _context.Dishes.FirstOrDefault(d => d.DishId == dishId);

			if(dbEntry != null)
			{
				_context.Dishes.Remove(dbEntry);
				_context.SaveChanges();
			}
			return dbEntry;
		}

		public void savePlanning(Card card)
		{
			Weekplanning weekplanning = new Weekplanning { Year = card.PlanningForWeek.Year, Week = (card.PlanningForWeek.DayOfYear / 7) + 1 };
			
			foreach(DayOfTheWeek day in card.DayOfTheWeek)
			{
				WeekplanningDay weekplanningDay = new WeekplanningDay();
				weekplanningDay.Weekplanning = weekplanning;

				weekplanning.WeekplanningDays.Add(weekplanningDay);
				Day dayToAdd = new Day {DayOfTheWeek = day.dayNr};
				weekplanningDay.Day = dayToAdd;

				foreach(Dish dish in day.Dishes)
				{
					DayDish dayDish = new DayDish();
					
					dayDish.Day = dayToAdd;

					dayDish.Day.WeekplanningDays.Add(weekplanningDay);

					Dish dishToInsert = _context.Dishes.FirstOrDefault(d => d.DishId == dish.DishId);

					dayDish.Dish = dishToInsert;

					dayToAdd.DayDishes.Add(dayDish);
				}
			}

			_context.Add(weekplanning);
			_context.SaveChanges();
		}
	}
}
