using SWApi.Infrastructure.Contracts.Entities.SWApi;

namespace SWApi.Infrastructure.Contracts
{
    public interface ISWApiPlanetsRepository
    {
        Task<List<PlanetStatisticsSWApiEntity>> GetAll();
        Task<PlanetResidentListSWApiEntity?> TryGetPlanetResidentListByPlanetUrl(string url);
    }
}
