using EFCoreGenericRepositoryPattern.Data.Repositories.Interfaces;
using EFCoreGenericRepositoryPattern.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFCoreGenericRepositoryPattern.Data.Repositories
{
    public class Repository<T> : IRepository<T> where T : class, IEntity
    {
        protected readonly ApiContext dbContext;
        public Repository(ApiContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task AddAsync(T entity)
        {
            await dbContext.Set<T>().AddAsync(entity);
        }

        public void Delete(T entity)
        {
            dbContext.Set<T>().Remove(entity);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await dbContext.Set<T>().ToListAsync();
        }

        public ValueTask<T> GetByIdAsync(int id)
        {
            return dbContext.Set<T>().FindAsync(id);
        }
    }
}
