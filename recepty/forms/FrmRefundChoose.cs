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
    public partial class FrmRefundChoose : Form
    {
        public FrmRefundChoose(List<Refundacja> refundList)
        {
            InitializeComponent();
            this.refundList = refundList;
            startup();
        }

        List<Refundacja> refundList;

        void startup()
        {
            dataGridViewSetup(dataGridView1);           
        }

        void dataGridViewSetup(DataGridView dataGridView)
        {
            dataGridView.DataSource = refundList;
            dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView.RowHeadersVisible = false;
            dataGridView.ReadOnly = true;
            dataGridView.ClearSelection();
            dataGridView.Columns[0].Visible = false;
            dataGridView.Columns[3].Visible = false;
            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
        }
    }
}
