using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Application.DTO.Login;
using Application.DTO.Register;
using Application.DTO.Token;
using Infrastructure.Identity;
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
        public UserManager<ApplicationUser> UserManager { get; }
        public IConfiguration Config { get; }
        public AccountController(UserManager<ApplicationUser> userManager , IConfiguration Config)
        {
            UserManager = userManager;
            this.Config = Config;
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

                
                await UserManager.AddToRoleAsync(user, userFromBody.AccountType);
                


                return Ok(new
                {
                    message = "Created"
                });

            }
            return BadRequest(ModelState);
        }


        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDTO userFromReq)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userFromDb = await UserManager.FindByEmailAsync(userFromReq.Email);
            if (userFromDb == null)
            {
                ModelState.AddModelError("Email", "Invalid email");
                return BadRequest(ModelState);
            }

            bool isValidPassword = await UserManager.CheckPasswordAsync(userFromDb, userFromReq.Password);
            if (!isValidPassword)
            {
                ModelState.AddModelError("Password", "Invalid password");
                return BadRequest(ModelState);
            }

            
            if (!string.IsNullOrEmpty(userFromReq.FcmToken))
            {
                userFromDb.FcmToken = userFromReq.FcmToken;
                await UserManager.UpdateAsync(userFromDb);
            }

            var UserRoles = await UserManager.GetRolesAsync(userFromDb);
            List<Claim> myClaims = new()
    {
        new Claim(ClaimTypes.NameIdentifier, userFromDb.Id),
        new Claim(ClaimTypes.Email, userFromDb.Email),
        new Claim(ClaimTypes.Name, userFromDb.FirstName + " " + userFromDb.LastName),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
    };

            foreach (var role in UserRoles)
            {
                myClaims.Add(new Claim(ClaimTypes.Role, role));
            }

            var SignKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Config["JWT:Secret"]));
            var signingCred = new SigningCredentials(SignKey, SecurityAlgorithms.HmacSha256);

            var Token = new JwtSecurityToken(
                issuer: Config["JWT:issuer"],
                audience: Config["JWT:audience"],
                expires: DateTime.Now.AddHours(1),
                claims: myClaims,
                signingCredentials: signingCred
            );

            var myToken = new MyTokenDTO
            {
                Token = new JwtSecurityTokenHandler().WriteToken(Token),
                Expiration = Token.ValidTo
            };

            return Ok(myToken);
        }








    }
}
