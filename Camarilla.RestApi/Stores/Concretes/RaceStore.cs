using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Camarilla.RestApi.Infrastructure;
using Camarilla.RestApi.Models;
using Camarilla.RestApi.Stores.Base;
using Camarilla.RestApi.Stores.Interfaces;
using Microsoft.AspNet.Identity;

namespace Camarilla.RestApi.Stores.Concretes
{
    public class RaceStore : RepositoryBase, IRaceStore<Race>
    {
        public RaceStore(CamarillaContext context)
            : base(context)
        {
        }

        public IQueryable<Race> GetAll()
        {
            return _context.Races
                .AsQueryable();
        }

        public async Task<IdentityResult> CreateAsync(Race entity)
        {
            return await CatchIdentityErrorsAsync(async () =>
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity));

                var race = _context.Races.Add(entity);
                entity.Id = race.Id;

                await _context.SaveChangesAsync();
            });
        }

        public async Task<IdentityResult> UpdateAsync(Race entity)
        {
            return await CatchIdentityErrorsAsync(async () =>
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity));

                _context.Entry(entity).State = EntityState.Modified;

                await _context.SaveChangesAsync();
            });
        }

        public async Task<IdentityResult> DeleteAsync(Race entity)
        {
            return await CatchIdentityErrorsAsync(async () =>
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity));

                _context.Races.Remove(entity);
                await _context.SaveChangesAsync();
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
            return await _context.Races
                .FirstOrDefaultAsync();
        }
    }
}