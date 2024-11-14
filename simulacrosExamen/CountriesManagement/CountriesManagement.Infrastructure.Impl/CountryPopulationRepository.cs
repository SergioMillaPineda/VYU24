using CountriesManagement.Infrastructure.Contracts;
using CountriesManagement.Infrastructure.Contracts.Entities;
using CountriesManagement.Infrastructure.Impl.ExternalConnections;
using System.Text.Json;

namespace CountriesManagement.Infrastructure.Impl
{
    public class CountryPopulationRepository : ICountryPopulationRepository
    {
        private readonly CountriesNowApiConnection _apiConnection;

        public CountryPopulationRepository(CountriesNowApiConnection apiConnection)
        {
            _apiConnection = apiConnection;
        }

        public async Task<AllCountriesPopulationEntity?> GetPopulationForAllCountries()
        {
            HttpResponseMessage dataFromWebApi = await _apiConnection.Get();
            string dataAsString = await dataFromWebApi.Content.ReadAsStringAsync();
            AllCountriesPopulationEntity? dataDeserialized = JsonSerializer.Deserialize<AllCountriesPopulationEntity>(dataAsString);

            return dataDeserialized;
        }

        public async Task<AllCountriesPopulationEntity> GetPopulationByCountryInitialAndYear(char initial, int year)
        {
            AllCountriesPopulationEntity result = new()
            {
                Error = true
            };

            AllCountriesPopulationEntity? allData = await GetPopulationForAllCountries();
            if (allData != null)
            {
                if (!allData.Error)
                {
                    result.Error = false;
                    result.Data = allData.Data?.Where(x => x.CountryName?.ToLower()[0] == initial)
                        .Select(x => new CountryPopulationEntity
                        {
                            PopulationListByYear = x.PopulationListByYear?.Where(y => y.Year == year).ToList()
                        }).ToList();
                }
            }

            return result;
        }
    }
}
