using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTO.Category;
using MediatR;

namespace Application.Category.Queries.GetAll
{
    public record GetAllCategoriesQuery : IRequest<List<CategoryDTO>>;
}
