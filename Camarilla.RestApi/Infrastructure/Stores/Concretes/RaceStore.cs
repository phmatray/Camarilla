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
    public class RaceStore : RepositoryBase, IRaceStore<Race>
    {
        public RaceStore(CamarillaContext context)
            : base(context)
        {
        }

        public IQueryable<Race> GetAll()
        {
            return Context.Races
                .AsQueryable();
        }

        public async Task<IdentityResult> CreateAsync(Race entity)
        {
            return await CatchIdentityErrorsAsync(async () =>
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity));

                var race = Context.Races.Add(entity);
                entity.Id = race.Id;

                await Context.SaveChangesAsync();
            });
        }

        public async Task<IdentityResult> UpdateAsync(Race entity)
        {
            return await CatchIdentityErrorsAsync(async () =>
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity));

                Context.Entry(entity).State = EntityState.Modified;

                await Context.SaveChangesAsync();
            });
        }

        public async Task<IdentityResult> DeleteAsync(Race entity)
        {
            return await CatchIdentityErrorsAsync(async () =>
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity));

                Context.Races.Remove(entity);
                await Context.SaveChangesAsync();
            });
        }

        public async Task<List<Race>> FindAllAsync()
        {
            return await GetAll()
                .ToListAsync();
        }

        public async Task<List<Race>> FindAllAsync(Expression<Func<Race, bool>> predicate)
        {
            return await GetAll()
                .ToListAsync();
        }

        public async Task<Race> FindByIdAsync(int id)
        {
            return await GetAll()
                .FirstOrDefaultAsync(clan => clan.Id == id);
        }

        public async Task<Race> FindByNameAsync(string name)
        {
            return await GetAll()
                .FirstOrDefaultAsync(clan => clan.Name == name);
        }

        public async Task<Race> FindDefaultAsync()
        {
            return await Context.Races
                .FirstOrDefaultAsync();
        }
    }
}