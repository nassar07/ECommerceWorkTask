using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.DTO.Order;
using MediatR;

namespace Application.Commands_Queries.Order.PlaceOrder.Queries.GetOrdersByClientId
{
    public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, OrderDTO?>
    {
        private readonly IOrderRepository _orderRepository;

        public GetOrderByIdQueryHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<OrderDTO?> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetOrderByIdAsync(request.OrderId);
            if (order == null)
                return null;

            return new OrderDTO
            {
                Id = order.Id,
                ClientId = order.ClientId,
                CreatedAt = order.CreatedAt,
                IsShipped = order.IsShipped,
                Items = order.OrderItems.Select(i => new OrderItemDTO
                {
                    ProductSizeId = i.ProductSizeId,
                    Quantity = i.Quantity,
                    UnitPrice = i.UnitPrice
                }).ToList()
            };
        }
    }
}
