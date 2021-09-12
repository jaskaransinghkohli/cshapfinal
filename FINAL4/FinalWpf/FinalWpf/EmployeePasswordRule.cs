using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace FinalWpf
{
    class EmployeePasswordRule : ValidationRule
    {
        private int min;

        public int Min { get => min; set => min = value; }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            
            string passwordS = string.Empty;
            if (passwordS.Length < Min)
            
                {
                    return new ValidationResult(false, $"Please enter password of minimum {Min} letters");
                }
                return ValidationResult.ValidResult;
            
        }
    }
}
