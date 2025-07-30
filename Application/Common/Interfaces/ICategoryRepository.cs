using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTO.Category;
using Application.DTO.Product;
using Domain.Entities;

namespace Application.Common.Interfaces
{
    public interface ICategoryRepository<T> where T : class
    {
        Task<List<CategoryDTO>> GetAllCategoriesAsync();
        Task<T> GetCategoryByIdAsync(int id);
        Task CreateCategoryAsync(T entity);
        Task<T> UpdateCategoryAsync(int id , T entity);
        Task<bool> DeleteCategoryAsync(int id);
        Task<List<Domain.Entities.Product>> GetProductsByCategoryIdAsync(int categoryId);
        Task SaveChangesAsync();

    }
}
