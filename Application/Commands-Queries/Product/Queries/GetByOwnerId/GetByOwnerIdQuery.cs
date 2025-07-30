using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTO.Product;
using MediatR;

namespace Application.Product.Queries.GetByOwnerId
{
    public record GetByOwnerIdQuery(string ownerId) : IRequest<List<ProductDTO>>;

}
