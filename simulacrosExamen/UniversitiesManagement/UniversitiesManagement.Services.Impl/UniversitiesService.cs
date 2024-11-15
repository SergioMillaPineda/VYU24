using System.Text.Json;
using System.Xml.Linq;
using UniversitiesManagement.Enums;
using UniversitiesManagement.Infrastructure.Contracts;
using UniversitiesManagement.Infrastructure.Contracts.DbEntities;
using UniversitiesManagement.Infrastructure.Contracts.WebApiEntities;
using UniversitiesManagement.Services.Contracts;
using UniversitiesManagement.Services.Contracts.Dtos;

namespace UniversitiesManagement.Services.Impl
{
    public class UniversitiesService : IUniversitiesService
    {
        private readonly IUniversitiesWebApiRepository _universitiesWebApiRepository;
        private readonly IUniversitiesDbRepository _universitiesDbRepository;

        public UniversitiesService(
            IUniversitiesWebApiRepository universitiesWebApiRepository,
            IUniversitiesDbRepository universitiesDbRepository)
        {
            _universitiesWebApiRepository = universitiesWebApiRepository;
            _universitiesDbRepository = universitiesDbRepository;
        }

        #region MigrateAll
        public async Task<MigrateAllRsDto> MigrateAllAsync()
        {
            MigrateAllRsDto result = new();

            try
            {
                List<UniversityWebApiEntity>? resultFromRepository =
                        await _universitiesWebApiRepository.GetAllAsync();

                if (resultFromRepository == null)
                {
                    result.errors ??= new();
                    result.errors.Add(ErrorsEnum.WebApiDataDeserializationReturnsNullError);
                }
                else
                {
                    List<UniversitiesDbTableRow> dbEntityList =
                        MapWebApiEntityListToDbEntityList(resultFromRepository);
                    try
                    {
                        _universitiesDbRepository.SaveAll(dbEntityList);
                    }
                    catch (Exception)
                    {
                        result.errors ??= new();
                        result.errors.Add(ErrorsEnum.DbSaveError);
                    }
                }
            }
            catch (Exception ex)
            {
                if (ex is InvalidOperationException ||
                    ex is HttpRequestException ||
                    ex is TaskCanceledException)
                {
                    result.errors ??= new();
                    result.errors.Add(ErrorsEnum.WebApiConnectionError);
                }
                else if (ex is ArgumentNullException ||
                    ex is JsonException ||
                    ex is NotSupportedException)
                {
                    result.errors ??= new();
                    result.errors.Add(ErrorsEnum.WebApiDataDeserializationExceptionError);
                }
            }

            return result;
        }

        private static List<UniversitiesDbTableRow> MapWebApiEntityListToDbEntityList(
            List<UniversityWebApiEntity> webApiEntityList)
        {
            List<UniversitiesDbTableRow> dbEntityList = new();
            for (int i = 0; i < webApiEntityList.Count; i++)
            {
                UniversityWebApiEntity currentWebApiEntity = webApiEntityList[i];
                UniversitiesDbTableRow rowToAdd = new()
                {
                    IdFromWebApi = i,
                    Name = currentWebApiEntity.Name,
                    AlphaTwoCode = currentWebApiEntity.Code,
                    StateProvince = currentWebApiEntity.StateProvince,
                    Country = currentWebApiEntity.Country,
                    WebDomains = currentWebApiEntity.DomainList?.Select(domainName =>
                    new WebDomainDbTableRow
                    {
                        DomainName = domainName
                    })
                    .ToList() ?? new List<WebDomainDbTableRow>(),
                    WebPages = currentWebApiEntity.WebPageList?.Select(webPageName =>
                    new WebPageDbTableRow
                    {
                        WebPageName = webPageName
                    })
                    .ToList() ?? new List<WebPageDbTableRow>()
                };
                dbEntityList.Add(rowToAdd);
            }

            return dbEntityList;
        }
        #endregion

        #region ListAll
        public ListAllRsDto ListAllAsync()
        {
            ListAllRsDto result = new();

            List<UniversitiesDbTableRow> resultFromRepository =
                        _universitiesDbRepository.GetAll();
            result.data = MapWebApiEntityListToDtoList(resultFromRepository);

            return result;
        }

        private static List<UniversityNameAndCountryDto> MapWebApiEntityListToDtoList(
            List<UniversitiesDbTableRow> dbEntityList)
        {
            return dbEntityList.Select(x => new UniversityNameAndCountryDto()
            {
                Name = x.Name,
                Country = x.Country
            }).ToList();
        }
        #endregion

        #region FilterByName
        public List<UniversityNameAndWebpageListDto> FilterByName(string name)
        {
            List<UniversitiesDbTableRow> resultFromRepository =
                        _universitiesDbRepository.GetByName(name);
            return MapDbEntityListToUniversityNameAndWebpageDtoList(resultFromRepository);
        }

        private static List<UniversityNameAndWebpageListDto> MapDbEntityListToUniversityNameAndWebpageDtoList(
            List<UniversitiesDbTableRow> dbEntityList)
        {
            return dbEntityList.Select(x => new UniversityNameAndWebpageListDto()
            {
                Name = x.Name,
                WebpageList = x.WebPages.Select(x => x.WebPageName).ToList()
            }).ToList();
        }
        #endregion

        #region FilterByAlphaTwoCode
        public List<UniversityNameDto> FilterByAlphaTwoCode(string alphaTwoCode)
        {
            List<UniversityNameAndWebpageListDto> result = new();

            List<UniversitiesDbTableRow> resultFromRepository =
                        _universitiesDbRepository.GetByAlphaTwoCode(alphaTwoCode);
            return MapDbEntityListToUniversityNameDtoList(resultFromRepository);
        }

        private static List<UniversityNameDto> MapDbEntityListToUniversityNameDtoList(
            List<UniversitiesDbTableRow> dbEntityList)
        {
            return dbEntityList.Select(x => new UniversityNameDto()
            {
                Name = x.Name
            }).ToList();
        } 
        #endregion
    }
}
