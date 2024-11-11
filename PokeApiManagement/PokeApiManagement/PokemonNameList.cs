using System.ComponentModel.DataAnnotations;

namespace PokeApiManagement
{
    public class PokemonNameList
    {
        [Required]
        public List<string?>? Names { get; set; }

        public int CountNumberPokemonWithFirstLetterOfInput(char firstLetter)
        {
            return Names?.Count(x => x != null && x.Length >= 1 && x[0] == firstLetter) ?? 0;
        }
    }
}
