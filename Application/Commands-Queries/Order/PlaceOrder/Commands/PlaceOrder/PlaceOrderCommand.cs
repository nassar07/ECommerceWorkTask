using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTO.Order;
using MediatR;

namespace Application.Commands_Queries.Order.PlaceOrder.Commands
{
    public class CreateOrderCommand : IRequest<int>
    {
        public string UserId { get; set; }
        public string ShippingAddress { get; set; }
        public List<OrderItemDTO> Items { get; set; }
    }


}
