using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using MediatR;

namespace Application.Product.Commands.Delete
{
    public class DeleteProductHandler : IRequestHandler<DeleteProductCommand , Unit>
    {
        private readonly IRepository<Domain.Entities.Product> _context;

        public DeleteProductHandler(IRepository<Domain.Entities.Product> context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _context.GetByIdAndOwner(request.ProductId, request.OwnerId);
            if (product is null)
                throw new KeyNotFoundException("Product not found or you are not the owner");

            await _context.Delete(product);
            await _context.SaveChanges();

            return Unit.Value;
        }
    }
}
