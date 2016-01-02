using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace Camarilla.RestApi.Infrastructure.Stores.Base
{
    public interface IStore
    {
    }

    public interface IStore<TEntity, in TKey>
        : IStore
        where TEntity : class
    {
        IQueryable<TEntity> GetAll();
        Task<IdentityResult> CreateAsync(TEntity entity);
        Task<IdentityResult> UpdateAsync(TEntity entity);
        Task<IdentityResult> DeleteAsync(TEntity entity);
        Task<List<TEntity>> FindAllAsync();
        Task<List<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> FindByIdAsync(TKey id);
    }
}