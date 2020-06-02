using System;
using System.Collections.Generic;
using System.Linq;

namespace EasyMealCore.DomainModel.CookModels
{
	public class Card
	{
		public List<DayOfTheWeek> DayOfTheWeek;
		public DateTime PlanningForWeek { get; set; } = DateTime.Now;
		public Card()
		{
			DayOfTheWeek = new List<DayOfTheWeek>();
		}

		public virtual void AddDish(int dayNr, Dish dish)
		{

				
			DayOfTheWeek day = DayOfTheWeek.FirstOrDefault(d => d.dayNr == dayNr);

			if (day == null)
			{
				
				DayOfTheWeek.Add(new DayOfTheWeek { dayNr = dayNr });
				day = DayOfTheWeek.FirstOrDefault(d => d.dayNr == dayNr);
				if (day != null && day.Dishes == null)
				{
					day.Dishes = new List<Dish>();
				}

				day?.Dishes.Add(dish);
			}
			else
			{
				if(day.Dishes.All(d => d.DishId != dish.DishId))
				{
					day.Dishes.Add(dish);
				}
				
			}

			
		}

		public virtual void RemoveDish(Dish dish, int dayNr)
		{
			DayOfTheWeek.ElementAt(dayNr-1).Dishes.RemoveAll(d => d.DishId == dish.DishId);
		}

		public virtual IEnumerable<DayOfTheWeek> day => DayOfTheWeek;
	}

	public class DayOfTheWeek
	{
		public int dayNr { get; set; }
		public List<Dish> Dishes { get; set; }

	}
}
