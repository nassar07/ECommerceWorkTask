using System.Threading.Tasks;
using Application.DTO;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        public UserManager<ApplicationUser> UserManager { get; }
        public AccountController(UserManager<ApplicationUser> userManager)
        {
            UserManager = userManager;
        }

        
        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterDTO userFromBody)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = new();

                user.FirstName = userFromBody.FirstName;
                user.LastName = userFromBody.LastName;
                user.UserName = userFromBody.EmailAddress;
                user.Email = userFromBody.EmailAddress;


                IdentityResult result = await UserManager.CreateAsync(user, userFromBody.Password);

                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("Password", error.Description);
                    }
                    return BadRequest(ModelState);
                }

                if (userFromBody.AccountType == "Customer" || userFromBody.AccountType == "ProductOwner")
                {
                    await UserManager.AddToRoleAsync(user, userFromBody.AccountType);
                }


                return Ok("Created");

            }
            return BadRequest(ModelState);
        }
        






    }
}
