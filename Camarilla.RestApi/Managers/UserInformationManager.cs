using Camarilla.RestApi.Models;
using Camarilla.RestApi.Stores.Interfaces;

namespace Camarilla.RestApi.Managers
{
    public class UserInformationManager : IManager
    {
        public UserInformationManager(IUserInformationStore<UserInformation> store)
        {
        }
    }
}