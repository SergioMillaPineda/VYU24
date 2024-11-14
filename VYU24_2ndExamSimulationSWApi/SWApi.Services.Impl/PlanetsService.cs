using SWApi.Infrastructure.Contracts;
using SWApi.Infrastructure.Contracts.Entities.SWApi;
using SWApi.Infrastructure.Contracts.Entities.SWDB;
using SWApi.Services.Contracts;
using SWApi.Services.Contracts.Dtos;

namespace SWApi.Services.Impl
{
    public class PlanetsService : IPlanetsService
    {
        private readonly ISWApiPlanetsRepository _swapiPlanetsRepository;
        private readonly ISWDBPlanetsRepository _swdbPlanetsRepository;

        public PlanetsService(ISWApiPlanetsRepository swapiPlanetsRepository,
            ISWDBPlanetsRepository swdbPlanetsRepository)
        {
            _swapiPlanetsRepository = swapiPlanetsRepository;
            _swdbPlanetsRepository = swdbPlanetsRepository;
        }

        public async Task<RefreshPlanetsRsDto> RefreshPlanets()
        {
            RefreshPlanetsRsDto result = new();

            List<PlanetStatisticsSWApiEntity> swapiEntityList;
            List<PlanetsTableRowEntity> dbEntityList;
            try
            {
                swapiEntityList = await _swapiPlanetsRepository.GetAll();
            }
            catch (Exception)
            {
                result.errors = new List<Enums.RefreshPlanetsErrorEnum> { Enums.RefreshPlanetsErrorEnum.SWApiErrorConnection };
                return result;
            }
            try
            {
                dbEntityList = swapiEntityList.Select(x => new PlanetsTableRowEntity
                {
                    Name = x.PlanetName,
                    RotationPeriod = x.RotationPeriod,
                    OrbitalPeriod = x.OrbitalPeriod,
                    Climate = x.Climate,
                    Population = x.Population == "unknown" ? 0 : long.Parse(x.Population),
                    Url = x.DetailedInfoUrl
                }).ToList();
            }
            catch (Exception)
            {
                result.errors = new List<Enums.RefreshPlanetsErrorEnum> { Enums.RefreshPlanetsErrorEnum.EntityMappingConnection };
                return result;
            }
            try
            {
                _swdbPlanetsRepository.InsertOrUpdate(dbEntityList);
            }
            catch (Exception)
            {
                result.errors = new List<Enums.RefreshPlanetsErrorEnum> { Enums.RefreshPlanetsErrorEnum.SWDbErrorConnection };
                return result;
            }

            result.data = swapiEntityList.Select(x => x.PlanetName).ToList();
            return result;
        }
    }
}
