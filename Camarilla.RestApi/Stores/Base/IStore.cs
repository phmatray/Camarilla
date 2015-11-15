using System.Collections.Generic;
using System.Threading.Tasks;
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
        Task<IdentityResult> CreateAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(TEntity entity);
        Task<List<TEntity>> FindAllAsync();
        Task<TEntity> FindByIdAsync(TKey id);
    }
}