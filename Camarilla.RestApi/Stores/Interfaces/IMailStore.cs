using Camarilla.RestApi.Models;
using Camarilla.RestApi.Stores.Base;

namespace Camarilla.RestApi.Stores.Interfaces
{
    public interface IMailStore<TModel>
        : IStore<TModel, int>
        where TModel : Mail
    {
        
    }
}