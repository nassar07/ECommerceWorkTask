using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Domain.Enums;
using Microsoft.AspNetCore.Http;
using Application.DTO.Order;

namespace Application.Commands_Queries.Order.PlaceOrder.Commands
{
    public class CreateOrderHandler : IRequestHandler<CreateOrderCommand, CreateOrderResultDto>
    {
        private readonly IOrderRepository _context;
        

        public CreateOrderHandler(IOrderRepository context)
        {
            _context = context;
        }

        public async Task<CreateOrderResultDto> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            using var transaction = await _context.BeginTransactionAsync(cancellationToken);

            var orderItems = new List<OrderItem>();

            foreach (var item in request.Items)
            {
                var productSize = await _context.GetProductSizeByIdAsync(item.ProductSizeId, cancellationToken);

                if (productSize == null)
                    throw new Exception($"ProductSize with ID {item.ProductSizeId} not found.");

                if (productSize.Quantity < item.Quantity)
                    throw new Exception($"Not enough quantity for size {productSize.Size}. Available: {productSize.Quantity}");

                productSize.Quantity -= item.Quantity;

                orderItems.Add(new OrderItem
                {
                    ProductSizeId = item.ProductSizeId,
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice
                });
            }

            var order = new Domain.Entities.Order
            {
                ClientId = request.UserId,
                CreatedAt = DateTime.UtcNow,
                ShippingAddress = request.ShippingAddress,
                IsShipped = false,
                OrderItems = orderItems,
                TotalPrice = request.TotalPrice,

                PaymentMethod = request.PaymentMethod,
                PaymentStatus = request.PaymentMethod == PaymentMethod.CashOnDelivery ? PaymentStatus.Pending : PaymentStatus.Pending
            };

            await _context.CreateOrderAsync(order);
            await _context.SaveChangesAsync();
            await transaction.CommitAsync(cancellationToken);

            string paymentUrl = null;
            if (request.PaymentMethod != PaymentMethod.CashOnDelivery)
            {
                paymentUrl = $"http://localhost:4200/checkout/payment/{order.Id}";
            }

            return new CreateOrderResultDto
            {
                OrderId = order.Id,
                PaymentUrl = paymentUrl
            };
        }

    }



}
