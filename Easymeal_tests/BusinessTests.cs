using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using EasyMealCookGUI.Controllers;
using Microsoft.AspNetCore.Mvc;
using EasyMealCore.DomainModel;
using EasyMealCore.DomainModel.CookModels;
using EasyMealCore.DomainModel.CookRepo;
using EasyMealCore.DomainModel.CustomerModels;
using EasyMealCore.DomainModel.Invoice;
using EasyMealCore.DomainServices;
using EasyMealCustomerGUI.Controllers;
using EF_SQLServer_OrderImpl;
using Microsoft.AspNetCore.Identity;
using Moq;
using Xunit;
using Day = EasyMealCustomerGUI.Models.Day;

namespace Easymeal_tests
{
	public class BusinessTests
	{
		[Fact]
		public void Every_Day_Contains_A_Full_Meal()
		{
			//arrange
			Dish starterDish = new Dish
			{
				Starter = true,
				Glutes = false,
				Diabetes = false,
				Salt = false
			};
			Dish mainDish1 = new Dish
			{
				MainDish = true,
				Glutes = false,
				Diabetes = false,
				Salt = false
			};
			Dish mainDish2 = new Dish
			{
				MainDish = true,
				Glutes = false,
				Diabetes = false,
				Salt = false
			};
			Dish mainDish3 = new Dish
			{
				MainDish = true,
				Glutes = false,
				Diabetes = false,
				Salt = false
			};
			Dish mainDish4 = new Dish
			{
				MainDish = true,
				Glutes = false,
				Diabetes = false,
				Salt = false
			};
			Dish mainDish5 = new Dish
			{
				MainDish = true,
				Glutes = false,
				Diabetes = false,
				Salt = false
			};
			Dish mainDish6 = new Dish
			{
				MainDish = true,
				Glutes = false,
				Diabetes = false,
				Salt = false
			};
			Dish mainDish7 = new Dish
			{
				MainDish = true,
				Glutes = false,
				Diabetes = false,
				Salt = false
			};

			DayOfTheWeek monday = new DayOfTheWeek();
			monday.dayNr = 1;
			DayOfTheWeek tuesday = new DayOfTheWeek();
			tuesday.dayNr = 2;
			DayOfTheWeek wednesday = new DayOfTheWeek();
			wednesday.dayNr = 3;
			DayOfTheWeek thursday = new DayOfTheWeek();
			thursday.dayNr = 4;
			DayOfTheWeek friday = new DayOfTheWeek();
			friday.dayNr = 5;
			DayOfTheWeek saturday = new DayOfTheWeek();
			saturday.dayNr = 6;
			DayOfTheWeek sunday = new DayOfTheWeek();
			sunday.dayNr = 7;

			monday.Dishes.Add(starterDish);
			monday.Dishes.Add(mainDish1);

			tuesday.Dishes.Add(starterDish);
			tuesday.Dishes.Add(mainDish2);

			wednesday.Dishes.Add(starterDish);
			wednesday.Dishes.Add(mainDish3);

			thursday.Dishes.Add(starterDish);
			thursday.Dishes.Add(mainDish4);

			friday.Dishes.Add(starterDish);
			friday.Dishes.Add(mainDish5);

			saturday.Dishes.Add(starterDish);
			saturday.Dishes.Add(mainDish6);

			sunday.Dishes.Add(starterDish);
			sunday.Dishes.Add(mainDish7);

			Card card = new Card();
			card.DayOfTheWeek.Add(monday);
			card.DayOfTheWeek.Add(tuesday);
			card.DayOfTheWeek.Add(wednesday);
			card.DayOfTheWeek.Add(thursday);
			card.DayOfTheWeek.Add(friday);
			card.DayOfTheWeek.Add(saturday);
			card.DayOfTheWeek.Add(sunday);

			PlanningController planningController = new PlanningController(null);

			//act
			var isSaved = planningController.Check(card);

			//assert
			Assert.True(isSaved);
		}

