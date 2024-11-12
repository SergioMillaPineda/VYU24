using CountriesManagement.Library.Contracts.DTOs;

namespace CountriesManagement.Library.Contracts
{
    public interface ICountryPopulationService
    {
        Task<GetPopByInitialAndYearRsDto> GetPopulationByInitialAndYear(QueryDataDto queryDataDto);
    }
}
