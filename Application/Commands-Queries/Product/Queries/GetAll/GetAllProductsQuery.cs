using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Domain.Entities;
using Application.DTO.Product;

namespace Application.Product.Queries.GetAll
{
    public record GetAllProductsQuery : IRequest<List<ProductDTO>>; 
    
}
