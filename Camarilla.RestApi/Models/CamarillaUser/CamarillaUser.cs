using System.Collections.Generic;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Camarilla.RestApi.Models
{
    public class CamarillaUser : IdentityUser, IModel
    {
        public CamarillaUser()
        {
        }

        public CamarillaUser(string userName)
            : base(userName)
        {
        }

        public virtual UserInformation UserInformation { get; set; }
        public virtual ICollection<Persona> Personae { get; set; }
    }
}