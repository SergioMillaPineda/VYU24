using CountriesManagement.CustomAttributes;
using System.ComponentModel.DataAnnotations;

namespace CountriesManagement.Domain
{
    public class QueryData
    {
        [IsLetter]
        public char CountryInitial { get; set; }

        [Range(1961, 2018)]
        public int Year { get; set; }

        public QueryData(char countryInitial, int year)
        {
            CountryInitial = countryInitial;
            Year = year;
        }
    }
}
