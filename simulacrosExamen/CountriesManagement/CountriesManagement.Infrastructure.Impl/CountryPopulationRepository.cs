using CountriesManagement.Infrastructure.Contracts;
using CountriesManagement.Infrastructure.Contracts.Entities;
using Microsoft.Extensions.Configuration;
using System.Text.Json;

namespace CountriesManagement.Infrastructure.Impl
{
    public class CountryPopulationRepository : ICountryPopulationRepository
    {
        private const string COUNTRIESPOPURLCONFIGKEY = "AllCountriesPopulationUrl";
        private readonly string _allCountriesPopulationUrl;

        public CountryPopulationRepository(IConfiguration configuration)
        {
            _allCountriesPopulationUrl = configuration[COUNTRIESPOPURLCONFIGKEY] ?? "";
        }

        public async Task<AllCountriesPopulationEntity?> GetPopulationForAllCountries()
        {
            using HttpClient client = new();
            HttpResponseMessage data = await client.GetAsync(_allCountriesPopulationUrl);
            string dataAsString = await data.Content.ReadAsStringAsync();
            AllCountriesPopulationEntity? dataDeserialized =
                JsonSerializer.Deserialize<AllCountriesPopulationEntity>(dataAsString);

            return dataDeserialized;
        }
    }
}
