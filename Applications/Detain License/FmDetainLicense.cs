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
    public partial class FmDetainLicense : Form
    {
        private int SelectedLicenseID = -1;

        public FmDetainLicense()
        {
            InitializeComponent();
        }

        private void FmDetainLicense_Load(object sender, EventArgs e)
        {
            btnDetain.Enabled = false;
            lblCreatedByUser.Text = clsGlobal.LogedInUser.UserName;
            lblDetainDate.Text = DateTime.Now.ToShortDateString();
            uctrlShowLicenseWithFilter1.txtFilterFocus();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void uctrlShowLicenseWithFilter1_OnLicenseSelected(int obj)
        {
            SelectedLicenseID = obj;
            lblLicenseID.Text = SelectedLicenseID.ToString();

            llShowLicenseHistory.Enabled = true;

            if(uctrlShowLicenseWithFilter1.SelectedLicenseInfo.IsDetained)
            {
                MessageBox.Show($"License with LicenseID : {SelectedLicenseID} already detained,choose another one.",
                    "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            btnDetain.Enabled = true;
        }

        private void txtFineFees_Validating(object sender, CancelEventArgs e)
        {
            if(string.IsNullOrWhiteSpace(txtFineFees.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtFineFees, "Control shouldn't be blank");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtFineFees, null);
            }

            if (!clsValidation.IsNumber(txtFineFees.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtFineFees, "Invalid Number.");
            }
            else
            {
                errorProvider1.SetError(txtFineFees, null);
            };
        }


        private void btnDetain_Click(object sender, EventArgs e)
        {
            if(!this.ValidateChildren())
            {
                MessageBox.Show("Fees shouldn't be blank", "Error"
                    , MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int DetainID = uctrlShowLicenseWithFilter1.SelectedLicenseInfo.DetainLicense(
                Convert.ToSingle(txtFineFees.Text), clsGlobal.LogedInUser.UserID);

            if(DetainID == -1)
            {
                MessageBox.Show("License didn't detaine successfully", "Failed",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            MessageBox.Show("License detained successfully", "Succeded",
                MessageBoxButtons.OK, MessageBoxIcon.Information);

            lblDetainID.Text = DetainID.ToString();
            btnDetain.Enabled = false;
            uctrlShowLicenseWithFilter1.FilterEnabled = false;
            txtFineFees.Enabled = false;
            llShowLicenseInfo.Enabled = true;
        }

        private void llShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            clsLicense LicenseInfo = clsLicense.Find(SelectedLicenseID);

            FmShowLicenseInfo showLicenseInfo = new FmShowLicenseInfo(LicenseInfo.ApplicationID);
            showLicenseInfo.ShowDialog();
        }

        private void llShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            clsLicense LicenseInfo = clsLicense.Find(SelectedLicenseID);

            FmDriverLicensesHistory driverLicensesHistory = new FmDriverLicensesHistory(LicenseInfo.DriverID);
            driverLicensesHistory.ShowDialog();
        }

    }
}
