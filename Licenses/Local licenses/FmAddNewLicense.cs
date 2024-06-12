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

namespace DVLD_Project.License
{
    public partial class FmAddNewLicense : Form
    {
        int _LDLApplicationID = -1;
        public FmAddNewLicense(int LDLApplicationID)
        {
            InitializeComponent();
            _LDLApplicationID = LDLApplicationID;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FmAddNewLicense_Load(object sender, EventArgs e)
        {
            uctrlLocalDrivingLicenseApplicationInfo1.LoadLocalDrivingLicenseApplicationInfo(_LDLApplicationID);
        }

        private void btnIssueLicense_Click(object sender, EventArgs e)
        {
            clsLocalDrivingLicenseApplication SelectedLDLApplication =
                clsLocalDrivingLicenseApplication.Find(_LDLApplicationID);

            clsApplication SelectedPublicApplication =
                clsApplication.Find(SelectedLDLApplication.ApplicationID);

            if (!clsDriver.IsPersonDriver(SelectedPublicApplication.ApplicantPersonID))
            {
                clsDriver NewDriver = new clsDriver();

                NewDriver.PersonID = SelectedPublicApplication.ApplicantPersonID;
                NewDriver.CreatedDate = DateTime.Now;
                NewDriver.CreatedByUserID = clsGlobal.LogedInUser.UserID;

                if (!NewDriver.Save())
                    return;
                   
            }
            
            clsLicense NewLicense = new clsLicense();

            NewLicense.ApplicationID = SelectedPublicApplication.ApplicationID;
            NewLicense.Notes = txtNotes.Text;
            NewLicense.IssueDate = DateTime.Now;
            NewLicense.ExpirationDate = DateTime.Now.AddYears(clsLicenseClass.Find(SelectedLDLApplication.LicenseClassID).DefaultValidityLength);
            NewLicense.IsActive = true;
            NewLicense.DriverID = clsDriver.GetDriverIDByPersonID(SelectedPublicApplication.ApplicantPersonID);
            NewLicense.IssueReason = 1;
            NewLicense.LicenseClass = SelectedLDLApplication.LicenseClassID;
            NewLicense.PaidFees = clsLicenseClass.Find(
                SelectedLDLApplication.LicenseClassID).ClassFees;
            NewLicense.CreatedByUserID = clsGlobal.LogedInUser.UserID;

            SelectedPublicApplication.ApplicationStatus = (int)clsApplication.enApplicationStatue.Completed;

            if (NewLicense.Save() && SelectedPublicApplication.Save())
            {
                MessageBox.Show($"License has been issued successfully with license ID : {NewLicense.LicenseID}", "Issued Successfully",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
                MessageBox.Show("License didn't issue", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
