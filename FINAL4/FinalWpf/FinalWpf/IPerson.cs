using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalWpf
{
    interface IPerson
    {
        string patientName { get; set; }
        int patientAge { get; set; }
        GenderType patientGender { get; set; }
        string ToothCleaned();
    }
}
