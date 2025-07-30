using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTO.Product;
using MediatR;

namespace Application.Category.Queries.GetProductsByCategory
{
    public record GetProductsByCategoryIdQuery(int Id) : IRequest<List<ProductDTO>>;
    
}
