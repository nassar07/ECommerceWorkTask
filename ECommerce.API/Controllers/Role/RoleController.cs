using Application.Commands_Queries.Roles.Commands.Add;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers.Role
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        public RoleController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public IMediator _mediator { get; }


        [HttpGet("GetListOfRoles")]
        public async Task<IActionResult> GetAll()
        {
            var query = new Application.Commands_Queries.Roles.Queries.GetAllRolesQuery();
            var result = await _mediator.Send(query);
            if (result == null || !result.Any())
            {
                return NotFound("No roles found.");
            }
            return Ok(result);
        }

        [HttpPost("AddRole")]
        public async Task<IActionResult> AddRole([FromBody] string roleName)
        {
            var result = await _mediator.Send(new CreateRoleCommand(roleName));
            if (result)
                return Ok("Role created successfully");
            return BadRequest("Role already exists or failed to create");
        }




    }
}
