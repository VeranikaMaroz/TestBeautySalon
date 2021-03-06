﻿using BeautySalonWebApi.DAL.Repositories;

namespace BeautySalonWebApi.DAL
{
    public interface IUnitOfWork
    {
        ICustomerRepository Customers { get; }
        IProductRepository Products { get; }
        IOrdersRepository Orders { get; }


        int SaveChanges();
    }
}
