using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace recepty
{
    public partial class FrmPrescriptionPreview : Form
    {
        public FrmPrescriptionPreview(int prescriptionId)
        {
            InitializeComponent();
            prescription = Prescription.get(prescriptionId);
            printDocument1.DefaultPageSettings.PaperSize = new PaperSize("recepta",390,788);
            printPreviewControl1.Document = printDocument1;
        }

        Prescription prescription;

        private void FrmPrescriptionPreview_Paint(object sender, PaintEventArgs e)
        {
            //prescription.preview(e);
        }

        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            prescription.print(e);
        }
    }
}
