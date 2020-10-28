using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BeautySalonWebApi.Models
{
    public class Order
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }

        public Guid CustomerId { get; set; }
        
        public Customer Customer { get; set; }

        public ICollection<OrderDetail> OrderDetails { get; set; }

        [NotMapped]
        public decimal TotalPrice
        {
            get
            {
                return this.OrderDetails?.Sum(d => d.Quantity * d.Product.Price) ?? 0;
            }
        }
    }
}
