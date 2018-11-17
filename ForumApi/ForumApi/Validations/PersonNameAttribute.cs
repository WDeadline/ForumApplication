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
                    //tecnical debt: "I don't know the regex text for vietnamese"
        const string personNameRegex = @"^(([A-ZÁÀẢÃẠĂẮẰẲẴẶÂẤẦẨẪẬÉÈẺẼẸÊẾỀỂỄỆĐÍÌỈĨỊÓÒỎÕỌÔỐỒỔỖỘƠỚỜỞỠỢÚÙỦŨỤƯỨỪỬỮỰ]" + 
                                       @"[a-zA-Záàảãạăắằẳẵặâấầẩẫậéèẻẽẹêếềểễệđíìỉĩịóòỏõọôốồổỗộơớờởỡợúùủũụưứừửữự" +
                                       @"ÁÀẢÃẠĂẮẰẲẴẶÂẤẦẨẪẬÉÈẺẼẸÊẾỀỂỄỆĐÍÌỈĨỊÓÒỎÕỌÔỐỒỔỖỘƠỚỜỞỠỢÚÙỦŨỤƯỨỪỬỮỰ]*)" +
                                       @"(([\',. -][a-zA-Záàảãạăắằẳẵặâấầẩẫậéèẻẽẹêếềểễệđíìỉĩịóòỏõọôốồổỗộơớờởỡợúùủũụưứừửữự" +
                                       @"ÁÀẢÃẠĂẮẰẲẴẶÂẤẦẨẪẬÉÈẺẼẸÊẾỀỂỄỆĐÍÌỈĨỊÓÒỎÕỌÔỐỒỔỖỘƠỚỜỞỠỢÚÙỦŨỤƯỨỪỬỮỰ ])?" +
                                       @"[a-zA-Záàảãạăắằẳẵặâấầẩẫậéèẻẽẹêếềểễệđíìỉĩịóòỏõọôốồổỗộơớờởỡợúùủũụưứừửữự" +
                                       @"ÁÀẢÃẠĂẮẰẲẴẶÂẤẦẨẪẬÉÈẺẼẸÊẾỀỂỄỆĐÍÌỈĨỊÓÒỎÕỌÔỐỒỔỖỘƠỚỜỞỠỢÚÙỦŨỤƯỨỪỬỮỰ]*))$";
        Regex regex = new Regex(personNameRegex);
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
