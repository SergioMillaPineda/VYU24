using SWApi.Enums;

namespace SWApi.Services.Contracts.Dtos
{
    public class GetResidentsByPlanetNameRsDto
    {
        public List<GetResidentsByPlanetNameErrorEnum>? errors;
        public List<string>? data; 
    }
}
