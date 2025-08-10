using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTO.Register;
using Infrastructure.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Application.Features.Auth.Register
{
    public class RegisterHandler : IRequestHandler<RegisterCommand, RegisterResult>
    {
        public RegisterHandler(UserManager<ApplicationUser> userManager)
        {
            UserManager = userManager;
        }

        public UserManager<ApplicationUser> UserManager { get; }

        public async Task<RegisterResult> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            ApplicationUser user = new();
            user.FirstName = request.RegisterFromRequest.FirstName;
            user.LastName = request.RegisterFromRequest.LastName;
            user.UserName = request.RegisterFromRequest.EmailAddress;
            user.Email = request.RegisterFromRequest.EmailAddress;

            IdentityResult result = await UserManager.CreateAsync(user, request.RegisterFromRequest.Password);

            if (!result.Succeeded)
            {
                return new RegisterResult()
                {
                    Success = false,
                    Errors = result.Errors.Select(e => e.Description).ToList()
                };
            }

            await UserManager.AddToRoleAsync(user, request.RegisterFromRequest.AccountType);

            return new RegisterResult()
            {
                Success = true
            };

        }
    }

        
}

