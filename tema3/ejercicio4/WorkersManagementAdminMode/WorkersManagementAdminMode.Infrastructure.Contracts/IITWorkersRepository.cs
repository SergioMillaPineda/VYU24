using WorkersManagementAdminMode.Infrastructure.Contracts.Entities;

namespace WorkersManagementAdminMode.Infrastructure.Contracts
{
    public interface IITWorkersRepository
    {
        void Register(ITWorkerEntity newElement);
    }
}
