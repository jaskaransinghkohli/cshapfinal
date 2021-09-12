using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalWpf
{
   public class Classification
    {
        private string name;
        private List<Appointment> appointments;
        private List<Patient> patients;
        
        private List<EmployeeRegistration> employeeRegistrations;
        public Classification() { }
        public Classification ( string name)
        {
            this.Name = name;

        }

        public string Name { get => name; set => name = value; }
        public List<Appointment> Appointments { get => appointments; set => appointments = value; }
        public List<Patient> Patients { get => patients; set => patients = value; }
       
        internal List<EmployeeRegistration> EmployeeRegistrations { get => employeeRegistrations; set => employeeRegistrations = value; }
    }
}
