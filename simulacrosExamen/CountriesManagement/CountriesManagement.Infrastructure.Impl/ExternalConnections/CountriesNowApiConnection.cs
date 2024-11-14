using Microsoft.Extensions.Configuration;

namespace CountriesManagement.Infrastructure.Impl.ExternalConnections
{
    public class CountriesNowApiConnection
    {
        private const string COUNTRIESPOPURLCONFIGKEY = "AllCountriesPopulationUrl";
        private readonly string _allCountriesPopulationUrl;

        public CountriesNowApiConnection(IConfiguration configuration)
        {
            _allCountriesPopulationUrl = configuration[COUNTRIESPOPURLCONFIGKEY] ?? "";
        }

        public async Task<HttpResponseMessage> Get()
        {
            using HttpClient client = new();
            return await client.GetAsync(_allCountriesPopulationUrl);
        }
    }
}
