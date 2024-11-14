using SWApi.Infrastructure.Contracts.Entities.SWDB;

namespace SWApi.Infrastructure.Contracts
{
    public interface ISWDBPlanetsRepository
    {
        void InsertOrUpdate(List<PlanetsTableRowEntity> dataToInsertOrUpdate);

        PlanetsTableRowEntity? TryGet(string planetName);
    }
}
