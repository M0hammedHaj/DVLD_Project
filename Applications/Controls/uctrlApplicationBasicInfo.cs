using DVLD_Project.People;
using DVLD_Project.People.Controls;
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

namespace DVLD_Project.Applications.Controls
{
    public partial class uctrlApplicationBasicInfo : UserControl
    {
        clsApplication _ApplicationInfo;
        clsUser _UserInfo;
        clsPerson _PersonInfo;

        public clsApplication ApplicationInfo
        {
            get
            { return _ApplicationInfo; }
        }
        public clsUser UserInfo
        {
            get
            {
                return _UserInfo;
            }
        }
        public clsPerson PersonInfo
        {
            get
            {
                return _PersonInfo;
            }
        }
        public uctrlApplicationBasicInfo()
        {
            InitializeComponent();
        }

        public void LoadApplicationInfo(int ApplicationID)
        {
            _ApplicationInfo = clsApplication.Find(ApplicationID);

            if(_ApplicationInfo != null)
            {
                _FillControls();
                return;
            }

            _ResetControls();
        }

        public void _ResetControls()
        {
            lblType.Text = "[???]";
            lblApplicant.Text = "[???]";
            lblCreatedByUser.Text = "[???]";
            lblFees.Text = "[$$$]";
            lblApplicationID.Text = "[???]";
            lblDate.Text = "[??/??/????]";
            lblStatus.Text = "[???]";
            lblStatusDate.Text = "[??/??/????]";

            llViewPersonInfo.Enabled = false;
        }
        private void _FillControls() 
        {
            _PersonInfo = clsPerson.Find(_ApplicationInfo.ApplicantPersonID);
            _UserInfo = clsUser.Find(_ApplicationInfo.CreatedByUserID);
            llViewPersonInfo.Enabled = true;

            lblStatusDate.Text = _ApplicationInfo.LastStatusDate.ToShortDateString();
            lblApplicant.Text = _PersonInfo.FullName;
            lblApplicationID.Text = _ApplicationInfo.ApplicationID.ToString();
            lblCreatedByUser.Text = _UserInfo.UserName;
            lblDate.Text = _ApplicationInfo.ApplicationDate.ToShortDateString();
            lblFees.Text = _ApplicationInfo.PaidFees.ToString();
            lblType.Text = clsApplicationType.Find(
                _ApplicationInfo.ApplicationTypeID).ApplicationTypeTitle;



            switch(_ApplicationInfo.ApplicationStatus)
            {
                case 1:
                    lblStatus.Text = "New";
                    break;

                case 2:
                    lblStatus.Text = "Canceled";
                    break;

                case 3:
                    lblStatus.Text = "Completed";
                    break;
            }
        }

        private void llViewPersonInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FmShowPersonInfo showPersonInfo = new FmShowPersonInfo(PersonInfo.PersonID);
            showPersonInfo.ShowDialog();
        }


    }
}
