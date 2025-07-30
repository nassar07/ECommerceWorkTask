using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using MediatR;

namespace Application.Product.Commands.Delete
{
    public class DeleteProductHandler : IRequestHandler<DeleteProductCommand , bool>
    {
        private readonly IRepository<Domain.Entities.Product> _context;

        public DeleteProductHandler(IRepository<Domain.Entities.Product> context)
        {
            _context = context;
        }

        public async Task<bool> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _context.GetById(request.ProductId);
            if (product is null)
                return false;

            await _context.Delete(product);
            await _context.SaveChanges();

            return true;
        }
    }
}
