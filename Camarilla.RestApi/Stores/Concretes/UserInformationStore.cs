using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Camarilla.RestApi.Models;
using Camarilla.RestApi.Stores.Interfaces;

namespace Camarilla.RestApi.Stores.Concretes
{
    //public class UserInformationStore : IUserInformationStore<UserInformation>
    //{
    //    public UserInformationStore(CamarillaContext context)
    //    {
    //        Context = context;
    //    }

    //    public bool AutoSaveChanges { get; set; } = true;
    //    public CamarillaContext Context { get; }

    //    public virtual IQueryable<UserInformation> UserInformations
    //    {
    //        get { return Context.Set<UserInformation>(); }
    //    }

    //    public async Task CreateAsync(UserInformation entity)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public async Task UpdateAsync(UserInformation entity)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public async Task DeleteAsync(UserInformation entity)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public async Task<List<UserInformation>> FindAllAsync()
    //    {
    //        return await Context.UserInformations.ToListAsync();
    //    }

    //    public async Task<UserInformation> FindByIdAsync(int id)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}
}