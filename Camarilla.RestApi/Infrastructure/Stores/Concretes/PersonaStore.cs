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
    public class PersonaStore : RepositoryBase, IPersonaStore<Persona>
    {
        public PersonaStore(CamarillaContext context)
            : base(context)
        {
        }

        public IQueryable<Persona> GetAll()
        {
            return _context.Personae
                .Include(x => x.Clan)
                .Include(x => x.Race)
                .Include(x => x.Mailbox)
                .AsQueryable();
        }

        public async Task<IdentityResult> CreateAsync(Persona entity)
        {
            return await CatchIdentityErrorsAsync(async () =>
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity));

                var persona = _context.Personae.Add(entity);
                entity.Id = persona.Id;

                await _context.SaveChangesAsync();
            });
        }

        public async Task<IdentityResult> UpdateAsync(Persona entity)
        {
            return await CatchIdentityErrorsAsync(async () =>
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity));

                _context.Entry(entity).State = EntityState.Modified;

                await _context.SaveChangesAsync();
            });
        }

        public async Task<IdentityResult> DeleteAsync(Persona entity)
        {
            return await CatchIdentityErrorsAsync(async () =>
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity));

                _context.Personae.Remove(entity);
                await _context.SaveChangesAsync();
            });
        }

        public async Task<List<Persona>> FindAllAsync()
        {
            return await GetAll()
                .ToListAsync();
        }
        public async Task<List<Persona>> FindAllAsync(Expression<Func<Persona, bool>> predicate)
        {
            return await GetAll()
                .Where(predicate)
                .ToListAsync();
        }

        public async Task<Persona> FindByIdAsync(int id)
        {
            return await GetAll()
                .FirstOrDefaultAsync(persona => persona.Id == id);
        }

        public async Task<Persona> FindByPseudoAsync(string pseudo)
        {
            return await GetAll()
                .FirstOrDefaultAsync(persona => persona.Pseudo == pseudo);
        }
    }
}