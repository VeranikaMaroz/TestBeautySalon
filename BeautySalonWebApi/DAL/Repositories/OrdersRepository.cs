using BeautySalonWebApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BeautySalonWebApi.DAL.Repositories
{
    public interface IOrdersRepository : IRepository<Order>
    {
        IEnumerable<Order> GetAllOrdersData();
        IEnumerable<Order> GetOrdersByCustomerData(Guid customerId);

    }

    public class OrdersRepository : Repository<Order>, IOrdersRepository
    {
        public OrdersRepository(DbContext context) : base(context)
        { }

        public IEnumerable<Order> GetAllOrdersData()
        {
            return _appContext.Orders
                .Include(o => o.Customer)
                .Include(o => o.OrderDetails)
                .ThenInclude(o => o.Product)
                .OrderBy(o => o.Date)
                .ToList();
        }

        public IEnumerable<Order> GetOrdersByCustomerData(Guid customerId)
        {
            return _appContext.Orders.Where(o => o.CustomerId == customerId)
                .Include(o => o.OrderDetails)
                .ThenInclude(o => o.Product)
                .OrderBy(o => o.Date)
                .ToList();
        }

        private CustomerOrdersContext _appContext => (CustomerOrdersContext)_context;
    }
}
