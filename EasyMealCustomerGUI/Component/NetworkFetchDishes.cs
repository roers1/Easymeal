using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using EasyMealCore.DomainModel;


namespace EasyMealCustomerGUI.Component
{
	public static class NetworkFetchDishes
	{
		private static readonly HttpClient Client = new HttpClient();
	
		public static async Task<Dish> GetDishAsync(string path)
		{
			Dish dish = null;

			HttpResponseMessage response = await Client.GetAsync(path);
			if (response.IsSuccessStatusCode)
			{
				dish = await response.Content.ReadAsAsync<Dish>();
			}
			return dish;
		}

		public static async Task<IEnumerable<Dish>> GetDishesAsync(string path)
		{
			IEnumerable<Dish> dish = null;

			HttpResponseMessage response = await Client.GetAsync(path);
			if (response.IsSuccessStatusCode)
			{
				dish = await response.Content.ReadAsAsync<IEnumerable<Dish>>();
			}
			return dish;
		}

	}
}
