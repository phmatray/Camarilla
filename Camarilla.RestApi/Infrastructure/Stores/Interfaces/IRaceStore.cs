using System.Threading.Tasks;
using Camarilla.RestApi.Infrastructure.Stores.Base;
using Camarilla.RestApi.Models;

namespace Camarilla.RestApi.Infrastructure.Stores.Interfaces
{
    public interface IRaceStore<TModel>
        : IStore<TModel, int>
        where TModel : Race
    {
        Task<Race> FindByNameAsync(string name);
        Task<Race> FindDefaultAsync();
    }
}