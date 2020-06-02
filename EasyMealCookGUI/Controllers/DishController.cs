using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using EasyMealCookGUI.Models.ViewModels;
using EasyMealCore.DomainModel;
using EasyMealCore.DomainServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EasyMealCookGUI.Controllers
{
    public class DishController : Controller
    {

		private IPlanningRepository repository;

		public DishController(IPlanningRepository repo)
		{
			repository = repo;
		}
		public IActionResult Create()
        {
            return View();
        }

		[HttpPost]
		[Authorize]
		public IActionResult CreateDish(DishViewModel data)
		{
			Console.WriteLine(data);

			Dish dish = new Dish
			{
				Name = data.Name,
				Description = data.Description,
				Price = data.Price,
				Starter = data.Starter,
				MainDish = data.MainDish,
				Dessert = data.Dessert,
				Salt = data.Salt,
				Glutes = data.Glutes,
				Diabetes = data.Diabetes,
			};

			{
				using (var ms = new MemoryStream())
				{
					data.Image.CopyTo(ms);
					dish.Image = ms.ToArray();
				}
			}
		
			repository.SaveDish(dish);
			TempData["message"] = $"{dish.Name} is opgeslagen";
			return RedirectToAction("Index","Home");
		}
	}
}