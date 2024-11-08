using WorkersManagementAdminMode.Domain.DomainModels;
using WorkersManagementAdminMode.Infrastructure.Contracts;
using WorkersManagementAdminMode.Infrastructure.Contracts.Entities;
using WorkersManagementAdminMode.Library.Contracts;
using WorkersManagementAdminMode.Library.Contracts.DTOs;
using WorkersManagementAdminMode.XCutting.Enums;

namespace WorkersManagementAdminMode.Library.Impl
{
    public class ITWorkerService : IITWorkerService
    {
        private readonly IITWorkersRepository _iTWorkersRepository;

        public ITWorkerService(IITWorkersRepository iTWorkersRepository)
        {
            _iTWorkersRepository = iTWorkersRepository;
        }

        public RegisterITWorkerRsDTO Register(RegisterITWorkerRqDTO registerITWorkerRqDTO)
        {
            RegisterITWorkerRsDTO result = new();

            ITWorker newWorker = MapDtoToDomainModel(registerITWorkerRqDTO);

            result.errors = ValidateWorker(newWorker);
            if (!result.errors.HasErrors)
            {
                ITWorkerEntity newItWorker = MapDomainModelToEntity(newWorker);

                try
                {
                    _iTWorkersRepository.Register(newItWorker);
                    ITWorkerRsDTO itWorker = MapEntityToDto(newItWorker);
                    result.itWorker = itWorker;
                }
                catch (Exception)
                {
                    result.errors.ErrorCodes.Add((int)RegisterITWorkerRsErrorsEnum.DBError);
                }
            }

            return result;
        }

        private static ResponseErrorsDTO ValidateWorker(ITWorker newWorker)
        {
            ResponseErrorsDTO result = new();

            if (!newWorker.IsValidLevel)
            {
                result.ErrorCodes.Add((int)RegisterITWorkerRsErrorsEnum.InvalidLevel);
            }
            if (!newWorker.CanWork)
            {
                result.ErrorCodes.Add((int)RegisterITWorkerRsErrorsEnum.CannotWork);
            }

            return result;
        }

        private static ITWorker MapDtoToDomainModel(RegisterITWorkerRqDTO iTWorker)
        {
            return new(
                iTWorker.Name,
                iTWorker.Surname,
                iTWorker.BirthDate,
                iTWorker.YearsOfExperience,
                iTWorker.TechKnowledges,
                iTWorker.Level
            );
        }

        private static ITWorkerEntity MapDomainModelToEntity(ITWorker iTWorker)
        {
            return new()
            {
                idWorkerNavigation = new()
                {
                    Name = iTWorker.Name,
                    Surname = iTWorker.Surname,
                    Birthdate = iTWorker.BirthDate
                },
                YearsOfExperience = iTWorker.YearsOfExperience,
                TechKnowledges = string.Join(",", iTWorker.TechKnowledges),
                Level = iTWorker.Level
            };
        }

        private static ITWorkerRsDTO MapEntityToDto(ITWorkerEntity iTWorkerEntity)
        {
            return new ITWorkerRsDTO(
                iTWorkerEntity.idWorker,
                iTWorkerEntity.idWorkerNavigation.Name,
                iTWorkerEntity.idWorkerNavigation.Surname,
                iTWorkerEntity.idWorkerNavigation.Birthdate,
                iTWorkerEntity.YearsOfExperience,
                iTWorkerEntity.TechKnowledges.Split(',').ToList(),
                iTWorkerEntity.Level
            );
        }
    }
}
