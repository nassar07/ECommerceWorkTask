using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTO.Login;
using MediatR;

namespace Application.Features.Auth.Login
{
    public record LoginCommand(LoginDTO loginFromRequest) : IRequest<LoginResult>; 
    
}
