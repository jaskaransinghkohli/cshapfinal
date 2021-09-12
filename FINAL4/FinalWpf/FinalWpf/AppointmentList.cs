using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FinalWpf
{
    [XmlRoot("AppointmentList")]
   public class AppointmentList : IDisposable
    {
        [XmlArray("Appointments")]
        [XmlArrayItem("Appointment")]
        public List<Appointment> Appointments;

        public AppointmentList()
        {
            Appointments = new List<Appointment>();

        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