		[Fact]
		public void Every_Day_Contains_A_Full_Meal_Should_Reject_If_False()
		{
			//arrange
			Dish starterDish = new Dish
			{
				Starter = true,
				Glutes = false,
				Diabetes = false,
				Salt = false
			};
			Dish mainDish1 = new Dish
			{
				MainDish = true,
				Glutes = false,
				Diabetes = false,
				Salt = false
			};
			Dish mainDish2 = new Dish
			{
				MainDish = true,
				Glutes = true,
				Diabetes = true,
				Salt = true
			};
			Dish mainDish3 = new Dish
			{
				MainDish = true,
				Glutes = false,
				Diabetes = false,
				Salt = false
			};
			Dish mainDish4 = new Dish
			{
				MainDish = true,
				Glutes = false,
				Diabetes = false,
				Salt = false
			};
			Dish mainDish5 = new Dish
			{
				MainDish = true,
				Glutes = false,
				Diabetes = false,
				Salt = false
			};
			Dish mainDish6 = new Dish
			{
				MainDish = true,
				Glutes = false,
				Diabetes = false,
				Salt = false
			};
			Dish mainDish7 = new Dish
			{
				MainDish = true,
				Glutes = false,
				Diabetes = false,
				Salt = false
			};

			DayOfTheWeek monday = new DayOfTheWeek();
			monday.dayNr = 1;
			DayOfTheWeek tuesday = new DayOfTheWeek();
			tuesday.dayNr = 2;
			DayOfTheWeek wednesday = new DayOfTheWeek();
			wednesday.dayNr = 3;
			DayOfTheWeek thursday = new DayOfTheWeek();
			thursday.dayNr = 4;
			DayOfTheWeek friday = new DayOfTheWeek();
			friday.dayNr = 5;
			DayOfTheWeek saturday = new DayOfTheWeek();
			saturday.dayNr = 6;
			DayOfTheWeek sunday = new DayOfTheWeek();
			sunday.dayNr = 7;

			monday.Dishes.Add(starterDish);
			monday.Dishes.Add(mainDish1);

			tuesday.Dishes.Add(starterDish);
			tuesday.Dishes.Add(mainDish2);

			wednesday.Dishes.Add(starterDish);
			wednesday.Dishes.Add(mainDish3);

			thursday.Dishes.Add(starterDish);
			thursday.Dishes.Add(mainDish4);

			friday.Dishes.Add(starterDish);
			friday.Dishes.Add(mainDish5);

			saturday.Dishes.Add(starterDish);
			saturday.Dishes.Add(mainDish6);

			sunday.Dishes.Add(starterDish);
			sunday.Dishes.Add(mainDish7);

			Card card = new Card();
			card.DayOfTheWeek.Add(monday);
			card.DayOfTheWeek.Add(tuesday);
			card.DayOfTheWeek.Add(wednesday);
			card.DayOfTheWeek.Add(thursday);
			card.DayOfTheWeek.Add(friday);
			card.DayOfTheWeek.Add(saturday);
			card.DayOfTheWeek.Add(sunday);

			PlanningController planningController = new PlanningController(null);

			//act
			var isSaved = planningController.Check(card);

			//assert
			Assert.False(isSaved);
		}

		[Fact]
		public void Minimun_Order_For_4_Work_Days()
		{
			//arrange
			Dish starterDish = new Dish();
			starterDish.Starter = true;
			Dish mainDish1 = new Dish();
			mainDish1.MainDish = true;
			Dish mainDish2 = new Dish();
			mainDish2.MainDish = true;
			Dish mainDish3 = new Dish();
			mainDish3.MainDish = true;
			Dish mainDish4 = new Dish();
			mainDish4.MainDish = true;

			Cart cart = new Cart();

			cart.AddToCart(1, starterDish);
			cart.AddToCart(1, mainDish1);
			cart.AddToCart(2, starterDish);
			cart.AddToCart(2, mainDish2);
			cart.AddToCart(3, starterDish);
			cart.AddToCart(3, mainDish3);
			cart.AddToCart(4, starterDish);
			cart.AddToCart(4, mainDish4);

			//act
			OrderController orderController = new OrderController(null, null);
			var result = orderController.CheckValidOrder(cart);

			//assert
			Assert.True(result);
		}

		[Fact]
		public void Minimun_Order_For_4_Work_Days_Should_Be_False_If_Only_Three_Days_Planned()
		{
			//arrange
			Dish starterDish = new Dish();
			starterDish.Starter = true;
			Dish mainDish1 = new Dish();
			mainDish1.MainDish = true;
			Dish mainDish2 = new Dish();
			mainDish2.MainDish = true;
			Dish mainDish3 = new Dish();
			mainDish3.MainDish = true;

			Cart cart = new Cart();

			cart.AddToCart(1, starterDish);
			cart.AddToCart(1, mainDish1);
			cart.AddToCart(2, starterDish);
			cart.AddToCart(2, mainDish2);
			cart.AddToCart(3, starterDish);
			cart.AddToCart(3, mainDish3);

			//act
			OrderController orderController = new OrderController(null, null);
			var result = orderController.CheckValidOrder(cart);

			//assert
			Assert.False(result);
		}

