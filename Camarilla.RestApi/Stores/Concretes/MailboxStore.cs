using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Camarilla.RestApi.Infrastructure;
using Camarilla.RestApi.Stores.Base;
using Camarilla.RestApi.Stores.Interfaces;
using Microsoft.AspNet.Identity;
using Mailbox = Camarilla.RestApi.Models.Mailbox;

namespace Camarilla.RestApi.Stores.Concretes
{
    public class MailboxStore : RepositoryBase, IMailboxStore<Mailbox>
    {
        public MailboxStore(CamarillaContext context)
            : base(context)
        {
        }

        public IQueryable<Mailbox> GetAll()
        {
            return _context.Mailboxes
                .Include(x => x.MailboxOf)
                .Include(x => x.Mails)
                .Include(x => x.Mails.Select(y => y.Mail))
                .Include(x => x.Mails.Select(y => y.Persona))
                .AsQueryable();
        }

        public async Task<IdentityResult> CreateAsync(Mailbox entity)
        {
            return await CatchIdentityErrorsAsync(async () =>
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity));

                var clan = _context.Mailboxes.Add(entity);
                entity.Id = clan.Id;

                await _context.SaveChangesAsync();
            });
        }

        public async Task<IdentityResult> UpdateAsync(Mailbox entity)
        {
            return await CatchIdentityErrorsAsync(async () =>
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity));

                _context.Entry(entity).State = EntityState.Modified;

                await _context.SaveChangesAsync();
            });
        }

        public async Task<IdentityResult> DeleteAsync(Mailbox entity)
        {
            return await CatchIdentityErrorsAsync(async () =>
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity));

                _context.Mailboxes.Remove(entity);
                await _context.SaveChangesAsync();
            });
        }

        public async Task<List<Mailbox>> FindAllAsync()
        {
            return await _context.Mailboxes
                .ToListAsync();
        }

        public async Task<Mailbox> FindByIdAsync(int id)
        {
            return await _context.Mailboxes
                .FirstOrDefaultAsync(mailbox => mailbox.Id == id);
        }
    }
}