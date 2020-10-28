using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BeautySalonWebApi.DAL;
using BeautySalonWebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace BeautySalonWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : Controller
    {
        CustomerOrdersContext db;
        private readonly IUnitOfWork _unitOfWork;
        public CustomerController(CustomerOrdersContext context, IUnitOfWork unitOfWork)
        {
            db = context;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IEnumerable<Customer> Get()
        {
             return _unitOfWork.Customers.GetAllCustomersData();
        }

        [HttpGet("{id}")]
        public Customer Get(Guid id)
        {
            Customer Customer = db.Customers.FirstOrDefault(x => x.Id == id);
            return Customer;
        }

        [HttpPost]
        public IActionResult Post([FromBody] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Customers.Add(customer);
                db.SaveChanges();
                return Ok(customer);
            }
            return BadRequest(ModelState);
        }

        [HttpPut]
        public IActionResult Put(Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Update(customer);
                db.SaveChanges();
                return Ok(customer);
            }
            return BadRequest(ModelState);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            Customer Customer = db.Customers.FirstOrDefault(x => x.Id == id);
            if (Customer != null)
            {
                db.Customers.Remove(Customer);
                db.SaveChanges();
            }
            return Ok(Customer);
        }
    }
}
