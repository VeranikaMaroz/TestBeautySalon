using BeautySalonWebApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BeautySalonWebApi.DAL
{
    public interface IDatabaseInitializer
    {
        void Seed();
    }
    public class DatabaseInitializer : IDatabaseInitializer
    {
        private readonly CustomerOrdersContext _context;
        public DatabaseInitializer(CustomerOrdersContext context)
        {
            _context = context;
        }

        public void Seed()
        {
            _context.Database.Migrate();

            if (!_context.Customers.Any() && ! _context.Products.Any())
            {
                Customer cust_1 = new Customer
                {
                    Name = "Александр Иванов",
                    PhoneNumber = "80291111111"
                };
                Customer cust_2 = new Customer
                {
                    Name = "Вася Пупкин",
                    PhoneNumber = "80292222222"
                };
                Customer cust_3 = new Customer
                {
                    Name = "Валентин Смирнов",
                    PhoneNumber = "80293333333"
                };
                Customer cust_4 = new Customer
                {
                    Name = "Николай Петров",
                    PhoneNumber = "80294444444"
                };

                Product prod_1 = new Product
                {
                    Name = "Маникюр",
                    Price = 30
                };
                Product prod_2 = new Product
                {
                    Name = "Педикюр",
                    Price = 50
                };
                Product prod_3 = new Product
                {
                    Name = "Окраска волос",
                    Price = 80
                };
                Product prod_4 = new Product
                {
                    Name = "Макияж",
                    Price = 30
                };
                Product prod_5 = new Product
                {
                    Name = "Стрижка волос",
                    Price = 20
                };

                Order ordr_1 = new Order
                {
                    Customer = cust_1,
                    Date = DateTime.UtcNow,
                    OrderDetails = new List<OrderDetail>()
                        {
                            new OrderDetail() { Quantity=1, Product = prod_1 },
                            new OrderDetail() { Quantity=1, Product = prod_5 },
                        }
                };
                Order ordr_2 = new Order
                {
                    Customer = cust_2,
                    Date = DateTime.UtcNow,
                    OrderDetails = new List<OrderDetail>()
                        {
                            new OrderDetail() { Quantity=1, Product = prod_2 },
                        }
                };
                Order ordr_3 = new Order
                {
                    Customer = cust_3,
                    Date = DateTime.UtcNow.AddDays(1),
                    OrderDetails = new List<OrderDetail>()
                        {
                            new OrderDetail() { Quantity=1, Product = prod_1 },
                            new OrderDetail() { Quantity=1, Product = prod_2 },
                            new OrderDetail() { Quantity=1, Product = prod_3 },
                            new OrderDetail() { Quantity=1, Product = prod_4 },
                            new OrderDetail() { Quantity=1, Product = prod_5 },
                        }
                };
                Order ordr_4 = new Order
                {
                    Customer = cust_4,
                    Date = DateTime.UtcNow.AddDays(-1),
                    OrderDetails = new List<OrderDetail>()
                        {
                            new OrderDetail() { Quantity=1, Product = prod_3 },
                            new OrderDetail() { Quantity=1, Product = prod_5 },
                        }
                };
                Order ordr_5 = new Order
                {
                    Customer = cust_4,
                    Date = DateTime.UtcNow.AddDays(-14),
                    OrderDetails = new List<OrderDetail>()
                        {
                            new OrderDetail() { Quantity=1, Product = prod_3 },
                            new OrderDetail() { Quantity=1, Product = prod_5 },
                        }
                };


                _context.Customers.Add(cust_1);
                _context.Customers.Add(cust_2);
                _context.Customers.Add(cust_3);
                _context.Customers.Add(cust_4);

                _context.Products.Add(prod_1);
                _context.Products.Add(prod_2);

                _context.Orders.Add(ordr_1);
                _context.Orders.Add(ordr_2);
                _context.Orders.Add(ordr_3);
                _context.Orders.Add(ordr_4);
                _context.Orders.Add(ordr_5);

                _context.SaveChanges();
            }
        }



    }
}
