using System.Text.Json.Serialization;

namespace UniversitiesManagement.Infrastructure.Contracts.WebApiEntities
{
    public class UniversityWebApiEntity
    {
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("alpha_two_code")]
        public string? Code { get; set; }

        [JsonPropertyName("domains")]
        public List<string>? DomainList { get; set; }

        [JsonPropertyName("stateprovince")]
        public string? StateProvince { get; set; }

        [JsonPropertyName("country")]
        public string? Country { get; set; }

        [JsonPropertyName("web_pages")]
        public List<string>? WebPageList { get; set; }
    }
}
