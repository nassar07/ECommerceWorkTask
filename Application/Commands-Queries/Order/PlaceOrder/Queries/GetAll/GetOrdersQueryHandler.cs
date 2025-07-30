using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.DTO.Order;
using MediatR;

namespace Application.Commands_Queries.Order.PlaceOrder.Queries.GetAll
{
    public class GetAllOrdersQueryHandler : IRequestHandler<GetAllOrdersQuery, List<OrderDTO>>
    {
        private readonly IOrderRepository _orderRepository;

        public GetAllOrdersQueryHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<List<OrderDTO>> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken)
        {
            var orders = await _orderRepository.GetAllOrdersAsync();

            // تحويل الكيانات إلى DTOs
            var orderDTOs = orders.Select(order => new OrderDTO
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

            return orderDTOs;
        }
    }

}
