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
    [RoutePrefix("api/mails")]
    public class MailsController : BaseApiController
    {
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

        [HttpPost]
        [Route("")]
        [ResponseType(typeof (MailReturnModel))]
        public async Task<IHttpActionResult> SendMail(CreateMailBindingModel createMailModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // get the sender
            var sender = await ThePersonaStore.FindByPseudoAsync(createMailModel.FromPseudo);
            if (sender == null)
                return BadRequest("Sender not found");

            // get all the receivers
            var pseudoList = createMailModel.ToPseudos.Split(';').ToList();
            var receivers = await ThePersonaStore.FindAllAsync(x => pseudoList.Contains(x.Pseudo));

            // create the mail
            var mail = new Mail
            {
                Message = createMailModel.Message,
                Subject = createMailModel.Subject,
                Sent = DateTime.Now,
                FromPseudo = createMailModel.FromPseudo,
                ToPseudos = string.Join(";", receivers.Select(x => x.Pseudo))
            };

            // store the mail in DB
            var addMailResult = await TheMailStore.CreateAsync(mail);
            if (!addMailResult.Succeeded)
                return GetErrorResult(addMailResult);

            // create the mailboxMails thanks to the mailboxIds
            var personaIds = new List<Persona>(receivers) { sender }.Select(x => x.Id).ToList();
            var personaMails = personaIds.Select(x => new PersonaMail { MailId = mail.Id, PersonaId = x }).ToList();

            // store the mailboxMails in DB
            foreach (var personaMail in personaMails)
            {
                var addPersonaMailResult = await ThePersonaMailStore.CreateAsync(personaMail);
                if (!addPersonaMailResult.Succeeded)
                    return GetErrorResult(addPersonaMailResult);
            }

            var locationHeader = new Uri(Url.Link("GetMailById", new {id = mail.Id}));

            return Created(locationHeader, TheModelFactory.Create(mail));
        }

        [HttpGet]
        [Route("{pseudo}")]
        [Route("~/api/personae/{pseudo}/mails")]
        [ResponseType(typeof(PersonaWithMailReturnModel))]
        public async Task<IHttpActionResult> GetMailsForPersona(string pseudo)
        {
            var persona = await ThePersonaStore.FindByPseudoWithMailsAsync(pseudo);

            if (persona != null)
                return Ok(TheModelFactory.CreateWithMail(persona));

            return NotFound();
        }
    }
}