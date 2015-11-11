namespace Camarilla.RestApi.Models
{
    public class Asset : IModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int AssetLevel { get; set; }
        public AssetCategory AssetCategory { get; set; }
        public AssetSideEffect AssetSideEffect { get; set; }
    }
}