using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BeautySalonWebApi.DAL;
using BeautySalonWebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BeautySalonWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : Controller
    {
        CustomerOrdersContext db;
        private readonly IUnitOfWork _unitOfWork;
        public OrderController(CustomerOrdersContext context, IUnitOfWork unitOfWork)
        {
            db = context;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IEnumerable<Order> Get()
        {
            return _unitOfWork.Orders.GetAllOrdersData();
        }

        [HttpGet("{id}")]
        public Order Get(Guid id)
        {
            Order order = db.Orders
                .Include(o => o.Customer)
                .Include(o => o.OrderDetails)
                .ThenInclude(o => o.Product)
                .FirstOrDefault(x => x.Id == id);
            return order;
        }

        [HttpPost]
        public IActionResult Post([FromBody] Order order)
        {
            if (ModelState.IsValid)
            {
                if(order.Customer != null)
                    db.Customers.Attach(order.Customer);
                db.Orders.Add(order);
                db.SaveChanges();
                return Ok(order);
            }
            return BadRequest(ModelState);
        }
        [HttpPut]
        public IActionResult Put(Order order)
        {
            if (ModelState.IsValid)
            {
                foreach (var det in order.OrderDetails)
                {
                    db.OrderDetails.Add(det);
                    if (det.Product != null)
                        db.Products.Attach(det.Product);
                }
                db.Update(order);
                db.SaveChanges();
                return Ok(order);
            }
            return BadRequest(ModelState);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            if (ModelState.IsValid)
            {
                Order order = db.Orders.FirstOrDefault(x => x.Id == id);
                if (order != null)
                {
                    db.Orders.Remove(order);
                    db.SaveChanges();
                }

                return Ok(order);
            }

            return BadRequest(ModelState);
        }
        
        
        [HttpPost("orderDetails")]
        public IActionResult AddDetal([FromBody] OrderDetail orderDetail)
        {
            if (ModelState.IsValid)
            {
                if(orderDetail.Product != null)
                    db.Products.Attach(orderDetail.Product);
                if (orderDetail.Order != null)
                    db.Orders.Attach(orderDetail.Order);
                db.OrderDetails.Add(orderDetail);
                db.SaveChanges();
                return Ok(orderDetail);
            }
            return BadRequest(ModelState);
        }

        [HttpPut("orderDetails")]
        public IActionResult UpdateDetal(OrderDetail orderDetail)
        {
            if (ModelState.IsValid)
            {
                if (orderDetail.Product != null)
                    db.Products.Attach(orderDetail.Product);
                if (orderDetail.Order != null)
                    db.Orders.Attach(orderDetail.Order);
                db.Update(orderDetail);
                db.SaveChanges();
                return Ok(orderDetail);
            }
            return BadRequest(ModelState);
        }

        [HttpDelete("orderDetails/{id}")]
        public IActionResult DeleteOrderDetails(Guid id)
        {
            if (ModelState.IsValid)
            {
                OrderDetail orderDetail = db.OrderDetails.FirstOrDefault(x => x.Id == id);
                if (orderDetail != null)
                {
                    db.OrderDetails.Remove(orderDetail);
                    db.SaveChanges();
                }

                return Ok(orderDetail);
            }

            return BadRequest(ModelState);
        }
    }
}
