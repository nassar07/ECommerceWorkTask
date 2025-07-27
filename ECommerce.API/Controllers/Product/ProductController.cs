using System.Threading.Tasks;
using Application.DTO.Product;
using Application.Product.Commands.Create;
using Application.Product.Commands.Delete;
using Application.Product.Commands.Update;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ECommerce.API.Controllers.Product
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        public ProductController(IMediator mediator)
        {
            Mediator = mediator;
        }

        public IMediator Mediator { get; }

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            
            var query = new Application.Product.Queries.GetAll.GetAllProductsQuery();
            var result = Mediator.Send(query).Result;
            if (result == null || !result.Any())
            {
                return NotFound("No products found.");
            }
            return Ok(result);
        }

        [HttpGet("GetByOwnerId/{id}")]
        public IActionResult GetByOwnerId(string id) 
        {
            var query = new Application.Product.Queries.GetByOwnerId.GetByOwnerIdQuery(id);
            var result = Mediator.Send(query).Result;
            if (result == null || !result.Any())
            {
                return NotFound($"No products found for owner ID: {id}");
            }
            return Ok(result);

        }

        [HttpPost("CreateProduct")]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductDTO dto)
        {
            var command = new CreateProductCommand(dto);
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromQuery] string ownerId, [FromBody] ProductDTO productDto)
        {
            await Mediator.Send(new UpdateProductCommand(id, ownerId, productDto));
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id, [FromQuery] string ownerId)
        {
            await Mediator.Send(new DeleteProductCommand(id, ownerId));
            return NoContent();
        }




    }
}
