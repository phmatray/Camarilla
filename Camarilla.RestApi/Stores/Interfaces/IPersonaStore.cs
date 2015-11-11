using System.Threading.Tasks;
using Camarilla.RestApi.Models;
using Camarilla.RestApi.Stores.Base;

namespace Camarilla.RestApi.Stores.Interfaces
{
    public interface IPersonaStore<TModel>
        : IStore<TModel, int>
        where TModel : Persona
    {
        Task<Persona> FindByNameAsync(string name);
    }
}