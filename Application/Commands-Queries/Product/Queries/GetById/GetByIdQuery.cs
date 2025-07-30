using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTO.Product;
using MediatR;

namespace Application.Commands_Queries.Product.Queries.GetById
{
    public record GetByIdQuery(int Id) : IRequest<ProductDTO>;

}
