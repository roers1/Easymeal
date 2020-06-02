using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using EasyMealCore.DomainModel.CookModels;
using EasyMealCore.DomainModel.CustomerModels;
using Microsoft.AspNetCore.Identity;

namespace EasyMealCore.DomainModel
{
	public class User : IdentityUser
	{
		public User() { }
		public User(string userName)
		{
			UserName = userName;
		}
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Address { get; set; }
		public DateTime DateOfBirth { get; set; }
		public bool Diabetes { get; set; }
		public bool Salt { get; set; }
		public bool Glutes { get; set; }
		public virtual Cook Cook { get; set; }
		public virtual Customer Customer { get; set; }
	}
}
