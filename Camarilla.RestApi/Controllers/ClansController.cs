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
    [RoutePrefix("api/clans")]
    public class ClansController : BaseApiController
    {
        private readonly ClanStore _theClanStore = null;

        protected ClanStore TheClanStore
        {
            get
            {
                return _theClanStore ??
                       new ClanStore(new CamarillaContext());
            }
        }

        [HttpGet]
        [Route("")]
        [ResponseType(typeof(List<ClanReturnModel>))]
        public async Task<IHttpActionResult> GetClans()
        {
            var clanReturnModels = (await TheClanStore.FindAllAsync())
                .Select(clan => TheModelFactory.Create(clan));

            return Ok(clanReturnModels);
        }

        [HttpGet]
        [Route("{id}", Name = "GetClanById")]
        [ResponseType(typeof(ClanReturnModel))]
        public async Task<IHttpActionResult> GetClan(int id)
        {
            var clan = await TheClanStore.FindByIdAsync(id);

            if (clan != null)
            {
                return Ok(TheModelFactory.Create(clan));
            }

            return NotFound();
        }

        //[HttpPut]
        //[Route("{id:Int32}")]
        //[ResponseType(typeof(void))]
        //public async Task<IHttpActionResult> PutClan(int id, Clan clan)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != clan.Id)
        //    {
        //        return BadRequest();
        //    }

        //    db.Entry(clan).State = EntityState.Modified;

        //    try
        //    {
        //        await db.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!ClanExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return StatusCode(HttpStatusCode.NoContent);
        //}

        [HttpPost]
        [Route("")]
        [ResponseType(typeof(Clan))]
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
        [Route("{id}")]
        [ResponseType(typeof(Clan))]
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
    }
}