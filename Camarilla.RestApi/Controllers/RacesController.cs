using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Camarilla.RestApi.ControllerModels;
using Camarilla.RestApi.Models;

namespace Camarilla.RestApi.Controllers
{
    [RoutePrefix("api/races")]
    public class RacesController : BaseApiController
    {
        [HttpGet]
        [Route("")]
        [ResponseType(typeof (List<RaceReturnModel>))]
        public async Task<IHttpActionResult> GetRaces()
        {
            var raceReturnModels = (await TheRaceStore.FindAllAsync())
                .Select(race => TheModelFactory.Create(race));

            return Ok(raceReturnModels);
        }

        [HttpGet]
        [Route("{id:int}", Name = "GetRaceById")]
        [ResponseType(typeof (RaceReturnModel))]
        public async Task<IHttpActionResult> GetRace(int id)
        {
            var race = await TheRaceStore.FindByIdAsync(id);

            if (race != null)
                return Ok(TheModelFactory.Create(race));

            return NotFound();
        }

        [HttpPut]
        [Route("{id:int}")]
        [ResponseType(typeof (RaceReturnModel))]
        public async Task<IHttpActionResult> PutRace(int id, UpdateRaceBindingModel updateRaceModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var race = await TheRaceStore.FindByIdAsync(id);

            if (race == null)
                return NotFound();

            UpdateEntity(ref race, updateRaceModel);

            var result = await TheRaceStore.UpdateAsync(race);

            return !result.Succeeded
                ? GetErrorResult(result)
                : Ok();
        }

        [HttpPost]
        [Route("")]
        [ResponseType(typeof (RaceReturnModel))]
        public async Task<IHttpActionResult> PostRace(CreateRaceBindingModel createRaceModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var race = new Race
            {
                Name = createRaceModel.Name,
                Description = createRaceModel.Description,
                Experience = createRaceModel.Experience
            };

            var addRaceResult = await TheRaceStore.CreateAsync(race);

            if (!addRaceResult.Succeeded)
                return GetErrorResult(addRaceResult);

            var locationHeader = new Uri(Url.Link("GetRaceById", new {id = race.Id}));

            return Created(locationHeader, TheModelFactory.Create(race));
        }

        [HttpDelete]
        [Route("{id:int}")]
        [ResponseType(typeof (void))]
        public async Task<IHttpActionResult> DeleteRace(int id)
        {
            var race = await TheRaceStore.FindByIdAsync(id);

            if (race != null)
            {
                var result = await TheRaceStore.DeleteAsync(race);

                return !result.Succeeded
                    ? GetErrorResult(result)
                    : Ok();
            }

            return NotFound();
        }

        protected void UpdateEntity(ref Race race, UpdateRaceBindingModel updateRaceModel)
        {
            if (updateRaceModel.Name != null)
                race.Name = updateRaceModel.Name;
            if (updateRaceModel.Description != null)
                race.Description = updateRaceModel.Description;
            if (updateRaceModel.Experience.HasValue)
                race.Experience = updateRaceModel.Experience.Value;
        }
    }
}