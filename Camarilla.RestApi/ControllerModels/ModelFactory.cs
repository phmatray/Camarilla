using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http.Routing;
using Camarilla.RestApi.Managers;
using Camarilla.RestApi.Models;
using Microsoft.AspNet.Identity.EntityFramework;

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

        public RoleReturnModel Create(IdentityRole appRole)
        {
            return new RoleReturnModel
            {
                Url = _urlHelper.Link("GetRoleById", new { id = appRole.Id }),
                Id = appRole.Id,
                Name = appRole.Name
            };
        }

        public ClanReturnModel Create(Clan clan)
        {
            return new ClanReturnModel
            {
                Url = _urlHelper.Link("GetClanById", new { id = clan.Id}),
                Id = clan.Id,
                ClanCategory = clan.ClanCategory,
                ClanKind = clan.ClanKind,
                Description = clan.Description,
                Name = clan.Name
            };
        }
    }

    public class UserReturnModel
    {
        public string Url { get; set; }
        public string Id { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public DateTime? JoinDate { get; set; }
        public IList<string> Roles { get; set; }
        public IList<System.Security.Claims.Claim> Claims { get; set; }
    }

    public class RoleReturnModel
    {
        public string Url { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
    }

    public class ClanReturnModel
    {
        public string Url { get; set; }
        public int Id { get; set; }
        public ClanCategory ClanCategory { get; set; }
        public ClanKind ClanKind { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
    }
}