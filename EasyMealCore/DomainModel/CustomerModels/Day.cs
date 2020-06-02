using System;
using System.Collections.Generic;

namespace EasyMealCore.DomainModel.CustomerModels
{
	public class Day
	{
		public int DayNr { get; set; }
		public int Size { get; set; }
		public DateTime Date { get; set; }
		public IList<Dish> Dishes { get; set; }

		public Day()
		{
			Dishes = new List<Dish>();
			Size = 1;
		}
	}
}
