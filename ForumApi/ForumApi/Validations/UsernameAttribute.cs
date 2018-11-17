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
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string username = value.ToString();
            if (!string.IsNullOrWhiteSpace(username) && !IsValidUsername(username))
            {
                return new ValidationResult(GetErrorMessage());
            }
            return ValidationResult.Success;
        }

        private bool IsValidUsername(string username)
        {
            const string usernameRegex = @"^[a-zA-Z][a-zA-Z0-9]{2,19}$";
            Regex regex = new Regex(usernameRegex);
            return regex.IsMatch(username);
        }

        private string GetErrorMessage()
        {
            if (!string.IsNullOrEmpty(this.ErrorMessage))
                return this.ErrorMessage;
            return "A username can only contain alphanumeric characters (letters a-zA-Z, numbers 0-9)";
        }
    }
}
