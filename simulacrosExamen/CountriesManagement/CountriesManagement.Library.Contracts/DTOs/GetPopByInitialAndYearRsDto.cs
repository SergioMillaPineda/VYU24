using CountriesManagement.Domain;
using CountriesManagement.Enums;

namespace CountriesManagement.Library.Contracts.DTOs
{
    public class GetPopByInitialAndYearRsDto
    {
        public List<CountryYearPopulation>? data;
        public List<GetPopByInitialAndYearErrorEnum>? errors;
    }
}
