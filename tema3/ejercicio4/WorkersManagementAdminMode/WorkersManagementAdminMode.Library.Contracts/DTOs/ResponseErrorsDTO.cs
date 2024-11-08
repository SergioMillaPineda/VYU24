namespace WorkersManagementAdminMode.Library.Contracts.DTOs
{
    public class ResponseErrorsDTO
    {
        public bool HasErrors {
            get
            {
                return ErrorCodes.Count > 0;
            }
        }
        public List<int> ErrorCodes = new();
    }
}
