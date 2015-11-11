using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Camarilla.RestApi.Models;
using Camarilla.RestApi.Stores.Interfaces;

namespace Camarilla.RestApi.Stores.Concretes
{
    public class PersonaStore : IPersonaStore<Persona>
    {
        public Task CreateAsync(Persona entity)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Persona entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Persona entity)
        {
            throw new NotImplementedException();
        }

        public Task<List<Persona>> FindAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Persona> FindByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Persona> FindByNameAsync(string name)
        {
            throw new NotImplementedException();
        }
    }
}