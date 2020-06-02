using EasyMealCore.DomainServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EasyMealCore.DomainModel;
using EasyMealCore.DomainModel.CustomerModels;
using EasyMealCore.DomainModel.Invoice;

namespace EF_SQLServer_OrderImpl
{
	public class EFOrderRepository : IOrderRepository
	{
		private readonly OrderDbContext _context;

		public EFOrderRepository(OrderDbContext ctx)
		{
			_context = ctx;
		}
		public IQueryable<Invoice> GetOrder(User user)
		{
			return _context.Invoices.Where(i => i.CustomerId == user.Id);
		}


		public void SaveOrder(Invoice invoice)
		{
			if (invoice.InvoiceId == 0)
			{
				_context.Add(invoice);
			}

			_context.SaveChanges();
		}
	}
}


public static class DateTimeExtensions
{
	public static DateTime StartOfWeek(this DateTime dt, DayOfWeek startOfWeek)
	{
		int diff = (7 + (dt.DayOfWeek - startOfWeek)) % 7;
		return dt.AddDays(-1 * diff).Date;
	}
}

