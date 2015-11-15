using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Camarilla.RestApi.Stores.Base;
using Microsoft.AspNet.Identity;

namespace Camarilla.RestApi.Stores.Helpers
{
    public static class StoreHelpers
    {
        public static async Task<IdentityResult> CatchIdentityErrorsAsync(this IStore store, Func<Task> resultBody)
        {
            var errors = new List<string>();

            try
            {
                await resultBody();
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