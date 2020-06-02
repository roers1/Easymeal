using System.Collections.Generic;

namespace EasyMealCore.DomainModel.CookRepo
{
	public class Day
	{
		public int DayId { get; set; }
		public int DayOfTheWeek { get; set; }

		public IList<WeekplanningDay> WeekplanningDays { get; set; }
		public IList<DayDish> DayDishes { get; set; }

		public Day()
		{
			WeekplanningDays = new List<WeekplanningDay>();
			DayDishes = new List<DayDish>();
		}

	}
}
