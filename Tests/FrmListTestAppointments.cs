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

namespace DVLD_Project.Tests
{
    public partial class FrmListTestAppointments : Form
    {
        private int _LDLApplicationID = -1;
        clsTestType.enTestType _TestType = clsTestType.enTestType.Vision;
        DataTable _dtLDLApplicationAppointments;

        public FrmListTestAppointments(int LDLApplicationID,
            clsTestType.enTestType TestType)
        {
            InitializeComponent();
            _LDLApplicationID = LDLApplicationID;
            _TestType = TestType;
        }
        private void RefreshDGV()
        {
            _dtLDLApplicationAppointments = clsTestAppointment.
    GetTestAppointmentsInfoByTestType(_LDLApplicationID, _TestType);
            dgvLicenseTestAppointments.DataSource = _dtLDLApplicationAppointments;
            lblRecordsCount.Text = _dtLDLApplicationAppointments.Rows.Count.ToString();
        }
        private void FrmListTestAppointments_Load(object sender, EventArgs e)
        {
            switch(_TestType)
            {
                case clsTestType.enTestType.Vision:
                    lblTitle.Text = "Vision Test Appointments";
                    pbTestTypeImage.Image = Resources.Vision_512;
                    break;

                case clsTestType.enTestType.Written:
                    lblTitle.Text = "Written Test Appointments";
                    pbTestTypeImage.Image = Resources.Written_Test_512;
                    break;

                case clsTestType.enTestType.Street:
                    lblTitle.Text = "Street Test Appointments";
                    pbTestTypeImage.Image = Resources.Schedule_Test_512;
                    break;
            }

            uctrlLocalDrivingLicenseApplicationInfo1.
                LoadLocalDrivingLicenseApplicationInfo(_LDLApplicationID);

            _dtLDLApplicationAppointments = clsTestAppointment.
                GetTestAppointmentsInfoByTestType(_LDLApplicationID, _TestType);
            dgvLicenseTestAppointments.DataSource = _dtLDLApplicationAppointments;

            lblRecordsCount.Text = _dtLDLApplicationAppointments.Rows.Count.ToString();
        }

        private void btnAddNewAppointment_Click(object sender, EventArgs e)
        {

            if (clsTestAppointment.IsThereActiveTestAppointment(
                _LDLApplicationID, _TestType))
            {
                MessageBox.Show("You already have an active test appointment", "Test Appointment Exist",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (clsTest.IsTestAppointmentPassedTheTest(clsTestAppointment.GetLatestTestAppintmentID(_LDLApplicationID, _TestType)))
            {
                MessageBox.Show("You already have passed the test", "Test Passed",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            FmSchduleTest schduleTest = new FmSchduleTest(_LDLApplicationID, _TestType,0);
            schduleTest.ShowDialog();
            RefreshDGV();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {

            FmSchduleTest schduleTest = new FmSchduleTest(_LDLApplicationID, _TestType,1);
            schduleTest.ShowDialog();
            RefreshDGV();
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            editToolStripMenuItem.Enabled = (clsTestAppointment.IsTestAppointmentActive(
                (int)dgvLicenseTestAppointments.SelectedCells[0].Value));
            takeTestToolStripMenuItem.Enabled = (clsTestAppointment.IsTestAppointmentActive(
                (int)dgvLicenseTestAppointments.SelectedCells[0].Value));
        }

        private void takeTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FmTakeTest takeTest = new FmTakeTest(
                (int)dgvLicenseTestAppointments.SelectedCells[0].Value);
            takeTest.ShowDialog();
            RefreshDGV();
        }
    }
}
