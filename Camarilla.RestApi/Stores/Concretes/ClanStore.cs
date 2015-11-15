using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using Camarilla.RestApi.Infrastructure;
using Camarilla.RestApi.Models;
using Camarilla.RestApi.Stores.Helpers;
using Camarilla.RestApi.Stores.Interfaces;
using Microsoft.AspNet.Identity;

namespace Camarilla.RestApi.Stores.Concretes
{
    public class ClanStore : IClanStore<Clan>
    {
        private readonly CamarillaContext _context;

        public ClanStore(CamarillaContext context)
        {
            _context = context;
        }


        public async Task<IdentityResult> CreateAsync(Clan entity)
        {
            return await this.CatchIdentityErrorsAsync(async () =>
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity));

                var clan = _context.Clans.Add(entity);
                entity.Id = clan.Id;

                await _context.SaveChangesAsync();
            });
        }

        public Task UpdateAsync(Clan entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Clan entity)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Clan>> FindAllAsync()
        {
            return await _context.Clans
                .ToListAsync();
        }

        public async Task<Clan> FindByIdAsync(int id)
        {
            return await _context.Clans
                .FirstOrDefaultAsync(clan => clan.Id == id);
        }

        public async Task<Clan> FindByNameAsync(string name)
        {
            return await _context.Clans
                .FirstOrDefaultAsync(clan => clan.Name == name);
        }
    }
}