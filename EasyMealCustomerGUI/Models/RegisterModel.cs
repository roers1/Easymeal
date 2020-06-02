using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EasyMealCustomerGUI.Models
{
	public class RegisterModel
	{
		[Required]
		public string FirstName { get; set; }
		[Required]
		public string LastName { get; set; }
		[Required]
		public string Password { get; set; }
		[Required]
		public string Email { get; set; }
		[Required]
		public DateTime DateOfBirth { get; set; }
		[Required]
		public string Address { get; set; }
		[Required]
		public string PhoneNumber { get; set; }
		[Required]
		public Boolean glutes { get; set; }
		[Required]
		public Boolean diabetes { get; set; }
		[Required]
		public Boolean salt { get; set; }
		public string ReturnUrl { get; set; } = "/";
	}
}
