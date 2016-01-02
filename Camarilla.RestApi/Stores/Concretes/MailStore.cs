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
    public class MailStore : RepositoryBase, IMailStore<Mail>
    {
        public MailStore(CamarillaContext context)
            : base(context)
        {
        }

        public IQueryable<Mail> GetAll()
        {
            return _context.Mails
                .Include(x => x.From)
                .Include(x => x.To)
                .AsQueryable();
        }

        public async Task<IdentityResult> CreateAsync(Mail entity)
        {
            return await CatchIdentityErrorsAsync(async () =>
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity));

                var clan = _context.Mails.Add(entity);
                entity.Id = clan.Id;

                await _context.SaveChangesAsync();
            });
        }

        public async Task<IdentityResult> UpdateAsync(Mail entity)
        {
            return await CatchIdentityErrorsAsync(async () =>
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity));

                _context.Entry(entity).State = EntityState.Modified;

                await _context.SaveChangesAsync();
            });
        }

        public async Task<IdentityResult> DeleteAsync(Mail entity)
        {
            return await CatchIdentityErrorsAsync(async () =>
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity));

                _context.Mails.Remove(entity);
                await _context.SaveChangesAsync();
            });
        }

        public async Task<List<Mail>> FindAllAsync()
        {
            return await GetAll()
                .ToListAsync();
        }

        public async Task<Mail> FindByIdAsync(int id)
        {
            return await GetAll()
                .FirstOrDefaultAsync(mail => mail.Id == id);
        }
    }
}