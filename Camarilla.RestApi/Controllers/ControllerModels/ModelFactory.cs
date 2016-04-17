using System;
using System.Linq;
using System.Net.Http;
using System.Web.Http.Routing;
using AutoMapper;
using Camarilla.RestApi.Infrastructure.Helpers;
using Camarilla.RestApi.Infrastructure.Managers;
using Camarilla.RestApi.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Camarilla.RestApi.Controllers.ControllerModels
{
    public class ModelFactory
    {
        private readonly UrlHelper _urlHelper;
        private readonly ApplicationUserManager _userManager;

        public ModelFactory(HttpRequestMessage request, ApplicationUserManager userManager)
        {
            _urlHelper = new UrlHelper(request);
            _userManager = userManager;
        }

        private UserReturnModelLite CreateLite(User user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

            var model = Mapper.Map<UserReturnModelLite>(user);
            model.Url = _urlHelper.Link("GetUserById", new {id = user.Id});
            return model;
        }

        public UserReturnModel Create(User user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

            var model = Mapper.Map<UserReturnModel>(user);
            model.Url = _urlHelper.Link("GetUserById", new {id = user.Id});
            model.Roles = _userManager.GetRolesAsync(user.Id).Result;
            model.Claims = _userManager.GetClaimsAsync(user.Id).Result;
            model.Personae = user.Personae.Select(CreateLite).ToList();
            return model;
        }

        public RoleReturnModelLite CreateLite(IdentityRole role)
        {
            if (role == null) throw new ArgumentNullException(nameof(role));

            var model = Mapper.Map<RoleReturnModelLite>(role);
            model.Url = _urlHelper.Link("GetRoleById", new {id = role.Id});
            return model;
        }

        public RoleReturnModel Create(IdentityRole role)
        {
            if (role == null) throw new ArgumentNullException(nameof(role));

            var model = Mapper.Map<RoleReturnModel>(role);
            model.Url = _urlHelper.Link("GetRoleById", new {id = role.Id});
            return model;
        }

        public RaceReturnModelLite CreateLite(Race race)
        {
            if (race == null) throw new ArgumentNullException(nameof(race));

            var model = Mapper.Map<RaceReturnModelLite>(race);
            model.Url = _urlHelper.Link("GetRaceById", new {id = race.Id});
            return model;
        }

        public RaceReturnModel Create(Race race)
        {
            if (race == null) throw new ArgumentNullException(nameof(race));

            var model = Mapper.Map<RaceReturnModel>(race);
            model.Url = _urlHelper.Link("GetRaceById", new {id = race.Id});
            return model;
        }

        public ClanReturnModelLite CreateLite(Clan clan)
        {
            if (clan == null) throw new ArgumentNullException(nameof(clan));

            var model = Mapper.Map<ClanReturnModelLite>(clan);
            model.Url = _urlHelper.Link("GetClanById", new {id = clan.Id});
            return model;
        }

        public ClanReturnModel Create(Clan clan)
        {
            if (clan == null) throw new ArgumentNullException(nameof(clan));

            var model = Mapper.Map<ClanReturnModel>(clan);
            model.Url = _urlHelper.Link("GetClanById", new {id = clan.Id});
            model.Category = clan.ClanCategory.GetDisplayName();
            model.Kind = clan.ClanKind.GetDisplayName();
            return model;
        }

        public PersonaReturnModelLite CreateLite(Persona persona)
        {
            if (persona == null) throw new ArgumentNullException(nameof(persona));

            var model = Mapper.Map<PersonaReturnModelLite>(persona);
            model.Url = _urlHelper.Link("GetPersonaById", new {id = persona.Id});
            return model;
        }

        public PersonaReturnModel Create(Persona persona)
        {
            if (persona == null) throw new ArgumentNullException(nameof(persona));

            var model = Mapper.Map<PersonaReturnModel>(persona);
            model.Url = _urlHelper.Link("GetPersonaById", new {id = persona.Id});
            model.Race = CreateLite(persona.Race);
            model.Clan = CreateLite(persona.Clan);
            model.User = CreateLite(persona.User);
            return model;
        }

        public PersonaWithMailReturnModel CreateWithMail(Persona persona)
        {
            if (persona == null) throw new ArgumentNullException(nameof(persona));

            var model = Mapper.Map<PersonaWithMailReturnModel>(persona);
            model.Url = _urlHelper.Link("GetPersonaById", new {id = persona.Id});
            model.SentMails = persona.SentMails.Select(Create).ToList();
            model.ReceivedMails = persona.ReceivedMails.Select(Create).ToList();
            return model;
        }

        public PersonaWithAllReturnModel CreateWithAll(Persona persona)
        {
            if (persona == null) throw new ArgumentNullException(nameof(persona));

            var model = Mapper.Map<PersonaWithAllReturnModel>(persona);
            model.Url = _urlHelper.Link("GetPersonaById", new {id = persona.Id});
            return model;
        }

        public MailReturnModelLite CreateLite(Mail mail)
        {
            if (mail == null) throw new ArgumentNullException(nameof(mail));

            var model = Mapper.Map<MailReturnModelLite>(mail);
            model.Url = _urlHelper.Link("GetMailById", new {id = mail.Id});
            return model;
        }

        public MailReturnModel Create(Mail mail)
        {
            if (mail == null) throw new ArgumentNullException(nameof(mail));

            var model = Mapper.Map<MailReturnModel>(mail);
            model.Url = _urlHelper.Link("GetMailById", new {id = mail.Id});
            return model;
        }

        public PersonaMailReturnModel Create(PersonaMail personaMail)
        {
            if (personaMail == null) throw new ArgumentNullException(nameof(personaMail));

            var model = Mapper.Map<PersonaMailReturnModel>(personaMail);
            model.Mail = Create(personaMail.Mail);
            return model;
        }
    }
}