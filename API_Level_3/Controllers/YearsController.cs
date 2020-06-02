using System.Collections.Generic;
using System.Linq;
using EasyMealCore.DomainModel;
using EasyMealCore.DomainServices;
using Halcyon.HAL;
using Halcyon.Web.HAL;
using Microsoft.AspNetCore.Mvc;

namespace API_Level_3.Controllers
{
	[Produces("application/json")]
	[Route("api/v1/[controller]")]
	[ApiController]
	public class YearsController : ControllerBase
	{
		private readonly IPlanningRepository _context;

		public YearsController(IPlanningRepository context)
		{
			_context = context;
		}

		/// <summary>
		/// shows all the weeks that have a planning of a current year 
		/// </summary>
		/// <param name="yearNr"></param>
		[HttpGet("{yearNr}")]
		public IActionResult GetYear(int yearNr)
		{
			var planning = (from w in _context.Weekplannings
				join wd in _context.weekplanningDays
					on w.WeekplanningId equals wd.WeekplanningId
				where w.Year == yearNr
				select new
				{
					Year = w.Year,
					Week = w.Week,
				}).ToList();

			return this.HAL(planning, new Link[] {
				new Link("self", "/api/v1/years/{Year}"),
				new Link("year:week", "/api/v1/years/{yearNr}/weeks/{Week}")
			});
		}


		/// <summary>
		/// shows all the days that have a planning of a current year and week
		/// </summary>
		/// <param name="yearNr"></param>
		/// <param name="weekNr"></param>
		[HttpGet("{yearNr}/weeks/{weekNr}")]
		public IActionResult Week(int weekNr, int yearNr)
		{
			var planning = (from w in _context.Weekplannings
				join wd in _context.weekplanningDays
					on w.WeekplanningId equals wd.WeekplanningId
				join d in _context.Days
					on wd.DayId equals d.DayId
				where w.Year == yearNr && w.Week == weekNr
				select new
				{
					Week = w.Week,
					DayOfTheWeek = d.DayOfTheWeek,
				}).ToList();

			return this.HAL(planning, new Link[] {
				new Link("self", "/api/v1/years/{yearNr}/weeks/{weekNr}"),
				new Link("year:week", "/api/v1/years/{yearNr}/weeks/{weekNr}/days/{dayNr}")
			});
		}

		/// <summary>
		/// shows the dishes of the requested planning using the date input fields
		/// </summary>
		/// <param name="yearNr"></param>
		/// <param name="weekNr"></param>
		/// <param name="dayNr"></param>
		[HttpGet("{yearNr}/weeks/{weekNr}/days/{dayNr}")]
		public IActionResult Day(int weekNr, int yearNr, int dayNr)
		{
			var planning = (from w in _context.Weekplannings
				join wd in _context.weekplanningDays
					on w.WeekplanningId equals wd.WeekplanningId
				join d in _context.Days
					on wd.DayId equals d.DayId
				join dd in _context.dayDishes
					on d.DayId equals dd.DayId
				join di in _context.Dishes
					on dd.DishId equals di.DishId
				where w.Year == yearNr && w.Week == weekNr && d.DayOfTheWeek == dayNr
				select new
				{
					DishId = di.DishId,
					Starter = di.Starter,
					MainDish = di.MainDish,
					Dessert = di.Dessert,
					Salt = di.Salt,
					Diabetes = di.Diabetes,
					Glutes = di.Glutes,
					Name = di.Name,
					Description = di.Description,
					Price = di.Price,
					Image = di.Image
				}).ToList();

			return this.HAL(planning, new Link[] {
				new Link("self", "/api/v1/years/{yearNr}/weeks/{weekNr}/days/{dayNr}")
			});
		}
	}
}