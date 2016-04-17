using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace Camarilla.RestApi.Controllers.ControllerModels
{
    public class UserReturnModelLite
    {
        public string Url { get; set; }
        public string Id { get; set; }
    }

    public class UserReturnModel : UserReturnModelLite
    {
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public DateTime? JoinDate { get; set; }
        public IList<string> Roles { get; set; }
        public IList<Claim> Claims { get; set; }
        public IList<PersonaReturnModelLite> Personae { get; set; } 
    }
}