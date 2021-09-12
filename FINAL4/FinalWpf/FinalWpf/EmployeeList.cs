using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FinalWpf
{
    [XmlRoot("EmployeeList")]
    public class EmployeeList
    {
        [XmlArray("EmployeeRegistrations")]
        [XmlArrayItem("EmployeeRegistration")]
        public List<EmployeeRegistration> EmployeeRegistrations;
        public EmployeeList()
        {
            EmployeeRegistrations = new List<EmployeeRegistration>();
        }
    }
}
