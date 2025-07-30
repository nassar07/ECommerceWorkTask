using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using MediatR;

namespace Application.Commands_Queries.Order.PlaceOrder.Commands.MarkAsShipped
{
    public class MarkOrderAsShippedCommandHandler : IRequestHandler<MarkOrderAsShippedCommand, bool>
    {
        private readonly IOrderRepository _orderRepository;

        public MarkOrderAsShippedCommandHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<bool> Handle(MarkOrderAsShippedCommand request, CancellationToken cancellationToken)
        {
            var result = await _orderRepository.MarkOrderAsShippedAsync(request.OrderId);
            if (result)
            {
                await _orderRepository.SaveChangesAsync();
            }

            return result;
        }
    }
}
