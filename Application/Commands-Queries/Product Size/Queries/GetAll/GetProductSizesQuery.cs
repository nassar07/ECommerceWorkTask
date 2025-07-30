using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTO.Product;
using MediatR;

namespace Application.Commands_Queries.Product_Size.Queries
{
    public class GetProductSizesQuery : IRequest<List<ProducSizesDTO>>
    {
        public int ProductId { get; set; }

        public GetProductSizesQuery(int productId)
        {
            ProductId = productId;
        }
    }

}
