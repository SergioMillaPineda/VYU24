using Microsoft.Extensions.Configuration;

namespace SWApi.Infrastructure.Impl.ExternalConnections
{
    public class SWApiContext
    {
        private const string SWAPIBASEURLCONFIGKEY = "SWApiBaseUrl";
        private const string SWAPIOUTPUTFORMATQUERYPARAMCONFIGKEY = "SWApiOutputFormatQueryParam";
        private readonly string _SWApiBaseUrl;
        private readonly string _SWApiOutputFormatQUeryParam;

        public SWApiContext(IConfiguration configuration)
        {
            _SWApiBaseUrl = configuration[SWAPIBASEURLCONFIGKEY] ?? "";
            _SWApiOutputFormatQUeryParam = configuration[SWAPIOUTPUTFORMATQUERYPARAMCONFIGKEY] ?? "";
        }

        public async Task<HttpResponseMessage> GetByRelativeUrlAsync(string relativeUrl)
        {
            using HttpClient client = new();
            return await client.GetAsync($"{_SWApiBaseUrl}/{relativeUrl}?{_SWApiOutputFormatQUeryParam}");
        }

        public static async Task<HttpResponseMessage> GetByFullUrlAsync(string fullUrl)
        {
            using HttpClient client = new();
            return await client.GetAsync(fullUrl);
        }
    }
}
