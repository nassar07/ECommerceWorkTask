using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTO.Register;
using MediatR;

namespace Application.Features.Auth.Register
{
    public record RegisterCommand(RegisterDTO RegisterFromRequest) : IRequest<RegisterResult>;
    
}
