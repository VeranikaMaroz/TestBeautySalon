using BeautySalonWebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BeautySalonWebApi.DAL.Repositories
{
    public interface IProductRepository : IRepository<Product>
    {

    }

    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(DbContext context) : base(context)
        { }

        private CustomerOrdersContext _appContext => (CustomerOrdersContext)_context;
    }
}
