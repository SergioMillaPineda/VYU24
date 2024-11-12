using System.Text.Json.Serialization;

namespace CountriesManagement.Infrastructure.Contracts.Entities
{
    public class PopulationByYearEntity
    {
        [JsonPropertyName("year")]
        public int Year { get; set; }

        [JsonPropertyName("value")]
        public long Value { get; set; }
    }
}
