using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EasyMealCore.DomainModel;
using EasyMealCore.DomainModel.CustomerModels;
using EasyMealCore.DomainModel.Invoice;

namespace EasyMealCore.DomainServices
{
	public interface IOrderRepository
	{
		void SaveOrder(Invoice invoice);
		IQueryable<Invoice> GetOrder(User user);
	}
}
