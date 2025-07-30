using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Commands_Queries.Order.PlaceOrder.Commands
{
    public class CreateOrderHandler : IRequestHandler<CreateOrderCommand, int>
    {
        private readonly IOrderRepository _context;

        public CreateOrderHandler(IOrderRepository context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var order = new Domain.Entities.Order
            {
                ClientId = request.UserId,
                CreatedAt = DateTime.UtcNow,
                ShippingAddress = request.ShippingAddress,
                IsShipped = false,
                OrderItems = request.Items.Select(i => new OrderItem
                {
                    ProductSizeId = i.ProductSizeId,
                    Quantity = i.Quantity,
                    UnitPrice = i.UnitPrice
                }).ToList()
            };

            await _context.CreateOrderAsync(order);
            await _context.SaveChangesAsync();

            return order.Id;
        }
    }



}
