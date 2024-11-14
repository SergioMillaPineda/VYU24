using System.Text.Json.Serialization;

namespace SWApi.Infrastructure.Contracts.Entities.SWApi
{
    public class PlanetResidentListSWApiEntity
    {
        [JsonPropertyName("residents")]
        public List<string> ResidentUrlList { get; set; } = new();
    }

}
