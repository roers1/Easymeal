using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using EasyMealCore.DomainModel.Invoice;

namespace EF_SQLServer_OrderImpl
{
	public class OrderDbContext : DbContext
	{
		public DbSet<Invoice> Invoices { get; set; }
		public DbSet<InvoiceLine> InvoiceLines { get; set; }

		public OrderDbContext(DbContextOptions<OrderDbContext> options)
			: base(options) { }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Invoice>()
				.HasMany(i => i.InvoiceLines)
				.WithOne(il => il.Invoice)
				.OnDelete(DeleteBehavior.Cascade);
		}
	}
}
