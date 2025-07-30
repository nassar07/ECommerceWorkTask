using Application.DTO.Category;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ECommerce.API.Controllers.Category
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        public IMediator Mediator { get; }
        public CategoryController(IMediator mediator)
        {
            Mediator = mediator;
        }



        [HttpGet("GetAll")]
        public IActionResult GetAllCategories()
        {
            var query = new Application.Category.Queries.GetAll.GetAllCategoriesQuery();
            var result = Mediator.Send(query).Result;
            if (result == null || !result.Any())
            {
                return NotFound("No categories found.");
            }
            return Ok(result);

        }

        
        [HttpGet("GetCategoryById/{id}")]
        public IActionResult GetCategoryById(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Category ID must be greater than zero.");
            }
            var query = new Application.Category.Queries.GetById.GetCategoryByIdQuery(id);
            var result = Mediator.Send(query).Result;
            if (result == null)
            {
                return NotFound($"Category with ID {id} not found.");
            }
            return Ok(result);

        }

        [HttpGet("GetProductsByCategoryId/{id}")]
        public IActionResult GetProductsByCategoryId(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Category ID must be greater than zero.");
            }
            var query = new Application.Category.Queries.GetProductsByCategory.GetProductsByCategoryIdQuery(id);
            var result = Mediator.Send(query).Result;
            if (result == null)
            {
                return NotFound($"Category with ID {id} not found.");
            }
            return Ok(result);

        }

        
        [HttpPost("AddCtegory")]
        public IActionResult Post([FromBody]CreateCategoryDTO category)
        {
            if (category == null)
            {
                return BadRequest("Category cannot be null.");
            }
            var command = new Application.Commands_Queries.Category.Commands.Create.CreateCategoryCommand(category);
            var result = Mediator.Send(command).Result;
            if (!result)
            {
                return BadRequest("Failed to create category.");
            }
            return Ok("Category created successfully.");

        }

        // PUT api/<CategoryController>/5
        [HttpPut("UpdateCategory/{id}")]
        public IActionResult Update(int id, [FromBody]UpdateCategoryDTO category)
        {
            if (id <= 0)
            {
                return BadRequest("Category ID must be greater than zero.");
            }
            if (category == null)
            {
                return BadRequest("Category cannot be null.");
            }
            var command = new Application.Commands_Queries.Category.Commands.Update.UpdateCategoryCommand(id, category);
            var result = Mediator.Send(command).Result;
            if (result == null)
            {
                return NotFound($"Category with ID {id} not found.");
            }
            return Ok(result);

        }

        // DELETE api/<CategoryController>/5
        [HttpDelete("DeleteCategory/{id}")]
        public IActionResult Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Category ID must be greater than zero.");
            }
            var command = new Application.Commands_Queries.Category.Commands.Delete.DeleteCategoryCommand(id);
            var result = Mediator.Send(command).Result;
            if (!result)
            {
                return NotFound($"Category with ID {id} not found.");
            }
            return Ok($"Category with ID {id} deleted successfully.");
        }
    }
}
