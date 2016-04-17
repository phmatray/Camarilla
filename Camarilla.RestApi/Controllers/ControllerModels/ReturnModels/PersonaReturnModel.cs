using System;
using System.Collections.Generic;

namespace Camarilla.RestApi.Controllers.ControllerModels
{
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

    public class PersonaWithMailReturnModel : PersonaReturnModelLite
    {
        public List<PersonaMailReturnModel> SentMails { get; set; }
        public List<PersonaMailReturnModel> ReceivedMails { get; set; }
    }

    public class PersonaWithAllReturnModel : PersonaReturnModelLite
    {
        public PersonaWithAllReturnModel(PersonaReturnModelLite modelLite)
        {
            Url = modelLite.Url;
            Id = modelLite.Id;
            Pseudo = modelLite.Pseudo;
        }
    }
}