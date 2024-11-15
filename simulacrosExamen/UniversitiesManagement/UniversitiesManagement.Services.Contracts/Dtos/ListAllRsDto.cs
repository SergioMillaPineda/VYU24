using UniversitiesManagement.Enums;

namespace UniversitiesManagement.Services.Contracts.Dtos
{
    public class ListAllRsDto
    {
        public List<ErrorsEnum>? errors;
        public List<UniversityNameAndCountryDto>? data;
    }
}
