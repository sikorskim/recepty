using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Zen.Barcode;

namespace recepty
{

    public class Prescription
    {
        public int PrescriptionId { get; private set; }
        public DateTime DateOfIssue { get; private set; }
        public int PrescriptionNumberId { get; private set; }
        [ForeignKey("PrescriptionNumberId")]
        public virtual PrescriptionNumber PrescriptionNumber { get; private set; }
        public int PatientId { get; private set; }
        [ForeignKey("PatientId")]
        public virtual Patient Patient { get; private set; }
        public int DoctorId { get; private set; }
        [ForeignKey("DoctorId")]
        public virtual Doctor Doctor { get; private set; }
        [NotMapped]
        public List<Lek> Leki { get; private set; }

        public Prescription()
        { }

        public Prescription(PrescriptionNumber prescriptionNumber, Patient patient, Doctor doctor, List<Lek> leki)
        {
            PrescriptionNumberId = prescriptionNumber.PrescriptionNumberId;
            PatientId = patient.PatientId;
            DoctorId = doctor.DoctorId;
            DateOfIssue = DateTime.Now;
            Leki = leki;
        }

        public static Prescription get(Prescription prescription)
        {
            Model1 model = new Model1();
            prescription.Doctor = model.Doctor.Single(p => p.DoctorId == prescription.DoctorId);
            prescription.Patient = model.Patient.Single(p => p.PatientId == prescription.PatientId);
            prescription.PrescriptionNumber = model.PrescriptionNumber.Single(p => p.PrescriptionNumberId == prescription.PrescriptionNumberId);
            prescription.Leki = model.PrescriptionItem.Where(p => p.PrescriptionId == prescription.PrescriptionId).Select(p => p.Lek).ToList();
            return prescription;
        }

        public static List<Prescription> getList()
        {
            Model1 model = new Model1();
            return model.Prescription.ToList();
        }

        public static List<PrescriptionView> getViewList(Patient patient)
        {
            Model1 model = new Model1();
            var items = model.Prescription.Where(p => p.PatientId == patient.PatientId).ToList();
            List<PrescriptionView> result = new List<PrescriptionView>();
            foreach (Prescription pres in items)
            {
                string number = model.PrescriptionNumber.Single(p => p.PrescriptionNumberId == pres.PrescriptionNumberId).Number;
                result.Add(new PrescriptionView(pres.PrescriptionId, pres.DateOfIssue, number, pres.Doctor.FullName));
            }
            return result;
        }

