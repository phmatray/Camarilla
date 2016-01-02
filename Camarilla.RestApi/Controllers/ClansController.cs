using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Camarilla.RestApi.Controllers.ControllerModels;
using Camarilla.RestApi.Models;

namespace Camarilla.RestApi.Controllers
{
    [RoutePrefix("api/clans")]
    public class ClansController : BaseApiController
    {
        [HttpGet]
        [Route("")]
        [ResponseType(typeof (List<ClanReturnModel>))]
        public async Task<IHttpActionResult> GetClans()
        {
            var clanReturnModels = (await TheClanStore.FindAllAsync())
                .Select(clan => TheModelFactory.Create(clan));

            return Ok(clanReturnModels);
        }

        [HttpGet]
        [Route("{id:int}", Name = "GetClanById")]
        [ResponseType(typeof (ClanReturnModel))]
        public async Task<IHttpActionResult> GetClan(int id)
        {
            var clan = await TheClanStore.FindByIdAsync(id);

            if (clan != null)
                return Ok(TheModelFactory.Create(clan));

            return NotFound();
        }

        [HttpPut]
        [Route("{id:int}")]
        [ResponseType(typeof (ClanReturnModel))]
        public async Task<IHttpActionResult> PutClan(int id, UpdateClanBindingModel updateClanModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var clan = await TheClanStore.FindByIdAsync(id);

            if (clan == null)
                return NotFound();

            UpdateEntity(ref clan, updateClanModel);

            var result = await TheClanStore.UpdateAsync(clan);

            return !result.Succeeded
                ? GetErrorResult(result)
                : Ok();
        }

        [HttpPost]
        [Route("")]
        [ResponseType(typeof (ClanReturnModel))]
        public async Task<IHttpActionResult> PostClan(CreateClanBindingModel createClanModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var clan = new Clan
            {
                Name = createClanModel.Name,
                ClanCategory = createClanModel.ClanCategory,
                ClanKind = createClanModel.ClanKind,
                Description = createClanModel.Description
            };

            var addClanResult = await TheClanStore.CreateAsync(clan);

            if (!addClanResult.Succeeded)
                return GetErrorResult(addClanResult);

            var locationHeader = new Uri(Url.Link("GetClanById", new {id = clan.Id}));

            return Created(locationHeader, TheModelFactory.Create(clan));
        }

        [HttpDelete]
        [Route("{id:int}")]
        [ResponseType(typeof (void))]
        public async Task<IHttpActionResult> DeleteClan(int id)
        {
            var clan = await TheClanStore.FindByIdAsync(id);

            if (clan != null)
            {
                var result = await TheClanStore.DeleteAsync(clan);

                return !result.Succeeded
                    ? GetErrorResult(result)
                    : Ok();
            }

            return NotFound();
        }

        protected void UpdateEntity(ref Clan clan, UpdateClanBindingModel updateClanModel)
        {
            if (updateClanModel.Name != null)
                clan.Name = updateClanModel.Name;
            if (updateClanModel.Description != null)
                clan.Description = updateClanModel.Description;
            if (updateClanModel.ClanCategory.HasValue)
                clan.ClanCategory = updateClanModel.ClanCategory.Value;
            if (updateClanModel.ClanKind.HasValue)
                clan.ClanKind = updateClanModel.ClanKind.Value;
        }
    }
}