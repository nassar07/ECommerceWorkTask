using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.DTO.Product;
using MediatR;

namespace Application.Product.Queries.GetAll
{
    public class GetProductListHandler : IRequestHandler<GetAllProductsQuery, List<ProductDTO>>
    {
        public GetProductListHandler(IRepository<Domain.Entities.Product> context)
        {
            _context = context;
        }

        public IRepository<Domain.Entities.Product> _context { get; }

        public async Task<List<ProductDTO>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            var products = await _context.GetAll();

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
                    Price = s.Price,
                    Quantity = s.Quantity
                }).ToList()
            }).ToList();

            return await Task.FromResult(result);
        }
    }
}
