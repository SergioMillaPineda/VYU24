using WorkersManagementAdminMode.Library.Contracts.DTOs;

namespace WorkersManagementAdminMode.Library.Contracts
{
    public interface IITWorkerService
    {
        void Register(RegisterITWorkerRqDTO registerITWorkerRqDTO);
    }
}
