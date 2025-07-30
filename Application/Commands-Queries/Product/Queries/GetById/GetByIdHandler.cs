using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.DTO.Product;
using MediatR;

namespace Application.Commands_Queries.Product.Queries.GetById
{
    public class GetByIdHandler : IRequestHandler<GetByIdQuery, ProductDTO>
    {
        public GetByIdHandler(IRepository<Domain.Entities.Product> context)
        {
            Context = context;
        }

        public IRepository<Domain.Entities.Product> Context { get; }

        public async Task<ProductDTO> Handle(GetByIdQuery request, CancellationToken cancellationToken)
        {
            var product = await Context.GetById(request.Id);
            if (product == null)
            {
                return null;
            }
            var result = new ProductDTO
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
            return await Task.FromResult(result);
        }
    }
}
