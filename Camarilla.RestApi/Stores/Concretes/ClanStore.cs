using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Camarilla.RestApi.Models;
using Camarilla.RestApi.Stores.Interfaces;

namespace Camarilla.RestApi.Stores.Concretes
{
    public class ClanStore : IClanStore<Clan>
    {
        public Task CreateAsync(Clan entity)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Clan entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Clan entity)
        {
            throw new NotImplementedException();
        }

        public Task<List<Clan>> FindAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Clan> FindByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Clan> FindByNameAsync(string name)
        {
            throw new NotImplementedException();
        }

        public Task<Clan> FindDefaultAsync()
        {
            throw new NotImplementedException();
        }
    }
}