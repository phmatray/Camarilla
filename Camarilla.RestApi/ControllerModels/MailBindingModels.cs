using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Camarilla.RestApi.ControllerModels
{
    public class CreateMailBindingModel
    {
        public string Message { get; set; }
        public string Subject { get; set; }
        public string FromPseudo { get; set; }
        public List<string> ToPseudos { get; set; } 
    }
}