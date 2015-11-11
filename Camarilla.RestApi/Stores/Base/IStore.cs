using System.Collections.Generic;
using System.Threading.Tasks;

namespace Camarilla.RestApi.Stores.Base
{
    public interface IStore<TEntity, in TKey> where TEntity : class
    {
        Task CreateAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(TEntity entity);
        Task<List<TEntity>> FindAllAsync();
        Task<TEntity> FindByIdAsync(TKey id);
    }
}