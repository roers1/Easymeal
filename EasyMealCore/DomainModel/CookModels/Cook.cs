using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using EasyMealCore.DomainModel.CookModels;
using EasyMealCore.DomainModel.CustomerModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;

namespace EasyMealCore.DomainModel.CookModels
{
	public class Cook
	{
		public User User { get; set; }
		[Key]
		public string Id { get; set; }
	}
}

