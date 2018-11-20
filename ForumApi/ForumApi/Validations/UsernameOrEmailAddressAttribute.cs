using ForumApi.Commons;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ForumApi.Validations
{
    public class UsernameOrEmailAddressAttribute : ValidationAttribute
    {
        private const string USERNAME_EMAIL_REGEX = @"^(([a-zA-Z][a-zA-Z0-9]{2,19})|(([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                                       @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)))$";

        private const string MSG_EMAIL_ERR = "Please enter your email address in format: yourname@example.com";
        private const string MSG_USER_ERR = "A username can only contain alphanumeric characters (letters a-zA-Z, numbers 0-9) and cannot be longer than 20 characters.";

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string usernameOrEmailAddress = value.ToString();
            if (!string.IsNullOrWhiteSpace(usernameOrEmailAddress) && !IsValidUsernameOrEmailAddress(usernameOrEmailAddress))
                return new ValidationResult(GetErrorMessage(usernameOrEmailAddress));
            return ValidationResult.Success;
        }

        private bool IsValidUsernameOrEmailAddress(string usernameOrEmailAddress)
        {
            Regex regex = new Regex(USERNAME_EMAIL_REGEX);
            return regex.IsMatch(usernameOrEmailAddress);
        }

        private string GetErrorMessage(string usernameOrEmailAddress)
        {
            if (!string.IsNullOrEmpty(this.ErrorMessage))
                return this.ErrorMessage;
            bool isEmailAddress = usernameOrEmailAddress.IndexOf('@') > -1;
            if (isEmailAddress)
                return MSG_EMAIL_ERR;
            return MSG_USER_ERR;
        }
    }
}
