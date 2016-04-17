using Camarilla.RestApi.Models;

namespace Camarilla.RestApi.Controllers.ControllerModels
{
    public class CreateClanBindingModel
    {
        public string Name { get; set; }
        public ClanCategory ClanCategory { get; set; }
        public ClanKind ClanKind { get; set; }
        public string Description { get; set; }
    }

    public class UpdateClanBindingModel
    {
        public string Name { get; set; }
        public ClanCategory? ClanCategory { get; set; }
        public ClanKind? ClanKind { get; set; }
        public string Description { get; set; }
    }
}