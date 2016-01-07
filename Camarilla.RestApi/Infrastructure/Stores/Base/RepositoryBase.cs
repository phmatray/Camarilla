using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace Camarilla.RestApi.Infrastructure.Stores.Base
{
    public abstract class RepositoryBase
    {
        protected CamarillaContext Context { get;}

        protected RepositoryBase(CamarillaContext context)
        {
            Context = context;
        }

        protected async Task<IdentityResult> CatchIdentityErrorsAsync(Func<Task> resultBody)
        {
            var errors = new List<string>();

            try
            {
                await resultBody();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                errors.Add(ex.Message);
            }
            catch (Exception ex)
            {
                errors.Add(ex.Message);
            }

            return errors.Any()
                ? IdentityResult.Failed(errors.ToArray())
                : IdentityResult.Success;
        }
    }
}