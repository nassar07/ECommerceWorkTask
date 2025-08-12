using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Domain.Enums;
using MediatR;

namespace Application.Commands_Queries.Order.PlaceOrder.Commands.FailPayment
{
    public class FailPaymentHandler : IRequestHandler<FailPaymentCommand, bool>
    {
        private readonly IOrderRepository _context;
        public FailPaymentHandler(IOrderRepository context)
        {
            _context = context;
        }
        public async Task<bool> Handle(FailPaymentCommand request, CancellationToken cancellationToken)
        {
            var order = await _context.GetOrderByIdAsync(request.OrderId);
            if (order == null)
                return false;
            if (order.PaymentStatus == PaymentStatus.Failed)
                throw new InvalidOperationException("Payment has already been failed for this order.");
            order.PaymentStatus = PaymentStatus.Failed;
            await _context.SaveChangesAsync();
            return true;
        }

    }
}
