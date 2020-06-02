using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using EasyMealCore.DomainModel.CustomerModels;

namespace EasyMealCore.DomainModel.Invoice
{
	public class Invoice
	{
		public int InvoiceId { get; set; }
		public string CustomerId { get; set; }
		public DateTime OrderDate { get; set; }
		public bool Discounted { get; set; }

		public List<InvoiceLine> InvoiceLines { get; set; }

		public Invoice()
		{
			InvoiceLines = new List<InvoiceLine>();
		}

	}
}
