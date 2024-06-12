using DVLD_Project.Licenses;
using DVLD_Project.Licenses.International_license;
using DVLD_Project.People;
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

namespace DVLD_Project.Applications.International_driving_license
{
    public partial class FmListInternationalLicenseApplications : Form
    {
        DataTable _dtInternationalLicenseApplications;
        public FmListInternationalLicenseApplications()
        {
            InitializeComponent();
        }

        private void FmListInternationalLicenseApplications_Load(object sender, EventArgs e)
        {
            _dtInternationalLicenseApplications = clsInternationalLicense.GetAllInternationalLicenses();
            dgvInternationalLicenses.DataSource = _dtInternationalLicenseApplications;
            lblInternationalLicensesRecords.Text = _dtInternationalLicenseApplications.Rows.Count.ToString();
        }

        private void showPersonDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int PerosnID = clsDriver.Find(
                (int)dgvInternationalLicenses.SelectedCells[2].Value).PersonID;

            FmShowPersonInfo showPersonInfo = new FmShowPersonInfo(PerosnID);
            showPersonInfo.ShowDialog();
        }

        private void showLicenseDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FmShowInternationalLicenseInfo showInternationalLicenseInfo = new FmShowInternationalLicenseInfo((int)dgvInternationalLicenses.SelectedCells[0].Value);
            showInternationalLicenseInfo.ShowDialog();
        }

        private void showPerosnLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FmDriverLicensesHistory driverLicensesHistory = new FmDriverLicensesHistory(
                (int)dgvInternationalLicenses.SelectedCells[2].Value);
            driverLicensesHistory.ShowDialog();
        }
    }
}
