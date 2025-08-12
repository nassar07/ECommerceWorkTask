using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Application.Commands_Queries.Order.PlaceOrder.Commands.ConfirmPayment
{
    public record ConfirmPaymentCommand(int OrderId) : IRequest<bool>;
    
}
