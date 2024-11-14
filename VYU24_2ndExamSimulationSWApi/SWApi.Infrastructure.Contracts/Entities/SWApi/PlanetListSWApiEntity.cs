using System.Text.Json.Serialization;

namespace SWApi.Infrastructure.Contracts.Entities.SWApi
{
    public class PlanetListSWApiEntity
    {
        [JsonPropertyName("results")]
        public List<PlanetStatisticsSWApiEntity> Data { get; set; } = new List<PlanetStatisticsSWApiEntity>();
    }
}
