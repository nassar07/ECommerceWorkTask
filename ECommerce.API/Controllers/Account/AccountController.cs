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

                if (userFromBody.AccountType == "Customer" || userFromBody.AccountType == "ProductOwner")
                {
                    await UserManager.AddToRoleAsync(user, userFromBody.AccountType);
                }


                return Ok("Created");

            }
            return BadRequest(ModelState);
        }


        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDTO userFromReq)
        {
            if (ModelState.IsValid)
            {
                var userFromDb = await UserManager.FindByEmailAsync(userFromReq.Email);

                if (userFromDb != null)
                {
                    
                    bool isValidPassword = await UserManager.CheckPasswordAsync(userFromDb, userFromReq.Password);
                    
                    
                    if (isValidPassword == true)
                    {

                        var UserRoles = await UserManager.GetRolesAsync(userFromDb);
                        List<Claim> myClaims = new List<Claim>();

                        myClaims.Add(new Claim(ClaimTypes.NameIdentifier, userFromDb.Id));
                        myClaims.Add(new Claim(ClaimTypes.Email, userFromDb.Email));
                        myClaims.Add(new Claim(ClaimTypes.Name, userFromDb.FirstName + " " + userFromDb.LastName));
                        myClaims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));


                        foreach (var role in UserRoles)
                        {
                            myClaims.Add(new Claim(ClaimTypes.Role, role));
                        }

                        SymmetricSecurityKey SignKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(Config["JWT:Secret"]));

                        SigningCredentials signingCred = new SigningCredentials(
                            SignKey, SecurityAlgorithms.HmacSha256);

                        JwtSecurityToken Token = new JwtSecurityToken(
                            issuer: Config["JWT:issuer"],
                            audience: Config["JWT:audience"],
                            expires: DateTime.Now.AddHours(1),
                            claims: myClaims,
                            signingCredentials: signingCred
                            );

                        MyTokenDTO myToken = new MyTokenDTO
                        { Token = new JwtSecurityTokenHandler().WriteToken(Token), Expiration = Token.ValidTo };

                        return Ok(myToken);

                        
                    }
                    ModelState.AddModelError("Password", "Invalid password");


                }
                
                ModelState.AddModelError("Email", "Invalid email");
                
            }
            return BadRequest(ModelState);
        }
        






    }
}
