using DVLD_Project.Licenses.International_license;
using DVLD_Project.Licenses.Local_licenses;
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

namespace DVLD_Project.Licenses.Controls
{
    public partial class uctrlDriverLicenses : UserControl
    {
        int _DriverID;
        DataTable _dtLocalLicenses;
        DataTable _dtInternationalLicenses;
        public uctrlDriverLicenses()
        {
            InitializeComponent();
        }

        public void LoadInfo(int DriverID)
        {
            _dtLocalLicenses = clsLicense.GetAllLicensesByDriverID(DriverID);
            _dtInternationalLicenses = clsInternationalLicense.GetAllLicensesInfoByDriverID(DriverID);

            dgvLocalLicenses.DataSource = _dtLocalLicenses;
            dgvInternationalLicenses.DataSource = _dtInternationalLicenses;

            lblLocalRecordsCount.Text = _dtLocalLicenses.Rows.Count.ToString();
            lblInternationalRecordsCount.Text = _dtInternationalLicenses.Rows.Count.ToString();
        }

        private void showToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LicenseID = (int)dgvLocalLicenses.SelectedCells[0].Value;
            clsLicense LicenseInfo = clsLicense.Find(LicenseID);

            FmShowLicenseInfo showLicenseInfo = new FmShowLicenseInfo(LicenseInfo.ApplicationID);
            showLicenseInfo.ShowDialog();
        }

        private void showLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int InternationalLicenseID = (int)dgvInternationalLicenses.SelectedCells[0].Value;

            FmShowInternationalLicenseInfo showInternationalLicenseInfo = new FmShowInternationalLicenseInfo(InternationalLicenseID);
            showInternationalLicenseInfo.ShowDialog();
        }
    }
}
