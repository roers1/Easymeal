using System.Collections.Generic;
using System.Linq;
using EasyMealCore.DomainModel;
using EasyMealCore.DomainModel.CookModels;
using EasyMealCore.DomainModel.CookRepo;

namespace EasyMealCore.DomainServices
{
	public interface IPlanningRepository
	{
		IEnumerable<Day> Days { get; }
		IEnumerable<Weekplanning> Weekplannings { get; }
		IEnumerable<WeekplanningDay> weekplanningDays { get; }
		IEnumerable<DayDish> dayDishes { get; }
		IEnumerable<Dish> Dishes { get; }
		void savePlanning(Card card);
		void SaveDish(Dish dish);
		Dish DeleteDish(int dishId);
	}
}
