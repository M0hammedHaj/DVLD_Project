using DVLD_Project.Applications.Local_Driving_License_Applications;
using DVLD_Project.Global_clases;
using DVLD_Project.License;
using DVLD_Project.Licenses;
using DVLD_Project.Licenses.Local_licenses;
using DVLD_Project.Tests;
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

namespace DVLD_Project.Applications.Local_Driving_License
{
    public partial class FmListLocalDrivingLicenseApplications : Form
    {
        private DataTable _dtLocalDrivingLicenseApplications;

        private void _RefreshDGV()
        {
            _dtLocalDrivingLicenseApplications = 
              clsLocalDrivingLicenseApplication.GetAllLocalDrivingLicenseApplications();
            dgvLocalDrivingLicenseApplications.DataSource =
                _dtLocalDrivingLicenseApplications;
        }
        public FmListLocalDrivingLicenseApplications()
        {
            InitializeComponent();
        }

        private void FmListLocalDrivingLicenses_Load(object sender, EventArgs e)
        {
            _dtLocalDrivingLicenseApplications =
              clsLocalDrivingLicenseApplication.GetAllLocalDrivingLicenseApplications();

            dgvLocalDrivingLicenseApplications.DataSource = 
                _dtLocalDrivingLicenseApplications;

            lblRecordsCount.Text = 
                _dtLocalDrivingLicenseApplications.Rows.Count.ToString();
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FmShowDetailsLocalDrivingLicenseApplication 
                localDrivingLicenseApplicationDetails = new
                FmShowDetailsLocalDrivingLicenseApplication(
       int.Parse(dgvLocalDrivingLicenseApplications.SelectedCells[0].Value.ToString()));

            localDrivingLicenseApplicationDetails.ShowDialog();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FmAddNewUpdateLocalDrivingLicenseApplication updateLocalDrivingLicenseApplication
                = new FmAddNewUpdateLocalDrivingLicenseApplication((int)
                dgvLocalDrivingLicenseApplications.SelectedCells[0].Value);
            updateLocalDrivingLicenseApplication.ShowDialog();

            _RefreshDGV();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure do want to delete this application?",
                "Confirm", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) == DialogResult.No)
                return;

            int LDLApplicationID = (int)dgvLocalDrivingLicenseApplications.
                SelectedCells[0].Value;

            clsLocalDrivingLicenseApplication LDLApplication =
                clsLocalDrivingLicenseApplication.Find(LDLApplicationID);

            if(clsLocalDrivingLicenseApplication.
                DeleteLocalDrivingLicenseApplication(LDLApplicationID))
            {
                if (clsApplication.DeleteApplication(LDLApplication.ApplicationID))
                {
                    MessageBox.Show("Application deleted successfully", "Deleted",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _RefreshDGV();
                }
                else
                    MessageBox.Show("Application didn't delete", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
                MessageBox.Show("Application didn't delete", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);


        }
        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            int LDLApplicationID = (int)dgvLocalDrivingLicenseApplications.
                SelectedCells[0].Value;
            clsLocalDrivingLicenseApplication LDLApplicationInfo =
                clsLocalDrivingLicenseApplication.Find(LDLApplicationID);

            clsApplication BaseApplicationInfo = clsApplication.
                Find(LDLApplicationInfo.ApplicationID);

            showLicenseToolStripMenuItem.Enabled = (BaseApplicationInfo.AppStatueAsEn ==
                clsApplication.enApplicationStatue.Completed);

            editToolStripMenuItem.Enabled = (BaseApplicationInfo.AppStatueAsEn ==
                clsApplication.enApplicationStatue.New);

            deleteToolStripMenuItem.Enabled = (BaseApplicationInfo.AppStatueAsEn ==
                clsApplication.enApplicationStatue.New);

            schduleTestToolStripMenuItem.Enabled = (BaseApplicationInfo.AppStatueAsEn ==
                clsApplication.enApplicationStatue.New &&
                clsLocalDrivingLicenseApplication.GetPassedTestsCount(
                    LDLApplicationID) != 3);

            cancelToolStripMenuItem.Enabled = (BaseApplicationInfo.AppStatueAsEn == 
                 clsApplication.enApplicationStatue.New);

            issueDrivingLicenseToolStripMenuItem.Enabled = (LDLApplicationInfo.PassedTests == 
                3 && BaseApplicationInfo.ApplicationStatus == (int)clsApplication.enApplicationStatue.New);

            int PassedTests = clsLocalDrivingLicenseApplication.GetPassedTestsCount((int)
                dgvLocalDrivingLicenseApplications.SelectedCells[0].Value);
            visionTestToolStripMenuItem.Enabled = (PassedTests == 0);
            writtenTestToolStripMenuItem.Enabled = (PassedTests == 1);
            streetTestToolStripMenuItem.Enabled = (PassedTests == 2);
        }

        private void visionTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmListTestAppointments listTestAppointments = new FrmListTestAppointments(
                (int)dgvLocalDrivingLicenseApplications.SelectedCells[0].Value,
                clsTestType.enTestType.Vision);

            listTestAppointments.ShowDialog();
            _RefreshDGV();
        }

        private void writtenTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmListTestAppointments listTestAppointments = new FrmListTestAppointments(
                (int)dgvLocalDrivingLicenseApplications.SelectedCells[0].Value,
                clsTestType.enTestType.Written);

            listTestAppointments.ShowDialog();
            _RefreshDGV();
        }

        private void streetTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmListTestAppointments listTestAppointments = new FrmListTestAppointments(
                (int)dgvLocalDrivingLicenseApplications.SelectedCells[0].Value,
                clsTestType.enTestType.Street);

            listTestAppointments.ShowDialog();
            _RefreshDGV();
        }

        private void cancelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int ApplicationID = clsLocalDrivingLicenseApplication.Find(
          (int)dgvLocalDrivingLicenseApplications.SelectedCells[0].Value).ApplicationID;

            if(clsApplication.CancelTheApplication(ApplicationID))
            {
                MessageBox.Show("Application canceled successfully","Canceled Successfully",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                _RefreshDGV();
            }
            else
                MessageBox.Show("Application didn't cancel", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void issueDrivingLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LDLApplicationID = 
                (int)dgvLocalDrivingLicenseApplications.SelectedCells[0].Value;

            FmAddNewLicense addNewLicense = new FmAddNewLicense(LDLApplicationID);
            addNewLicense.ShowDialog();
            _RefreshDGV();
        }

        private void showLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int ApplicationID = clsLocalDrivingLicenseApplication.Find((int)dgvLocalDrivingLicenseApplications.SelectedCells[0].Value).ApplicationID;

            FmShowLicenseInfo showLicenseInfo = new FmShowLicenseInfo(ApplicationID);
            showLicenseInfo.ShowDialog();
        }

        private void shoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string NationalNo = (string)dgvLocalDrivingLicenseApplications.SelectedCells[2].Value;
            int DriverID = clsDriver.FindByPersonID(clsPerson.Find(NationalNo).PersonID).DriverID;

            FmDriverLicensesHistory driverLicensesHistory = new FmDriverLicensesHistory(DriverID);
            driverLicensesHistory.ShowDialog();
        }
    }
}
