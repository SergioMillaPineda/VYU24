using SWApi.Services.Contracts.Dtos;

namespace SWApi.Services.Contracts
{
    public interface IResidentsService
    {
        Task<GetResidentsByPlanetNameRsDto> GetResidentsByPlanetName(string planetName);
    }
}
