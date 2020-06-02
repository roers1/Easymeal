using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EasyMealCore.DomainServices;
using Microsoft.AspNetCore.Authorization;
using EasyMealCore.DomainModel;

namespace EasyMealCookGUI.Controllers
{
	[Authorize]
	public class CardController : Controller
    {
		private IPlanningRepository repository;

		public CardController(IPlanningRepository repo)
		{
			repository = repo;
		}

		
		public IActionResult Import()
		{
			return View("Import",repository.Dishes);
		}

		[Authorize]
		public IActionResult add(int DishId)
		{
			Dish dish = repository.Dishes.FirstOrDefault(d =>
		   d.DishId == DishId);

			if(dish != null)
			{
				repository.SaveDish(dish);
				TempData["message"] = $"{dish.Name} is selecteerbaar";
			
			}
			return RedirectToAction("Import");
		}
		[Authorize]
		public IActionResult remove(int DishId)
		{
			Dish dish = repository.Dishes.FirstOrDefault(d =>
			d.DishId == DishId);

			if (dish != null)
			{
				repository.SaveDish(dish);
				TempData["message"] = $"{dish.Name} is niet meer selecteerbaar";
				
			}
			return RedirectToAction("Import");
		}

		
		public ViewResult Create() => View("Create", new Dish());

	}
}