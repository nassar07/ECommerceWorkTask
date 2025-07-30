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
    public class GetOrdersByClientIdQueryHandler : IRequestHandler<GetOrdersByClientIdQuery, List<OrderDTO>>
    {
        private readonly IOrderRepository _orderRepository;

        public GetOrdersByClientIdQueryHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<List<OrderDTO>> Handle(GetOrdersByClientIdQuery request, CancellationToken cancellationToken)
        {
            var orders = await _orderRepository.GetOrdersByClientIdAsync(request.ClientId);

            return orders.Select(order => new OrderDTO
            {
                Id = order.Id,
                ClientId = order.ClientId,
                CreatedAt = order.CreatedAt,
                IsShipped = order.IsShipped,
                Items = order.OrderItems.Select(item => new OrderItemDTO
                {
                    ProductSizeId = item.ProductSizeId,
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice
                }).ToList()
            }).ToList();
        }
    }
}
