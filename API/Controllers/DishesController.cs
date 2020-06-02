using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EasyMealCore.DomainServices;
using EasyMealCore.DomainModel;

namespace API.Controllers
{
	[Produces("application/json")]
	[Route("api/v1/[controller]")]
	[ApiController]
	public class DishesController : ControllerBase
	{
		private readonly IPlanningRepository _context;

		public DishesController(IPlanningRepository context)
		{
			_context = context;
		}

		/// <summary>
		/// retrieves all dishes.
		/// </summary>
		/// <remarks>
		/// Sample request:
		///
		///     get api/v1/dishes
		///     {
		///			"dishId": 7,
		///			"starter": true,
		///			"mainDish": false,
		///			"dessert": false,
		///			"salt": false,
		///			"diabetes": false,
		///			"glutes": false,
		///			"name": "Tomato soup",
		///			"description": "Soup made from tomatoes",
		///			"price": 3.99,
		///			"image": "",
		///			"dayDishes": null
		///		}
		/// </remarks>
		// GET: api/Dishes
		[HttpGet]
		public ActionResult<IEnumerable<Dish>> GetDishes()
		{
			return _context.Dishes.ToList();
		}

		/// <summary>
		/// retrieves a specific dish.
		/// </summary>
		/// <param name="id"></param>
		// GET: api/Dishes/5
		[HttpGet("{id}")]
		public ActionResult<Dish> GetDish(int id)
		{
			var dish = _context.Dishes.FirstOrDefault(d => d.DishId == id);

			if (dish == null)
			{
				return NotFound();
			}

			return dish;
		}

		/// <summary>
		/// updates a specific dish.
		/// </summary>
		/// <param name="id"></param>
		// PUT: api/Dishes/5
		[HttpPut("{id}")]
		public Task<IActionResult> PutDish(int id, Dish dish)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// creates a specific dish.
		/// </summary>
		/// <param name="dish"></param>
		// POST: api/Dishes
		[HttpPost]
		public Task<ActionResult<Dish>> PostDish(Dish dish)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// deletes a specific dish.
		/// </summary>
		/// <param name="id"></param>
		// DELETE: api/Dishes/5
		[HttpDelete("{id}")]
		public Task<ActionResult<Dish>> DeleteDish(int id)
		{
			throw new NotImplementedException();
		}

		private bool DishExists(int id)
		{
			return _context.Dishes.Any(e => e.DishId == id);
		}
	}
}
