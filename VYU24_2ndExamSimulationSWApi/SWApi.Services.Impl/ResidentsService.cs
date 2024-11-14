using SWApi.Enums;
using SWApi.Infrastructure.Contracts;
using SWApi.Infrastructure.Contracts.Entities.SWApi;
using SWApi.Infrastructure.Contracts.Entities.SWDB;
using SWApi.Services.Contracts;
using SWApi.Services.Contracts.Dtos;

namespace SWApi.Services.Impl
{
    public class ResidentsService : IResidentsService
    {
        private readonly ISWDBPlanetsRepository _swdbPlanetsRepository;
        private readonly ISWApiPlanetsRepository _swapiPlanetsRepository;
        private readonly ISWApiPeopleRepository _swapiPeopleRepository;

        public ResidentsService(
            ISWDBPlanetsRepository swdbPlanetsRepository,
            ISWApiPlanetsRepository swapiPlanetsRepository,
            ISWApiPeopleRepository swapiPeopleRepository)
        {
            _swdbPlanetsRepository = swdbPlanetsRepository;
            _swapiPlanetsRepository = swapiPlanetsRepository;
            _swapiPeopleRepository = swapiPeopleRepository;
        }

        public async Task<GetResidentsByPlanetNameRsDto> GetResidentsByPlanetName(string planetName)
        {
            GetResidentsByPlanetNameRsDto result = new();

            try
            {
                PlanetsTableRowEntity? candidatePlanet = _swdbPlanetsRepository.TryGet(planetName);
                if (candidatePlanet != null)
                {
                    PlanetResidentListSWApiEntity? residentUrlList = await _swapiPlanetsRepository.TryGetPlanetResidentListByPlanetUrl(candidatePlanet.Url);
                    if (residentUrlList != null && residentUrlList.ResidentUrlList != null)
                    {
                        foreach (string residentUrl in residentUrlList.ResidentUrlList)
                        {
                            PeopleSWApiEntity? candidateResident = await _swapiPeopleRepository.TryGetByUrl(residentUrl);
                            if (candidateResident != null)
                            {
                                result.data ??= new List<string>();
                                result.data.Add(candidateResident.Name);
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                result.errors ??= new List<GetResidentsByPlanetNameErrorEnum>();
                result.errors.Add(GetResidentsByPlanetNameErrorEnum.ServiceError);
            }

            return result;
        }
    }
}
