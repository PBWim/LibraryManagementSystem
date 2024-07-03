using System.ComponentModel.DataAnnotations;

namespace Library.Shared.Validations
{
	public class NoFutureDateAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return true; // Consider null values as valid. Use [Required] attribute for null checks.
            }

            var dateValue = (DateTime)value;
            return dateValue <= DateTime.Now;
        }

        public override string FormatErrorMessage(string name)
        {
            return $"The {name} field cannot be a future date.";
        }
    }
}