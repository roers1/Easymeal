using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EasyMealCore.DomainModel.CookRepo;

namespace EasyMealCore.DomainModel
{
	public class Dish
	{
		public int DishId { get; set; }
		[Required(ErrorMessage = "Please  specify if meal is starter")]
		public Boolean Starter { get; set; }

		[Required(ErrorMessage = "Please specify if meal is main dish")]
		public Boolean MainDish { get; set; }

		[Required(ErrorMessage = "Please specify if meal is dessert")]
		public Boolean Dessert { get; set; }

		[Required(ErrorMessage = "Please specify if meal contains salt")]
		public Boolean Salt { get; set; }

		[Required(ErrorMessage = "Please specify if meal contains diabetes ")]
		public Boolean Diabetes { get; set; }

		[Required(ErrorMessage = "Please specify if meal contains glutes")]
		public Boolean Glutes { get; set; }

		[Required(ErrorMessage = "Please specify a name")]
		public String Name { get; set; }

		[Required(ErrorMessage = "Please specify a description")]
		public String Description { get; set; }

		[Required]
		[Column(TypeName = "decimal(18,2)")]
		[Range(0.01, double.MaxValue,
		ErrorMessage = "Please enter a positive price")]
		public decimal Price { get; set; }

		public byte[] Image { get; set; }
		public IList<DayDish> DayDishes { get; set; }
	}
}
