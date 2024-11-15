using UniversitiesManagement.Infrastructure.Contracts.DbEntities;

namespace UniversitiesManagement.Infrastructure.Contracts
{
    public interface IUniversitiesDbRepository
    {
        void SaveAll(List<UniversitiesDbTableRow> dataToSave);
        List<UniversitiesDbTableRow> GetAll();
        List<UniversitiesDbTableRow> GetByName(string name);
        List<UniversitiesDbTableRow> GetByAlphaTwoCode(string alphaTwoCode);
    }
}
