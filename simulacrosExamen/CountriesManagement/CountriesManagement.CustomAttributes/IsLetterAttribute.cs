using System.ComponentModel.DataAnnotations;

namespace CountriesManagement.CustomAttributes
{
    public class IsLetterAttribute : ValidationAttribute
    {
        public IsLetterAttribute()
        { }

        protected override ValidationResult? IsValid(
               object? value, ValidationContext validationContext)
        {

        if (value != null && !char.IsLetter((char)value))
            {
                return new ValidationResult("The field CountryInitial must be in [a-zA-Z]");
            }
            return null; // everything OK
        }
    }
}
