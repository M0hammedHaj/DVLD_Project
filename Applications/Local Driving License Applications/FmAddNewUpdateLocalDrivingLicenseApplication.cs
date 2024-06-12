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

namespace DVLD_Project.Applications.Local_Driving_License
{
    public partial class FmAddNewUpdateLocalDrivingLicenseApplication : Form
    {
        enum enApplicationStatue { New = 1,Cancelled,Completed }
        enum enMode { Add,Update};
        enMode Mode = enMode.Add;

        float Fees;
        private int _LDLApplicationID = -1;
        private DataTable _dtLicenseClasses;
        private clsApplication _ApplicationInfo = new clsApplication();
        private clsLocalDrivingLicenseApplication _LocalDrivingLicenseApplicationInfo
            = new clsLocalDrivingLicenseApplication();
        public FmAddNewUpdateLocalDrivingLicenseApplication()
        {
            InitializeComponent();
            Mode = enMode.Add;
        }

        public FmAddNewUpdateLocalDrivingLicenseApplication(int LDLApplicationID)
        {
            InitializeComponent();
            _LDLApplicationID = LDLApplicationID;
            _LocalDrivingLicenseApplicationInfo = 
                clsLocalDrivingLicenseApplication.Find(_LDLApplicationID);
            _ApplicationInfo = 
                clsApplication.Find(_LocalDrivingLicenseApplicationInfo.ApplicationID);
            Mode = enMode.Update;
        }

        private void btClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btNext_Click(object sender, EventArgs e)
        {
            if (Mode == enMode.Add)
            {
                if (uctrlPersonCardWithFilter1.PersonID == -1)
                {
                    MessageBox.Show("You must choose a person first", "Choose Person",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                tabControl1.SelectedTab = tabControl1.TabPages[1];
                tpLocalDrivingLicenseInfo.Enabled = true;
                _GiveLocalDrivingLicenseControlsInitialValues();
                btSave.Enabled = true;
            }
            else
                tabControl1.SelectedTab = tabControl1.TabPages[1];
        }

        private void _GiveLocalDrivingLicenseControlsInitialValues()
        {
            _FillLicensesNameComboBox();
            lblApplicationDate.Text = DateTime.Now.ToShortDateString();
            Fees = clsApplicationType.GetLocalDrivingLicenseFee();
            lblFees.Text = Fees.ToString();
            cbLicenseClass.SelectedIndex = 0;
        }

        private void _FillLicensesNameComboBox()
        {
            _dtLicenseClasses = clsLicenseClass.GetAllLicensesIDNameMinimumAllowAge();

            foreach(DataRow Row in _dtLicenseClasses.Rows)
            {
                cbLicenseClass.Items.Add(Row[1].ToString());
            }
        }

        private void FmAddNewLocalDrivingLicense_Load(object sender, EventArgs e)
        {
            if(Mode == enMode.Add)
            {
                btSave.Enabled = false;
                tpLocalDrivingLicenseInfo.Enabled = false;
            }
            else
            {
                _MakeUpdateActions();
            }
        }
        private void _MakeUpdateActions()
        {
            btSave.Enabled = true;
            tpLocalDrivingLicenseInfo.Enabled = true;
            uctrlPersonCardWithFilter1.LoadPerson(_ApplicationInfo.ApplicantPersonID);
            _GiveLocalDrivingLicenseControlsInitialValues();
            _LoadLDLApplicationInfo();
        }
        private void _LoadLDLApplicationInfo()
        {
            lblLocalDrivingLicebseApplicationID.Text = 
                _LocalDrivingLicenseApplicationInfo.LocalDrivingLicenseApplicationID.ToString();
            lblApplicationDate.Text = 
                _ApplicationInfo.ApplicationDate.ToShortDateString();
            cbLicenseClass.Text = clsLicenseClass.
                Find(_LocalDrivingLicenseApplicationInfo.LicenseClassID).ClassName;
            lblFees.Text = _ApplicationInfo.PaidFees.ToString();
            lblCreatedByUser.Text = _ApplicationInfo.CreatedByUserID.ToString();
        }
        private void btSave_Click(object sender, EventArgs e)
        {
            if (clsApplication.IsLocalDrivingLicenseApplicationExistAndNew(
                uctrlPersonCardWithFilter1.PersonID, _GetLicenseClassID()))
            {
                MessageBox.Show("You already have an active appllication", "Exist",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            if(clsApplication.IsLDLApplicationCompleted(uctrlPersonCardWithFilter1.PersonID, _GetLicenseClassID()))
            {
                MessageBox.Show("You already have license from this class", "Class Existed",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int PersonAgeInYears = _GetPersonAgeInYears();
            int LicenseMinimumAllowedAge = _GetLocalDrivingLicenseMinimumAge();

            if (PersonAgeInYears < LicenseMinimumAllowedAge)
            {
                MessageBox.Show("The minimum allow age to get this license is "
                    + LicenseMinimumAllowedAge.ToString(), "Age",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _SaveApplicationInfo();
        }
        private int _GetLicenseClassID()
        {
            foreach(DataRow Row in _dtLicenseClasses.Rows)
            {
                if (Row["ClassName"].ToString() == cbLicenseClass.Text)
                    return int.Parse(Row["LicenseClassID"].ToString());
            }
            return 0;
        }
        private void _SaveApplicationInfo()
        {

            if (Mode == enMode.Add)
            {
                _ApplicationInfo.ApplicationStatus =
                    Convert.ToByte(enApplicationStatue.New);
                _ApplicationInfo.LastStatusDate = DateTime.Now;
                _ApplicationInfo.ApplicationDate = DateTime.Now;
                _ApplicationInfo.ApplicationTypeID = 1; //LocalDrivingLicense
                _ApplicationInfo.ApplicantPersonID = uctrlPersonCardWithFilter1.PersonID;
                _ApplicationInfo.CreatedByUserID = clsGlobal.LogedInUser.UserID;
                _ApplicationInfo.PaidFees = Fees;

                if (_ApplicationInfo.Save())
                {
                    _LocalDrivingLicenseApplicationInfo.ApplicationID =
                        _ApplicationInfo.ApplicationID;
                    _LocalDrivingLicenseApplicationInfo.LicenseClassID =
                        _GetLicenseClassID();

                    if (_LocalDrivingLicenseApplicationInfo.Save())
                    {
                        MessageBox.Show("Application saved successfully", "Saved"
                            , MessageBoxButtons.OK, MessageBoxIcon.Information);

                        lblLocalDrivingLicebseApplicationID.Text =
                            _ApplicationInfo.ApplicationID.ToString();
                        lblCreatedByUser.Text = _ApplicationInfo.CreatedByUserID.ToString();
                    }
                }
            }
            else
            {
                _ApplicationInfo.LastStatusDate = DateTime.Now;
                if(_ApplicationInfo.Save())
                {
                    _LocalDrivingLicenseApplicationInfo.LicenseClassID = 
                        _GetLicenseClassID();

                    if(_LocalDrivingLicenseApplicationInfo.Save())
                        MessageBox.Show("Application saved successfully", "Saved"
                            , MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

        }
        private int _GetLocalDrivingLicenseMinimumAge()
        {
            foreach(DataRow Row in _dtLicenseClasses.Rows)
            {
                if (Row["ClassName"].ToString() == cbLicenseClass.Text)
                    return int.Parse(Row["MinimumAllowedAge"].ToString());
            }
            return -1;
        }
        private int _GetPersonAgeInYears()
        {
            return DateTime.Now.Year -
             uctrlPersonCardWithFilter1.SelectedPersonInfo.DateOfBirth.Year;
        }
    }
}
