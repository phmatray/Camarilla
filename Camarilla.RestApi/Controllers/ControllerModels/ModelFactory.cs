using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http.Routing;
using Camarilla.RestApi.Infrastructure.Helpers;
using Camarilla.RestApi.Infrastructure.Managers;
using Camarilla.RestApi.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Camarilla.RestApi.ControllerModels
{
    public class ModelFactory
    {
        private readonly ApplicationUserManager _userManager;
        private readonly UrlHelper _urlHelper;

        public ModelFactory(HttpRequestMessage request, ApplicationUserManager userManager)
        {
            _urlHelper = new UrlHelper(request);
            _userManager = userManager;
        }

        private UserReturnModelLite CreateLite(User user)
        {
            if (user == null)
                return null;

            return new UserReturnModelLite
            {
                Url = _urlHelper.Link("GetUserById", new {id = user.Id}),
                Id = user.Id
            };
        }

        public UserReturnModel Create(User user)
        {
            if (user == null)
                return null;

            return new UserReturnModel
            {
                Url = _urlHelper.Link("GetUserById", new {id = user.Id}),
                Id = user.Id,
                UserName = user.UserName,
                FullName = $"{user.FirstName} {user.LastName}",
                Email = user.Email,
                EmailConfirmed = user.EmailConfirmed,
                JoinDate = user.JoinDate,
                Roles = _userManager.GetRolesAsync(user.Id).Result,
                Claims = _userManager.GetClaimsAsync(user.Id).Result,
                Personae = user.Personae.Select(CreateLite).ToList()
            };
        }

        public RoleReturnModelLite CreateLite(IdentityRole role)
        {
            if (role == null)
                return null;

            return new RoleReturnModelLite
            {
                Url = _urlHelper.Link("GetRoleById", new {id = role.Id}),
                Id = role.Id,
            };
        }

        public RoleReturnModel Create(IdentityRole role)
        {
            if (role == null)
                return null;

            return new RoleReturnModel
            {
                Url = _urlHelper.Link("GetRoleById", new {id = role.Id}),
                Id = role.Id,
                Name = role.Name
            };
        }

        public RaceReturnModelLite CreateLite(Race race)
        {
            if (race == null)
                return null;

            return new RaceReturnModelLite
            {
                Url = _urlHelper.Link("GetRaceById", new { id = race.Id }),
                Id = race.Id,
                Name = race.Name
            };
        }

        public RaceReturnModel Create(Race race)
        {
            if (race == null)
                return null;

            return new RaceReturnModel
            {
                Url = _urlHelper.Link("GetRaceById", new { id = race.Id }),
                Id = race.Id,
                Name = race.Name,
                Description = race.Description,
                Experience = race.Experience
            };
        }

        public ClanReturnModelLite CreateLite(Clan clan)
        {
            if (clan == null)
                return null;

            return new ClanReturnModelLite
            {
                Url = _urlHelper.Link("GetClanById", new { id = clan.Id }),
                Id = clan.Id,
                Name = clan.Name
            };
        }

        public ClanReturnModel Create(Clan clan)
        {
            if (clan == null)
                return null;

            return new ClanReturnModel
            {
                Url = _urlHelper.Link("GetClanById", new {id = clan.Id}),
                Id = clan.Id,
                ClanCategory = clan.ClanCategory,
                ClanKind = clan.ClanKind,
                Description = clan.Description,
                Name = clan.Name
            };
        }

        public PersonaReturnModelLite CreateLite(Persona persona)
        {
            if (persona == null)
                return null;

            return new PersonaReturnModelLite
            {
                Url = _urlHelper.Link("GetPersonaById", new { id = persona.Id }),
                Id = persona.Id,
                Pseudo = persona.Pseudo
            };
        }

        public PersonaReturnModel Create(Persona persona)
        {
            if (persona == null)
                return null;

            return new PersonaReturnModel
            {
                Url = _urlHelper.Link("GetPersonaById", new {id = persona.Id}),
                Id = persona.Id,
                Pseudo = persona.Pseudo,
                Name = persona.Name,
                Gender = persona.PersonaGender.GetDisplayName(),
                BirthDate = persona.BirthDate,
                BirthPlace = persona.BirthPlace,
                Background = persona.Background,
                Generation = persona.Generation,
                ExperienceActual = persona.ExperienceActual,
                ExperienceRemaining = persona.ExperienceRemaining,
                Nights = persona.Nights,
                Willingness = persona.Willingness,
                Humanity = persona.Humanity,
                PictureUrl = persona.PictureUrl,
                Race = CreateLite(persona.Race),
                Clan = CreateLite(persona.Clan),
                User = CreateLite(persona.User)
            };
        }

        public MailReturnModelLite CreateLite(Mail mail)
        {
            if (mail == null)
                return null;

            return new MailReturnModelLite
            {
                Url = _urlHelper.Link("GetMailById", new {id = mail.Id}),
                Id = mail.Id
            };
        }

        public MailReturnModel Create(Mail mail)
        {
            if (mail == null)
                return null;

            return new MailReturnModel
            {
                Url = _urlHelper.Link("GetMailById", new { id = mail.Id }),
                Id = mail.Id,
                Subject = mail.Subject,
                Message = mail.Message,
                From = CreateLite(mail.From),
                To = mail.To.Select(CreateLite).ToList()
            };
        }

        public MailboxReturnModel Create(Mailbox letterBox)
        {
            throw new NotImplementedException();
        }
    }

    public class UserReturnModelLite
    {
        public string Url { get; set; }
        public string Id { get; set; }
    }

    public class UserReturnModel : UserReturnModelLite
    {
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public DateTime? JoinDate { get; set; }
        public IList<string> Roles { get; set; }
        public IList<Claim> Claims { get; set; }
        public IList<PersonaReturnModelLite> Personae { get; set; } 
    }

    public class MailboxReturnModel
    {
        
    }

    public class RoleReturnModelLite
    {
        public string Url { get; set; }
        public string Id { get; set; }
    }

    public class RoleReturnModel : RoleReturnModelLite
    {
        public string Name { get; set; }
    }

    public class RaceReturnModelLite
    {
        public string Url { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class RaceReturnModel : RaceReturnModelLite
    {
        public string Description { get; set; }
        public int Experience { get; set; }
    }

    public class ClanReturnModelLite
    {
        public string Url { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class ClanReturnModel : ClanReturnModelLite
    {
        public ClanCategory ClanCategory { get; set; }
        public ClanKind ClanKind { get; set; }
        public string Description { get; set; }
    }

    public class MailReturnModelLite
    {
        public string Url { get; set; }
        public int Id { get; set; }
    }

    public class MailReturnModel : MailReturnModelLite
    {
        public string Message { get; set; }
        public string Subject { get; set; }
        public PersonaReturnModelLite From { get; set; }
        public IList<PersonaReturnModelLite> To { get; set; } 
    }

    public class PersonaReturnModelLite
    {
        public string Url { get; set; }
        public int Id { get; set; }
        public string Pseudo { get; set; }
    }

    public class PersonaReturnModel : PersonaReturnModelLite
    {
        public string Name { get; set; }
        public string Gender { get; set; }
        public DateTime? BirthDate { get; set; }
        public string BirthPlace { get; set; }
        public string Background { get; set; }
        public int Generation { get; set; }
        public int ExperienceActual { get; set; }
        public int ExperienceRemaining { get; set; }
        public int Nights { get; set; }
        public int Willingness { get; set; }
        public int Humanity { get; set; }
        public string PictureUrl { get; set; }
        public RaceReturnModelLite Race { get; set; }
        public ClanReturnModelLite Clan { get; set; }
        public UserReturnModelLite User { get; set; }
    }

    //public class PersonaMailReturnModel
    //{
    //    public string Url { get; set; }
    //    public int PersonaId { get; set; }
    //    public int MailId { get; set; }
    //    public DateTime? Read { get; set; }
    //    public DateTime? Deleted { get; set; }
    //    public PersonaReturnModel Persona { get; set; } // connectedPersona
    //    public MailReturnModel Mail { get; set; }
    //}

    //public class MailReturnModel
    //{
    //    public string Url { get; set; }
    //    public int Id { get; set; }
    //    public string Subject { get; set; } = string.Empty;
    //    public string Message { get; set; } = string.Empty;
    //    public DateTime Sent { get; set; }
    //    public PersonaReturnModel From { get; set; }
    //    public ICollection<Persona> To { get; set; } = new List<Persona>();
    //    public ICollection<PersonaMail> ConnectedPersonae { get; set; } = new List<PersonaMail>();
    //}
}