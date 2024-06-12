using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVLD_Project.Properties;
using DVLDBusinessLayer;

namespace DVLD_Project.People.Controls
{
    public partial class uctrlPersonCard : UserControl
    {

        public uctrlPersonCard()
        {
            InitializeComponent();
        }

        private int _PersonID = -1;
        public int PersonID
        {
            get { return _PersonID; }
        }

        private clsPerson _PersonInfo;
        public clsPerson SelectedPersonInfo
        {
            get { return _PersonInfo; }
        }

        public void LoadPersonInfo(int PerosnID)
        {
            _PersonInfo = clsPerson.Find(PerosnID);
            if(_PersonInfo == null)
            {
                ResetAllLabels();
                MessageBox.Show("Person with PersonID : " + PerosnID + " didn't find");
                return;
            }
            _FillAllLabels();
        }
        public void LoadPersonInfo(string NationalNO)
        {
            _PersonInfo = clsPerson.Find(NationalNO);
            if (_PersonInfo == null)
            {
                ResetAllLabels();
                MessageBox.Show("Person with NationalNo : " + NationalNO +
                    " didn't find");
                return;
            }
            _FillAllLabels();
        }

        public void ResetAllLabels()
        {
            _PersonID = -1;
            lblPersonID.Text = "[????]";
            lblNationalNo.Text = "[????]";
            lblFullName.Text = "[????]";
            pbGendor.Image = Resources.Man_32;
            lblGendor.Text = "[????]";
            lblEmail.Text = "[????]";
            lblPhone.Text = "[????]";
            lblDateOfBirth.Text = "[????]";
            lblCountry.Text = "[????]";
            lblAddress.Text = "[????]";
            pbPersonImage.Image = Resources.Male_512;
            llEditPersonInfo.Visible = false;
        }
        private void _FillAllLabels()
        {
            llEditPersonInfo.Visible = true;
            _PersonID = _PersonInfo.PersonID;
            lblPersonID.Text = _PersonInfo.PersonID.ToString();
            lblAddress.Text = _PersonInfo.Address;
            lblCountry.Text = clsCountry.Find(_PersonInfo.NationalityCountryID).CountryName;
            lblFullName.Text = _PersonInfo.FullName;
            lblDateOfBirth.Text = _PersonInfo.DateOfBirth.ToShortDateString();
            lblGendor.Text = _PersonInfo.Gendor == 0 ? "Male" : "Female";
            lblNationalNo.Text = _PersonInfo.NationalNo;
            lblPhone.Text = _PersonInfo.Phone;
            lblEmail.Text = _PersonInfo.Email;

            _LoadImagePath();
        }
        private void _LoadImagePath()
        {
            if(_PersonInfo.Gendor == 0)
            {
                pbGendor.Image = Resources.Man_32;
                pbPersonImage.Image = Resources.Male_512;
            }
            else
            {
                pbGendor.Image = Resources.Woman_32;
                pbPersonImage.Image = Resources.Female_512;
            }

            if (_PersonInfo.ImagePath != "")
            {
                if (File.Exists(_PersonInfo.ImagePath))
                    pbPersonImage.ImageLocation = _PersonInfo.ImagePath;
                else
                    MessageBox.Show("Could not find this image: = " + _PersonInfo.ImagePath,
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void llEditPersonInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FmAddUpdatePerson AddUpdatePerson = new FmAddUpdatePerson(
                PersonID);
            AddUpdatePerson.ShowDialog();
            this.LoadPersonInfo(PersonID);
        }
    }
}
