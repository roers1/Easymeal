using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyMealCore.DomainServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using EasyMealCookGUI.Models.ViewModels;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Authorization;
using EasyMealCore.DomainModel;
using EasyMealCore.DomainModel.CookModels;

namespace EasyMealCookGUI.Controllers
{
	[Authorize]
	public class PlanningController : Controller
	{
		private readonly IPlanningRepository _repository;
		private readonly OverviewModel _overviewModel = new OverviewModel();

		public PlanningController(IPlanningRepository repo)
		{
			_repository = repo;
			_overviewModel.Dishes = _repository.Dishes;
		}

		public IActionResult Week()
		{
			var card = SessionExtensions.Get<Card>(HttpContext.Session, "Planning");

			if (card == null)
			{
				card = new Card();
			}
			return View(card);
		}

		public IActionResult Monday()
		{
			_overviewModel.card = setCard();
			_overviewModel.dayNr = 1;
			return View(_overviewModel);
		}
		public IActionResult Tuesday()
		{
			_overviewModel.card = setCard();
			_overviewModel.dayNr = 2;
			return View(_overviewModel);
		}
		public IActionResult Wednesday()
		{
			_overviewModel.card = setCard();
			_overviewModel.dayNr = 3;
			return View(_overviewModel);
		}
		public IActionResult Thursday()
		{
			_overviewModel.card = setCard();
			_overviewModel.dayNr = 4;
			return View(_overviewModel);
		}
		public IActionResult Friday()
		{
			_overviewModel.card = setCard();
			_overviewModel.dayNr = 5;
			return View(_overviewModel);
		}
		public IActionResult Saturday()
		{
			_overviewModel.card = setCard();
			_overviewModel.dayNr = 6;
			return View(_overviewModel);
		}
		public IActionResult Sunday()
		{
			_overviewModel.card = setCard();
			_overviewModel.dayNr = 7;
			return View(_overviewModel);
		}

		public ActionResult AddToPlanning(int DayNr, int DishId)
		{
			Dish dish = _repository.Dishes.FirstOrDefault(d => d.DishId == DishId);

			var card = SessionExtensions.Get<Card>(HttpContext.Session, "Planning");

			if (card == null)
			{
				card = new Card();
			}
			card.AddDish(DayNr, dish);
			SessionExtensions.Set<Card>(HttpContext.Session, "Planning", card);
			return Redirect(HttpContext.Request.Headers["Referer"].ToString());
		}

		public ActionResult RemoveFromPlanning(int DayNr, int DishId)
		{
			Dish dish = _repository.Dishes.FirstOrDefault(d => d.DishId == DishId);

			var card = SessionExtensions.Get<Card>(HttpContext.Session, "Planning");

			if (card == null)
			{
				card = new Card();
			}
			card.RemoveDish(dish, DayNr);
			SessionExtensions.Set<Card>(HttpContext.Session, "Planning", card);
			return Redirect(HttpContext.Request.Headers["Referer"].ToString());
		}


		public RedirectToActionResult SaveWeek(DateTime date)
		{
			var card = SessionExtensions.Get<Card>(HttpContext.Session, "Planning");

			if (card == null)
			{
				card = new Card();
			}
			card.PlanningForWeek = date;
			SessionExtensions.Set<Card>(HttpContext.Session, "Planning", card);

			return RedirectToAction("Monday", _repository.Dishes);
		}

