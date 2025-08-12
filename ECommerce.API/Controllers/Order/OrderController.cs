using Application.Commands_Queries.Order.PlaceOrder.Commands;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Application.Commands_Queries.Order.PlaceOrder.Queries.GetAll;
using Application.Commands_Queries.Order.PlaceOrder.Queries.GetOrdersByClientId;
using Application.Commands_Queries.Order.PlaceOrder.Commands.MarkAsShipped;
using Application.Commands_Queries.Order.PlaceOrder.Commands.FailPayment;
using Application.Commands_Queries.Order.PlaceOrder.Commands.ConfirmPayment;

namespace ECommerce.API.Controllers.Order
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediator;
        public OrderController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        [HttpGet("all")]
        public async Task<IActionResult> GetAllOrders()
        {
            var result = await _mediator.Send(new GetAllOrdersQuery());
            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderById(int id)
        {
            var result = await _mediator.Send(new GetOrderByIdQuery(id));
            if (result is null)
                return NotFound();

            return Ok(result);
        }

        [HttpGet("client/{clientId}")]
        public async Task<IActionResult> GetOrdersByClientId(string clientId)
        {
            var result = await _mediator.Send(new GetOrdersByClientIdQuery(clientId));
            return Ok(result);
        }



        [HttpPut("{orderId}/ship")]
        public async Task<IActionResult> MarkAsShipped(int orderId)
        {
            var result = await _mediator.Send(new MarkOrderAsShippedCommand(orderId));
            if (!result) return NotFound();
            return Ok(new { success = true });
        }

        [HttpPut("{orderId}/confirm-payment")]
        public async Task<IActionResult> ConfirmPayment(int orderId)
        {
            var result = await _mediator.Send(new ConfirmPaymentCommand(orderId));
            if (!result) return NotFound();
            return Ok(new { success = true, message = "Payment confirmed successfully" });
        }


        [HttpPut("{orderId}/fail-payment")]
        public async Task<IActionResult> FailPayment(int orderId)
        {
            var result = await _mediator.Send(new FailPaymentCommand(orderId));
            if (!result) return NotFound();
            return Ok(new { success = true, message = "Payment marked as failed" });
        }






    }
}
