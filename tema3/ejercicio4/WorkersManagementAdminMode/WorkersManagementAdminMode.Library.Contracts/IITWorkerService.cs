using WorkersManagementAdminMode.Library.Contracts.DTOs;

namespace WorkersManagementAdminMode.Library.Contracts
{
    public interface IITWorkerService
    {
        RegisterITWorkerRsDTO Register(RegisterITWorkerRqDTO registerITWorkerRqDTO);
    }
}
