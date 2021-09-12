using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalWpf
{
    public class EmployeeRegistration
    {
        private int empId;
        private string empUsername;
        private string empPassword;

        public EmployeeRegistration() { }

        public EmployeeRegistration(int empId,  string empUsername,string empPassword)
        {
            this.EmpId = empId;     
            this.EmpUsername = empUsername; 
            this.EmpPassword = empPassword;
            
        }
       
        public int EmpId { get => empId; set => empId = value; }
        public string EmpUsername { get => empUsername; set => empUsername = value; }
        public string EmpPassword { get => empPassword; set => empPassword = value; }
        
    }
}
