using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Infrastructure.Presistence;

namespace Infrastructure.Repository
{
    /*
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ECommerceDbContext _context;

        public Repository(ECommerceDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public T DeleteById(int id)
        {
            var entity = _context.Set<T>().Find(id);
            if (entity == null)
            {
                throw new ArgumentException($"Entity with id {id} not found.");
            }
            _context.Remove(entity);
            return entity;
        }

        public T GetByOwnerId(string ownerId)
        {
            var entity = _context.Set<T>().Find(ownerId);
            if (entity == null)
            {
                throw new ArgumentException($"Entity with owner id {ownerId} not found.");
            }
            return entity;
        }

        public List<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }

        public T GetById(int id)
        {
            var entity = _context.Set<T>().Find(id);
            if (entity == null)
            {
                throw new ArgumentException($"Entity with id {id} not found.");
            }
            return entity;
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }

        public T Update(T entity)
        {
            _context.Update(entity);
            return entity;
        }

        public void Add(T entity)
        {
            _context.Add(entity);
            
        }
    }
    */
}
