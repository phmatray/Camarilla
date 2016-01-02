using Camarilla.RestApi.Infrastructure.Stores.Base;
using Camarilla.RestApi.Models;

namespace Camarilla.RestApi.Infrastructure.Stores.Interfaces
{
    public interface IMailboxStore<TModel>
        : IStore<TModel, int>
        where TModel : Mailbox
    {
    }
}