using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyMealCore.DomainModel;
using EasyMealCore.DomainModel.CustomerModels;

namespace EasyMealCustomerGUI.Models
{
	public class OrderModel
	{
		public Week Week { get; set; }
		public string Course { get; set; }
		public int DayOfTheWeek { get; set; }

		public Cart Cart { get; set; }

		public OrderModel()
		{
			Week = new Week();
			Cart = new Cart();
		}
	}

	public class Week
	{
		public IList<Day> Days { get; set; }

		public Week()
		{
			Days = new List<Day>();
		}
	}

	public class Day
	{
		public int DayOfTheWeek { get; set; }
		public IEnumerable<Dish> Dishes { get; set; }
	}
}
