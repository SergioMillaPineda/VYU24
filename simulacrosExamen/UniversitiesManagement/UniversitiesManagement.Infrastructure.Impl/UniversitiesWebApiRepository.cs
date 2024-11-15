using System.Text.Json;
using UniversitiesManagement.Infrastructure.Contracts;
using UniversitiesManagement.Infrastructure.Contracts.WebApiEntities;
using UniversitiesManagement.Infrastructure.Impl.ExternalConnections;

namespace UniversitiesManagement.Infrastructure.Impl
{
    public class UniversitiesWebApiRepository : IUniversitiesWebApiRepository
    {
        public async Task<List<UniversityWebApiEntity>?> GetAllAsync()
        {
            HttpResponseMessage dataFromWebApi = await WebApiConnection.GetAll();
            string dataAsString = await dataFromWebApi.Content.ReadAsStringAsync();
            List<UniversityWebApiEntity>? dataDeserialized =
                JsonSerializer.Deserialize<List<UniversityWebApiEntity>>(dataAsString);

            return dataDeserialized;
        }
    }
}
