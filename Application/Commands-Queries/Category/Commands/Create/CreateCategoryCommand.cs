using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTO.Category;
using Domain.Entities;
using MediatR;

namespace Application.Commands_Queries.Category.Commands.Create
{
    public record CreateCategoryCommand(CreateCategoryDTO category) : IRequest<bool>;
    
}
