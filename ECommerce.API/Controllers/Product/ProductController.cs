using System.Threading.Tasks;
using Application.Commands_Queries.Product_Size.Commands;
using Application.Commands_Queries.Product_Size.Commands.Update;
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

        [HttpGet("GetListOfProducts")]
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

        [HttpGet("GetProductById/{id}")]
        public IActionResult GetById(int id)
        {
            var query = new Application.Commands_Queries.Product.Queries.GetById.GetByIdQuery(id);
            var result = Mediator.Send(query).Result;
            if (result == null)
            {
                return NotFound($"Product with ID: {id} not found.");
            }
            return Ok(result);
        }

        [HttpGet("GetListOfProductsOfOwnerByOwnerId/{id}")]
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

        [HttpGet("GetProductByIdAndOwnerId/{productId}/{ownerId}")]
        public IActionResult GetProductByIdAndOwne(int productId , string ownerId)
        {
            var query = new Application.Product.Queries.GetByOwnerIdAndProductId.GetProductByIdAndOwnerQuery(productId, ownerId);
            var result = Mediator.Send(query).Result;
            if (result == null)
            {
                return NotFound($"Product with ID: {productId} for owner ID: {ownerId} not found.");
            }
            return Ok(result);
        }



        [HttpPost("AddProduct")]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductDTO dto)
        {
            var command = new CreateProductCommand(dto);
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        [HttpPut("UpdateProduct/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateProductDTO productFromRequest)
        {
            if (productFromRequest == null)
            {
                return BadRequest("Product data is required.");
            }
            var command = new UpdateProductCommand(id, productFromRequest);
            var result = await Mediator.Send(command);
            if (!result)
            {
                return NotFound($"Product with ID: {id} not found or you are not the owner.");
            }
            return Ok("Product updated successfully.");
        }

        [HttpDelete("DeleteProduct/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await Mediator.Send(new DeleteProductCommand(id));
            if(!result)
            {
                return NotFound($"Product with ID: {id} not found.");
            }
            return Ok("Product deleted successfully.");
        }

        [HttpGet("GetProductSizes/{productId}")]
        public async Task<IActionResult> GetProductSizes(int productId)
        {
            var query = new Application.Commands_Queries.Product_Size.Queries.GetProductSizesQuery(productId);
            var result = await Mediator.Send(query);
            if (result == null || !result.Any())
            {
                return NotFound($"No sizes found for product ID: {productId}");
            }
            return Ok(result);
        }

        [HttpPost("CreateProductSize")]
        public async Task<IActionResult> CreateProductSize([FromBody] ProducSizesDTO producSizes)
        {
            var command = new CreateProductSizeCommand(producSizes);
            if (command == null)
            {
                return BadRequest("Product size data is required.");
            }
            var result = await Mediator.Send(command);
            if (result == 0)
            {
                return BadRequest("Failed to create product size. Product not found or invalid data.");
            }
            return Ok($"Product size created successfully with ID: {result}");
        }

        [HttpPut("UpdateSize/{Id}")]
        public async Task<bool> UpdateProductSize(int Id , UpdateProductSizeDTO productSizeDTO)
        {
            var command = new UpdateProductSizeCommand(Id, productSizeDTO);
            if (command == null)
            {
                return false;
            }
            var result = await Mediator.Send(command);
            if (!result)
            {
                return false;
            }
            return true;

        }

    }
}