		public RedirectToActionResult Save()
		{
			Card card = SessionExtensions.Get<Card>(HttpContext.Session, "Planning");
			if (card == null)
			{
				card = new Card();
			}

			DateTime PlanningForWeek = card.PlanningForWeek;

			if (_repository.Weekplannings.Any(w => w.Year == PlanningForWeek.Year && w.Week == (PlanningForWeek.DayOfYear / 7) + 1))
			{
				TempData["message"] = $"Er is al een planning voor week {(PlanningForWeek.DayOfYear / 7) + 1}";
				return RedirectToAction("Overview");
			}

			int currentYear = DateTime.Now.Year;
			int currentWeek = (DateTime.Now.DayOfYear / 7) + 1;
			if (currentYear <= card.PlanningForWeek.Year && currentWeek < (card.PlanningForWeek.DayOfYear / 7) + 1)
			{
				if (Check(card))
				{
					_repository.savePlanning(card);
					TempData["message"] = $"Planning is succesvol verstuurd voor gebruik in week {(card.PlanningForWeek.DayOfYear / 7) + 1}";
					HttpContext.Session.Remove("Planning");
				}
				return RedirectToAction("Overview");
			}
			else
			{
				TempData["message"] = "Planning moet minimaal op zondag 23:59 de week van tevoren ingevuld worden";
				return RedirectToAction("Overview");
			}

		}
		public IActionResult Overview()
		{
			Card card = SessionExtensions.Get<Card>(HttpContext.Session, "Planning");
			if (card == null)
			{
				card = new Card();
			}
			return View("Overview", card);
		}
		public Boolean Check(Card card)
		{
			Boolean isValid = true;

			if (!CheckMealCompletenes(card))
			{
				isValid = false;
				return isValid;
			}

			if (!CheckMainDishVariation(card))
			{
				isValid = false;
				return isValid;
			}

			if (!CheckForDietConstraints(card))
			{
				isValid = false;
				return isValid;
			}

			return isValid;
		}
		private Boolean CheckMealCompletenes(Card card)
		{
			Boolean AllMealsContainsAllCourse = true;
			if (card.DayOfTheWeek.Count < 7)
			{
				TempData["message"] = "Niet alle dagen hebben een maaltijd";
				AllMealsContainsAllCourse = false;
				return AllMealsContainsAllCourse;
			}
			foreach (DayOfTheWeek day in card.DayOfTheWeek)
			{
				Boolean MealContainsStarter = false;
				Boolean MealContainsMainDish = false;
				Boolean MealContainsDessert = false;

				foreach (Dish dish in day.Dishes)
				{
					if (dish.Starter == true)
					{
						MealContainsStarter = true;
					}
					else if (dish.MainDish == true)
					{
						MealContainsMainDish = true;
					}
					else if (dish.Dessert == true)
					{
						MealContainsDessert = true;
					}
				}
				if (!(MealContainsStarter && MealContainsMainDish && MealContainsDessert))
				{
					string dayOfTheWeek = getDayOfTheWeek(day.dayNr);
					TempData["message"] = $"{dayOfTheWeek} heeft geen complete maaltijd tot hun beschikking";
					AllMealsContainsAllCourse = false;
					return AllMealsContainsAllCourse;
				}
			}
			return AllMealsContainsAllCourse;
		}
		private Boolean CheckMainDishVariation(Card card)
		{
			Boolean DifferentMainDishEachDay = true;
			List<Dish> dishes = new List<Dish>();
			foreach (DayOfTheWeek day in card.DayOfTheWeek)
			{
				Boolean hasUniqueMainDish = false;
				foreach (Dish dish in day.Dishes)
				{

					if (dish.MainDish == true)
					{
						Dish dishToCheck = dishes.FirstOrDefault(d => d.DishId == dish.DishId);

						if (dishToCheck == null)
						{
							dishes.Add(dish);
							hasUniqueMainDish = true;
						}

					}
				}
				if (!hasUniqueMainDish)
				{
					string dayOfTheWeek = getDayOfTheWeek(day.dayNr);

					TempData["message"] = $"{dayOfTheWeek} heeft geen uniek hoofdgerecht";

					DifferentMainDishEachDay = false;
					return DifferentMainDishEachDay;
				}
			}
			return DifferentMainDishEachDay;
		}
		private Boolean CheckForDietConstraints(Card card)
		{
			Boolean DietConstraintsAreCorrect = true;

			foreach (DayOfTheWeek day in card.DayOfTheWeek)
			{
				Boolean StarterDietFree = false;
				Boolean MainDishDietFree = false;
				Boolean DessertDietFree = false;

				List<Dish> dishes = day.Dishes;
				foreach (Dish dish in dishes)
				{
					if (dish.Starter)
					{
						if (!(dish.Diabetes || dish.Glutes || dish.Salt))
						{
							StarterDietFree = true;
						}
					}
					else if (dish.MainDish)
					{
						if (!(dish.Diabetes || dish.Glutes || dish.Salt))
						{
							MainDishDietFree = true;
						}
					}
					else if (dish.Dessert)
					{
						if (!(dish.Diabetes || dish.Glutes || dish.Salt))
						{
							DessertDietFree = true;
						}
					}
				}
				if (!(StarterDietFree && MainDishDietFree && DessertDietFree))
				{
					string dayOfTheWeek = getDayOfTheWeek(day.dayNr);
					if (!StarterDietFree)
					{
						TempData["message"] = $"Het voorgerecht van {dayOfTheWeek} is niet gluten, zout en diabetes vrij";
					}
					else if (!MainDishDietFree)
					{
						TempData["message"] = $"Het hoofdgerecht van {dayOfTheWeek} is niet gluten, zout en diabetes vrij";
					}
					else if (!DessertDietFree)
					{
						TempData["message"] = $"Het nagerecht van {dayOfTheWeek} is niet gluten, zout en diabetes vrij";
					}

					DietConstraintsAreCorrect = false;
					return DietConstraintsAreCorrect;
				}

			}
			return DietConstraintsAreCorrect;
		}
		private string getDayOfTheWeek(int dayNr)
		{
			string dayOfTheWeek = "";
			switch (dayNr)
			{
				case 1:
					dayOfTheWeek = "maandag";
					break;
				case 2:
					dayOfTheWeek = "dinsdag";
					break;
				case 3:
					dayOfTheWeek = "woensdag";
					break;
				case 4:
					dayOfTheWeek = "donderdag";
					break;
				case 5:
					dayOfTheWeek = "vrijdag";
					break;
				case 6:
					dayOfTheWeek = "zaterdag";
					break;
				case 7:
					dayOfTheWeek = "zondag";
					break;
				default:
					dayOfTheWeek = "een dag";
					break;
			}

			return dayOfTheWeek;
		}

		private Card setCard()
		{
			var card = SessionExtensions.Get<Card>(HttpContext.Session, "Planning");

			if (card == null)
			{
				card = new Card();
			}

			return card;
		}
	}


	public static class SessionExtensions
	{
		public static void Set<T>(this ISession session, string key, T value)
		{
			session.SetString(key, JsonConvert.SerializeObject(value));
		}

		public static T Get<T>(this ISession session, string key)
		{
			var value = session.GetString(key);
			JsonSerializerSettings settings = new JsonSerializerSettings();
			settings.ObjectCreationHandling = ObjectCreationHandling.Replace;
			return value == null ? default(T) :
				JsonConvert.DeserializeObject<T>(value, settings);
		}
	}

}