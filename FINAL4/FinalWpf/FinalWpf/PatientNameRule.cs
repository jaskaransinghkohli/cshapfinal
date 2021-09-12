using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace FinalWpf
{
    class PatientNameRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            bool error = true;

            if (value != null)
            {
                string[] splitPtntName = value.ToString().Split(' ');
                string ptntNameS = string.Empty;


                for (int i = 0; i < splitPtntName.Length; i++)
                {
                    ptntNameS += splitPtntName[i];
                }

                char[] ptntNameCharArr = ptntNameS.ToCharArray();

                for (int i = 0; i < ptntNameCharArr.Length; i++)
                {
                    if (char.IsLetter(ptntNameCharArr[i]))
                    {
                        error = true;
                    }
                    else
                    {
                        error = false;
                        break;
                    }
                }
            }
            else
            {
                error = false;
            }


            if (error == true)
            {
                return new ValidationResult(error, null);
            }
            else
                return new ValidationResult(error, "Name should be in alphabets");
        }
    }
}
