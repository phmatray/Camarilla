using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Camarilla.RestApi.Models;

namespace Camarilla.RestApi.ControllerModels
{
    public class CreateClanBindingModel
    {
        public string Name { get; set; }
        public ClanCategory ClanCategory { get; set; }
        public ClanKind ClanKind { get; set; }
        public string Description { get; set; }
    }
}