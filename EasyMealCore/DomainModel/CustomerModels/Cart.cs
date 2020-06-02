using System;
using System.Collections.Generic;
using System.Linq;

namespace EasyMealCore.DomainModel.CustomerModels
{
	public class Cart
	{
		public IList<Day> Days;
		public decimal Price { get; set; }
		public DateTime Date { get; set; }

		public Cart()
		{
			Days = new List<Day>
			{
				new Day {DayNr = 1},
				new Day {DayNr = 2},
				new Day {DayNr = 3},
				new Day {DayNr = 4},
				new Day {DayNr = 5},
				new Day {DayNr = 6},
				new Day {DayNr = 7}
			};
			Date = DateTime.Now.Date;
			Price = 0;
		}

		public void AddToCart(int dayNr, Dish dish)
		{
			var day = Days.First(d => d.DayNr == dayNr);

			day?.Dishes.Add(dish);
			Price += dish.Price;
		}

		public void RemoveFromCart(int dayNr, Dish dish)
		{
			Day day = Days.First(d => d.DayNr == dayNr);
			var dishToRemove = (day?.Dishes ?? throw new InvalidOperationException()).First(d => d.DishId == dish.DishId);
			day.Dishes.Remove(dishToRemove);
			Price -= dish.Price;
		}
	}
}
