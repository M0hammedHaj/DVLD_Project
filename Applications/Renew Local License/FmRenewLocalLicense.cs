using DVLD_Project.Global_clases;
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

namespace DVLD_Project.Applications.Renew_Local_License
{
    public partial class FmRenewLocalLicense : Form
    {
        private int SelectedLicenseID = -1;
        public FmRenewLocalLicense()
        {
            InitializeComponent();
        }

        private void FmRenewLocalLicense_Load(object sender, EventArgs e)
        {
            lblApplicationDate.Text = DateTime.Now.ToShortDateString();
            lblApplicationFees.Text = clsApplicationType.Find(
                (int)clsApplicationType.enAppType.ReNewLocal).ApplicationFees.ToString();
            lblCreatedByUser.Text = clsGlobal.LogedInUser.UserName;
            lblIssueDate.Text = DateTime.Now.ToShortDateString();
            
        }

        private void uctrlShowLicenseWithFilter1_OnLicenseSelected(int obj)
        {
            SelectedLicenseID = obj;

            lblOldLicenseID.Text = SelectedLicenseID.ToString();
            lblExpirationDate.Text = DateTime.Now.AddYears(uctrlShowLicenseWithFilter1.SelectedLicenseInfo.LicenseClassInfo.DefaultValidityLength).ToShortDateString();
            lblLicenseFees.Text = uctrlShowLicenseWithFilter1.SelectedLicenseInfo.LicenseClassInfo.ClassFees.ToString();
            lblTotalFees.Text = (Convert.ToSingle(lblApplicationFees.Text) + Convert.ToSingle(lblLicenseFees.Text)).ToString();
            

            if (!uctrlShowLicenseWithFilter1.SelectedLicenseInfo.IsLicenseExpired())
            {
                MessageBox.Show($"License with LicenseID : {SelectedLicenseID} isn't expired yet, it will expired on {uctrlShowLicenseWithFilter1.SelectedLicenseInfo.ExpirationDate.ToShortDateString()}",
                    "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if(!uctrlShowLicenseWithFilter1.SelectedLicenseInfo.IsLicenseActive())
            {
                MessageBox.Show($"License with LicenseID : {SelectedLicenseID} isn't active ,choose another one.",
                    "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            btnRenew.Enabled = true;
            txtNotes.Enabled = true;
        }

        private void btnRenew_Click(object sender, EventArgs e)
        {
            clsLicense RenewedLicense = uctrlShowLicenseWithFilter1.SelectedLicenseInfo.RenewLicense(
                txtNotes.Text, clsGlobal.LogedInUser.UserID);

            if (RenewedLicense != null)
            {
                lblApplicationID.Text = RenewedLicense.ApplicationID.ToString();
                lblRenewedLicenseID.Text = RenewedLicense.LicenseID.ToString();
                btnRenew.Enabled = false;
                uctrlShowLicenseWithFilter1.FilterEnabled = false;
                txtNotes.Enabled = false;

                MessageBox.Show("License has been renewed successfully", "Renewed Successfully",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                return;
            }

            MessageBox.Show("License didn't renew", "Error", MessageBoxButtons.OK
                , MessageBoxIcon.Error);
        }
    }
}
