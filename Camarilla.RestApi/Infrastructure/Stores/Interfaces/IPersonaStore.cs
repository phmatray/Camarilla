using System.Collections.Generic;
using System.Threading.Tasks;
using Camarilla.RestApi.Infrastructure.Stores.Base;
using Camarilla.RestApi.Models;

namespace Camarilla.RestApi.Infrastructure.Stores.Interfaces
{
    public interface IPersonaStore<TModel>
        : IStore<TModel, int>
        where TModel : Persona
    {
        Task<Persona> FindByPseudoAsync(string pseudo);
        Task<Persona> FindByPseudoWithMailsAsync(string pseudo);
    }
}