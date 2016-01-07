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
    public class PersonaMailStore : RepositoryBase, IMailboxMailStore<PersonaMail>
    {
        public PersonaMailStore(CamarillaContext context)
            : base(context)
        {
        }

        public IQueryable<PersonaMail> GetAll()
        {
            return Context.MailboxMails
                .Include(x => x.Persona)
                .Include(x => x.Mail)
                .AsQueryable();
        }

        public async Task<IdentityResult> CreateAsync(PersonaMail entity)
        {
            return await CatchIdentityErrorsAsync(async () =>
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity));

                var mailboxMail = Context.MailboxMails.Add(entity);
                entity.PersonaId = mailboxMail.PersonaId;
                entity.MailId = mailboxMail.MailId;

                await Context.SaveChangesAsync();
            });
        }

        public async Task<IdentityResult> UpdateAsync(PersonaMail entity)
        {
            return await CatchIdentityErrorsAsync(async () =>
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity));

                Context.Entry(entity).State = EntityState.Modified;

                await Context.SaveChangesAsync();
            });
        }

        public async Task<IdentityResult> DeleteAsync(PersonaMail entity)
        {
            return await CatchIdentityErrorsAsync(async () =>
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity));

                Context.MailboxMails.Remove(entity);
                await Context.SaveChangesAsync();
            });
        }

        public async Task<List<PersonaMail>> FindAllAsync()
        {
            return await GetAll()
                .ToListAsync();
        }

        public async Task<List<PersonaMail>> FindAllAsync(Expression<Func<PersonaMail, bool>> predicate)
        {
            return await GetAll()
                .Where(predicate)
                .ToListAsync();
        }

        public async Task<PersonaMail> FindByIdAsync(CompositeKey id)
        {
            return await GetAll()
                .FirstOrDefaultAsync(x => x.PersonaId == id.KeyPersona &&
                                          x.MailId == id.KeyMail);
        }
    }
}