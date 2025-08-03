using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using MediatR;

namespace Application.Commands_Queries.Product_Size.Commands.Update
{
    public class UpdateProductSizeHandler : IRequestHandler<UpdateProductSizeCommand, bool>
    {
        public UpdateProductSizeHandler(IRepository<Domain.Entities.Product> context)
        {
            Context = context;
        }

        public IRepository<Domain.Entities.Product> Context { get; }

        public async Task<bool> Handle(UpdateProductSizeCommand request, CancellationToken cancellationToken)
        {
            var existingProductSize = await Context.GetProductSizeById(request.Id);
            if (existingProductSize == null)
                return await Task.FromResult(false);
            existingProductSize.Size = request.ProductSize.Size;
            existingProductSize.Price = request.ProductSize.Price;
            existingProductSize.Quantity = request.ProductSize.Quantity;
            var result = await Context.UpdateProductSize(existingProductSize);
            if (!result)
                return await Task.FromResult(false);
            await Context.SaveChanges();
            return await Task.FromResult(true);

        }
    }
}
