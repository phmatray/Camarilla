using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Camarilla.RestApi.Infrastructure.Stores.Base;
using Camarilla.RestApi.Infrastructure.Stores.Interfaces;
using Camarilla.RestApi.Models;
using Microsoft.AspNet.Identity;

namespace Camarilla.RestApi.Infrastructure.Stores.Concretes
{
    public class ClanStore : RepositoryBase, IClanStore<Clan>
    {
        public ClanStore(CamarillaContext context)
            : base(context)
        {
        }

        public IQueryable<Clan> GetAll()
        {
            return Context.Clans
                .AsQueryable();
        }

        public async Task<IdentityResult> CreateAsync(Clan entity)
        {
            return await CatchIdentityErrorsAsync(async () =>
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity));

                var clan = Context.Clans.Add(entity);
                entity.Id = clan.Id;

                await Context.SaveChangesAsync();
            });
        }

        public async Task<IdentityResult> UpdateAsync(Clan entity)
        {
            return await CatchIdentityErrorsAsync(async () =>
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity));

                Context.Entry(entity).State = EntityState.Modified;

                await Context.SaveChangesAsync();
            });
        }

        public async Task<IdentityResult> DeleteAsync(Clan entity)
        {
            return await CatchIdentityErrorsAsync(async () =>
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity));

                Context.Clans.Remove(entity);
                await Context.SaveChangesAsync();
            });
        }

        public async Task<List<Clan>> FindAllAsync()
        {
            return await GetAll()
                .ToListAsync();
        }

        public async Task<List<Clan>> FindAllAsync(Expression<Func<Clan, bool>> predicate)
        {
            return await GetAll()
                .Where(predicate)
                .ToListAsync();
        }

        public async Task<Clan> FindByIdAsync(int id)
        {
            return await Context.Clans
                .FirstOrDefaultAsync(clan => clan.Id == id);
        }

        public async Task<Clan> FindByNameAsync(string name)
        {
            return await Context.Clans
                .FirstOrDefaultAsync(clan => clan.Name == name);
        }
    }
}