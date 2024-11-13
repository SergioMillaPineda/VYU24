using CountriesManagement.Domain;
using CountriesManagement.Enums;
using CountriesManagement.Infrastructure.Contracts;
using CountriesManagement.Infrastructure.Contracts.Entities;
using CountriesManagement.Library.Contracts;
using CountriesManagement.Library.Contracts.DTOs;
using System.ComponentModel.DataAnnotations;

namespace CountriesManagement.Library.Impl
{
    public class CountryPopulationService : ICountryPopulationService
    {
        private readonly ICountryPopulationRepository _countryPopulationRepository;

        public CountryPopulationService(ICountryPopulationRepository countryPopulationRepository)
        {
            _countryPopulationRepository = countryPopulationRepository;
        }

        public async Task<GetPopByInitialAndYearRsDto> GetPopulationByInitialAndYear(QueryDataDto queryDataDto)
        {
            GetPopByInitialAndYearRsDto result = new();

            QueryData queryData = MapDtoToDomain(queryDataDto);
            List<GetPopByInitialAndYearErrorEnum> errorResults = ValidateInput(queryData);
            if (errorResults.Count > 0)
            {
                result.errors = errorResults;
            }
            else
            {
                try
                {
                    AllCountriesPopulationEntity? entity =
                                await _countryPopulationRepository.GetPopulationForAllCountries();
                    if (entity != null)
                    {
                        List<CountryYearPopulation> allCountriesPopForAllYears = MapEntityToDomain(entity);
                        CountryYearPopulationList domainObjectToProcessFiltering =
                            new(allCountriesPopForAllYears);

                        List<CountryYearPopulation> filteredData =
                            domainObjectToProcessFiltering.FilterByCountryInitialAndYear(
                                queryData.CountryInitial,
                                queryData.Year
                            );
                        result.data = filteredData;
                    }
                }
                catch (Exception)
                {
                    result.errors ??= new List<GetPopByInitialAndYearErrorEnum>();
                    result.errors.Add(GetPopByInitialAndYearErrorEnum.DBError);
                }
            }

            return result;
        }

        private static QueryData MapDtoToDomain(QueryDataDto queryDataDto)
        {
            return new QueryData(queryDataDto.CountryInitial, queryDataDto.Year);
        }

        private static List<GetPopByInitialAndYearErrorEnum> ValidateInput(QueryData queryData)
        {
            List<GetPopByInitialAndYearErrorEnum> result = new();

            var validationResults = queryData.Validate();
            if (validationResults != null)
            {
                foreach (ValidationResult validationError in validationResults)
                {
                    if (!string.IsNullOrEmpty(validationError.ErrorMessage))
                    {
                        if (validationError.ErrorMessage.Contains("CountryInitial"))
                        {
                            result.Add(GetPopByInitialAndYearErrorEnum.InvalidInitial);
                        }
                        else if (validationError.ErrorMessage.Contains("Year"))
                        {
                            result.Add(GetPopByInitialAndYearErrorEnum.InvalidYear);
                        }
                    }
                }
            }

            return result;
        }

        private static List<CountryYearPopulation> MapEntityToDomain(AllCountriesPopulationEntity entity)
        {
            List<CountryYearPopulation> result = new();

            return entity?.Data?.SelectMany(x =>
                x.PopulationListByYear?.Select(y =>
                    new CountryYearPopulation(x.Country ?? "", y.Year, y.Value)
                ) ?? new List<CountryYearPopulation>()
            ).ToList() ?? new List<CountryYearPopulation>();
        }
    }
}
