using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace recepty
{
    public partial class FrmPrescriptionPreview : Form
    {
        public FrmPrescriptionPreview(Prescription prescription)
        {
            InitializeComponent();
            this.prescription = prescription;
            prescription = Prescription.get(prescription);
        }

        Prescription prescription;

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            prescription.preview(e);
        }
    }
}
