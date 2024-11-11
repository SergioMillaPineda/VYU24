using Microsoft.AspNetCore.Mvc;
using PokeApiManagement.DbContext;
using PokeApiManagement.EntitiesDB;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace PokeApiManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PokeApiController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> ShowPokemonStatistics(string firstCharacterAsString)
        {
            if (firstCharacterAsString.Length != 1)
            {
                return NotFound("El parámetro de entrada debe ser un solo caracter");
            }

            char firstCharacter = firstCharacterAsString[0];

            PokeApiContext dbContext = new();
            PokemonNameList domainModel;
            using HttpClient client = new();
            try
            {
                HttpResponseMessage data = await client.GetAsync("https://pokeapi.co/api/v2/pokemon?limit=151");
                string dataAsString = await data.Content.ReadAsStringAsync();
                PokemonPageFromJsonEntity? dataDeserialized = JsonSerializer.Deserialize<PokemonPageFromJsonEntity>(dataAsString);

                domainModel = new()
                {
                    Names = dataDeserialized?.PokemonList?.Select(x => x.Name).ToList()
                };
            }
            catch (HttpRequestException)
            {
                List<Pokemon>? pokemonsFromDB = dbContext.Pokemon.ToList();
                if (pokemonsFromDB != null)
                {
                    domainModel = new()
                    {
                        Names = pokemonsFromDB?.Select(x => x.Name).ToList()
                    };
                }
                else
                {
                    return BadRequest("Pokemon list not found");
                }
            }

            var results = new List<ValidationResult>();
            var context = new ValidationContext(domainModel, null, null);
            if (!Validator.TryValidateObject(domainModel, context, results))
            {
                return BadRequest(results.Select(x => x.ErrorMessage));
            }

            int result = domainModel.CountNumberPokemonWithFirstLetterOfInput(firstCharacter);

            PokeStatisticsByInitial? dataFromDB = dbContext.PokeStatisticsByInitial.FirstOrDefault(x => x.initial == firstCharacter.ToString());
            if (dataFromDB == null)
            {
                dbContext.PokeStatisticsByInitial.Add(new PokeStatisticsByInitial
                {
                    initial = firstCharacter.ToString(),
                    counter = result
                });
            }
            else
            {
                dataFromDB.counter = result;
            }
            dbContext.SaveChanges();

            return Ok($"El número de pokemon que empiezan por '{firstCharacter}' es {result}");
        }
    }
}
