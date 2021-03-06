﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Groger.DAL.Repositories
{
    /// <summary>
    /// Generic Repository
    /// </summary>
    /// <typeparam name="TEntity">The entity of this repository</typeparam>
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "");

        TEntity GetByID(object id);

        IQueryable<TEntity> GetQueryable();

        void Insert(TEntity entity);

        void Delete(object ID);

        void Delete(TEntity entityToDelete);

        void Update(TEntity entityToUpdate);
    }
}
