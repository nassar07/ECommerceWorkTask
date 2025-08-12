using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTO.Order;
using Domain.Enums;
using MediatR;

namespace Application.Commands_Queries.Order.PlaceOrder.Commands
{
    public class CreateOrderCommand : IRequest<CreateOrderResultDto>
    {
        public string UserId { get; set; }
        public string ShippingAddress { get; set; }
        public decimal TotalPrice { get; set; }
        public List<OrderItemDTO> Items { get; set; }

        public PaymentMethod PaymentMethod { get; set; }
        public PaymentStatus PaymentStatus { get; set; } = PaymentStatus.Pending;
    }


}
