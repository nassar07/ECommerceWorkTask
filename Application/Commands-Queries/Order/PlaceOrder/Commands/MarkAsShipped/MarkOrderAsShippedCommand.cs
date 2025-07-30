using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Application.Commands_Queries.Order.PlaceOrder.Commands.MarkAsShipped
{
    public class MarkOrderAsShippedCommand : IRequest<bool>
    {
        public int OrderId { get; set; }

        public MarkOrderAsShippedCommand(int orderId)
        {
            OrderId = orderId;
        }
    }
}
