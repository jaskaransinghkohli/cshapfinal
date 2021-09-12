using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace FinalWpf
{
    class PatientAgeRule : ValidationRule
    {
        
        int minAge;
        int maxAge;

        public int MinAge { get => minAge; set => minAge = value; }
        public int MaxAge { get => maxAge; set => maxAge = value; }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            int ageVal = 0;
            if (!int.TryParse(value.ToString(), out ageVal))
            {
                return new ValidationResult(false, "Please Enter Correct Data");

            }
            if(ageVal <MinAge|| ageVal > MaxAge)
            {
                return new ValidationResult(false, $"Please enter an age in the range: {MinAge}-{MaxAge}.");
            }
            return ValidationResult.ValidResult;
        }
    }
}
