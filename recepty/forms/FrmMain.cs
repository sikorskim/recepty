using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace recepty
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
            startup();
        }

        void startup()
        {
            dataGridViewSettings();
            patientsListLoad();

            tabControl1.Appearance = TabAppearance.FlatButtons;
            tabControl1.ItemSize = new Size(0, 1);
            tabControl1.SizeMode = TabSizeMode.Fixed;
            radioButton1.Checked = true;
        }

        void dataGridViewSettings()
        {
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.ReadOnly = true;
            dataGridView1.ClearSelection();

            dataGridView2.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView2.RowHeadersVisible = false;
            dataGridView2.ReadOnly = true;
            //dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView2.ClearSelection();

            dataGridView3.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView3.RowHeadersVisible = false;
            dataGridView3.ReadOnly = true;
            //dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView3.ClearSelection();
        }

        int getId()
        {
            int id;
            try
            {
                id = (int)dataGridView1[0, dataGridView1.CurrentRow.Index].Value;
            }
            catch (Exception e)
            {
                id = 0;
                MessageBox.Show(e.ToString());
            }
            return id;
        }

        void createPrescription()
        {
            int patientId = getId();
            if (patientId != 0)
            {
                FrmPrescriptionEdit frmPrescriptionEdit = new FrmPrescriptionEdit(patientId);
                frmPrescriptionEdit.Show();
            }
        }

        void patientsListLoad()
        {
            tabControl1.SelectedTab = tabPage1;
            dataGridView1.DataSource = Patient.getAll();
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[6].Visible = false;
            dataGridView1.Columns[5].Visible = false;
            dataGridView1.Columns[8].Visible = false;
            dataGridView1.Columns[10].Visible = false;
        }

        void prescriptionListImport()
        {
            string path = openFile("Pliki XMZ|*.xmz");
            PulaRecept.importFromXML(path);
        }

        void drugListImport()
        {
            string path = openFile("Pliki XML|*.xml");
            Lek.importFromXML(path);
        }

        string openFile(string extensionString)
        {
            string file = null;
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = false;
            openFileDialog.Filter = extensionString;
            DialogResult dialogResult = openFileDialog.ShowDialog();

            if (dialogResult == DialogResult.OK)
            {
                file = openFileDialog.FileName;
            }
            else
            {
                MessageBox.Show("Nie wybrano pliku!", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                file = null;
            }
            return file;
        }

        void getPrescriptionList()
        {
            tabControl1.SelectedTab = tabPage3;
            dataGridView3.DataSource=PulaRecept.getAll();
        }

        private void menuToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void ustawieniaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmSettings frmSettings = new FrmSettings();
            frmSettings.Show();
        }

          private void FrmMain_Load(object sender, EventArgs e)
        {

        }

        private void oProgramieToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox1 aboutBox1 = new AboutBox1();
            aboutBox1.Show();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            patientsListLoad();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            FrmPatientEdit frmPatientEdit = new FrmPatientEdit(0);
            frmPatientEdit.Show();
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            createPrescription();
        }

        private void importPuliReceptToolStripMenuItem_Click(object sender, EventArgs e)
        {
            prescriptionListImport();
        }

        private void importListyLekówToolStripMenuItem_Click(object sender, EventArgs e)
        {
            drugListImport();
        }

        private void wyświetlPulęReceptToolStripMenuItem_Click(object sender, EventArgs e)
        {
            getPrescriptionList();
        }

        private void wyświetlListęLekówToolStripMenuItem_Click(object sender, EventArgs e)
        {
            getDrugsList();
        }

        private void getDrugsList()
        {
            tabControl1.SelectedTab = tabPage4;
            dataGridView4.DataSource = Lek.getAll();
        }

        private void wyświetlNumeryReceptToolStripMenuItem_Click(object sender, EventArgs e)
        {
            getPrescriptionNumbers();
        }

        private void getPrescriptionNumbers()
        {
            tabControl1.SelectedTab = tabPage3;
            dataGridView3.DataSource = PrescriptionNumber.getAll();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int id = (int)dataGridView1[0, dataGridView1.CurrentRow.Index].Value;
            FrmPatientEdit frmPatientEdit = new FrmPatientEdit(id);
            frmPatientEdit.Show();
        }

        private void oznaczNrJakoWykorzystanyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PrescriptionNumber.setNumberUsed(PrescriptionNumber.getUnusedNumber("Rp"));
        }

        private void diagnostykaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage3;
            dataGridView3.DataSource= Diagnostics.getAll();
        }

        private void policzLekiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string path = openFile("Pliki XML|*.xml");
            MessageBox.Show(Lek.countElements(path).ToString());
        }

        private void policzNryReceptToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string path = openFile("Pliki XML|*.xml");
            MessageBox.Show(PulaRecept.countElements(path).ToString());
        }

        private void wyświetlWystawioneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage2;
            dataGridView2.DataSource = Prescription.getList();
        }

        private void nowyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmPatientEdit frmPatientEdit = new FrmPatientEdit(0);
            frmPatientEdit.Show();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            createPrescription();
        }

        private void nowaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            createPrescription();
        }

        private void receptaToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void wyświetlToolStripMenuItem_Click(object sender, EventArgs e)
        {
            patientsListLoad();
        }

        private void zakończToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void nowaToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            createPrescription();
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string query = textBox1.Text;
            int queryType = 0;
            if (radioButton2.Checked)
            {
                queryType = 1;
            }
            dataGridView1.DataSource = Patient.search(query, queryType);
        }

        private void sToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //FrmPrescriptionPreview frmPrescriptionPreview = new FrmPrescriptionPreview();
            //frmPrescriptionPreview.Show();
        }

        private void narzędziaToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView4_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void tabControl1_TabIndexChanged(object sender, EventArgs e)
        {

        }

        void loadDrugFilters()
        {
            comboBox1.DataSource = Lek.getSearchQueryTypes();
            comboBox2.DataSource = Lek.getSearchFilters();
            comboBox1.SelectedIndex = 0;
            comboBox1.SelectedIndex = 0;
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == tabPage4)
            {
                loadDrugFilters();
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            drugSearch();
        }

        private void drugSearch()
        {
            string query = textBox2.Text;
            if (query != null && comboBox2.SelectedValue != null)
            {
                string queryType = comboBox1.SelectedValue.ToString();
                string filter = comboBox2.SelectedValue.ToString();
                dataGridView4.DataSource = Lek.search(query, queryType, filter);
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            drugSearch();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            drugSearch();
        }

        private void dataGridView2_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int id = (int)dataGridView2[0, dataGridView2.CurrentRow.Index].Value;
            FrmPrescriptionPreview frmPrescriptionPreview = new FrmPrescriptionPreview(id);
            frmPrescriptionPreview.Show();
        }
    }
}
