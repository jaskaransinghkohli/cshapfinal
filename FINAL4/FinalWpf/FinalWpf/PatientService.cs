using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalWpf
{
    class PatientService
    {
        private string servicePatient;

        public string ServicePatient { get => servicePatient; set => servicePatient = value; }

        public PatientService(string patientService)
        {
            this.ServicePatient = servicePatient;
        }
    }
}
