using System.Threading.Tasks;
using Camarilla.RestApi.Models;
using Camarilla.RestApi.Stores.Base;

namespace Camarilla.RestApi.Stores.Interfaces
{
    public interface IRaceStore<TModel>
        : IStore<TModel, int>
        where TModel : Race
    {
        Task<Race> FindByNameAsync(string name);
        Task<Race> FindDefaultAsync();
    }
}