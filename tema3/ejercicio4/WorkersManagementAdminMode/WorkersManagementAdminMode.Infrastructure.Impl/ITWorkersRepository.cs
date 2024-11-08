using WorkersManagementAdminMode.Infrastructure.Contracts;
using WorkersManagementAdminMode.Infrastructure.Contracts.Entities;
using WorkersManagementAdminMode.Infrastructure.Impl.DbContexts;

namespace WorkersManagementAdminMode.Infrastructure.Impl
{
    public class ITWorkersRepository : IITWorkersRepository
    {
        private readonly WorkersManagementAdminModeContext _workersManagementAdminModeContext;

        public ITWorkersRepository(WorkersManagementAdminModeContext workersManagementAdminModeContext)
        {
            _workersManagementAdminModeContext = workersManagementAdminModeContext;
        }

        public void Register(ITWorkerEntity newElement)
        {
            _workersManagementAdminModeContext.ITWorkersTable.Add(newElement);
            _workersManagementAdminModeContext.SaveChanges();
        }
    }
}
