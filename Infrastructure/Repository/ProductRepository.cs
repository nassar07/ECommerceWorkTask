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

        public Task<Product> GetById(int id)
        {
            throw new NotImplementedException();
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
