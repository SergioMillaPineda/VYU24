using SWApi.Infrastructure.Contracts.Entities.SWApi;

namespace SWApi.Infrastructure.Contracts
{
    public interface ISWApiPeopleRepository
    {
        Task<PeopleSWApiEntity?> TryGetByUrl(string url);
    }
}
