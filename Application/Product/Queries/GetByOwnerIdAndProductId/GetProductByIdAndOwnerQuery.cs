using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTO.Product;
using MediatR;

namespace Application.Product.Queries.GetByOwnerIdAndProductId
{
    public record GetProductByIdAndOwnerQuery(int ProductId, string OwnerId) : IRequest<ProductDTO>;

}
