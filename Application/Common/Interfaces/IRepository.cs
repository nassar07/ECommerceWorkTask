using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTO.Product;
using Domain.Entities;

namespace Application.Common.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<T?> GetById(int id);
        Task<List<T>> GetByOwnerId(string ownerId);
        Task<List<T>> GetAll();
        Task<T> Update(T entity);
        Task Add(T entity);
        Task<T> DeleteById(int id);
        Task SaveChanges();
        Task Delete(T entity);
        Task<T?> GetByIdAndOwner(int id, string ownerId);
        Task<List<ProducSizesDTO>> GetProductSizes(int productId);
        Task AddProductSize(ProductSize entity);

        Task<List<T>> GetAllProductsByCategoryId(int id);
    }
}
