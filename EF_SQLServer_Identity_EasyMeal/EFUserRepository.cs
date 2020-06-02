using System;
using System.Collections.Generic;
using System.Text;
using EasyMealCore.DomainModel;
using EasyMealCore.DomainModel.CookModels;
using EasyMealCore.DomainModel.CustomerModels;
using EasyMealCore.DomainServices;

namespace EF_SQLServer_Identity_EasyMeal
{
	public class EFUserRepository : IUserContext
	{
		private readonly AppIdentityDbContext _context;

		public EFUserRepository(AppIdentityDbContext ctx)
		{
			_context = ctx;
		}

		public IEnumerable<User> Users => _context.Users;
		public IEnumerable<Cook> Cook => _context.Cooks;
		public IEnumerable<Customer> Customer => _context.Customers;
	}
}
