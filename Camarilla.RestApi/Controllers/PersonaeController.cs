using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Camarilla.RestApi.ControllerModels;
using Camarilla.RestApi.Infrastructure;
using Camarilla.RestApi.Models;
using Camarilla.RestApi.Stores.Concretes;

namespace Camarilla.RestApi.Controllers
{
    [RoutePrefix("api/personae")]
    public class PersonaeController : BaseApiController
    {
        private readonly PersonaStore _thePersonaStore = null;

        protected PersonaStore ThePersonaStore
            => _thePersonaStore ?? new PersonaStore(new CamarillaContext());

        [HttpGet]
        [Route("")]
        [ResponseType(typeof(List<PersonaReturnModel>))]
        public async Task<IHttpActionResult> GetPersonae()
        {
            var personaReturnModels = (await ThePersonaStore.FindAllAsync())
                .Select(persona => TheModelFactory.Create(persona));

            return Ok(personaReturnModels);
        }

        [HttpGet]
        [Route("{id:int}", Name = "GetPersonaById")]
        [ResponseType(typeof(PersonaReturnModel))]
        public async Task<IHttpActionResult> GetPersona(int id)
        {
            var persona = await ThePersonaStore.FindByIdAsync(id);

            if (persona != null)
                return Ok(TheModelFactory.Create(persona));

            return NotFound();
        }

        [HttpPut]
        [Route("{id:int}")]
        [ResponseType(typeof(PersonaReturnModel))]
        public async Task<IHttpActionResult> PutPersona(int id, UpdatePersonaBindingModel updatePersonaModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var persona = await ThePersonaStore.FindByIdAsync(id);

            if (persona == null)
                return NotFound();

            UpdateEntity(ref persona, updatePersonaModel);

            var result = await ThePersonaStore.UpdateAsync(persona);

            return !result.Succeeded
                ? GetErrorResult(result)
                : Ok();
        }

        [HttpPost]
        [Route("")]
        [ResponseType(typeof(PersonaReturnModel))]
        public async Task<IHttpActionResult> PostPersona(CreatePersonaBindingModel createPersonaModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (string.IsNullOrWhiteSpace(createPersonaModel.Username))
                return BadRequest("Username cannot be null");
            
            var persona = new Persona
            {
                Name = createPersonaModel.Name,
                Background = createPersonaModel.Background,
                Generation = 1,
                ExperienceActual = 3000,
                ExperienceRemaining = 3000,
                Nights = 0,
                Willingness = 10,
                Humanity = 10,
            };

            var user = await TheUserManager.FindByNameAsync(createPersonaModel.Username);
            if (user == null)
                return BadRequest("User not found");

            user.Personae.Add(persona);

            var updateUserResult = await TheUserManager.UpdateAsync(user);

            if (!updateUserResult.Succeeded)
                return GetErrorResult(updateUserResult);

            var locationHeader = new Uri(Url.Link("GetPersonaById", new { id = persona.Id }));

            return Created(locationHeader, TheModelFactory.Create(persona));
        }

        //[HttpPost]
        //[Route("")]
        //[ResponseType(typeof(PersonaReturnModel))]
        //public async Task<IHttpActionResult> PostPersona(CreatePersonaBindingModel createPersonaModel)
        //{
        //    if (!ModelState.IsValid)
        //        return BadRequest(ModelState);
        //    if (string.IsNullOrWhiteSpace(createPersonaModel.Username))
        //        return BadRequest("Username cannot be null");

        //    var user = await TheUserManager.FindByNameAsync(createPersonaModel.Username);
        //    if (user == null)
        //        return BadRequest("User not found");

        //    var persona = new Persona
        //    {
        //        Name = createPersonaModel.Name,
        //        Background = createPersonaModel.Background,
        //        Generation = 1,
        //        ExperienceActual = 3000,
        //        ExperienceRemaining = 3000,
        //        Nights = 0,
        //        Willingness = 10,
        //        Humanity = 10,
        //        User = user
        //    };

        //    var addPersonaResult = await ThePersonaStore.CreateAsync(persona);

        //    if (!addPersonaResult.Succeeded)
        //        return GetErrorResult(addPersonaResult);

        //    var locationHeader = new Uri(Url.Link("GetPersonaById", new { id = persona.Id }));

        //    return Created(locationHeader, TheModelFactory.Create(persona));
        //}

        [HttpDelete]
        [Route("{id:int}")]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> DeletePersona(int id)
        {
            var persona = await ThePersonaStore.FindByIdAsync(id);

            if (persona != null)
            {
                var result = await ThePersonaStore.DeleteAsync(persona);

                return !result.Succeeded
                    ? GetErrorResult(result)
                    : Ok();
            }

            return NotFound();
        }

        //[HttpGet]
        //[Route("{id:int}/mails")]
        //[ResponseType(typeof(List<Mail>))]
        //public async Task<IHttpActionResult> GetLetterBox(int id)
        //{
        //    var persona = await ThePersonaStore.FindByIdAsync(id);

        //    if (persona != null)
        //    {
        //        var result = persona.LetterBox
        //            .Select(mail => TheModelFactory.Create(mail));

        //        return !result.Succeeded
        //            ? GetErrorResult(result)
        //            : Ok();
        //    }

        //    return NotFound();
        //}

        protected void UpdateEntity(ref Persona persona, UpdatePersonaBindingModel updatePersonaModel)
        {
            if (updatePersonaModel.Name != null)
                persona.Name = updatePersonaModel.Name;
            if (updatePersonaModel.Background != null)
                persona.Background = updatePersonaModel.Background;
            if (updatePersonaModel.Generation.HasValue)
                persona.Generation = updatePersonaModel.Generation.Value;
            if (updatePersonaModel.ExperienceActual.HasValue)
                persona.ExperienceActual = updatePersonaModel.ExperienceActual.Value;
            if (updatePersonaModel.ExperienceRemaining.HasValue)
                persona.ExperienceRemaining = updatePersonaModel.ExperienceRemaining.Value;
            if (updatePersonaModel.Nights.HasValue)
                persona.Nights = updatePersonaModel.Nights.Value;
            if (updatePersonaModel.Willingness.HasValue)
                persona.Willingness = updatePersonaModel.Willingness.Value;
            if (updatePersonaModel.Humanity.HasValue)
                persona.Humanity = updatePersonaModel.Humanity.Value;
        }
    }
}
