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

namespace DVLD_Project.Tests
{
    public partial class FmTakeTest : Form
    {
        private int _TestAppointmentID;

        public FmTakeTest(int testAppointmentID)
        {
            InitializeComponent();
            _TestAppointmentID = testAppointmentID;
        }

        private bool HandleRetakeApplication(clsTestAppointment TestAppointment)
        {
            if(TestAppointment.RetakeTestApplicationID != -1)
            {
                clsApplication Retake = clsApplication.Find(
                    TestAppointment.RetakeTestApplicationID);

                Retake.ApplicationStatus = (int)clsApplication.enApplicationStatue.Completed;

                if (Retake.Save())
                    return true;
                else
                    return false;
            }

            return true;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            clsTest TestInfo = new clsTest();
            clsTestAppointment TestAppointment = clsTestAppointment.Find(
                _TestAppointmentID);

            TestInfo.TestAppointmentID = _TestAppointmentID;
            TestInfo.TestResult = rbPass.Checked;
            TestInfo.CreatedByUserID = clsGlobal.LogedInUser.UserID;
            TestAppointment.IsLocked = true;

            if(!string.IsNullOrEmpty(txtNotes.Text))
                TestInfo.Notes = txtNotes.Text;

            if (!HandleRetakeApplication(TestAppointment))
                return;


            if (TestInfo.Save() && TestAppointment.Save())
                MessageBox.Show("Data saved successfully", "Saved", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            else
                MessageBox.Show("Data didn't save", "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
        }

        private void FmTakeTest_Load(object sender, EventArgs e)
        {
            uctrlSchduledTest1.LoadSchduledTest(_TestAppointmentID);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
