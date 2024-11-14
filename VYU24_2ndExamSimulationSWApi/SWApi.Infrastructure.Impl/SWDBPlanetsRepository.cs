using Microsoft.EntityFrameworkCore;
using SWApi.Infrastructure.Contracts;
using SWApi.Infrastructure.Contracts.Entities.SWDB;
using SWApi.Infrastructure.Impl.ExternalConnections;

namespace SWApi.Infrastructure.Impl
{
    public class SWDBPlanetsRepository : ISWDBPlanetsRepository
    {
        private readonly SWDBContext _swdbContext;

        public SWDBPlanetsRepository(SWDBContext swdbContext)
        {
            _swdbContext = swdbContext;
        }

        public void InsertOrUpdate(List<PlanetsTableRowEntity> dataToInsertOrUpdate)
        {
            foreach (PlanetsTableRowEntity candidateRow in dataToInsertOrUpdate)
            {
                if (_swdbContext.Planets.Any(existingRow => existingRow.Name == candidateRow.Name))
                {
                    PlanetsTableRowEntity rowToUpdate = _swdbContext.Planets.First(existingRow => existingRow.Name == candidateRow.Name);
                    rowToUpdate.RotationPeriod = candidateRow.RotationPeriod;
                    rowToUpdate.OrbitalPeriod = candidateRow.OrbitalPeriod;
                    rowToUpdate.Climate = candidateRow.Climate;
                    rowToUpdate.Population = candidateRow.Population;
                    rowToUpdate.Url = candidateRow.Url;
                }
                else
                {
                    _swdbContext.Planets.Add(candidateRow);
                }
            }
            _swdbContext.SaveChanges();
        }

        public PlanetsTableRowEntity? TryGet(string planetName)
        {
            return _swdbContext.Planets.FirstOrDefault(x => x.Name == planetName);
        }
    }
}
