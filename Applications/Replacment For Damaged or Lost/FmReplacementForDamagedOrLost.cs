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

namespace DVLD_Project.Applications.Replacment_For_Damaged_or_Lost
{
    public partial class FmReplacementForDamagedOrLost : Form
    {
        private int SelectedLicenseID = -1;
        private clsApplicationType.enAppType ReplaceReason;
        private int NewApplicationID = -1;
        public FmReplacementForDamagedOrLost()
        {
            InitializeComponent();
        }

        private void rbDamagedLicense_CheckedChanged(object sender, EventArgs e)
        {
            lblTitle.Text = "Replacment For Damaged";
            this.Text = lblTitle.Text;
            lblApplicationFees.Text = clsApplicationType.Find(
                (int)clsApplicationType.enAppType.ReplacmentForDamaged).ApplicationFees.ToString();
            ReplaceReason = clsApplicationType.enAppType.ReplacmentForDamaged;
        }

        private void rbLostLicense_CheckedChanged(object sender, EventArgs e)
        {
            lblTitle.Text = "Replacment For Lost";
            this.Text = lblTitle.Text;
            lblApplicationFees.Text = clsApplicationType.Find(
                (int)clsApplicationType.enAppType.ReplacmentForLost).ApplicationFees.ToString();
            ReplaceReason = clsApplicationType.enAppType.ReplacmentForLost;
        }

        private void FmReplacementForDamagedOrLost_Load(object sender, EventArgs e)
        {
            lblApplicationDate.Text = DateTime.Now.ToShortDateString();
            lblCreatedByUser.Text = clsGlobal.LogedInUser.UserName;
            btnIssueReplacement.Enabled = false;
            rbDamagedLicense.Checked = true;
        }

        private void uctrlShowLicenseWithFilter1_OnLicenseSelected(int obj)
        {
            SelectedLicenseID = obj;
            lblOldLicenseID.Text = SelectedLicenseID.ToString();
            llShowLicenseHistory.Enabled = true;

            if(!uctrlShowLicenseWithFilter1.SelectedLicenseInfo.IsActive)
            {
                MessageBox.Show("This license isn't active, choose another one", "Not Allowed",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            btnIssueReplacement.Enabled = true;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void llShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FmDriverLicensesHistory DriverLicenseHistory = new FmDriverLicensesHistory(uctrlShowLicenseWithFilter1.SelectedLicenseInfo.DriverID);
            DriverLicenseHistory.ShowDialog();
        }

        private void btnIssueReplacement_Click(object sender, EventArgs e)
        {
            clsLicense NewLicense = uctrlShowLicenseWithFilter1.SelectedLicenseInfo.Replace(
                clsGlobal.LogedInUser.UserID, ReplaceReason);

            if (NewLicense == null)
            {
                MessageBox.Show("New license didn't save successfully", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            NewApplicationID = NewLicense.ApplicationID;

            lblApplicationID.Text = NewLicense.ApplicationID.ToString();
            lblRreplacedLicenseID.Text = NewLicense.LicenseID.ToString();

            llShowLicenseInfo.Enabled = true;
            uctrlShowLicenseWithFilter1.FilterEnabled = false;
            btnIssueReplacement.Enabled = false;

            MessageBox.Show($"License has been issued successfully with licenseID : {NewLicense.LicenseID}", "Issued",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void llShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FmShowLicenseInfo showLicenseInfo = new FmShowLicenseInfo(NewApplicationID);
            showLicenseInfo.ShowDialog();
        }
    }
}
