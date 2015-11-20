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
    public class PersonaStore : RepositoryBase, IPersonaStore<Persona>
    {
        private readonly CamarillaContext _context;

        public PersonaStore(CamarillaContext context)
        {
            _context = context;
        }

        public IQueryable<Persona> GetAll()
        {
            return _context.Personae
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
            return await _context.Personae
                .ToListAsync();
        }

        public async Task<Persona> FindByIdAsync(int id)
        {
            return await _context.Personae
                .FirstOrDefaultAsync(persona => persona.Id == id);
        }

        public async Task<Persona> FindByNameAsync(string name)
        {
            return await _context.Personae
                .FirstOrDefaultAsync(persona => persona.Name == name);
        }
    }
}