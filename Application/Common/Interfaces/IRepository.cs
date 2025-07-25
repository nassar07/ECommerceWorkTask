using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Application.Common.Interfaces
{
    public interface IRepository<T> where T : class
    {
        public T GetById(int id);
        public IEnumerable<T> GetAll();
        public T Update(T entity);
        public T DeleteById(int id);
        public Task SaveChanges();


    }
}
