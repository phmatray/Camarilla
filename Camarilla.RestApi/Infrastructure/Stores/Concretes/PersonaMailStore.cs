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
            return _context.MailboxMails
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

                var mailboxMail = _context.MailboxMails.Add(entity);
                entity.PersonaId = mailboxMail.PersonaId;
                entity.MailId = mailboxMail.MailId;

                await _context.SaveChangesAsync();
            });
        }

        public async Task<IdentityResult> UpdateAsync(PersonaMail entity)
        {
            return await CatchIdentityErrorsAsync(async () =>
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity));

                _context.Entry(entity).State = EntityState.Modified;

                await _context.SaveChangesAsync();
            });
        }

        public async Task<IdentityResult> DeleteAsync(PersonaMail entity)
        {
            return await CatchIdentityErrorsAsync(async () =>
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity));

                _context.MailboxMails.Remove(entity);
                await _context.SaveChangesAsync();
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