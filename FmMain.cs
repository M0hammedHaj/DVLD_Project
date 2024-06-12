using DVLD_Project.Login;
using DVLD_Project.People;
using DVLD_Project.Users;
using DVLD_Project.Global_clases;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVLD_Project.Applications;
using DVLD_Project.Tests.TestTypes;
using DVLD_Project.Applications.Local_Driving_License;
using DVLD_Project.Drivers;
using DVLD_Project.Licenses.International_license;
using DVLD_Project.Applications.International_driving_license;
using DVLD_Project.Applications.Renew_Local_License;
using DVLD_Project.Applications.Replacment_For_Damaged_or_Lost;
using DVLD_Project.Applications.Detain_License;

namespace DVLD_Project
{
    public partial class FmMain : Form
    {
        FmLogin Login;
        public FmMain(FmLogin login)
        {
            
            InitializeComponent();
            Login = login;
        }


        private void peopleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FmListPeople ListPeople = new FmListPeople();
            ListPeople.ShowDialog();
        }

        private void usersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FmUsersList UsersList = new FmUsersList();
            UsersList.ShowDialog();
        }

        private void currentUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FmShowUserDetails UserDetails = new FmShowUserDetails(
                clsGlobal.LogedInUser.UserID);
            UserDetails.ShowDialog();
        }

        private void signOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
            Login.Show();
        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FmChangeUserPassword changeUserPassword = new FmChangeUserPassword();
            changeUserPassword.ShowDialog();
        }

        private void manageApplictionsTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FmListApplicationTypes listApplicationTypes = new FmListApplicationTypes();
            listApplicationTypes.ShowDialog();
        }

        private void manageTestTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FmListTestTypes listTestTypes = new FmListTestTypes();
            listTestTypes.ShowDialog();
        }

        private void localDrivingLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FmAddNewUpdateLocalDrivingLicenseApplication localDrivingLicense = new 
                FmAddNewUpdateLocalDrivingLicenseApplication();
            localDrivingLicense.ShowDialog();
        }

        private void FmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            Login.Close();
        }


        private void localDrivingLicenseToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FmListLocalDrivingLicenseApplications localDrivingLicenses =
            new FmListLocalDrivingLicenseApplications();

            localDrivingLicenses.ShowDialog();
        }

        private void driversToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FmDriversList driversList = new FmDriversList();
            driversList.ShowDialog();
        }

        private void internationalDrivingLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FmIssueInternationalLicense issueInternationalLicense = new FmIssueInternationalLicense();
            issueInternationalLicense.ShowDialog();
        }

        private void internationalLicenseApplicationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FmListInternationalLicenseApplications listInternationalLicenseApplications = new FmListInternationalLicenseApplications();
            listInternationalLicenseApplications.ShowDialog();
        }

        private void renewLiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FmRenewLocalLicense RenewLocalLicense = new FmRenewLocalLicense();
            RenewLocalLicense.ShowDialog();
        }

        private void replacementForLostOrDamagedLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FmReplacementForDamagedOrLost replacementForDamagedOrLost = new FmReplacementForDamagedOrLost();
            replacementForDamagedOrLost.ShowDialog();
        }

        private void manageDetainedLicensesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FmManageDetainLicenses manageDetainLicenses = new FmManageDetainLicenses();
            manageDetainLicenses.ShowDialog();
        }

        private void detainLicenseToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FmDetainLicense DetainLicense = new FmDetainLicense();
            DetainLicense.ShowDialog();
        }

        private void releaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FmReleaseDetainLicense releaseDetainLicense = new FmReleaseDetainLicense();
            releaseDetainLicense.ShowDialog();
        }
    }
}