        public bool insertToDb()
        {
            try
            {
                Model1 model = new Model1();
                model.Prescription.Add(this);
                List<PrescriptionItem> items = new List<PrescriptionItem>();
                foreach (Lek lek in Leki)
                {
                    items.Add(new PrescriptionItem(this, lek));
                }

                model.PrescriptionItem.AddRange(items);
                model.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
            return true;
        }

        Image drawBarcode(string value)
        {
            Code128BarcodeDraw code = BarcodeDrawFactory.Code128WithChecksum;
            Image img = code.Draw(value, 20);
            return img;
        }

        public void print(System.Drawing.Printing.PrintPageEventArgs e)
        {
            Graphics g = e.Graphics;
            drawPrescriptionTemplate(g);
        }
        public void preview(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            drawPrescriptionTemplate(g);
        }

        void drawPrescriptionTemplate(Graphics g)
        {
            g.PageUnit = GraphicsUnit.Millimeter;
            float lineSize = 0.2f;
            Pen p = new Pen(Color.Black, lineSize);
            Pen penDotted = new Pen(Color.Black, lineSize);
            penDotted.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;

            int pageWidth = 99;
            int pageHeight = 200;
            Point rectLocation = new Point(0, 0);
            Size rectSize = new Size(pageWidth, pageHeight);
            Rectangle rectangle = new Rectangle(rectLocation, rectSize);
            g.DrawRectangle(p, rectangle);
            g.FillRectangle(Brushes.White, rectangle);

            Point hLine00 = new Point(0, 35);
            Point hLine01 = new Point(99, 35);
            Point hLine10 = new Point(0, 74);
            Point hLine11 = new Point(99, 74);
            Point hLine20 = new Point(0, 160);
            Point hLine21 = new Point(99, 160);
            g.DrawLine(p, hLine00, hLine01);
            g.DrawLine(p, hLine10, hLine11);
            g.DrawLine(p, hLine20, hLine21);

            Point vLine00 = new Point(69, 35);
            Point vLine01 = new Point(69, 74);
            g.DrawLine(p, vLine00, vLine01);

            Point vLine10 = new Point(69, 74);
            Point vLine11 = new Point(69, 148);
            g.DrawLine(penDotted, vLine10, vLine11);

            Point hLine60 = new Point(0, 79);
            Point hLine61 = new Point(99, 79);
            g.DrawLine(penDotted, hLine60, hLine61);

            Point hLine70 = new Point(0, 93);
            Point hLine71 = new Point(99, 93);
            g.DrawLine(penDotted, hLine70, hLine71);
            Point hLine80 = new Point(0, 107);
            Point hLine81 = new Point(99, 107);
            g.DrawLine(penDotted, hLine80, hLine81);
            Point hLine90 = new Point(0, 121);
            Point hLine91 = new Point(99, 121);
            g.DrawLine(penDotted, hLine90, hLine91);
            Point hLine100 = new Point(0, 135);
            Point hLine101 = new Point(99, 135);
            g.DrawLine(penDotted, hLine100, hLine101);

            Point hLine50 = new Point(0, 148);
            Point hLine51 = new Point(99, 148);
            g.DrawLine(penDotted, hLine50, hLine51);

            Point hLine30 = new Point(69, 55);
            Point hLine31 = new Point(99, 55);
            g.DrawLine(p, hLine30, hLine31);

            Point vLine20 = new Point(35, 160);
            Point vLine21 = new Point(35, 200);
            g.DrawLine(p, vLine20, vLine21);

            Point hLine40 = new Point(0, 180);
            Point hLine41 = new Point(35, 180);
            g.DrawLine(p, hLine40, hLine41);

            int marginLeft = 3;
            int marginTop = 3;

            Font fontDefault = new Font("Arial", 10);
            Font fontSmall = new Font("Arial", 8);
            Brush brush = Brushes.Black;

            Point ptReceptaStr = new Point(marginLeft, marginTop);
            Point ptReceptaVal = new Point(marginLeft + 30, marginTop);
            Point ptReceptaVal2 = new Point(30, 155);
            Point ptSwiadczeniodawcaStr = new Point(marginLeft, 30);
            Point ptPacjentStr = new Point(marginLeft, 35);
            Point ptPacjentFullNameVal = new Point(marginLeft, 40);
            Point ptPatientFullAddressVal = new Point(marginLeft, 45);
            Point ptPeselStr = new Point(marginLeft, 69);
            Point ptPeselVal = new Point(marginLeft + 20, 69);
            Point ptNfzStr = new Point(72, 35);
            Point ptNfzVal = new Point(72, 40);
            Point ptUprawnieniaStr = new Point(72, 55);
            Point ptUprawnieniaVal = new Point(72, 65);
            Point ptRpStr = new Point(marginLeft, 75);
            Point ptDataWystawieniaStr = new Point(marginLeft, 160);
            Point ptDataWystawieniaVal = new Point(marginLeft, 165);
            Point ptDataRealizacjiStr = new Point(marginLeft, 180);

            g.DrawString("Recepta", fontDefault, brush, ptReceptaStr);
            g.DrawString(PrescriptionNumber.Number, fontDefault, brush, ptReceptaVal);
            g.DrawString("Świadczeniodawca", fontDefault, brush, ptSwiadczeniodawcaStr);
            g.DrawString("Pacjent", fontDefault, brush, ptPacjentStr);
            g.DrawString(Patient.FullName, fontDefault, brush, ptPacjentFullNameVal);
            // g.DrawString(Patient.FullAddress, fontDefault, brush, ptPatientFullAddressVal);
            g.DrawString("R.p.", fontDefault, brush, ptRpStr);
            g.DrawString("PESEL", fontDefault, brush, ptPeselStr);
            g.DrawString(Patient.PESEL, fontDefault, brush, ptPeselVal);
            g.DrawString("Oddział NFZ", fontDefault, brush, ptNfzStr);
            g.DrawString(Patient.Kod, fontDefault, brush, ptNfzVal);
            g.DrawString("Uprawnienia \ndodatkowe", fontDefault, brush, ptUprawnieniaStr);
            g.DrawString(Patient.Uprawnienie, fontDefault, brush, ptUprawnieniaVal);
            g.DrawString(PrescriptionNumber.Number, fontDefault, brush, ptReceptaVal2);
            g.DrawString("Data wystawienia", fontSmall, brush, ptDataWystawieniaStr);
            g.DrawString(DateOfIssue.ToShortDateString(), fontDefault, brush, ptDataWystawieniaVal);
            g.DrawString("Data realizacji 'od dnia'", fontSmall, brush, ptDataRealizacjiStr);

            Point ptBarcodePrescriptionNumber = new Point(27, 150);

            Image imgPrescriptionNumberBarcode = drawBarcode(PrescriptionNumber.Number);
            g.DrawImage(imgPrescriptionNumberBarcode, ptBarcodePrescriptionNumber);
        }
    }
}
