using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.DTO.Product;
using Domain.Entities;
using Infrastructure.Presistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class ProductRepository : IRepository<Product>
    {
        public ProductRepository(ECommerceDbContext context)
        {
            Context = context;
        }

        public ECommerceDbContext Context { get; }

        public async Task Add(Product entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "Product cannot be null");

            await Context.Set<Product>().AddAsync(entity);
        }

        public async Task AddProductSize(ProductSize entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "ProductSize cannot be null");
            
            await Context.Set<ProductSize>().AddAsync(entity);
        }

        public async Task Delete(Product entity)
        {
            Context.Set<Product>().Remove(entity);
            await Task.CompletedTask;
        }

        public Task<Product> DeleteById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Product>> GetAll()
        {
            return await Context.Set<Product>()
                .Include(p => p.Sizes)
                .ToListAsync();
        }

        public async Task<List<Product>> GetAllProductsByCategoryId(int id)
        {
            return await Context.Set<Product>()
                .Include(p => p.Sizes).Where(p => p.CategoryId == id).ToListAsync();
                
        }



        public async Task<Product?> GetById(int id)
        {
            return await Context.Set<Product>()
                .Include(p => p.Sizes)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Product?> GetByIdAndOwner(int id, string ownerId)
        {
            return await Context.Set<Product>()
                .Include(p => p.Sizes)
                .FirstOrDefaultAsync(p => p.Id == id && p.OwnerId == ownerId);
        }

        public async Task<List<Product>> GetByOwnerId(string ownerId)
        {
            return await Context.Set<Product>()
                .Include(p => p.Sizes)
                .Where(p => p.OwnerId == ownerId)
                .ToListAsync();
        }

        public async Task<List<ProducSizesDTO>> GetProductSizes(int productId)
        {
            return await Context.Set<ProductSize>()
                .Where(ps => ps.ProductId == productId)
                .Select(ps => new ProducSizesDTO
                {
                    ProductId = ps.ProductId,
                    Size = ps.Size,
                    Price = ps.Price,
                    Quantity = ps.Quantity
                }).ToListAsync();
        }

        public Task SaveChanges()
        {
            return Context.SaveChangesAsync();
        }

        public async Task<Product> Update(Product entity)
        {
            Context.Set<Product>().Update(entity);
            return await Task.FromResult(entity);
        }
    }
}
