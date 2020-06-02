using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyMealCore.DomainModel;
using EasyMealCore.DomainModel.CustomerModels;
using EasyMealCore.DomainModel.Invoice;
using EasyMealCore.DomainServices;
using EasyMealCustomerGUI.Component;
using EasyMealCustomerGUI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Day = EasyMealCustomerGUI.Models.Day;

namespace EasyMealCustomerGUI.Controllers
{
    public class OrderController : Controller
    {
	    private readonly UserManager<User> _userManager;
		private readonly OrderModel _orderModel = new OrderModel();
		private readonly IOrderRepository _repository;
		public OrderController(IOrderRepository repo, UserManager<User> userMgr)
		{
			_userManager = userMgr;
			_repository = repo;
			FillRepo();
		}

		private void FillRepo()
		{
			for (int i = 1; i < 8; i++)
			{
				var day = new Day
				{
					DayOfTheWeek = i, 
					Dishes = GetDishes(i).Result
				};

				_orderModel.Week.Days.Add(day);
			}
		}

		private async Task<Dish> GetDish(int id)
		{
			Dish dish = await NetworkFetchDishes.GetDishAsync($"http://easymealapi.azurewebsites.net/api/v1/dishes/{id}");
			//Dish dish = await NetworkFetchDishes.GetDishAsync($"https://localhost:44372/api/v1/dishes/{id}");
			return dish;
		}

		private async Task<IEnumerable<Dish>> GetDishes(int dayNr)
		{
			int currentYear = DateTime.Now.Year;
			int currentWeek = (DateTime.Now.DayOfYear / 7) + 1;

			IEnumerable<Dish> dish = await NetworkFetchDishes.GetDishesAsync($"http://easymealapi.azurewebsites.net/api/v1/years/{currentYear}/weeks/{currentWeek}/days/{dayNr}");
			//IEnumerable<Dish> dish = await NetworkFetchDishes.GetDishesAsync($"https://localhost:44372/api/v1/years/{currentYear}/weeks/{currentWeek}/days/{dayNr}");
			return dish;
		}

		public IActionResult Monday()
		{
			_orderModel.Cart = GetCart();
			_orderModel.DayOfTheWeek = 1;
			return View("Monday", _orderModel);
		}
		public IActionResult Tuesday()
		{
			_orderModel.Cart = GetCart();
			_orderModel.DayOfTheWeek = 2;
			return View("Tuesday", _orderModel);
		}
		public IActionResult Wednesday()
		{
			_orderModel.Cart = GetCart();
			_orderModel.DayOfTheWeek = 3;
			return View("Wednesday", _orderModel);
		}
		public IActionResult Thursday()
		{
			_orderModel.Cart = GetCart();
			_orderModel.DayOfTheWeek = 4;
			return View("Thursday", _orderModel);
		}
		public IActionResult Friday()
		{
			_orderModel.Cart = GetCart();
			_orderModel.DayOfTheWeek = 5;
			return View("Friday", _orderModel);
		}
		public IActionResult Saturday()
		{
			_orderModel.Cart = GetCart();
			_orderModel.DayOfTheWeek = 6;
			return View("Saturday", _orderModel);
		}
		public IActionResult Sunday()
		{
			_orderModel.Cart = GetCart();
			_orderModel.DayOfTheWeek = 7;
			return View("Sunday", _orderModel);
		}

		public IActionResult Overview()
		{
			_orderModel.Cart = GetCart();
			return View("PayUpOverview", _orderModel);
		}

		[Authorize]
		public IActionResult PublishOrder(int mondaySize, int tuesdaySize, int wednesdaySize, int thursdaySize, int fridaySize, int saturdaySize, int sundaySize)
		{
			var user = _userManager.GetUserAsync(HttpContext.User).Result;
			var cart = GetCart();
			cart.Days.Where(d => d.DayNr == 1).ToList().ForEach(d=> d.Size = mondaySize);
			cart.Days.Where(d => d.DayNr == 2).ToList().ForEach(d=> d.Size = tuesdaySize);
			cart.Days.Where(d => d.DayNr == 3).ToList().ForEach(d=> d.Size = wednesdaySize);
			cart.Days.Where(d => d.DayNr == 4).ToList().ForEach(d=> d.Size = thursdaySize);
			cart.Days.Where(d => d.DayNr == 5).ToList().ForEach(d=> d.Size = fridaySize);
			cart.Days.Where(d => d.DayNr == 6).ToList().ForEach(d=> d.Size = saturdaySize);
			cart.Days.Where(d => d.DayNr == 7).ToList().ForEach(d=> d.Size = sundaySize);
			var temp = CheckValidOrder(cart);
			var temp2 = CheckTime();
			if (temp && temp2)
			{
				var invoice = Process(cart, user);
				_repository.SaveOrder(invoice);
				TempData["message"] = "Order succesfully saved and is being prepared!";
			}
			else if (!temp)
			{
				TempData["message"] = "Your order is not sufficient, please check if you have at least 4 workdays with meals.";
				return RedirectToAction("Overview");
			}
			else
			{
				TempData["message"] = "The kitchen is closed, please wait till monday to place a new order for the upcoming week";
				return RedirectToAction("Overview");
			}
			
			return Redirect("/");
		}

