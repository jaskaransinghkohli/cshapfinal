using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalWpf
{

   public class Patient : Person
    {
        private int patientId;
        public int PatientId { get => patientId; set => patientId = value; }

        public Patient() { }

        public Patient (string patientName, int patientAge, GenderType patientGender, int patientId) : base(patientName, patientAge, patientGender)
        {
            this.PatientId = patientId;
           
        }

        public override string ToothCleaned()
        {
            return "Patient's tooth was cleaned";
        }
    }
}
