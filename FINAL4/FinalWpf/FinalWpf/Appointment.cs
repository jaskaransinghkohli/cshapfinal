using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalWpf
{
    public enum AppointmentType { Dental_Cleanings, Root_Canal, Teeth_Whitening, Bridges, Fillings }
    public enum LocationType { Clinic, Home_Treatement }
    public enum StatusType { Pending, Treated }
    public class Appointment
    {
        private int appointmentId;
        private AppointmentType appointmentType;
        private string datetime;
        private LocationType location;
        private StatusType status;
        private int ptntId;
        public Appointment() { }

        public Appointment(int appointmentId, AppointmentType appointmentType, string datetime, LocationType location, StatusType status, int ptntId)
        {
            this.AppointmentId = appointmentId;
            this.AppointmentType = appointmentType;
            this.Datetime = datetime;
            this.Location = location;
            this.Status = status;
            this.PtntId = ptntId;
        }

        public int AppointmentId { get => appointmentId; set => appointmentId = value; }
        public AppointmentType AppointmentType { get => appointmentType; set => appointmentType = value; }
        public string Datetime { get => datetime; set => datetime = value; }
        public LocationType Location { get => location; set => location = value; }
        public StatusType Status { get => status; set => status = value; }
        public int PtntId { get => ptntId; set => ptntId = value; }
    }
}
