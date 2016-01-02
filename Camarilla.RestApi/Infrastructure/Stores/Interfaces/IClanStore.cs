using System.Threading.Tasks;
using Camarilla.RestApi.Infrastructure.Stores.Base;
using Camarilla.RestApi.Models;

namespace Camarilla.RestApi.Infrastructure.Stores.Interfaces
{
    public interface IClanStore<TModel>
        : IStore<TModel, int>
        where TModel : Clan
    {
        Task<Clan> FindByNameAsync(string name);
    }
}