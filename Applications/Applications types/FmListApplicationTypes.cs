using DVLD_Project.Applications.Applications_types;
using DVLDBusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_Project.Applications
{
    public partial class FmListApplicationTypes : Form
    {
        private DataTable _dtApplicationTypes;
        public FmListApplicationTypes()
        {
            InitializeComponent();
        }

        private void _RefreshDataGridView()
        {
            _dtApplicationTypes = clsApplicationType.GetAllApplicationTypes();
            dgvApplicationTypes.DataSource = _dtApplicationTypes;
        }
        private void FmListApplicationTypes_Load(object sender, EventArgs e)
        {
            _dtApplicationTypes = clsApplicationType.GetAllApplicationTypes();
            dgvApplicationTypes.DataSource = _dtApplicationTypes;
            lblRecordsCount.Text = _dtApplicationTypes.Rows.Count.ToString();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FmEditApplicationType editApplicationType = new FmEditApplicationType(
                (int)dgvApplicationTypes.SelectedCells[0].Value);
            editApplicationType.ShowDialog();
            _RefreshDataGridView();
        }
    }
}
