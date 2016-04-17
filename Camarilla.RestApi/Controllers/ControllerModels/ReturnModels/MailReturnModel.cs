using System;

namespace Camarilla.RestApi.Controllers.ControllerModels
{
    public class MailReturnModelLite
    {
        public string Url { get; set; }
        public int Id { get; set; }
    }

    public class MailReturnModel : MailReturnModelLite
    {
        public string Subject { get; set; }
        public string Message { get; set; }
        public DateTime Sent { get; set; }
        public string FromPseudo { get; set; }
        public string ToPseudos { get; set; }
    }
}