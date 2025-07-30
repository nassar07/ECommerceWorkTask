using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTO.Role;
using Microsoft.AspNetCore.Identity;
using MediatR;

namespace Application.Commands_Queries.Roles.Queries
{
    public class GetAllRolesHandler : IRequestHandler<GetAllRolesQuery, List<RoleDTO>>
    {
        public GetAllRolesHandler(RoleManager<IdentityRole> context)
        {
            Context = context;
        }

        public RoleManager<IdentityRole> Context { get; }

        public async Task<List<RoleDTO>> Handle(GetAllRolesQuery request, CancellationToken cancellationToken)
        {
            var roles = Context.Roles.ToList();
            if (roles == null || !roles.Any())
            {
                return new List<RoleDTO>();
            }
            var roleDtos = roles.Select(role => new RoleDTO
            {
                 Id = role.Id,
                 RoleName = role.Name
            }).ToList();
            return await Task.FromResult(roleDtos);



        }
    }
}
