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

namespace DVLD_Project.Licenses.International_license
{
    public partial class FmIssueInternationalLicense : Form
    {
        int _LocalDrivingLicenseID = -1;
        public FmIssueInternationalLicense()
        {
            InitializeComponent();
        }

        private void btClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void uctrlShowLicenseWithFilter1_OnLicenseSelected(int obj)
        {
            _LocalDrivingLicenseID = obj;
            lblLocalLicenseID.Text = _LocalDrivingLicenseID.ToString();
            clsLicense LDL = clsLicense.Find(_LocalDrivingLicenseID);


            if(LDL.LicenseClass != 3)
            {
                MessageBox.Show("Selected license should be Class 3, choose another one",
                    "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            int InternationalLicenseID = clsInternationalLicense.IsThereActiveInternationalLicenseByLDLID(_LocalDrivingLicenseID);
            if (InternationalLicenseID != -1)
            {
                MessageBox.Show($"There is already an active International license with ID : {InternationalLicenseID} for this person",
                    "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            btIssue.Enabled = true;
        }

        private void FmIssueInternationalLicense_Load(object sender, EventArgs e)
        {
            lblFees.Text = clsApplicationType.Find(
                (int)clsApplicationType.enAppType.NewInternational).ApplicationFees.ToString();
            lblIssueDate.Text = DateTime.Now.ToShortDateString();
            lblApplicationDate.Text = DateTime.Now.ToShortDateString();
            lblExpirationDate.Text = DateTime.Now.AddYears(1).ToShortDateString();
            lblCreatedByUser.Text = clsGlobal.LogedInUser.UserName;

            btIssue.Enabled = false;
        }

        private void btIssue_Click(object sender, EventArgs e)
        {
            clsInternationalLicense InternationalLicense = new clsInternationalLicense();
            //base
            InternationalLicense.ApplicationDate = DateTime.Now;
            InternationalLicense.ApplicantPersonID = clsDriver.Find(uctrlShowLicenseWithFilter1.SelectedLicenseInfo.DriverID).PersonID;
            InternationalLicense.ApplicationStatus = (int)clsApplication.enApplicationStatue.Completed;
            InternationalLicense.ApplicationTypeID = (int)clsApplicationType.enAppType.NewInternational;
            InternationalLicense.CreatedByUserID = clsGlobal.LogedInUser.UserID;
            InternationalLicense.LastStatusDate = DateTime.Now;
            InternationalLicense.PaidFees = clsApplicationType.Find((int)clsApplicationType.enAppType.NewInternational).ApplicationFees;

            //child
            InternationalLicense.IsActive = true;
            InternationalLicense.IssueDate = DateTime.Now;
            InternationalLicense.IssuedUsingLocalLicenseID = _LocalDrivingLicenseID;
            InternationalLicense.DriverID = uctrlShowLicenseWithFilter1.SelectedLicenseInfo.DriverID;
            InternationalLicense.ExpirationDate = DateTime.Now.AddYears(1); 

            if(!InternationalLicense.Save())
            {
                MessageBox.Show("Faild to Issue International License", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            lblApplicationID.Text = InternationalLicense.ApplicationID.ToString();
            lblInternationalLicenseID.Text = InternationalLicense.InternationalLicenseID.ToString();

            MessageBox.Show($"International license issued successfully with ID : {InternationalLicense.InternationalLicenseID.ToString()}",
                "Issued Successfully",
                MessageBoxButtons.OK, MessageBoxIcon.Information);

            btIssue.Enabled = false;
            uctrlShowLicenseWithFilter1.FilterEnabled = false;
        }
    }
}
