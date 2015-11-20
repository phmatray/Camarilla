using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Camarilla.RestApi.Models;
using Microsoft.AspNet.Identity;

namespace Camarilla.RestApi.Stores.Base
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
        Task<TEntity> FindByIdAsync(TKey id);
    }
}