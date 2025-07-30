using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTO.Category;
using MediatR;

namespace Application.Commands_Queries.Category.Commands.Update
{
    public record UpdateCategoryCommand(int Id , UpdateCategoryDTO Category) : IRequest<CategoryDTO>;

}
