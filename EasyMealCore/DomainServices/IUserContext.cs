using System;
using System.Collections.Generic;
using System.Text;
using EasyMealCore.DomainModel;
using EasyMealCore.DomainModel.CookModels;
using EasyMealCore.DomainModel.CustomerModels;

namespace EasyMealCore.DomainServices
{
	public interface IUserContext
	{
		IEnumerable<User> Users { get; }
		IEnumerable<Cook> Cook { get; }
		IEnumerable<Customer> Customer { get; }
	}
}
