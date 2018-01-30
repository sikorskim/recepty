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
    public partial class FrmPrescriptionEdit : Form
    {
        public FrmPrescriptionEdit(int patientId)
        {
            InitializeComponent();
            patient = Patient.get(patientId);
            startup();
        }

        Patient patient;

        void startup()
        {       
            loadDrugFilters();
            dataGridViewSettings();
        }

        void dataGridViewSettings()
        {
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.ReadOnly = true;
            dataGridView1.ClearSelection();
        }

        void loadDrugFilters()
        {
            comboBox2.DataSource = Lek.getSearchQueryTypes();
            comboBox3.DataSource = Lek.getSearchFilters();
            comboBox2.SelectedIndex = 0;
            comboBox3.SelectedIndex = 0;
        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {
            drugSearch();
        }

        private void drugSearch()
        {
            string query = textBox9.Text;
            if (query != null && comboBox3.SelectedValue!=null)
            {
                string queryType = comboBox2.SelectedValue.ToString();
                string filter = comboBox3.SelectedValue.ToString();
                //dataGridView1.DataSource = Lek.search(query, queryType, filter);
                dataGridView1.DataSource = Lek.searchViewList(query, queryType, filter);
            }
        }

        List<Lek> list = new List<Lek>();
        void addDrugToPrescription()
        {
            if (list.Count < 5)
            {
                string bl7 = (string)dataGridView1[0, dataGridView1.CurrentRow.Index].Value;
                Lek lek = Lek.get(bl7);
                list.Add(lek);
                dataGridView2.DataSource = null;
                dataGridView2.DataSource = list;
            }
            else
            {
                MessageBox.Show("Osiągnięto maksymalną ilość pozycij na recepcie");
            }
        }

        void createPrescription()
        {
            Model1 model = new Model1();
            Doctor doctor = model.Doctor.FirstOrDefault();

            Prescription prescription = new Prescription(PrescriptionNumber.getUnusedNumber("Rp"), patient, doctor,list);
            prescription.insertToDb();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            drugSearch();
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            drugSearch();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            addDrugToPrescription();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            createPrescription();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Model1 model = new Model1();
            Doctor doctor = model.Doctor.FirstOrDefault();

            Prescription prescription = new Prescription(PrescriptionNumber.getUnusedNumber("Rp"), patient, doctor, list);
            FrmPrescriptionPrint prescriptionPrint = new FrmPrescriptionPrint(prescription);
            prescriptionPrint.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Model1 model = new Model1();
            Doctor doctor = model.Doctor.FirstOrDefault();

            Prescription prescription = new Prescription(PrescriptionNumber.getUnusedNumber("Rp"), patient, doctor, list);
            FrmPrescriptionPreview frmPrescriptionPreview = new FrmPrescriptionPreview(prescription);
            frmPrescriptionPreview.Show();
        }
    }
}
