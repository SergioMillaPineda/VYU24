using System.Text.Json.Serialization;

namespace PokeApiManagement
{
    public class PokemonPageFromJsonEntity
    {
        [JsonPropertyName("count")]
        public int Count { get; set; }
        [JsonPropertyName("next")]
        public string? Next { get; set; }
        [JsonPropertyName("previous")]
        public string? Previous { get; set; }
        [JsonPropertyName("results")]
        public List<PokemonInfoFromJsonEntity>? PokemonList { get; set; }
    }
}