		[Fact]
		public void Price_Is_Showable_To_The_Customer()
		{
			//arrange
			Dish starterDish = new Dish();
			starterDish.Starter = true;
			starterDish.Price = 1;
			Dish mainDish1 = new Dish();
			mainDish1.MainDish = true;
			mainDish1.Price = 1;
			Dish mainDish2 = new Dish();
			mainDish2.MainDish = true;
			mainDish2.Price = 1;
			Dish mainDish3 = new Dish();
			mainDish3.MainDish = true;
			mainDish3.Price = 1;
			Dish mainDish4 = new Dish();
			mainDish4.MainDish = true;
			mainDish4.Price = 2;

			Cart cart = new Cart();

			cart.AddToCart(1, starterDish);
			cart.AddToCart(1, mainDish1);
			cart.AddToCart(2, starterDish);
			cart.AddToCart(2, mainDish2);
			cart.AddToCart(3, starterDish);
			cart.AddToCart(3, mainDish3);
			cart.AddToCart(4, starterDish);
			cart.AddToCart(4, mainDish4);

			//act
			var result = cart.Price;

			//assert
			Assert.True((int)result == 9);
		}

		[Fact]
		public void Discount_Of_10_Percent_After_15_Meals()
		{
			//arrange

			var user = new User
			{
				DateOfBirth = new DateTime(1998, 3, 28),
				Id = "1"
			};

			Invoice invoice = new Invoice
			{
				OrderDate = new DateTime(2000, 3, 10),
				CustomerId = "1"
			};

			for (int i = 0; i < 15; i++)
			{
				InvoiceLine invoiceLine = new InvoiceLine
				{
					Price = 10
				};
				invoice.InvoiceLines.Add(invoiceLine);
			}

			var invoices = new List<Invoice>
			{
				invoice
			};

			var mock = new Mock<IOrderRepository>();

			mock.SetupGet(m => m.GetOrder(null)).Returns(invoices.AsQueryable);

			var orderController = new OrderController(mock.Object, null);


			Dish starterDish = new Dish
			{
				Starter = true,
				Price = 10
			};
			Dish mainDish1 = new Dish
			{
				MainDish = true, 
				Price = 10
			};
			Dish mainDish2 = new Dish
			{
				MainDish = true,
				Price = 10
			};
			Dish mainDish3 = new Dish
			{
				MainDish = true,
				Price = 10
			};
			Dish mainDish4 = new Dish
			{
				MainDish = true,
				Price = 10
			};

			Cart cart = new Cart
			{
				Date = new DateTime(2000, 3, 15)
			};

			cart.AddToCart(1, starterDish);
			cart.AddToCart(1, mainDish1);

			cart.AddToCart(2, starterDish);
			cart.AddToCart(2, mainDish2);

			cart.AddToCart(3, starterDish);
			cart.AddToCart(3, mainDish3);

			cart.AddToCart(4, starterDish);
			cart.AddToCart(4, mainDish4);


			//act
			var result = orderController.Process(cart, user);

			//assert
			Assert.True(result.Discounted);

			Decimal price = 0;
			foreach (var resultInvoiceLine in result.InvoiceLines)
			{
				price += resultInvoiceLine.Price;
			}

			Assert.Equal(207,price);
		}

		[Fact]
		public void Discount_Of_10_Percent_After_15_Meals_With_Birthday_Check()
		{
			//arrange

			var user = new User
			{
				DateOfBirth = new DateTime(1998, 3, 28),
				Id = "1"
			};

			Invoice invoice = new Invoice
			{
				OrderDate = new DateTime(2000, 3, 28),
				CustomerId = "1"
			};

			for (var i = 0; i < 15; i++)
			{
				var invoiceLine = new InvoiceLine
				{
					Price = 10
				};
				invoice.InvoiceLines.Add(invoiceLine);
			}

			var invoices = new List<Invoice>
			{
				invoice
			};

			var mock = new Mock<IOrderRepository>();

			mock.SetupGet(m => m.GetOrder(null)).Returns(invoices.AsQueryable);

			var orderController = new OrderController(mock.Object, null);


			Dish starterDish = new Dish
			{
				Starter = true,
				Price = 10
			};
			Dish mainDish1 = new Dish
			{
				MainDish = true,
				Price = 10
			};
			Dish mainDish2 = new Dish
			{
				MainDish = true,
				Price = 10
			};
			Dish mainDish3 = new Dish
			{
				MainDish = true,
				Price = 10
			};
			Dish mainDish4 = new Dish
			{
				MainDish = true,
				Price = 10
			};

			Cart cart = new Cart
			{
				Date = new DateTime(2000, 3, 15)
			};

			cart.AddToCart(1, starterDish);
			cart.AddToCart(1, mainDish1);

			cart.AddToCart(2, starterDish);
			cart.AddToCart(2, mainDish2);

			cart.AddToCart(3, starterDish);
			cart.AddToCart(3, mainDish3);

			cart.AddToCart(4, starterDish);
			cart.AddToCart(4, mainDish4);


			//act
			var result = orderController.Process(cart, user);

			//assert
			Assert.True(result.Discounted);

			Decimal price = 0;
			foreach (var resultInvoiceLine in result.InvoiceLines)
			{
				price += resultInvoiceLine.Price;
			}

			Assert.Equal(198, price);
		}
	}
}
