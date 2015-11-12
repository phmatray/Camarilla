using System.Net.Http;
using System.Web.Http.Routing;
using Camarilla.RestApi.Managers;
using Camarilla.RestApi.Models;

namespace Camarilla.RestApi.ControllerModels
{
    public class ModelFactory
    {
        private readonly UrlHelper _urlHelper;
        private readonly UserManager _appUserManager;

        public ModelFactory(HttpRequestMessage request, UserManager userManager)
        {
            _urlHelper = new UrlHelper(request);
            _appUserManager = userManager;
        }

        public UserReturnModel Create(User user)
        {
            return new UserReturnModel
            {
                Url = _urlHelper.Link("GetUserById", new { id = user.Id }),
                Id = user.Id,
                UserName = user.UserName,
                FullName = $"{user.FirstName} {user.LastName}",
                Email = user.Email,
                EmailConfirmed = user.EmailConfirmed,
                JoinDate = user.JoinDate,
                Roles = _appUserManager.GetRolesAsync(user.Id).Result,
                Claims = _appUserManager.GetClaimsAsync(user.Id).Result
            };
        }
    }
}