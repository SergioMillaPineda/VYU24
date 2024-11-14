using System.Text.Json.Serialization;

namespace SWApi.Infrastructure.Contracts.Entities.SWApi
{
    public class PlanetStatisticsSWApiEntity
    {
        [JsonPropertyName("name")]
        public string PlanetName { get; set; } = string.Empty;
        [JsonPropertyName("rotation_period")]
        public int RotationPeriod { get; set; } = 0;
        [JsonPropertyName("orbital_period")]
        public int OrbitalPeriod { get; set; } = 0;
        public string Climate { get; set; } = string.Empty;
        public string Population { get; set; } = string.Empty;
        [JsonPropertyName("url")]
        public string DetailedInfoUrl { get; set; } = string.Empty;
    }
}
