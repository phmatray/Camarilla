using System.Data.Entity;
using System.Threading.Tasks;
using Camarilla.RestApi.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Camarilla.RestApi.Managers
{
    public class ApplicationUserStore<T> : UserStore<T> where T : User
    {
        public ApplicationUserStore(DbContext context)
            : base(context)
        {
        }

        public override Task<T> FindByIdAsync(string userId)
        {
            return Users
                .Include(x => x.Roles)
                .Include(x => x.Claims)
                .Include(x => x.Personae)
                .FirstOrDefaultAsync(x => x.Id == userId);
        }

        public override Task<T> FindByNameAsync(string username)
        {
            return Users
                .Include(x => x.Roles)
                .Include(x => x.Claims)
                .Include(x => x.Personae)
                .FirstOrDefaultAsync(x => x.UserName == username);
        }
    }
}