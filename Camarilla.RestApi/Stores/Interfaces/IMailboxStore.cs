using System.Threading.Tasks;
using Camarilla.RestApi.Models;
using Camarilla.RestApi.Stores.Base;

namespace Camarilla.RestApi.Stores.Interfaces
{
    public interface IMailboxStore<TModel>
        : IStore<TModel, int>
        where TModel : Mailbox
    {
    }
}