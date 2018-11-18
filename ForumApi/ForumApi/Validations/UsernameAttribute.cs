using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ForumApi.Validations
{
    public class UsernameAttribute : ValidationAttribute
    {
        private const string UsernameRegex = @"^[a-zA-Z][a-zA-Z0-9]{2,19}$";
        private const string ErrorMessageUsername = "A username can only contain alphanumeric characters (letters a-zA-Z, numbers 0-9).";
        
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string username = value.ToString();
            if (!string.IsNullOrWhiteSpace(username) && !IsValidUsername(username))
                return new ValidationResult(GetErrorMessage());
            return ValidationResult.Success;
        }

        private bool IsValidUsername(string username)
        {
            Regex regex = new Regex(UsernameRegex);
            return regex.IsMatch(username);
        }

        private string GetErrorMessage()
        {
            if (!string.IsNullOrEmpty(this.ErrorMessage))
                return this.ErrorMessage;
            return ErrorMessageUsername;
        }
    }
}
