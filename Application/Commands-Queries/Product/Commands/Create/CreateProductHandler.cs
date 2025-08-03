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

namespace Application.Product.Commands.Create
{
    public class CreateProductHandler : IRequestHandler<CreateProductCommand, ProductResponseDTO>
    {
        public CreateProductHandler(IRepository<Domain.Entities.Product> context , IFileService fileService)
        {
            _context = context;
            _fileService = fileService;
        }

        public IRepository<Domain.Entities.Product> _context { get; }
        public IFileService _fileService { get; }

        public async Task<ProductResponseDTO> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var imageUrl = await _fileService.SaveImageAsync(request.ProductFromRequest.Image); 
            var sizes = JsonConvert.DeserializeObject<List<ProductSizeForCreateDTO>>(request.ProductFromRequest.SizesJson);



            var product = new Domain.Entities.Product
            {
                Title = request.ProductFromRequest.Title,
                Description = request.ProductFromRequest.Description,
                ImageUrl = imageUrl,
                CategoryId = request.ProductFromRequest.CategoryId,
                OwnerId = request.ProductFromRequest.OwnerId,
                Sizes = sizes.Select(size => new ProductSize
                {
                    Size = size.Size,
                    Price = size.Price,
                    Quantity = size.Quantity
                }).ToList()
            };

            await _context.Add(product);
            await _context.SaveChanges();

            
            var response = new ProductResponseDTO
            {
                Id = product.Id,
                Title = product.Title,
                Description = product.Description,
                ImageUrl = product.ImageUrl,
                CategoryId = product.CategoryId,
                OwnerId = product.OwnerId,
                Sizes = product.Sizes.Select(s => new ProductSizeResponseDTO
                {
                    Id = s.Id,
                    Size = s.Size,
                    Price = s.Price,
                    Quantity = s.Quantity
                }).ToList()
            };

            return response;
        }
    }
}
