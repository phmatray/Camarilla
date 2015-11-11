using System.Threading.Tasks;
using Camarilla.RestApi.Models;
using Camarilla.RestApi.Stores.Base;

namespace Camarilla.RestApi.Stores.Interfaces
{
    public interface IClanStore<TModel>
        : IStore<TModel, int>
        where TModel : Clan
    {
        Task<Clan> FindByNameAsync(string name);
        Task<Clan> FindDefaultAsync();
    }
}