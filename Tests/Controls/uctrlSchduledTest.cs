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

namespace DVLD_Project.Tests.Controls
{
    public partial class uctrlSchduledTest : UserControl
    {
        private int _TestAppointmentID;
        private clsTestAppointment _TestAppointmentInfo;
        public uctrlSchduledTest()
        {
            InitializeComponent();
        }

        public void LoadSchduledTest(int TesAppointmentID)
        {
            _TestAppointmentID = TesAppointmentID;

            _TestAppointmentInfo = clsTestAppointment.Find(TesAppointmentID);
            if(_TestAppointmentInfo != null)
            {
                FillFields();
                return;
            }
            MessageBox.Show("Test appointment didn't find", "Not Exist",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void FillFields()
        {
            switch(_TestAppointmentInfo.TestTypeID)
            {
                case (int)clsTestType.enTestType.Vision:
                    pbTestTypeImage.Image = Resources.Vision_512;
                    lblTitle.Text = "Vision Test";
                    break;

                case (int)clsTestType.enTestType.Written:
                    pbTestTypeImage.Image = Resources.Written_Test_512;
                    lblTitle.Text = "Written Test";
                    break;


                case (int)clsTestType.enTestType.Street:
                    pbTestTypeImage.Image = Resources.driving_test_512;
                    lblTitle.Text = "Driving Test";
                    break;
            }


            lblDate.Text = _TestAppointmentInfo.AppointmentDate.ToShortDateString();
            lblDrivingClass.Text = clsLicenseClass.Find(
                clsLocalDrivingLicenseApplication.Find(
                    _TestAppointmentInfo.LocalDrivingLicenseApplicationID).LicenseClassID).ClassName;
            lblFees.Text = clsTestType.Find(
                _TestAppointmentInfo.TestTypeID).TestTypeFees.ToString();
            lblFullName.Text = clsUser.Find(
                _TestAppointmentInfo.CreatedByUserID).SelectedPersonInfo.FullName;
            lblLocalDrivingLicenseAppID.Text = _TestAppointmentInfo.
                LocalDrivingLicenseApplicationID.ToString();
            lblTrial.Text = clsTestAppointment.GetTrialsForTestType(
                _TestAppointmentInfo.LocalDrivingLicenseApplicationID, 
                (clsTestType.enTestType)_TestAppointmentInfo.TestTypeID).ToString();
        }
    }
}
