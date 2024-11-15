using System.Text.Json.Serialization;

namespace UniversitiesManagement.Services.Contracts.Dtos
{
    public class UniversityNameAndCountryDto
    {
        public string? Name { get; set; }

        public string? Country { get; set; }
    }
}
