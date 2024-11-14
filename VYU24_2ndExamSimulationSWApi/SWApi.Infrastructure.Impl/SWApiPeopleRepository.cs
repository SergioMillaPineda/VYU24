using SWApi.Infrastructure.Contracts;
using SWApi.Infrastructure.Contracts.Entities.SWApi;
using SWApi.Infrastructure.Impl.ExternalConnections;
using System.Text.Json;

namespace SWApi.Infrastructure.Impl
{
    public class SWApiPeopleRepository : ISWApiPeopleRepository
    {
        public async Task<PeopleSWApiEntity?> TryGetByUrl(string url)
        {
            HttpResponseMessage dataFromWebApi = await SWApiContext.GetByFullUrlAsync(url);
            string dataAsString = await dataFromWebApi.Content.ReadAsStringAsync();

            JsonSerializerOptions deserializerOptions = new()
            {
                PropertyNameCaseInsensitive = true
            };
            PeopleSWApiEntity? dataDeserialized = JsonSerializer.Deserialize<PeopleSWApiEntity?>(dataAsString, deserializerOptions);

            return dataDeserialized;
        }
    }
}
