using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Camarilla.RestApi.ControllerModels;

namespace Camarilla.RestApi.Controllers
{
    [RoutePrefix("api/mailboxes")]
    public class MailboxesController : BaseApiController
    {
        [HttpGet]
        [Route("")]
        [ResponseType(typeof (List<MailboxReturnModel>))]
        public async Task<IHttpActionResult> GetMailboxes()
        {
            var mailboxReturnModels = (await TheMailboxStore.FindAllAsync())
                .Select(mailbox => TheModelFactory.Create(mailbox));

            return Ok(mailboxReturnModels);
        }

        [HttpGet]
        [Route("{id:int}", Name = "GetMailboxById")]
        [ResponseType(typeof (MailboxReturnModel))]
        public async Task<IHttpActionResult> GetMailbox(int id)
        {
            var mailbox = await TheMailboxStore.FindByIdAsync(id);

            if (mailbox != null)
                return Ok(TheModelFactory.Create(mailbox));

            return NotFound();
        }

        //[HttpPost]
        //[Route("mails", Name = "GetMailboxById")]
        //public async Task<IHttpActionResult> SendMail(CreateMailBindingModel createMailModel)
        //{
        //    if (!ModelState.IsValid)
        //        return BadRequest(ModelState);

        //    // absolute mail
        //    var mail = new Mail
        //    {
        //        Message = createMailModel.Message,
        //        Subject = createMailModel.Subject,
        //        Sent = DateTime.Now
        //    };

        //    var addMailResult = await TheMailStore.CreateAsync(mail);

        //    if (!addMailResult.Succeeded)
        //        return GetErrorResult(addMailResult);

        //    return Created()


        //    var locationHeader = new Uri(Url.Link("GetClanById", new { id = clan.Id }));

        //    return Created(locationHeader, TheModelFactory.Create(mail));


        //    // sender
        //    sender.Mailbox.Mails.Add(new PersonaMail() {Mail = mail, });
        //    // receivers
        //}

        //[HttpPut]
        //[Route("{id:int}")]
        //[ResponseType(typeof (MailboxReturnModel))]
        //public async Task<IHttpActionResult> PutMailbox(int id, UpdateMailboxBindingModel updateMailboxModel)
        //{
        //    if (!ModelState.IsValid)
        //        return BadRequest(ModelState);

        //    var mailbox = await TheMailboxStore.FindByIdAsync(id);

        //    if (mailbox == null)
        //        return NotFound();

        //    UpdateEntity(ref mailbox, updateMailboxModel);

        //    var result = await TheMailboxStore.UpdateAsync(mailbox);

        //    return !result.Succeeded
        //        ? GetErrorResult(result)
        //        : Ok();
        //}

        //[HttpPost]
        //[Route("")]
        //[ResponseType(typeof (MailboxReturnModel))]
        //public async Task<IHttpActionResult> PostMailbox(CreateMailboxBindingModel createMailboxModel)
        //{
        //    if (!ModelState.IsValid)
        //        return BadRequest(ModelState);

        //    var mailbox = new Mailbox
        //    {
        //        Name = createMailboxModel.Name,
        //        MailboxCategory = createMailboxModel.MailboxCategory,
        //        MailboxKind = createMailboxModel.MailboxKind,
        //        Description = createMailboxModel.Description
        //    };

        //    var addMailboxResult = await TheMailboxStore.CreateAsync(mailbox);

        //    if (!addMailboxResult.Succeeded)
        //        return GetErrorResult(addMailboxResult);

        //    var locationHeader = new Uri(Url.Link("GetMailboxById", new {id = mailbox.Id}));

        //    return Created(locationHeader, TheModelFactory.Create(mailbox));
        //}

        [HttpDelete]
        [Route("{id:int}")]
        [ResponseType(typeof (void))]
        public async Task<IHttpActionResult> DeleteMailbox(int id)
        {
            var mailbox = await TheMailboxStore.FindByIdAsync(id);

            if (mailbox != null)
            {
                var result = await TheMailboxStore.DeleteAsync(mailbox);

                return !result.Succeeded
                    ? GetErrorResult(result)
                    : Ok();
            }

            return NotFound();
        }

        //protected void UpdateEntity(ref Mailbox mailbox, UpdateMailboxBindingModel updateMailboxModel)
        //{
        //    if (updateMailboxModel.Name != null)
        //        mailbox.Name = updateMailboxModel.Name;
        //    if (updateMailboxModel.Description != null)
        //        mailbox.Description = updateMailboxModel.Description;
        //    if (updateMailboxModel.MailboxCategory.HasValue)
        //        mailbox.MailboxCategory = updateMailboxModel.MailboxCategory.Value;
        //    if (updateMailboxModel.MailboxKind.HasValue)
        //        mailbox.MailboxKind = updateMailboxModel.MailboxKind.Value;
        //}
    }
}