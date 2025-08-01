﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.DTO.Product;
using Domain.Entities;
using MediatR;

namespace Application.Product.Commands.Update
{
    public class UpdateProductHandler : IRequestHandler<UpdateProductCommand, bool>
    {
        private readonly IRepository<Domain.Entities.Product> _context;

        public UpdateProductHandler(IRepository<Domain.Entities.Product> context)
        {
            _context = context;
        }

        //public async Task<ProductDTO> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        //{
        //    var product = await _context.GetByIdAndOwner(request.ProductId, request.OwnerId);
        //    if (product is null)
        //        throw new KeyNotFoundException("Product not found or you are not the owner");

        //    // Update product fields
        //    product.Title = request.Product.Title;
        //    product.Description = request.Product.Description;
        //    product.ImageUrl = request.Product.ImageUrl;
        //    product.CategoryId = request.Product.CategoryId;

        //    // Replace Sizes
        //    product.Sizes.Clear();
        //    foreach (var size in request.Product.Sizes)
        //    {
        //        product.Sizes.Add(new ProductSize
        //        {
        //            Size = size.Size,
        //            Price = size.Price,
        //            Quantity = size.Quantity
        //        });
        //    }

        //    await _context.Update(product);
        //    await _context.SaveChanges();

        //    // Return updated ProductDTO
        //    return new ProductDTO
        //    {
        //        Id = product.Id,
        //        Title = product.Title,
        //        Description = product.Description,
        //        ImageUrl = product.ImageUrl,
        //        CategoryId = product.CategoryId,
        //        OwnerId = product.OwnerId,
        //        Sizes = product.Sizes.Select(s => new ProductSizeDTO
        //        {
        //            Size = s.Size,
        //            Price = s.Price,
        //            Quantity = s.Quantity
                    
        //        }).ToList()
        //    };
        //}

        async Task<bool> IRequestHandler<UpdateProductCommand, bool>.Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var existingProduct = await _context.GetById(request.ProductId);
            if (existingProduct == null)
                return false;
            existingProduct.Title = request.Product.Title;
            existingProduct.Description = request.Product.Description;
            existingProduct.ImageUrl = request.Product.ImageUrl;
            existingProduct.CategoryId = request.Product.CategoryId;
            existingProduct.Sizes.Clear();
            foreach (var size in request.Product.Sizes)
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

