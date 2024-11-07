using WorkersManagementAdminMode.Domain.DomainModels;
using WorkersManagementAdminMode.Library.Contracts;
using WorkersManagementAdminMode.Library.Contracts.DTOs;

namespace WorkersManagementAdminMode.Library.Impl
{
    internal class ITWorkerService : IITWorkerService
    {
        public void Register(RegisterITWorkerRqDTO registerITWorkerRqDTO)
        {
            ITWorker newWorker = new ITWorker(
                registerITWorkerRqDTO.Name,
                registerITWorkerRqDTO.Surname,
                registerITWorkerRqDTO.BirthDate,
                registerITWorkerRqDTO.YearsOfExperience,
                registerITWorkerRqDTO.TechKnowledges,
                registerITWorkerRqDTO.Level
                );
            if(newWorker.IsValidLevel && newWorker.CanBeAssignedAsTeamManager && newWorker.CanWork)
            {

            }
        }
    }
}
