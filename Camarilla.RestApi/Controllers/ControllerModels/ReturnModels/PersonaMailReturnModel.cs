using System;

namespace Camarilla.RestApi.Controllers.ControllerModels
{
    public class PersonaMailReturnModel
    {
        public int PersonaId { get; set; }
        public int MailId { get; set; }
        public DateTime? Read { get; set; }
        public DateTime? Deleted { get; set; }
        public PersonaReturnModel Persona { get; set; } // associed Persona
        public MailReturnModel Mail { get; set; } // associed Mail
    }
}