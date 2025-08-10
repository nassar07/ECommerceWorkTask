using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Application.DTO.Login;
using Application.DTO.Register;
using Application.DTO.Token;
using Application.Features.Auth.Login;
using Application.Features.Auth.Register;
using Infrastructure.Identity;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace ECommerce.API.Controllers.Account
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        public IMediator Mediator { get; }

        public AccountController(IMediator mediator, IConfiguration Config)
        {
            Mediator = mediator;
        }

        
        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterDTO regFromBody)
        {
            var command = new RegisterCommand(regFromBody);
            var result = await Mediator.Send(command);

            if (!result.Success)
                return BadRequest(result.Errors);

            return Ok(new {Message = "Created"}); 
        }

        
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDTO userFromReq)
        {
            var command = new LoginCommand(userFromReq);
            var result = await Mediator.Send(command);

            if (!result.Success)
                return BadRequest(result.Errors);

            return Ok(result.Token);
        }

        






    }
}
