using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace recepty
{
    public class ReceptyView
    {
        public int PrescriptionId { get; set; }
        [DisplayName("Data wystawienia")]
        public DateTime DateOfIssue { get; set; }
        [DisplayName("Numer")]
        public string Number { get; set; }
        [DisplayName("Wystawił")]
        public string DoctorName { get; set; }

        public ReceptyView()
        { }

        public ReceptyView(int prescriptionId, DateTime dateOfIssue, string number, string doctorFullName)
        {
            PrescriptionId = prescriptionId;
            DateOfIssue = dateOfIssue;
            Number = number;
            DoctorName = doctorFullName;
        }
    }
}
