using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace EasyMealCore.DomainModel.CustomerModels
{
	public class Customer
	{
		public User User { get; set; }
		[Key]
		public string Id { get; set; }
	}
}
