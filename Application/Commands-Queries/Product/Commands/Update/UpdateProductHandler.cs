using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.DTO.Product;
using Domain.Entities;
using MediatR;
using Newtonsoft.Json;

namespace Application.Product.Commands.Update
{
    public class UpdateProductHandler : IRequestHandler<UpdateProductCommand, bool>
    {
        private readonly IRepository<Domain.Entities.Product> _context;
        private readonly IFileService _fileService;

        public UpdateProductHandler(IRepository<Domain.Entities.Product> context, IFileService fileService)
        {
            _context = context;
            _fileService = fileService;
        }

        public async Task<bool> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var existingProduct = await _context.GetById(request.Product.ProductId);
            if (existingProduct == null)
                return false;

            existingProduct.Title = request.Product.Title;
            existingProduct.Description = request.Product.Description;
            existingProduct.CategoryId = request.Product.CategoryId;

            if (request.Product.ImageFile != null)
            {
                var imageUrl = await _fileService.SaveImageAsync(request.Product.ImageFile);
                existingProduct.ImageUrl = imageUrl;
            }

            // Parse SizesJson
            var sizes = JsonConvert.DeserializeObject<List<UpdateProductSizeDTO>>(request.Product.SizesJson);

            existingProduct.Sizes.Clear();
            foreach (var size in sizes)
            {
                existingProduct.Sizes.Add(new ProductSize
                {
                    Size = size.Size,
                    Price = size.Price,
                    Quantity = size.Quantity
                });
            }

            await _context.Update(existingProduct);
            await _context.SaveChanges();

            return true;
        }
    }

}

