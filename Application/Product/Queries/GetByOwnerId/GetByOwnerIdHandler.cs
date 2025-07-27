using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.DTO.Product;
using MediatR;

namespace Application.Product.Queries.GetByOwnerId
{
    class GetByOwnerIdHandler : IRequestHandler<GetByOwnerIdQuery, List<ProductDTO>>
    {
        public GetByOwnerIdHandler(IRepository<Domain.Entities.Product> Context)
        {
            _context = Context;
        }

        public IRepository<Domain.Entities.Product> _context { get; }

        public async Task<List<ProductDTO>> Handle(GetByOwnerIdQuery request, CancellationToken cancellationToken)
        {
            var products = await _context.GetByOwnerId(request.ownerId);

            var result = products.Select(p => new ProductDTO
            {
                Id = p.Id,
                Title = p.Title,
                Description = p.Description,
                ImageUrl = p.ImageUrl,
                CategoryId = p.CategoryId,
                OwnerId = p.OwnerId,
                Sizes = p.Sizes.Select(s => new ProductSizeDTO
                {
                    Size = s.Size,
                    Price = s.Price
                }).ToList()
            }).ToList();

            return await Task.FromResult(result);
        }
    }
}
