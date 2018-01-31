using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Zen.Barcode;

namespace recepty
{
    public partial class FrmPrescriptionPrint : Form
    {
        public FrmPrescriptionPrint(Prescription prescription)
        {
            InitializeComponent();
            this.prescription = prescription;
            startup();
        }

        Prescription prescription;

        void startup()
        {
            //prescription = Prescription.get(prescription);
            drawPrescription();
            setData();
        }

        void drawPrescription()
        {
            panel1.BackColor = Color.White;
            panel1.BorderStyle = BorderStyle.FixedSingle;
        }

        void setData()
        {
            label1.Text = prescription.PrescriptionNumber.Number;
            drawCode(prescription.PrescriptionNumber.Number,pictureBox1);
            drawCode(prescription.PrescriptionNumber.Number, pictureBox5);
            lblPesel.Text = prescription.Patient.PESEL;
            drawCode(prescription.Patient.PESEL,pictureBox3);
            lblNfz.Text = prescription.Patient.Kod;
            lblUpr.Text = prescription.Patient.Uprawnienie;
            lblLekarz.Text = prescription.Doctor.FullName;
            drawCode(prescription.Doctor.RightToPracticeNumber, pictureBox2);
            label15.Text = prescription.DateOfIssue.ToShortDateString();
            label17.Text = prescription.Doctor.RightToPracticeNumber;
            label18.Text = prescription.Patient.FullName;
            label19.Text = Address.get(prescription.Patient.AddressId).FullAddress;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            base.OnPaint(e);
            using (Graphics g = e.Graphics)
            {
                var p = new Pen(Color.Black, 1);
                var point1 = new Point(0, 90);
                var point2 = new Point(300, 90);                
                var point3 = new Point(0, 227);
                var point4 = new Point(300, 227);
                var point5 = new Point(0, 490);
                var point6 = new Point(300, 490);
                var point11 = new Point(210, 155);
                var point12 = new Point(300, 155);
                var point7 = new Point(130,490);
                var point8 = new Point(130, 600);
                var point9 = new Point(210, 90);
                var point10 = new Point(210, 227);

                g.DrawLine(p, point1, point2);
                g.DrawLine(p, point3, point4);
                g.DrawLine(p, point5, point6);
                g.DrawLine(p, point7, point8);
                g.DrawLine(p, point9, point10);
                g.DrawLine(p, point11, point12);

            }
        }

        void drawCode(string value, PictureBox pictureBox)
        {
            try
            {
                Code128BarcodeDraw code = BarcodeDrawFactory.Code128WithChecksum;
                System.Drawing.Image img = code.Draw(value, 40);
                pictureBox.Image = img;
            }
            catch (Exception)
            {
                pictureBox.Image = null;
            }
        }

        private void label17_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //printDocument1.PrintPage += new PrintPageEventHandler(printDocument1_PrintPage);
            //PrintDialog myPrintDialog = new PrintDialog();

            //if (myPrintDialog.ShowDialog() == DialogResult.OK)
            //{
            //    PrinterSettings values;
            //    values = myPrintDialog.PrinterSettings;
            //    myPrintDialog.Document = printDocument1;
            //    printDocument1.PrintController = new StandardPrintController();
            //    printDocument1.Print();
            //    //PrintPreviewDialog printPreviewDialog = new PrintPreviewDialog();
            //    //printPreviewDialog.Document = printDocument1;
            //    //printPreviewDialog.Show();
            //}
            //printDocument1.Dispose();
        }

        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            Bitmap memoryImage = new Bitmap(panel1.Width, panel1.Height);
            panel1.DrawToBitmap(memoryImage, panel1.ClientRectangle);
            

            e.Graphics.InterpolationMode = InterpolationMode.High;
            e.Graphics.CompositingQuality = CompositingQuality.HighQuality;
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            memoryImage = new Bitmap(memoryImage, 363, 743);

            e.Graphics.DrawImage(memoryImage, 0, 0);
        }

    }
}