		private bool CheckTime()
		{
			return DateTime.Now.DayOfWeek < DayOfWeek.Friday;
		}

		public Invoice Process(Cart cart, User user)
		{
			var invoiceDate = cart.Date.Date.StartOfWeek(DayOfWeek.Monday).AddDays(7);

			Invoice invoice = _repository.GetOrder(user).FirstOrDefault(a =>
								  a.OrderDate.Year == cart.Date.Year && a.OrderDate.Date.Month == cart.Date.Month && a.CustomerId == user.Id) ??
							  new Invoice
							  {
								  CustomerId = user.Id,
								  OrderDate = invoiceDate,
								  Discounted = false
							  };

			foreach (var day in cart.Days)
			{
				DateTime dishDeliveryDate = invoiceDate.AddDays(day.DayNr - 1);

				foreach (var dish in day.Dishes)
				{
					decimal price = dish.Price;

					if (day.Size == 0)
					{
						price = decimal.Multiply(price, (decimal)0.80);
					}
					else if (day.Size == 2)
					{
						price = decimal.Multiply(price, (decimal)1.20);
					}

					if (day.Date.Month == user.DateOfBirth.Month && day.Date.Day == user.DateOfBirth.Day)
					{
						price = 0;
					}

					InvoiceLine invoiceLine = new InvoiceLine
					{
						DeliveryDate = dishDeliveryDate,
						DishId = dish.DishId,
						Invoice = invoice,
						Name = dish.Name,
						Size = day.Size,
						Price = price
					};

					invoice.InvoiceLines.Add(invoiceLine);

					if (!invoice.Discounted)
					{
						if (invoice.InvoiceLines.Count >= 15 && invoice.InvoiceLines.Any(i => i.Price != 0) ||
							invoice.InvoiceLines.Count > 15 && invoice.InvoiceLines.Any(i => i.Price == 0))
						{
							foreach (var invoiceline in invoice.InvoiceLines)
							{
								invoiceline.Price = decimal.Multiply(invoiceline.Price, (decimal)0.90);
							}

							invoice.Discounted = true;
						}
					}
					else if (invoice.InvoiceLines.Count >= 15 && invoice.InvoiceLines.Any(i => i.Price != 0) ||
							 invoice.InvoiceLines.Count > 15 && invoice.InvoiceLines.Any(i => i.Price == 0))
					{
						invoice.InvoiceLines.Last().Price = decimal.Multiply(invoice.InvoiceLines.Last().Price, (decimal)0.90);
					}
				}
			}
			return invoice;
		}

		public bool CheckValidOrder(Cart cart)
		{
			bool isValid = false;
			bool mondayHasMeal = false;
			bool tuesdayHasMeal = false;
			bool wednesdayHasMeal = false;
			bool thursdayHasMeal = false;
			bool fridayHasMeal = false;
			int validMealsCount = 0;

			foreach (var day in cart.Days)
			{
				bool dayHasStarterOrDessert = false;
				bool dayHasMain = false;
				foreach (var dish in day.Dishes)
				{
					if (dish.Starter || dish.Dessert)
					{
						dayHasStarterOrDessert = true;
					} else if (dish.MainDish)
					{
						dayHasMain = true;
					}
				}

				if (dayHasStarterOrDessert && dayHasMain)
				{
					switch (day.DayNr)
					{
						case 1:
							mondayHasMeal = true;
							validMealsCount++;
							break;
						case 2:
							tuesdayHasMeal = true;
							validMealsCount++;
							break;
						case 3:
							wednesdayHasMeal = true;
							validMealsCount++;
							break;
						case 4:
							thursdayHasMeal = true;
							validMealsCount++;
							break;
						case 5:
							fridayHasMeal = true;
							validMealsCount++;
							break;
					}
				}
			}

			if (validMealsCount >= 4)
			{
				isValid = true;
			}

			return isValid;
		}

		public Cart GetCart()
		{
			var cart = HttpContext.Session.Get<Cart>("Cart") ?? new Cart();
			return cart;
		}

		public IActionResult AddToCart(int dayNr, int DishId)
		{
			var cart = HttpContext.Session.Get<Cart>("Cart") ?? new Cart();

			Dish dish = GetDish(DishId).Result;

			cart.AddToCart(dayNr, dish);

			HttpContext.Session.Set("Cart", cart);
			
			return Redirect(HttpContext.Request.Headers["Referer"].ToString());
		}

		public IActionResult RemoveFromCart(int dayNr, int dishId)
		{

			var cart = HttpContext.Session.Get<Cart>("Cart") ?? new Cart();

			Dish dish = GetDish(dishId).Result;

			cart.RemoveFromCart(dayNr, dish);

			HttpContext.Session.Set("Cart", cart);

			return Redirect(HttpContext.Request.Headers["Referer"].ToString());
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
			JsonSerializerSettings settings = new JsonSerializerSettings
			{
				ObjectCreationHandling = ObjectCreationHandling.Replace
			};
			return value == null ? default(T) :
				JsonConvert.DeserializeObject<T>(value, settings);
		}
	}
}