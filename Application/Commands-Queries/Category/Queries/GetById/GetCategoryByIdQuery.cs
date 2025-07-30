using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTO.Category;
using MediatR;

namespace Application.Category.Queries.GetById
{
    public record GetCategoryByIdQuery(int id) : IRequest<CategoryDTO>;
 
}
