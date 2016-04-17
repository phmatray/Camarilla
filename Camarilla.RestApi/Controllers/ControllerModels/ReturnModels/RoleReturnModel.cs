namespace Camarilla.RestApi.Controllers.ControllerModels
{
    public class RoleReturnModel : RoleReturnModelLite
    {
        public string Name { get; set; }
    }

    public class RoleReturnModelLite
    {
        public string Url { get; set; }
        public string Id { get; set; }
    }
}