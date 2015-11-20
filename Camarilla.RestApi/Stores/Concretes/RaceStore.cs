using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Camarilla.RestApi.Infrastructure;
using Camarilla.RestApi.Models;
using Camarilla.RestApi.Stores.Interfaces;
using Microsoft.AspNet.Identity;

namespace Camarilla.RestApi.Stores.Concretes
{
    public class RaceStore : IRaceStore<Race>
    {
        private readonly CamarillaContext _context;

        public RaceStore(CamarillaContext context)
        {
            _context = context;
        }

        public IQueryable<Race> GetAll()
        {
            return _context.Races
                .AsQueryable();
        }

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