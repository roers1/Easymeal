using EasyMealCore.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyMealCore.DomainModel.CookModels;

namespace EasyMealCookGUI.Models.ViewModels
{
	public class OverviewModel
	{
		public IEnumerable<Dish> Dishes { get; set; }
		public Card card { get; set; }
		public int dayNr { get; set; }
		public string course { get; set; } = "";

	}
}
