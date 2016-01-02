namespace Camarilla.RestApi.Controllers.ControllerModels
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