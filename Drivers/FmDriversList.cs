using DVLD_Project.Licenses;
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

namespace DVLD_Project.Drivers
{
    public partial class FmDriversList : Form
    {
        DataTable _dtDriversInfo;
        public FmDriversList()
        {
            InitializeComponent();
        }

        private void FmDriversList_Load(object sender, EventArgs e)
        {
            _dtDriversInfo = clsDriver.GetAllDrivers();
            dgvDrivers.DataSource = _dtDriversInfo;
            lblRecordsNumber.Text = _dtDriversInfo.Rows.Count.ToString();
        }

        private void showDriverLicensesHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FmDriverLicensesHistory driverLicensesHistory = new FmDriverLicensesHistory((int)dgvDrivers.SelectedCells[0].Value);
            driverLicensesHistory.ShowDialog();
        }
    }
}
