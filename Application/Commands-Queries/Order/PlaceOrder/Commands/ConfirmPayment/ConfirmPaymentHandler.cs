using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Enums;
using Application.Common.Interfaces;
using MediatR;

namespace Application.Commands_Queries.Order.PlaceOrder.Commands.ConfirmPayment
{
    public class ConfirmPaymentHandler : IRequestHandler<ConfirmPaymentCommand, bool>
    {
        public ConfirmPaymentHandler(IOrderRepository context)
        {
            _context = context;
        }

        public IOrderRepository _context { get; }

        public async Task<bool> Handle(ConfirmPaymentCommand request, CancellationToken cancellationToken)
        {
            var order = await _context.GetOrderByIdAsync(request.OrderId);
            if (order == null)
                return false;

            if(order.PaymentStatus == PaymentStatus.Paid)
                throw new InvalidOperationException("Payment has already been confirmed for this order.");


            order.PaymentStatus = PaymentStatus.Paid;

            await _context.SaveChangesAsync();
            return true;
        }
    }
}
