using DVLD_Project.Global_clases;
using DVLD_Project.Licenses;
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

namespace DVLD_Project.Applications.Detain_License
{
    public partial class FmReleaseDetainLicense : Form
    {
        private int _SelectedLicenseID = -1;
        public FmReleaseDetainLicense()
        {
            InitializeComponent();
        }

        private void FmReleaseDetainLicense_Load(object sender, EventArgs e)
        {
            lblDetainDate.Text = DateTime.Now.ToShortDateString();
            lblCreatedByUser.Text = clsGlobal.LogedInUser.UserName;
            lblApplicationFees.Text = clsApplicationType.Find((int)clsApplicationType.enAppType.ReleaseDetaine).ApplicationFees.ToString();

        }

        private void uctrlShowLicenseWithFilter1_OnLicenseSelected(int obj)
        {
            _SelectedLicenseID = obj;

            lblLicenseID.Text = _SelectedLicenseID.ToString();
            llShowLicenseHistory.Enabled = true;

            if(!uctrlShowLicenseWithFilter1.SelectedLicenseInfo.IsDetained)
            {
                MessageBox.Show("This license didn't detain,choose another one.",
                    "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            if(uctrlShowLicenseWithFilter1.SelectedLicenseInfo.DetainedInfo == null)
            {
                MessageBox.Show("There is no detained license", "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            lblDetainID.Text = uctrlShowLicenseWithFilter1.SelectedLicenseInfo.DetainedInfo.DetainID.ToString();
            lblFineFees.Text = uctrlShowLicenseWithFilter1.SelectedLicenseInfo.DetainedInfo.FineFees.ToString();
            lblTotalFees.Text = (Convert.ToSingle(lblFineFees.Text) + Convert.ToSingle(lblApplicationFees.Text)).ToString();

            btnRelease.Enabled = true;
        }

        private void llShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FmDriverLicensesHistory driverLicensesHistory = new 
                FmDriverLicensesHistory(
                uctrlShowLicenseWithFilter1.SelectedLicenseInfo.DriverID);

            driverLicensesHistory.ShowDialog();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnRelease_Click(object sender, EventArgs e)
        {
            if(!uctrlShowLicenseWithFilter1.SelectedLicenseInfo.DetainedInfo
                .ReleaseDetain(clsGlobal.LogedInUser.UserID))
            {
                MessageBox.Show("Detain didn't release successfully", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int ApplicationID = uctrlShowLicenseWithFilter1.SelectedLicenseInfo.DetainedInfo.ReleaseApplicationID;
            lblApplicationID.Text = ApplicationID.ToString();

            MessageBox.Show($"Detain has been released successfully with ApplicationID : {ApplicationID}",
                "Detain Released", MessageBoxButtons.OK, MessageBoxIcon.Information);

            btnRelease.Enabled = false;
            uctrlShowLicenseWithFilter1.FilterEnabled = false;
            llShowLicenseInfo.Enabled = true;
        }

        private void llShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FmShowLicenseInfo showLicenseInfo = new FmShowLicenseInfo(uctrlShowLicenseWithFilter1.SelectedLicenseInfo.ApplicationID);
            showLicenseInfo.ShowDialog();
        }
    }
}
