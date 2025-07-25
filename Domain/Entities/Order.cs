using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public string ClientId { get; set; } 
        public DateTime CreatedAt { get; set; }
        public bool IsShipped { get; set; }
        public string ShippingAddress { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; }
    }
}
