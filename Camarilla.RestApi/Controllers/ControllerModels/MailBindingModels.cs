using System.Collections.Generic;

namespace Camarilla.RestApi.Controllers.ControllerModels
{
    public class CreateMailBindingModel
    {
        public string Message { get; set; }
        public string Subject { get; set; }
        public string FromPseudo { get; set; }
        public List<string> ToPseudos { get; set; } 
    }
}