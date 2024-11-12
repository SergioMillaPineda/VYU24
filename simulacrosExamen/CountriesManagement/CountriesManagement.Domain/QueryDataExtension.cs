using System.ComponentModel.DataAnnotations;

namespace CountriesManagement.Domain
{
    public static class QueryDataExtension
    {
        public static List<ValidationResult>? Validate(this QueryData myself)
        {
            var validationResults = new List<ValidationResult>();
            var context = new ValidationContext(myself, null, null);
            Validator.TryValidateObject(myself, context, validationResults, true);

            return validationResults;
        }
    }
}
