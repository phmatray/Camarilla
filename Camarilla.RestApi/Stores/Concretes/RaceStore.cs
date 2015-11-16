using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Camarilla.RestApi.Models;
using Camarilla.RestApi.Stores.Interfaces;
using Microsoft.AspNet.Identity;

namespace Camarilla.RestApi.Stores.Concretes
{
    public class RaceStore : IRaceStore<Race>
    {
        public Task<IdentityResult> CreateAsync(Race entity)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> UpdateAsync(Race entity)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> DeleteAsync(Race entity)
        {
            throw new NotImplementedException();
        }

        public Task<List<Race>> FindAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Race> FindByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Race> FindByNameAsync(string name)
        {
            throw new NotImplementedException();
        }

        public Task<Race> FindDefaultAsync()
        {
            throw new NotImplementedException();
        }
    }
}