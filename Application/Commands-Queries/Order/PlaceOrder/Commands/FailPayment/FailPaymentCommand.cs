using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Application.Commands_Queries.Order.PlaceOrder.Commands.FailPayment
{
    public record FailPaymentCommand(int OrderId) : IRequest<bool>;
    
}
