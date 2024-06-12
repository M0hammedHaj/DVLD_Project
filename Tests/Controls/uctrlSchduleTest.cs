using DVLD_Project.Global_clases;
using DVLD_Project.Properties;
using DVLDBusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_Project.Tests.Controls
{
    public partial class uctrlSchduleTest : UserControl
    {
        enum enMode { Add,Update}
        enMode Mode = enMode.Add;
        enum enCreationMode { First,Retake}
        enCreationMode CreationMode = enCreationMode.First;

        private int _LDLApplicationID = -1;
        private clsLocalDrivingLicenseApplication _LDLApplicationInfo;
        private clsApplication _LDLBaseApplicationInfo;
        private clsLicenseClass _LicenseClassInfo;
        private clsTestAppointment _TestAppointmentInfo;
        private clsApplication ReTake;

        private clsTestType.enTestType _TestType = clsTestType.enTestType.Vision;
        public clsTestType.enTestType TestType
        {
            get
            {
                return _TestType;
            }
            set
            {
                _TestType = value;

                switch(_TestType)
                {
                    case clsTestType.enTestType.Vision:
                        pbTestTypeImage.Image = Resources.Vision_512;
                        lblTitle.Text = "Vision Test";
                        break;

                    case clsTestType.enTestType.Written:
                        pbTestTypeImage.Image = Resources.Written_Test_512;
                        lblTitle.Text = "Written Test";
                        break;

                    case clsTestType.enTestType.Street:
                        pbTestTypeImage.Image = Resources.driving_test_512;
                        lblTitle.Text = "Street Test";
                        break;
                }
            }
        }

        public uctrlSchduleTest()
        {
            InitializeComponent();
        }

       public void LoadSchduleTest(int LDLApplicationID,clsTestType.enTestType testType,int ModeAsInt)
       {
            _LDLApplicationID = LDLApplicationID;
            TestType = testType;

             //Not working propably
            CreationMode = (!clsTestAppointment.IsThereTestAppointment(LDLApplicationID,
                testType) || clsTestAppointment.IsThereOneTestAppointmentAndActive(
                    LDLApplicationID,(int)testType)) ? enCreationMode.First : 
                    enCreationMode.Retake;

            Mode = (enMode)ModeAsInt;

            if (Mode == enMode.Update)
            {
                _TestAppointmentInfo = clsTestAppointment.Find(
                    clsTestAppointment.GetLatestTestAppintmentID(
                        LDLApplicationID, testType));
                dtpTestDate.MinDate = _TestAppointmentInfo.AppointmentDate;
            }
            else
            {
                _TestAppointmentInfo = new clsTestAppointment();
                dtpTestDate.MinDate = DateTime.Now;
            }

            FillControls();
       }

        private void FillControls()
        {
            _LDLApplicationInfo = clsLocalDrivingLicenseApplication.
                Find(_LDLApplicationID);
            _LDLBaseApplicationInfo = clsApplication.
                Find(_LDLApplicationInfo.ApplicationID);
            _LicenseClassInfo = clsLicenseClass.
                Find(_LDLApplicationInfo.LicenseClassID);

            lblDrivingClass.Text = _LicenseClassInfo.ClassName;
            lblFees.Text = clsTestType.Find((int)_TestType).TestTypeFees.ToString();
            lblFullName.Text = clsPerson.Find(_LDLBaseApplicationInfo.ApplicantPersonID).
                FullName;
            lblLocalDrivingLicenseAppID.Text = _LDLApplicationID.ToString();
            lblTrial.Text = clsTestAppointment.GetTrialsForTestType(_LDLApplicationID,
                TestType).ToString();

            if(CreationMode == enCreationMode.First)
            {
                gbRetakeTest.Enabled = false;
                lblRetakeTestAppID.Text = "[???]";
                lblRetakeAppFees.Text = "[???]";
                lblTotalFees.Text = "[???]";
            }
            
            else
            {
                if (Mode == enMode.Add)
                {
                    ReTake = new clsApplication();

                    gbRetakeTest.Enabled = true;
                    lblRetakeTestAppID.Text = "[???]";
                    lblRetakeAppFees.Text = clsApplicationType.Find((int)clsApplicationType.enAppType.RetakeTest).ApplicationFees.ToString();
                    lblTotalFees.Text = (Convert.ToSingle(lblFees.Text) + Convert.ToSingle(lblRetakeAppFees.Text)).ToString();
                }
                else
                {
                    ReTake = clsApplication.Find(_TestAppointmentInfo.RetakeTestApplicationID);

                    gbRetakeTest.Enabled = true;
                    lblRetakeTestAppID.Text = ReTake.ApplicationID.ToString();
                    lblRetakeAppFees.Text = clsApplicationType.Find((int)clsApplicationType.enAppType.RetakeTest).ApplicationFees.ToString();
                    lblTotalFees.Text = (Convert.ToSingle(lblFees.Text) + Convert.ToSingle(lblRetakeAppFees.Text)).ToString();
                }
            }
        }

        private bool HandleRetakeTest()
        {
            if(CreationMode == enCreationMode.Retake)
            {

                ReTake.ApplicationStatus = (int)clsApplication.enApplicationStatue.New;
                ReTake.ApplicationDate = DateTime.Now;
                ReTake.LastStatusDate = DateTime.Now;
                ReTake.ApplicantPersonID = _LDLBaseApplicationInfo.ApplicantPersonID;
                ReTake.ApplicationTypeID = (int)clsApplicationType.enAppType.RetakeTest;
                ReTake.CreatedByUserID = clsGlobal.LogedInUser.UserID;
                ReTake.PaidFees = int.Parse(lblRetakeAppFees.Text);

                if (ReTake.Save())
                {
                    lblRetakeTestAppID.Text = ReTake.ApplicationID.ToString();
                    _TestAppointmentInfo.RetakeTestApplicationID = ReTake.ApplicationID;
                    return true;
                }

                return false;
            }
            return true;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!HandleRetakeTest())
                return;

            _TestAppointmentInfo.PaidFees = int.Parse(lblFees.Text);
            _TestAppointmentInfo.LocalDrivingLicenseApplicationID = _LDLApplicationID;
            _TestAppointmentInfo.AppointmentDate = dtpTestDate.Value;
            _TestAppointmentInfo.CreatedByUserID = clsGlobal.LogedInUser.UserID;
            _TestAppointmentInfo.TestTypeID = (int)TestType;
            _TestAppointmentInfo.IsLocked = false;
            
            if(_TestAppointmentInfo.Save())
            {
                MessageBox.Show("Appointment saved successfully", "Appointment Saved",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                
            }
            else
            {
                MessageBox.Show("Save error", "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            
        }
    }
}
