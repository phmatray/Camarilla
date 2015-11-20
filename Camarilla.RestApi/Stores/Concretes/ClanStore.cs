using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Camarilla.RestApi.Infrastructure;
using Camarilla.RestApi.Models;
using Camarilla.RestApi.Stores.Base;
using Camarilla.RestApi.Stores.Interfaces;
using Microsoft.AspNet.Identity;

namespace Camarilla.RestApi.Stores.Concretes
{
    public class ClanStore : RepositoryBase, IClanStore<Clan>
    {
        public ClanStore(CamarillaContext context)
            : base(context)
        {
        }

        public IQueryable<Clan> GetAll()
        {
            return _context.Clans
                .AsQueryable();
        }

        public async Task<IdentityResult> CreateAsync(Clan entity)
        {
            return await CatchIdentityErrorsAsync(async () =>
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity));

                var clan = _context.Clans.Add(entity);
                entity.Id = clan.Id;

                await _context.SaveChangesAsync();
            });
        }

        public async Task<IdentityResult> UpdateAsync(Clan entity)
        {
            return await CatchIdentityErrorsAsync(async () =>
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity));

                _context.Entry(entity).State = EntityState.Modified;

                await _context.SaveChangesAsync();
            });
        }

        public async Task<IdentityResult> DeleteAsync(Clan entity)
        {
            return await CatchIdentityErrorsAsync(async () =>
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity));

                _context.Clans.Remove(entity);
                await _context.SaveChangesAsync();
            });
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