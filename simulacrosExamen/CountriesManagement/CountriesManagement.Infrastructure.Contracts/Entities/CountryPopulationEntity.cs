using System.Text.Json.Serialization;

namespace CountriesManagement.Infrastructure.Contracts.Entities
{
    public class CountryPopulationEntity
    {
        [JsonPropertyName("country")]
        public string? CountryName { get; set; }

        [JsonPropertyName("populationCounts")]
        public List<PopulationByYearEntity>? PopulationListByYear { get; set; }
    }
}
