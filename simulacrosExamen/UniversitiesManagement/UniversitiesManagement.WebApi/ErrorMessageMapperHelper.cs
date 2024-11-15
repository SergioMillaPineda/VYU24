using UniversitiesManagement.Enums;

namespace UniversitiesManagement.WebApi
{
    public static class ErrorMessageMapperHelper
    {
        public static List<string> MapListAllErrorsEnumToStringMessages(List<ErrorsEnum> enums)
        {
            return enums.Select(x =>
            {
                return x switch
                {
                    ErrorsEnum.WebApiConnectionError => "Error occured while trying to connect to Universities external Api",
                    ErrorsEnum.WebApiDataDeserializationExceptionError => "Error occured while trying to deserialize data from Universities external Api",
                    ErrorsEnum.WebApiDataDeserializationReturnsNullError => "null value returned by deserializer when trying to deserialize data from Universities external Api",
                    ErrorsEnum.DbSaveError => "Error occured while trying to save data into Universities external Db",
                    _ => "Unknown error occured",
                };
            }).ToList();
        }
    }
}
