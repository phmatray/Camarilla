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
            return Context.Personae
                .Include(x => x.Clan)
                .Include(x => x.Race)
                .AsQueryable();
        }

        public async Task<IdentityResult> CreateAsync(Persona entity)
        {
            return await CatchIdentityErrorsAsync(async () =>
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity));

                var persona = Context.Personae.Add(entity);
                entity.Id = persona.Id;

                await Context.SaveChangesAsync();
            });
        }

        public async Task<IdentityResult> UpdateAsync(Persona entity)
        {
            return await CatchIdentityErrorsAsync(async () =>
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity));

                Context.Entry(entity).State = EntityState.Modified;

                await Context.SaveChangesAsync();
            });
        }

        public async Task<IdentityResult> DeleteAsync(Persona entity)
        {
            return await CatchIdentityErrorsAsync(async () =>
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity));

                Context.Personae.Remove(entity);
                await Context.SaveChangesAsync();
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

        public async Task<Persona> FindByPseudoWithMailsAsync(string pseudo)
        {
            return await Context.Personae
                .Include(x => x.Clan)
                .Include(x => x.Race)
                .Include(x => x.Mails)
                .Include(x => x.Mails.Select(y => y.Mail))
                .FirstOrDefaultAsync(x => x.Pseudo == pseudo);
        }

        public async Task<Persona> FindByPseudoWithAllAsync(string pseudo)
        {
            return await Context.Personae
                .Include(x => x.Clan)
                .Include(x => x.Clan.Disciplines)
                .Include(x => x.Race)
                .Include(x => x.Sire)
                .Include(x => x.User)
                .Include(x => x.Children)
                .FirstOrDefaultAsync(x => x.Pseudo == pseudo);
        }
    }
}