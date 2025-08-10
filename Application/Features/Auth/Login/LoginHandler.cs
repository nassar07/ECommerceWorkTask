using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Application.DTO.Login;
using Application.DTO.Token;
using Infrastructure.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;

namespace Application.Features.Auth.Login
{
    public class LoginHandler : IRequestHandler<LoginCommand, LoginResult>
    {
        public LoginHandler(UserManager<ApplicationUser> userManager, IConfiguration Config)
        {
            UserManager = userManager;
            this.Config = Config;
        }

        public UserManager<ApplicationUser> UserManager { get; }
        public IConfiguration Config { get; }

        public async Task<LoginResult> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var UserFromDb = await UserManager.FindByEmailAsync(request.loginFromRequest.Email);
            if (UserFromDb == null)
            {
                var error = "Invalid Email";
                return new LoginResult()
                {

                    Success = false,
                    Errors = new List<string>() { error }
                };
            }


            bool PasswordCheck = await UserManager.CheckPasswordAsync(UserFromDb, request.loginFromRequest.Password);
            if (!PasswordCheck)
            {
                var error = "Invalid Password";
                return new LoginResult()
                {
                    Success = false,
                    Errors = new List<string>() { error }
                };
            }


            if (!string.IsNullOrEmpty(request.loginFromRequest.FcmToken))
            {
                UserFromDb.FcmToken = request.loginFromRequest.FcmToken;
                await UserManager.UpdateAsync(UserFromDb);
            }


            var UserRoles = await UserManager.GetRolesAsync(UserFromDb);


            List<Claim> MyClaims = new()
            {
                new Claim(ClaimTypes.NameIdentifier, UserFromDb.Id),
                new Claim(ClaimTypes.Name, UserFromDb.FirstName + " " + UserFromDb.LastName),
                new Claim(ClaimTypes.Email, UserFromDb.Email),
                new Claim(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };


            foreach (var role in UserRoles)
            {
                MyClaims.Add(new Claim(ClaimTypes.Role, role));
            }


            var signKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Config["JWT:Secret"]));
            var SignCred = new SigningCredentials(signKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: Config["JWT:issuer"],
                audience: Config["JWT:audience"],
                claims: MyClaims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: SignCred);



            var MyToken = new MyTokenDTO
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = DateTime.Now.AddHours(1)
            };

            return new LoginResult
            { Success = true, Token = MyToken };

        }
    }
}
