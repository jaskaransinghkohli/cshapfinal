using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FinalWpf
{
    public enum GenderType { Male, Female, Other }
    [XmlInclude(typeof(Patient))]
    public abstract class Person : IPerson
    {
        private string patientName;
        private int patientAge;
        private GenderType patientGender;

        public string PatientName { get => patientName; set => patientName = value; }
        public int PatientAge { get => patientAge; set => patientAge = value; }
        public GenderType PatientGender { get => patientGender; set => patientGender = value; }
        string IPerson.patientName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        int IPerson.patientAge { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        GenderType IPerson.patientGender { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public Person() { }
        public Person(string patientName, int patientAge, GenderType patientGender)
        {
            this.PatientName = patientName;
            this.PatientAge= patientAge;
            this.PatientGender= patientGender;
        }
        public abstract string ToothCleaned();
       
    }
}