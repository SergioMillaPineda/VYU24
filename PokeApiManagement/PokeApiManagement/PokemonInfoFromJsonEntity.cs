using System.Text.Json.Serialization;

namespace PokeApiManagement
{
    public class PokemonInfoFromJsonEntity
    {
        [JsonPropertyName("name")]
        public string? Name { get; set; }
        [JsonPropertyName("url")]
        public string? Url { get; set; }
    }
}
