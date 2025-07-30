﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Application.Commands_Queries.Roles.Commands.Add
{
    public record CreateRoleCommand(string RoleName) : IRequest<bool>;
}
