using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.DTO.Product;
using Domain.Entities;
using MediatR;

namespace Application.Commands_Queries.Product_Size.Queries
{
    public class GetProductSizesQueryHandler : IRequestHandler<GetProductSizesQuery, List<ProducSizesDTO>>
    {
        private readonly IRepository<Domain.Entities.Product> _context;

        public GetProductSizesQueryHandler(IRepository<Domain.Entities.Product> context)
        {
            _context = context;
        }

        public async Task<List<ProducSizesDTO>> Handle(GetProductSizesQuery request, CancellationToken cancellationToken)
        {
            return await _context.GetProductSizes(request.ProductId);
        }
    }

}
