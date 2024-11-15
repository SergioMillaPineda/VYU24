namespace UniversitiesManagement.Infrastructure.Impl.ExternalConnections
{
    public class WebApiConnection
    {
        private const string _universitiesSearchUrl = "http://universities.hipolabs.com/search";
        public static async Task<HttpResponseMessage> GetAll()
        {
            using HttpClient client = new();
            return await client.GetAsync(_universitiesSearchUrl);
        }
    }
}
