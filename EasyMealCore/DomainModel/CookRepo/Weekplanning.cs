using System.Collections.Generic;

namespace EasyMealCore.DomainModel.CookRepo
{
	public class Weekplanning
	{
		public int WeekplanningId { get; set; }
		public int Year { get; set; }
		public int Week { get; set; }

		public IList<WeekplanningDay> WeekplanningDays { get; set; }

		public Weekplanning()
		{
			WeekplanningDays = new List<WeekplanningDay>();
		}

	}
}
