using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.DTO.Product;
using MediatR;

namespace Application.Product.Queries.GetByOwnerIdAndProductId
{
    public class GetProductByIdAndOwnerHandler : IRequestHandler<GetProductByIdAndOwnerQuery, ProductDTO>
    {
        private readonly IRepository<Domain.Entities.Product> _context;

        public GetProductByIdAndOwnerHandler(IRepository<Domain.Entities.Product> context)
        {
            _context = context;
        }

        public async Task<ProductDTO> Handle(GetProductByIdAndOwnerQuery request, CancellationToken cancellationToken)
        {
            var product = await _context.GetByIdAndOwner(request.ProductId, request.OwnerId);

            if (product == null)
                throw new KeyNotFoundException("Product not found or access denied.");

            return new ProductDTO
            {
                Id = product.Id,
                Title = product.Title,
                Description = product.Description,
                ImageUrl = product.ImageUrl,
                CategoryId = product.CategoryId,
                OwnerId = product.OwnerId,
                Sizes = product.Sizes.Select(s => new ProductSizeDTO
                {
                    Size = s.Size,
                    Price = s.Price,
                    Quantity = s.Quantity
                }).ToList()
            };
        }
    }

}
