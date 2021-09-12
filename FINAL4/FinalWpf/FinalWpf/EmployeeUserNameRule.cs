using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace FinalWpf
{
    class EmployeeUserNameRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            bool error = false;
            if (!Regex.IsMatch(value.ToString(), "^[ A-Za-z0-9]$"))
            {
                error = true;
                return new ValidationResult(error, "Name should be in alphabets");
            }
            else
                return new ValidationResult(error, null);
        }
    }
}
