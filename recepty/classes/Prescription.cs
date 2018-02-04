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
        public Lek[] Leki { get; private set; }
        [NotMapped]
        public Gabinet Gabinet { get; private set; }

        public Prescription()
        { }

        public Prescription(PrescriptionNumber prescriptionNumber, Patient patient, Doctor doctor, Lek[] leki)
        {
            PrescriptionNumberId = prescriptionNumber.PrescriptionNumberId;
            PatientId = patient.PatientId;
            DoctorId = doctor.DoctorId;
            DateOfIssue = DateTime.Now;
            Leki = leki;
        }

        public static Prescription get(int prescriptionId)
        {
            Model1 model = new Model1();
            Prescription prescription = model.Prescription.Single(p => p.PrescriptionId == prescriptionId);
            prescription.Doctor = model.Doctor.Single(p => p.DoctorId == prescription.DoctorId);
            prescription.Patient = Patient.get(prescription.PatientId);
            prescription.PrescriptionNumber = model.PrescriptionNumber.Single(p => p.PrescriptionNumberId == prescription.PrescriptionNumberId);
            prescription.Leki = model.PrescriptionItem.Where(p => p.PrescriptionId == prescription.PrescriptionId).Select(p => p.Lek).ToArray();
            prescription.Gabinet = Gabinet.get();
            return prescription;
        }

        public static List<PrescriptionView> getViewList()
        {
            Model1 model = new Model1();
            List<Prescription> items= model.Prescription.ToList();
            List<PrescriptionView> result = new List<PrescriptionView>();
            foreach (Prescription prescription in items)
            {
                result.Add(new PrescriptionView(prescription));
            }
            return result;
        }

        public static List<PrescriptionView> getViewList(Patient patient)
        {
            Model1 model = new Model1();
            List<Prescription> items = model.Prescription.Where(p => p.PatientId == patient.PatientId).ToList();
            List<PrescriptionView> result = new List<PrescriptionView>();
            foreach (Prescription prescription in items)
            {                
                result.Add(new PrescriptionView(prescription));
            }
            return result;
        }

        Lek[] getDrugs()
        {
            Model1 model = new Model1();
            Lek[] drugs = new Lek[5];
            drugs = model.PrescriptionItem.Where(p => p.PrescriptionId == PrescriptionId).Select(p => p.Lek).ToArray();
            return drugs;
        }

        public bool insertToDb()
        {
            //try
            //{
                Model1 model = new Model1();
                model.Prescription.Add(this);
                List<PrescriptionItem> items = new List<PrescriptionItem>();
                foreach (Lek lek in Leki)
                {
                if (lek != null)
                {
                    items.Add(new PrescriptionItem(this, lek));
                }
                }

                model.PrescriptionItem.AddRange(items);
                model.SaveChanges();
            //}
            //catch (Exception e)
            //{
            //    Console.WriteLine(e);
            //    return false;
            //}
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

            Point hLine30 = new Point(69, 48);
            Point hLine31 = new Point(99, 48);
            g.DrawLine(p, hLine30, hLine31);
            Point hLine110 = new Point(69, 61);
            Point hLine111 = new Point(99, 61);
            g.DrawLine(p, hLine110, hLine111);

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

            Point ptPrescriptonStr = new Point(marginLeft, marginTop);
            Point ptPrescriptionNumberVal = new Point(marginLeft + 30, marginTop);
            Point ptSwiadczeniodawcaVal = new Point(marginLeft, marginTop + 5);
            Point ptSwiadczeniodawcaStr = new Point(marginLeft, 30);
            Point ptPacjentStr = new Point(marginLeft, 35);
            Point ptPacjentFullNameVal = new Point(marginLeft, 40);
            Point ptPatientFullAddressVal = new Point(marginLeft, 45);
            Point ptWiek = new Point(marginLeft, 60);
            Point ptPeselStr = new Point(marginLeft, 69);
            Point ptPeselVal = new Point(marginLeft + 20, 69);
            Point ptNfzStr = new Point(70, 35);
            Point ptNfzVal = new Point(75, 40);
            Point ptUprawnieniaStr = new Point(70, hLine30.Y);
            Point ptUprawnieniaVal = new Point(75, hLine30.Y+5);
            Point ptChorobyPrzewlekle = new Point(70, hLine110.Y);
            Point ptRpStr = new Point(marginLeft, 75);
            Point ptRefundacjaStr = new Point(70, 75);
            Point ptPosition0 = new Point(marginLeft, hLine70.Y - 12);
            Point ptPosition1 = new Point(marginLeft, hLine80.Y - 12);
            Point ptPosition2 = new Point(marginLeft, hLine90.Y - 12);
            Point ptPosition3 = new Point(marginLeft, hLine100.Y - 12);
            Point ptPosition4 = new Point(marginLeft, hLine50.Y - 12);
            Point ptPrescriptionNumberVal2 = new Point(27, 155);
            Point ptDaneLekarzaStr = new Point(40, 160);
            Point ptDaneLekarzaVal = new Point(40, 165);
            Point ptDaneLekarzaNrPrawaVal = new Point(40, 170);
            Point ptDataWystawieniaStr = new Point(marginLeft, 160);
            Point ptDataWystawieniaVal = new Point(marginLeft, 165);
            Point ptDataRealizacjiStr = new Point(marginLeft, 180);
            Point ptWydrukWlasnyStr = new Point(70, 195);

            g.DrawString("Recepta", fontDefault, brush, ptPrescriptonStr);
            g.DrawString(PrescriptionNumber.Number, fontDefault, brush, ptPrescriptionNumberVal);
            g.DrawString(Gabinet.Nazwa+"\n"+"REGON "+Gabinet.REGON,fontDefault,brush,ptSwiadczeniodawcaVal);
            g.DrawString("Świadczeniodawca", fontDefault, brush, ptSwiadczeniodawcaStr);
            g.DrawString("Pacjent", fontDefault, brush, ptPacjentStr);
            g.DrawString(Patient.FullName, fontDefault, brush, ptPacjentFullNameVal);
            g.DrawString(Patient.FullAddress, fontDefault, brush, ptPatientFullAddressVal);
            g.DrawString("R.p.", fontDefault, brush, ptRpStr);
            g.DrawString("Refundacja", fontDefault, brush, ptRefundacjaStr);
            g.DrawString("Wiek: "+Patient.Age,fontDefault, brush,ptWiek);
            g.DrawString("PESEL", fontDefault, brush, ptPeselStr);
            g.DrawString(Patient.PESEL, fontDefault, brush, ptPeselVal);
            g.DrawString("Oddział NFZ", fontDefault, brush, ptNfzStr);
            g.DrawString(Patient.Kod, fontDefault, brush, ptNfzVal);
            g.DrawString("Uprawnienia", fontDefault, brush, ptUprawnieniaStr);
            g.DrawString(Patient.Uprawnienie, fontDefault, brush, ptUprawnieniaVal);
            g.DrawString("Ch. przewlekłe", fontDefault, brush, ptChorobyPrzewlekle);
            g.DrawString(PrescriptionNumber.Number, fontDefault, brush, ptPrescriptionNumberVal2);
            g.DrawString("Dane i podpis lekarza", fontDefault, brush, ptDaneLekarzaStr);
            g.DrawString(Doctor.FullName, fontDefault,brush,ptDaneLekarzaVal);
            g.DrawString("Nr prawa: "+Doctor.RightToPracticeNumber, fontDefault, brush, ptDaneLekarzaNrPrawaVal);
            g.DrawString("Data wystawienia", fontSmall, brush, ptDataWystawieniaStr);
            g.DrawString(DateOfIssue.ToShortDateString(), fontDefault, brush, ptDataWystawieniaVal);
            g.DrawString("Data realizacji 'od dnia'", fontSmall, brush, ptDataRealizacjiStr);
            g.DrawString("Wydruk własny", fontSmall, brush, ptWydrukWlasnyStr);

            Point[] items = new Point[5];
            items[0] = ptPosition0;
            items[1] = ptPosition1;
            items[2] = ptPosition2;
            items[3] = ptPosition3;
            items[4] = ptPosition4;

            int i = 0;
            foreach (Lek lek in Leki)
            {
                if (Leki[i] != null)
                    g.DrawString(Leki[i].FullInfo, fontDefault, brush, items[i]);
                i++;
            }

            Point ptBarcodeREGON = new Point(65, 28);
            Image imgREGONBarcode = drawBarcode(Gabinet.REGON);
            g.DrawImage(imgREGONBarcode, ptBarcodeREGON);
            Point ptBarcodePrescriptionNumber = new Point(27, 150);
            Image imgPrescriptionNumberBarcode = drawBarcode(PrescriptionNumber.Number);
            g.DrawImage(imgPrescriptionNumberBarcode, ptBarcodePrescriptionNumber);
            Point ptBarcodeRightToPractice = new Point(45, 175);
            Image imgRightToPracticeBarcode = drawBarcode(Doctor.RightToPracticeNumber);
            g.DrawImage(imgRightToPracticeBarcode, ptBarcodeRightToPractice);
        }
    }
}
