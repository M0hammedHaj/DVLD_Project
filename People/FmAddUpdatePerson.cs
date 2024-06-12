using DVLD_Project.Global_clases;
using DVLD_Project.Properties;
using DVLDBusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_Project.People
{
    public partial class FmAddUpdatePerson : Form
    {
        public delegate void SendDataBack(int PersonID);
        public event SendDataBack DataBack;

        private int _PersonID = -1;
        private clsPerson _PersonInfo;

        public clsPerson PersonInfo
        {
            get { return _PersonInfo; }
        }
        enum enMode { Add,Update};
        enMode Mode= enMode.Add;

        public FmAddUpdatePerson()
        {
            InitializeComponent();
            Mode = enMode.Add;
        }
        public FmAddUpdatePerson(int PersonID)
        {
            InitializeComponent();
            this._PersonID = PersonID;
            Mode = enMode.Update;
        }

        private void ValidateEmptyTextBox(object sender, CancelEventArgs e)
        {
            TextBox Temp = (TextBox)sender;
            if(string.IsNullOrEmpty(Temp.Text.Trim()))
            {
                errorProvider1.SetError(Temp, "This field is required!");
                e.Cancel = true;
            }
            else
            {
                errorProvider1.SetError(Temp, null);
                e.Cancel = false;
            }
        }

        private void txtEmail_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtEmail.Text.Trim()))
                return;

            if (!clsValidation.ValidateEmail(txtEmail.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtEmail, "Invalid Email Address Format!");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtEmail, null);
            };
        }

        private void txtNationalNo_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtNationalNo.Text.Trim()))
            {
                errorProvider1.SetError(txtNationalNo, "This field is required!");
                e.Cancel = true;
            }
            else if (txtNationalNo.Text.Trim() != _PersonInfo.NationalNo 
                && clsPerson.IsPersonExist(txtNationalNo.Text.Trim()))
            {
                errorProvider1.SetError(txtNationalNo,
                    "National number is used by another person");
                e.Cancel = true;
            }
            else
            {
                errorProvider1.SetError(txtNationalNo,null);
                e.Cancel = false;
            }
        }

        private void FmAddUpdatePerson_Load(object sender, EventArgs e)
        {
            ResetDefaultValues();

            if(Mode == enMode.Update)
            {
                _PersonInfo = clsPerson.Find(_PersonID);
                if(_PersonInfo != null)
                {
                    LoadData();
                }
            }
        }
        private void ResetDefaultValues()
        {
            AddCountriesToComboBox();

            _PersonInfo = new clsPerson();

            dtpDateOfBirth.MaxDate = DateTime.Today.AddYears(-18);
            dtpDateOfBirth.Value = dtpDateOfBirth.Value;
            dtpDateOfBirth.MinDate = DateTime.Today.AddYears(-118);
        }
        private void AddCountriesToComboBox()
        {
            DataTable dtCountries = clsCountry.GetAllCountries();
            foreach(DataRow Row in dtCountries.Rows)
            {
                cbCountry.Items.Add(Row["CountryName"].ToString());
            }
        }
        private void LoadData()
        {
            lblTitle.Text = "Update Person";
            lblPersonID.Text = _PersonInfo.PersonID.ToString();
            txtAddress.Text = _PersonInfo.Address;
            txtEmail.Text = _PersonInfo.Email;
            txtFirstName.Text = _PersonInfo.FirstName;
            txtLastName.Text = _PersonInfo.LastName;
            txtNationalNo.Text = _PersonInfo.NationalNo;
            txtPhone.Text = _PersonInfo.Phone;
            txtSecondName.Text = _PersonInfo.SecondName;
            txtThirdName.Text = _PersonInfo.ThirdName;
            dtpDateOfBirth.Value = _PersonInfo.DateOfBirth;
            cbCountry.SelectedItem = clsCountry.Find(_PersonInfo.NationalityCountryID)
                .CountryName;

            if (_PersonInfo.Gendor == 0)
                rbMale.Checked = true;
            else
                rbFemale.Checked = true;

            if (_PersonInfo.ImagePath != "")
            {
                pbPersonImage.ImageLocation = _PersonInfo.ImagePath;
                llRemoveImage.Visible = true;
            }
                
        }
        private void rbMale_CheckedChanged(object sender, EventArgs e)
        {
            if (pbPersonImage.ImageLocation == null)
                pbPersonImage.Image = Resources.Male_512;
        }

        private void rbFemale_CheckedChanged(object sender, EventArgs e)
        {
            if (pbPersonImage.ImageLocation == null)
                pbPersonImage.Image = Resources.Female_512;
        }

        private void llSetImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            openFileDialog1.InitialDirectory = @"E:\";
            openFileDialog1.Title = "Choose an image";
            if(openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                pbPersonImage.ImageLocation = openFileDialog1.FileName;

                llRemoveImage.Visible = true;
            }
        }

        private void llRemoveImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            pbPersonImage.ImageLocation = null;
            llRemoveImage.Visible = false;

            if (rbMale.Checked)
                pbPersonImage.Image = Resources.Male_512;
            else
                pbPersonImage.Image = Resources.Female_512;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(!this.ValidateChildren())
            {
                MessageBox.Show("Some fileds are not valide!," +
                    " put the mouse over the red icon(s) to see the erro",
                    "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!HandlePersonImage())
                return;

            int NationalityCountryID = clsCountry.Find(cbCountry.Text).CountryID;

            _PersonInfo.FirstName = txtFirstName.Text.Trim();
            _PersonInfo.SecondName = txtSecondName.Text.Trim();
            _PersonInfo.ThirdName = txtThirdName.Text.Trim();
            _PersonInfo.LastName = txtLastName.Text.Trim();
            _PersonInfo.NationalNo = txtNationalNo.Text.Trim();
            _PersonInfo.Email = txtEmail.Text.Trim();
            _PersonInfo.Phone = txtPhone.Text.Trim();
            _PersonInfo.Address = txtAddress.Text.Trim();
            _PersonInfo.DateOfBirth = dtpDateOfBirth.Value;

            if (rbMale.Checked)
                _PersonInfo.Gendor = 0;
            else
                _PersonInfo.Gendor = 1;

            _PersonInfo.NationalityCountryID = NationalityCountryID;

            if (pbPersonImage.ImageLocation != null)
                _PersonInfo.ImagePath = pbPersonImage.ImageLocation;
            else
                _PersonInfo.ImagePath = "";
            if (_PersonInfo.Save())
            {
                lblPersonID.Text = _PersonInfo.PersonID.ToString();
                //change form mode to update.
                Mode = enMode.Update;
                lblTitle.Text = "Update Person";
                

                MessageBox.Show("Data Saved Successfully.", "Saved",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);



            }
            else
                MessageBox.Show("Error: Data Is not Saved Successfully.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

            DataBack?.Invoke(_PersonInfo.PersonID);
        }
        private bool HandlePersonImage()
        {
            if(_PersonInfo.ImagePath != pbPersonImage.ImageLocation)
            {
                if(_PersonInfo.ImagePath != "")
                {
                    try
                    {
                        File.Delete(_PersonInfo.ImagePath);
                    }
                    catch(IOException IOX)
                    {
                        MessageBox.Show(IOX.Message, "Error", MessageBoxButtons.OK
                            , MessageBoxIcon.Error);
                    }
                }
                if(pbPersonImage.ImageLocation != null)
                {
                    string SourceFile = pbPersonImage.ImageLocation;

                    if(clsUtil.CopyImageWithGuid(ref SourceFile))
                    {
                        pbPersonImage.ImageLocation = SourceFile;
                        return true;
                    }
                    else
                    {
                        MessageBox.Show("Error Copying Image File", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                    
                }
            }
            return true;
        }
    }
}
