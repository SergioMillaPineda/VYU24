using SWApi.Services.Contracts.Dtos;

namespace SWApi.Services.Contracts
{
    public interface IPlanetsService
    {
        Task<RefreshPlanetsRsDto> RefreshPlanets();
    }
}
