namespace CountriesManagement.Library.Contracts.DTOs
{
    public class CountryYearPopulationDto
    {
        public string country;
        public int year;
        public long population;

        public CountryYearPopulationDto(string country, int year, long population)
        {
            this.country = country;
            this.year = year;
            this.population = population;
        }
    }
}
