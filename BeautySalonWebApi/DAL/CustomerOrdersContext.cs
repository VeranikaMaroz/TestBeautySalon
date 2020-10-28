using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BeautySalonWebApi.Models;

namespace BeautySalonWebApi.DAL
{
	public class CustomerOrdersContext : DbContext
	{

		public CustomerOrdersContext(DbContextOptions<CustomerOrdersContext> options)
			: base(options)
		{
			Database.EnsureCreated();
		}

		public DbSet<Customer> Customers { get; set; }
		public DbSet<Order> Orders { get; set; }
		public DbSet<OrderDetail> OrderDetails { get; set; }
		public DbSet<Product> Products { get; set; }

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);
			const string priceDecimalType = "decimal(18,2)";
			
			
			builder.Entity<Customer>().HasKey(x => x.Id); // Notice this!
			builder.Entity<Customer>().Property(x => x.Id).ValueGeneratedOnAdd();

			builder.Entity<Customer>().Property(c => c.Name).IsRequired().HasMaxLength(100);
			builder.Entity<Customer>().Property(c => c.PhoneNumber).IsUnicode(false).HasMaxLength(30);
			builder.Entity<Customer>()
				.HasMany(g => g.Orders)
				.WithOne(s => s.Customer)
				.HasForeignKey(s => s.CustomerId);

			builder.Entity<Product>().Property(p => p.Name).IsRequired().HasMaxLength(100);
			builder.Entity<Product>().Property(p => p.Price).HasColumnType(priceDecimalType);

			builder.Entity<Order>()
				.HasMany(g => g.OrderDetails)
				.WithOne(s => s.Order)
				.HasForeignKey(s => s.OrderId);

			//builder.Entity<OrderDetail>()
			//	.HasOne(s => s.Product);

		}
	}
}
