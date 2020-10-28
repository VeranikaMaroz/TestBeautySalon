using System;
using System.Collections.Generic;
using System.Linq;
using BeautySalonWebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BeautySalonWebApi.DAL.Repositories
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        IEnumerable<Customer> GetAllCustomersData();
    }

    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
        public CustomerRepository(CustomerOrdersContext context) : base(context)
        { }

        public IEnumerable<Customer> GetAllCustomersData()
        {
            return _appContext.Customers
                .OrderBy(c => c.Name)
                .ToList();
        }

        private CustomerOrdersContext _appContext => (CustomerOrdersContext)_context;
    }
}
