using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FinalWpf
{
    [XmlRoot("PatientList")]

   public class PatientList
    {
        [XmlArray("Patients")]
        [XmlArrayItem("Patient")]
        public List<Patient> Patients;
        public PatientList()
        {
            Patients = new List<Patient>();
        }
    }
}
