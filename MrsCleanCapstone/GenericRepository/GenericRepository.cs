﻿using Microsoft.EntityFrameworkCore;
using MrsCleanCapstone.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MrsCleanCapstone.GenericRepository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        #region Fields

        protected ApplicationDbContext Context;

        #endregion

        public GenericRepository(ApplicationDbContext context)
        {
            Context = context;
        }

        #region Public Methods

        public Task<T> GetById(int id) => Context.Set<T>().FindAsync(id).AsTask();

        public Task<T> FirstOrDefault(Expression<Func<T, bool>> predicate)
            => Context.Set<T>().FirstOrDefaultAsync(predicate);

        public async Task Add(T entity)
        {
            // await Context.AddAsync(entity);
            await Context.Set<T>().AddAsync(entity);
            await Context.SaveChangesAsync();
        }

        public Task Update(T entity)
        {
            // In case AsNoTracking is used
            Context.Entry(entity).State = EntityState.Modified;
            return Context.SaveChangesAsync();
        }

        public Task Remove(T entity)
        {
            Context.Set<T>().Remove(entity);
            return Context.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await Context.Set<T>().ToListAsync();
        }

        public async Task<IEnumerable<T>> GetWhere(Expression<Func<T, bool>> predicate)
        {
            return await Context.Set<T>().Where(predicate).ToListAsync();
        }

        public Task<int> CountAll() => Context.Set<T>().CountAsync();

        public Task<int> CountWhere(Expression<Func<T, bool>> predicate)
            => Context.Set<T>().CountAsync(predicate);

        public bool GetExists(Expression<Func<T, bool>> predicate)
        {
            return Context.Set<T>().Any(predicate);
        }

        #endregion

    }
}
