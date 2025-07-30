using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Commands_Queries.Product_Size.Commands
{
    public class CreateProductSizeCommandHandler : IRequestHandler<CreateProductSizeCommand, int>
    {
        

        public CreateProductSizeCommandHandler(IRepository<Domain.Entities.Product> context)
        {
            _context = context;
            
        }

        public IRepository<Domain.Entities.Product> _context { get; }

        public async Task<int> Handle(CreateProductSizeCommand request, CancellationToken cancellationToken)
        {
            var product = await _context.GetById(request.producSizes.ProductId);

            if (product == null)
                return 0;

            var size = new ProductSize
            {
                ProductId = request.producSizes.ProductId,
                Size = request.producSizes.Size,
                Price = request.producSizes.Price,
                Quantity = request.producSizes.Quantity
            };

            await _context.AddProductSize(size);
            await _context.SaveChanges();

            return size.Id;
        }
    }
    
}
