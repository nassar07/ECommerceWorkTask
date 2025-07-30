using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTO.Role;
using MediatR;

namespace Application.Commands_Queries.Roles.Queries
{
    public record GetAllRolesQuery : IRequest<List<RoleDTO>>;
    
}
