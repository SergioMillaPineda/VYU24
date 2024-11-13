using System.Text.Json.Serialization;

namespace CountriesManagement.Infrastructure.Contracts.Entities
{
    public class CountryPopulationEntity
    {
        [JsonPropertyName("country")]
        public string? Country { get; set; }

        [JsonPropertyName("code")]
        public string? Code { get; set; }

        [JsonPropertyName("iso3")]
        public string? Iso3 { get; set; }

        [JsonPropertyName("populationCounts")]
        public List<PopulationByYearEntity>? PopulationListByYear { get; set; }
    }
}
