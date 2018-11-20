using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ForumApi.Validations
{
    public class PersonNameAttribute : ValidationAttribute
    {
        private const string NAME_REGEX = @"^([A-ZÀ-ÝẠ-ỸĂĐĨŨƠƯ][a-zà-ýạ-ỹăđĩũơư]*)(([',.-]$)|(([',.-]?[ ]|[ ])([A-ZÀ-ÝẠ-ỸĂĐĨŨƠƯ][a-zà-ýạ-ỹăđĩũơư]*)))*$";

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string firstOrLastName = value.ToString();
            if (!string.IsNullOrWhiteSpace(firstOrLastName) && !IsValidUsername(firstOrLastName))
            {
                return new ValidationResult(GetErrorMessage(validationContext));
            }
            return ValidationResult.Success;
        }

        private bool IsValidUsername(string username)
        {
            Regex regex = new Regex(NAME_REGEX);
            return regex.IsMatch(username);
        }

        private string GetErrorMessage(ValidationContext validationContext)
        {
            if (!string.IsNullOrEmpty(this.ErrorMessage))
                return this.ErrorMessage;
            return $"Please enter your {validationContext.DisplayName} in format: Youname";
        }
    }
}
