using Camarilla.RestApi.Infrastructure.Stores.Base;
using Camarilla.RestApi.Models;

namespace Camarilla.RestApi.Infrastructure.Stores.Interfaces
{
    public interface IMailboxMailStore<TModel>
        : IStore<TModel, CompositeKey>
        where TModel : PersonaMail
    {
    }
}