namespace Camarilla.RestApi.Controllers.ControllerModels
{
    public class RaceReturnModelLite
    {
        public string Url { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class RaceReturnModel : RaceReturnModelLite
    {
        public string Description { get; set; }
        public int Experience { get; set; }
    }
}