using EFCoreGenericRepositoryPattern.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EFCoreGenericRepositoryPattern.Data.Repositories.Interfaces
{
    public interface IRepository<T> where T : class, IEntity
    {
        Task<IEnumerable<T>> GetAllAsync();
        ValueTask<T> GetByIdAsync(int id);
        Task AddAsync(T entity);
        void Delete(T entity);
    }
}
