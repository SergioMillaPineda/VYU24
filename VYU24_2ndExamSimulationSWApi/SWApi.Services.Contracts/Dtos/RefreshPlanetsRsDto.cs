using SWApi.Enums;

namespace SWApi.Services.Contracts.Dtos
{
    public class RefreshPlanetsRsDto
    {
        public List<RefreshPlanetsErrorEnum>? errors;
        public List<string>? data;
    }
}
