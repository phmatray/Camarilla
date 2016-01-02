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
    [RoutePrefix("api/mails")]
    public class MailsController : BaseApiController
    {
        private readonly MailStore _theMailStore = null;

        protected MailStore TheMailStore 
            => _theMailStore ?? new MailStore(new CamarillaContext());

        [HttpGet]
        [Route("")]
        [ResponseType(typeof (List<MailReturnModel>))]
        public async Task<IHttpActionResult> GetMails()
        {
            var mailReturnModels = (await TheMailStore.FindAllAsync())
                .Select(mail => TheModelFactory.Create(mail));

            return Ok(mailReturnModels);
        }

        [HttpGet]
        [Route("{id:int}", Name = "GetMailById")]
        [ResponseType(typeof (MailReturnModel))]
        public async Task<IHttpActionResult> GetMail(int id)
        {
            var mail = await TheMailStore.FindByIdAsync(id);

            if (mail != null)
                return Ok(TheModelFactory.Create(mail));

            return NotFound();
        }

        //[HttpPut]
        //[Route("{id:int}")]
        //[ResponseType(typeof (MailReturnModel))]
        //public async Task<IHttpActionResult> PutMail(int id, UpdateMailBindingModel updateMailModel)
        //{
        //    if (!ModelState.IsValid)
        //        return BadRequest(ModelState);

        //    var mail = await TheMailStore.FindByIdAsync(id);

        //    if (mail == null)
        //        return NotFound();

        //    UpdateEntity(ref mail, updateMailModel);

        //    var result = await TheMailStore.UpdateAsync(mail);

        //    return !result.Succeeded
        //        ? GetErrorResult(result)
        //        : Ok();
        //}

        [HttpPost]
        [Route("")]
        [ResponseType(typeof (MailReturnModel))]
        public async Task<IHttpActionResult> PostMail(CreateMailBindingModel createMailModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // get the sender
            var sender = await ThePersonaStore.FindByPseudoAsync(createMailModel.FromPseudo);
            if (sender == null)
                return BadRequest("Sender not found");

            // get all the receivers
            var tasks = createMailModel.ToPseudos
                .Select(x => ThePersonaStore.FindByPseudoAsync(x));
            var receivers = await Task.WhenAll(tasks);

            // create the mail
            var mail = new Mail
            {
                Message = createMailModel.Message,
                Subject = createMailModel.Subject,
                Sent = DateTime.Now,
                From = sender,
                To = receivers
            };

            var addMailResult = await TheMailStore.CreateAsync(mail);

            if (!addMailResult.Succeeded)
                return GetErrorResult(addMailResult);

            var locationHeader = new Uri(Url.Link("GetMailById", new {id = mail.Id}));

            return Created(locationHeader, TheModelFactory.Create(mail));
        }

        //[HttpDelete]
        //[Route("{id:int}")]
        //[ResponseType(typeof (void))]
        //public async Task<IHttpActionResult> DeleteMail(int id)
        //{
        //    var mail = await TheMailStore.FindByIdAsync(id);

        //    if (mail != null)
        //    {
        //        var result = await TheMailStore.DeleteAsync(mail);

        //        return !result.Succeeded
        //            ? GetErrorResult(result)
        //            : Ok();
        //    }

        //    return NotFound();
        //}

        //protected void UpdateEntity(ref Mail mail, UpdateMailBindingModel updateMailModel)
        //{
        //    if (updateMailModel.Name != null)
        //        mail.Name = updateMailModel.Name;
        //    if (updateMailModel.Description != null)
        //        mail.Description = updateMailModel.Description;
        //    if (updateMailModel.MailCategory.HasValue)
        //        mail.MailCategory = updateMailModel.MailCategory.Value;
        //    if (updateMailModel.MailKind.HasValue)
        //        mail.MailKind = updateMailModel.MailKind.Value;
        //}
    }
}