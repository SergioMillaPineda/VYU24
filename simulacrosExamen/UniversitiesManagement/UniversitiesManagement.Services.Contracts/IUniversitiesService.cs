using UniversitiesManagement.Services.Contracts.Dtos;

namespace UniversitiesManagement.Services.Contracts
{
    public interface IUniversitiesService
    {
        Task<MigrateAllRsDto> MigrateAllAsync();
        ListAllRsDto ListAllAsync();
        List<UniversityNameAndWebpageListDto> FilterByName(string name);
        List<UniversityNameDto> FilterByAlphaTwoCode(string alphaTwoCode);
    }
}
