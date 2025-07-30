using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTO.Order;
using MediatR;

namespace Application.Commands_Queries.Order.PlaceOrder.Queries.GetOrdersByClientId
{
    public class GetOrdersByClientIdQuery : IRequest<List<OrderDTO>>
    {
        public string ClientId { get; set; }

        public GetOrdersByClientIdQuery(string clientId)
        {
            ClientId = clientId;
        }
    }
}
