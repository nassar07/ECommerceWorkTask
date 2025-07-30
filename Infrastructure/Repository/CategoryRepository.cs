using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.DTO.Category;
using Application.DTO.Product;
using Domain.Entities;
using Infrastructure.Presistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class CategoryRepository : ICategoryRepository<Category>
    {
        public CategoryRepository(ECommerceDbContext context , IRepository<Product> product)
        {
            Context = context;
            Product = product;
        }
        public ECommerceDbContext Context { get; }
        public IRepository<Product> Product { get; }

        public async Task CreateCategoryAsync(Category entity)
        {
            await Context.Set<Category>().AddAsync(entity);
        }

        public async Task<bool> DeleteCategoryAsync(int id)
        {
            var category = await Context.Set<Category>().FindAsync(id);
            if (category == null)
                return false;
                
            Context.Set<Category>().Remove(category);
            return true;
        }

        public async Task<List<CategoryDTO>> GetAllCategoriesAsync()
        {
            var categories = await Context.Set<Category>().ToListAsync();
            if (categories == null || !categories.Any())
                throw new InvalidOperationException("No categories found.");
            return categories.Select(c => new CategoryDTO
            {
                Id = c.Id,
                Name = c.Name
            }).ToList();
        }

        public async Task<Category> GetCategoryByIdAsync(int id)
        {
            var category = await Context.Set<Category>().FindAsync(id);
            if (category == null)
                return null;
            return category;
            
        }

        public async Task<List<Product>> GetProductsByCategoryIdAsync(int categoryId)
        {
            var products = await Product.GetAllProductsByCategoryId(categoryId);
            if (products == null)
            {
                return null;
            }
            return products;
        }

        public async Task<Category> UpdateCategoryAsync(int id , Category entity)
        {
            Context.Set<Category>().Update(entity);
            return await Task.FromResult(entity);
        }

        public Task SaveChangesAsync()
        {
            return Context.SaveChangesAsync();
        }

        //Task<CategoryDTO> ICategoryRepository<Category>.GetCategoryByIdAsync(int id)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
