using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace recepty
{
    public class PrescriptionView
    {
        public int PrescriptionId { get; set; }
        [DisplayName("Data wystawienia")]
        public DateTime DateOfIssue { get; set; }
        [DisplayName("Numer")]
        public string Number { get; set; }
        [DisplayName("Wystawił")]
        public string DoctorFullName { get; set; }
        [DisplayName("Pacjent")]
        public string PatientFullName { get; set; }

        public PrescriptionView()
        { }

        public PrescriptionView(Prescription prescription)
        {
            PrescriptionId = prescription.PrescriptionId;
            DateOfIssue = prescription.DateOfIssue;
            Number = prescription.PrescriptionNumber.Number;
            DoctorFullName = prescription.Doctor.FullName;
            PatientFullName = prescription.Patient.FullName;
        }
    }
}
