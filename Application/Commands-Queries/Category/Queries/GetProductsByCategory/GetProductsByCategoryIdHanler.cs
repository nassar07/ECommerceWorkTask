using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.DTO.Product;
using MediatR;

namespace Application.Category.Queries.GetProductsByCategory
{
    public class GetProductsByCategoryIdHanler : IRequestHandler<GetProductsByCategoryIdQuery, List<ProductDTO>>
    {
        public GetProductsByCategoryIdHanler(ICategoryRepository<Domain.Entities.Category> context)
        {
            Context = context;
        }

        public ICategoryRepository<Domain.Entities.Category> Context { get; }

        public async Task<List<ProductDTO>> Handle(GetProductsByCategoryIdQuery request, CancellationToken cancellationToken)
        {
            var products = await Context.GetProductsByCategoryIdAsync(request.Id);
            if (products == null)
                return null;
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
