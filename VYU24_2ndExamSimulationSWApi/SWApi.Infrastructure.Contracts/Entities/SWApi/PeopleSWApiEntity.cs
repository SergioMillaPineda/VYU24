using System.Text.Json.Serialization;

namespace SWApi.Infrastructure.Contracts.Entities.SWApi
{
    public class PeopleSWApiEntity
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}
