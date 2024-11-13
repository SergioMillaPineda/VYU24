using System.Text.Json.Serialization;

namespace CountriesManagement.Infrastructure.Contracts.Entities
{
    public class AllCountriesPopulationEntity
    {
        [JsonPropertyName("error")]
        public bool Error { get; set; }

        [JsonPropertyName("msg")]
        public string? Title { get; set; }

        [JsonPropertyName("data")]
        public List<CountryPopulationEntity>? Data { get; set; }
    }

}
