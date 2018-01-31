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
    public partial class FrmPatientEdit : Form
    {
        public FrmPatientEdit(int patientId)
        {
            InitializeComponent();
            this.patientId = patientId;
            startup();
        }

        int patientId;
        Patient patient;

        ErrorProvider errorProviderPesel = new ErrorProvider();
        ErrorProvider errorProviderName = new ErrorProvider();
        ErrorProvider errorProviderLastname = new ErrorProvider();

        void startup()
        {
            getNFZdepartements();
            getUprawnienia();
            if (patientId != 0)
            {
                patient = Patient.get(patientId);
                setPatientDetails();
            }

            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.ReadOnly = true;
            dataGridView1.ClearSelection();
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            textBox3.MaxLength = 11;
            button1.Enabled = false;
        }

        void getNFZdepartements()
        {
            comboBox1.DataSource = OddzialNFZ.getOddzialy();
            comboBox1.DisplayMember = "Nazwa";
        }

        void getUprawnienia()
        {
            comboBox2.DataSource = Uprawnienie.getAll();
            comboBox2.DisplayMember = "Kod";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        void setPatientDetails()
        {
            textBox1.Text = patient.Name;
            textBox2.Text = patient.Lastname;
            textBox3.Text = patient.PESEL;
            textBox4.Text = patient.Address.Street;
            textBox5.Text = patient.Address.BuldingNumber;
            textBox6.Text = patient.Address.LocalNumber;
            textBox7.Text = patient.Address.PostalCode;
            textBox8.Text = patient.Address.City;
            comboBox1.SelectedItem = patient.NFZDepartament;
            comboBox2.SelectedText = patient.Uprawnienie;

            dataGridView1.DataSource = Prescription.getViewList(patient);
            dataGridView1.Columns[0].Visible = false;
        }

        Patient getPatientDetails()
        {
            string name = textBox1.Text;
            string lastname = textBox2.Text;
            string pesel = textBox3.Text;
            string street = textBox4.Text;
            string buildingNo = textBox5.Text;
            string localNo = textBox6.Text;
            string city = textBox8.Text;
            string postalCode = textBox7.Text;
            OddzialNFZ oddzialNFZ = comboBox1.SelectedItem as OddzialNFZ;
            Uprawnienie uprawnienie = comboBox2.SelectedItem as Uprawnienie;

            Address adres = new Address(street, buildingNo, localNo, postalCode, city);
            Patient netPatient = new Patient(lastname, name, pesel, adres, oddzialNFZ, uprawnienie);
            return netPatient;
        }

        void addPatient(Patient pacjent)
        {
            pacjent.insertToDb();
        }

        bool validatePESEL(TextBox textBox)
        {
            if (!Patient.checkPESEL(textBox.Text))
            {
                errorProviderPesel.SetError(textBox, "Nieprawidłowy numer PESEL!");
                return false;
            }
            else
            {
                errorProviderPesel.Clear();
                return true;
            }
        }

        bool validateName(TextBox textBox)
        {
            if (textBox.Text.Length<3)
            {
                errorProviderName.SetError(textBox, "Imię jest wymagane!");
                return false;
            }
            else
            {
                errorProviderName.Clear();
                return true;
            }
        }

        bool validateLastname(TextBox textBox)
        {
            if (textBox.Text.Length < 3)
            {
                errorProviderLastname.SetError(textBox2, "Nazwisko jest wymagane!");
                return false;
            }
            else
            {
                errorProviderLastname.Clear();
                return true;
            }
        }

        void validateInput()
        {
            bool peselValid = validatePESEL(textBox3);
            bool nameValid = validateName(textBox1);
            bool lastnameValid = validateLastname(textBox2);

            if (peselValid && nameValid && lastnameValid)
            {
                button1.Enabled = true;
            }
            else
            {
                button1.Enabled = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            addPatient(getPatientDetails());
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (!checkBox1.Checked)
            {
                textBox3.Enabled = true;
            }
            else
            {
                textBox3.Enabled = false;
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int id = (int)dataGridView1[0, dataGridView1.CurrentRow.Index].Value;
            dataGridView2.DataSource = null;
            dataGridView2.DataSource = PrescriptionItem.get(id);
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            validateInput();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            validateInput();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            validateInput();
        }
    }
}
