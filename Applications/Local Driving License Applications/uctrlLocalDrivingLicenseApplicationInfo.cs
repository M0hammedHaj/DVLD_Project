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
    public partial class uctrlLocalDrivingLicenseApplicationInfo : UserControl
    {
        private clsLocalDrivingLicenseApplication _LocalDrivingLicenseApplicationInfo;

        public clsLocalDrivingLicenseApplication LocalDrivingLicenseApplicationInfo
        {
            get
            {
                return _LocalDrivingLicenseApplicationInfo;
            }
        }
        public clsApplication ApplicationInfo
        {
            get
            {
                return uctrlApplicationBasicInfo1.ApplicationInfo;
            }
        }
        public clsUser UserInfo
        {
            get
            {
                return uctrlApplicationBasicInfo1.UserInfo;
            }
        }
        public clsPerson PersonInfo
        {
            get
            {
                return uctrlApplicationBasicInfo1.PersonInfo;
            }
        }
        public uctrlLocalDrivingLicenseApplicationInfo()
        {
            InitializeComponent();
        }

        public void LoadLocalDrivingLicenseApplicationInfo(int ID)
        {
            _LocalDrivingLicenseApplicationInfo =
                clsLocalDrivingLicenseApplication.Find(ID);

            if(_LocalDrivingLicenseApplicationInfo == null)
            {
                ResetControls();
                return;
            }
            LoadData();
        }
        public void ResetControls()
        {
            llShowLicenceInfo.Enabled = false;
            lblAppliedFor.Text = "[???]";
            lblLocalDrivingLicenseApplicationID.Text = "[???]";
            lblPassedTests.Text = "0";
            uctrlApplicationBasicInfo1._ResetControls();
        }

        private void LoadData()
        {
            uctrlApplicationBasicInfo1.LoadApplicationInfo(
                _LocalDrivingLicenseApplicationInfo.ApplicationID);

            lblPassedTests.Text = LocalDrivingLicenseApplicationInfo.
                                PassedTests.ToString();
            lblAppliedFor.Text = clsLicenseClass.Find(
                LocalDrivingLicenseApplicationInfo.LicenseClassID).ClassName;
            lblLocalDrivingLicenseApplicationID.Text =
                _LocalDrivingLicenseApplicationInfo.LocalDrivingLicenseApplicationID.ToString();
        }
    }
}
