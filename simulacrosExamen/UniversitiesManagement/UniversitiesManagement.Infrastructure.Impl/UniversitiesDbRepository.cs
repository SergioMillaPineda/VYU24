using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;
using UniversitiesManagement.Infrastructure.Contracts;
using UniversitiesManagement.Infrastructure.Contracts.DbEntities;
using UniversitiesManagement.Infrastructure.Impl.ExternalConnections;

namespace UniversitiesManagement.Infrastructure.Impl
{
    public class UniversitiesDbRepository : IUniversitiesDbRepository
    {
        private readonly UniversitiesManagementContext _dbContext;

        public UniversitiesDbRepository(UniversitiesManagementContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void SaveAll(List<UniversitiesDbTableRow> dataToSave)
        {
            _dbContext.Universities.AddRange(dataToSave);
            _dbContext.SaveChanges();
        }

        public List<UniversitiesDbTableRow> GetAll()
        {
            return _dbContext.Universities.ToList();
        }

        public List<UniversitiesDbTableRow> GetByName(string name)
        {
            return _dbContext.Universities
                .Include(x => x.WebPages)
                .Where(x => x.Name.ToLower().Contains(name.ToLower()))
                .ToList();
        }

        public List<UniversitiesDbTableRow> GetByAlphaTwoCode(string alphaTwoCode)
        {
            return _dbContext.Universities
                .Where(x => x.AlphaTwoCode.ToLower().Contains(alphaTwoCode.ToLower()))
                .ToList();
        }
    }
}
