using DVLD_Project.Properties;
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

namespace DVLD_Project.Licenses.Local_licenses.Controls
{
    public partial class uctrlShowLicense : UserControl
    {
        int _ApplicationID = -1;
        int _LicensesID = -1;
        public clsLicense LicenseInfo;
        clsDriver DriverInfo;
        public uctrlShowLicense()
        {
            InitializeComponent();
        }

        public void LoadLicenseInfoByApplicationID(int ApplicationID)
        {
            _ApplicationID = ApplicationID;
            LicenseInfo = clsLicense.FindByApplicationID(ApplicationID);
            if(LicenseInfo == null)
            {
                MessageBox.Show("License didn't find", "Not found", MessageBoxButtons.OK
                    , MessageBoxIcon.Error);
                return;
            }
            FillControls();
        }

        public void LoadLicenseInfo(int LicenseID)
        {
            _LicensesID = LicenseID;
            LicenseInfo = clsLicense.Find(LicenseID);
            if (LicenseInfo == null)
            {
                MessageBox.Show("License didn't find", "Not found", MessageBoxButtons.OK
                    , MessageBoxIcon.Error);
                return;
            }
            _ApplicationID = LicenseInfo.ApplicationID;
            FillControls();
        }

        private void FillControls()
        {
            DriverInfo = clsDriver.FindByPersonID(
                clsApplication.Find(_ApplicationID).ApplicantPersonID);

            lblClass.Text = clsLicenseClass.Find(LicenseInfo.LicenseClass).ClassName;
            lblDateOfBirth.Text = DriverInfo.PersonInfo.DateOfBirth.ToShortDateString();
            lblDriverID.Text = DriverInfo.DriverID.ToString();
            lblExpirationDate.Text = LicenseInfo.ExpirationDate.ToShortDateString();
            lblFullName.Text = DriverInfo.PersonInfo.FullName;
            lblGendor.Text = (DriverInfo.PersonInfo.Gendor == 0) ? "Male" : "Female";
            lblIsActive.Text = (LicenseInfo.IsActive) ? "Yes" : "No";
            lblIssueDate.Text = LicenseInfo.IssueDate.ToShortDateString();
            lblIssueReason.Text = LicenseInfo.IssueReasonString;
            lblLicenseID.Text = LicenseInfo.LicenseID.ToString();
            lblNationalNo.Text = DriverInfo.PersonInfo.NationalNo;
            lblIsDetained.Text = LicenseInfo.IsDetained ? "Yes" : "No";
            

            if (LicenseInfo.Notes == "")
                lblNotes.Text = "No notes";
            else
                lblNotes.Text = LicenseInfo.Notes;

            LoadImages();
        }

        private void LoadImages()
        {
            pbGendor.Image = (DriverInfo.PersonInfo.Gendor == 0) ?
                Resources.Man_32 : Resources.Woman_32;

            if (DriverInfo.PersonInfo.ImagePath != "")
                pbPersonImage.ImageLocation = DriverInfo.PersonInfo.ImagePath;

            else
            {
                pbPersonImage.Image = (DriverInfo.PersonInfo.Gendor == 0) ?
                Resources.Male_512 : Resources.Female_512;
            }
        }
    }
}
