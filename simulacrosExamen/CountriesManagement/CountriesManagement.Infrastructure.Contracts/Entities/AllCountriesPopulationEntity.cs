using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

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
