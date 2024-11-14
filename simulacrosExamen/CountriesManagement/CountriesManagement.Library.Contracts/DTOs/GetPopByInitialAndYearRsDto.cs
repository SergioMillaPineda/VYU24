using CountriesManagement.Enums;

namespace CountriesManagement.Library.Contracts.DTOs
{
    public class GetPopByInitialAndYearRsDto
    {
        public List<CountryYearPopulationDto>? data;
        public List<GetPopByInitialAndYearErrorEnum>? errors;
    }
}
