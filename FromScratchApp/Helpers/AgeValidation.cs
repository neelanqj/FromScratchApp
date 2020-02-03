using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FromScratchApp.Helpers
{
    public class AgeValidation : ValidationAttribute
    {
        private readonly int _minage;
        public AgeValidation(int minage)
        {
            _minage = minage;
        }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            DateTime dateOfBirth = Convert.ToDateTime(value.ToString());
            var age = DateTime.Now.Year - dateOfBirth.Year;

            if (dateOfBirth.AddYears(age) > DateTime.Today) age--;

            return (age >= _minage) ? ValidationResult.Success : new ValidationResult("You are too young to sign up!");
        }
    }
}
