using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Camarilla.RestApi.ControllerModels
{
    public class CreateRaceBindingModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Experience { get; set; } 
    }

    public class UpdateRaceBindingModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int? Experience { get; set; }
    }
}