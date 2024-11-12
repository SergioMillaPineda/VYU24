namespace CountriesManagement.Domain
{
    public class CountryYearPopulationList
    {
        private readonly List<CountryYearPopulation> _list;

        public CountryYearPopulationList(List<CountryYearPopulation> data)
        {
            _list = data;
        }

        public List<CountryYearPopulation> FilterByCountryInitialAndYear(char initial, int year)
        {
            return _list.Where(x => x.Country.ToLower().StartsWith(char.ToLower(initial)) && x.Year == year).ToList();
        }
    }
}
