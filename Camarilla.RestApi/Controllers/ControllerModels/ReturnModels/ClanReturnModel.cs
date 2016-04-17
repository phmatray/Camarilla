using Camarilla.RestApi.Models;

namespace Camarilla.RestApi.Controllers.ControllerModels
{
    public class ClanReturnModelLite
    {
        public string Url { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class ClanReturnModel : ClanReturnModelLite
    {
        public string Category { get; set; }
        public string Kind { get; set; }
        public string Description { get; set; }
    }
}