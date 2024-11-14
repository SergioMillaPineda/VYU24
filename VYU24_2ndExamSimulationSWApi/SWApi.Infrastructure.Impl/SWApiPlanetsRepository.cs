using Microsoft.Extensions.Configuration;
using SWApi.Infrastructure.Contracts;
using SWApi.Infrastructure.Contracts.Entities.SWApi;
using SWApi.Infrastructure.Impl.ExternalConnections;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SWApi.Infrastructure.Impl
{
    public class SWApiPlanetsRepository : ISWApiPlanetsRepository
    {
        private readonly string _getAllPlanetsRelativeUrl;
        private readonly SWApiContext _swapiContext;

        public SWApiPlanetsRepository(IConfiguration config, SWApiContext swapiContext)
        {
            _getAllPlanetsRelativeUrl = config["SWApiGetPlanetsRelativeUrl"];
            _swapiContext = swapiContext;
        }

        public async Task<List<PlanetStatisticsSWApiEntity>> GetAll()
        {
            HttpResponseMessage dataFromWebApi = await _swapiContext.GetByRelativeUrlAsync(_getAllPlanetsRelativeUrl);
            string dataAsString = await dataFromWebApi.Content.ReadAsStringAsync();

            JsonSerializerOptions deserializerOptions = new()
            {
                PropertyNameCaseInsensitive = true,
                NumberHandling = JsonNumberHandling.AllowReadingFromString
            };
            PlanetListSWApiEntity? dataDeserialized = JsonSerializer.Deserialize<PlanetListSWApiEntity>(dataAsString, deserializerOptions);

            return dataDeserialized?.Data ?? new List<PlanetStatisticsSWApiEntity>();
        }

        public async Task<PlanetResidentListSWApiEntity?> TryGetPlanetResidentListByPlanetUrl(string url)
        {
            HttpResponseMessage dataFromWebApi = await SWApiContext.GetByFullUrlAsync(url);
            string dataAsString = await dataFromWebApi.Content.ReadAsStringAsync();

            JsonSerializerOptions deserializerOptions = new()
            {
                PropertyNameCaseInsensitive = true,
                NumberHandling = JsonNumberHandling.AllowReadingFromString
            };
            PlanetResidentListSWApiEntity? dataDeserialized = JsonSerializer.Deserialize<PlanetResidentListSWApiEntity?>(dataAsString, deserializerOptions);

            return dataDeserialized;
        }
    }
}
