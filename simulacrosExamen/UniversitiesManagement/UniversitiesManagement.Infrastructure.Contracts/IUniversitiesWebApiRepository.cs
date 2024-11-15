using UniversitiesManagement.Infrastructure.Contracts.WebApiEntities;

namespace UniversitiesManagement.Infrastructure.Contracts
{
    public interface IUniversitiesWebApiRepository
    {
        Task<List<UniversityWebApiEntity>?> GetAllAsync();
    }
}
