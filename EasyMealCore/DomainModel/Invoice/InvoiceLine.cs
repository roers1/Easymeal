using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EasyMealCore.DomainModel.Invoice
{
	public class InvoiceLine
	{
	
		public int InvoiceLineId { get; set; }
		public DateTime DeliveryDate { get; set; }
		public int DishId { get; set; }
		public string Name { get; set; }
		public int Size { get; set; }
		[Column(TypeName = "decimal(18,2)")]
		public decimal Price { get; set; }

		public Invoice Invoice { get; set; }
	}
}
