using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.Order
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public string ClientId { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public bool IsShipped { get; set; }
        public List<OrderItemDTO> Items { get; set; } = [];
    }
}
