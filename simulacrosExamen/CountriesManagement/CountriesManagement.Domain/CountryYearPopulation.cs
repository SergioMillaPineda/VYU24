namespace CountriesManagement.Domain
{
    public class CountryYearPopulation
    {
        public string Country { get; set; }

        public int Year { get; set; }

        public long Population { get; set; }

        public CountryYearPopulation(string country, int year, long population)
        {
            Country = country;
            Year = year;
            Population = population;
        }
    }
}
