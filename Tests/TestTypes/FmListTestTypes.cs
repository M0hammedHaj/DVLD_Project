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

namespace DVLD_Project.Tests.TestTypes
{
    public partial class FmListTestTypes : Form
    {
        private DataTable _dtTestTypes;
        public FmListTestTypes()
        {
            InitializeComponent();
        }
        private void _RefreshDgv()
        {
            _dtTestTypes = clsTestType.GetAllTestTypes();
            dgvTestTypes.DataSource = _dtTestTypes;
        }
        private void FmListTestTypes_Load(object sender, EventArgs e)
        {
            _dtTestTypes = clsTestType.GetAllTestTypes();
            dgvTestTypes.DataSource = _dtTestTypes;
            lblRecordsCount.Text = _dtTestTypes.Rows.Count.ToString();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FmEditTestTypes editTestTypes = new FmEditTestTypes(
                (int)dgvTestTypes.SelectedCells[0].Value);
            editTestTypes.ShowDialog();
            _RefreshDgv();
        }
    }
}
