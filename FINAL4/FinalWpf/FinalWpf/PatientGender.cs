using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalWpf
{
   
   public class PatientGender
    {
        private string gender;

        public string Gender { get => gender; set => gender = value; }
        public GenderType ptntGender { get; internal set; }

        public PatientGender(string gender)
        {
            this.Gender = gender;
        }
    }
       
}
