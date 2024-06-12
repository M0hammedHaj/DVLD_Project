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

namespace DVLD_Project.Users
{
    public partial class FmAddUpdateUser : Form
    {
        private int _PersonID = -1;
        private int _UserID = -1;
        private clsUser UserInfo = new clsUser();

        private enum enMode { Update,Add};
        enMode Mode = enMode.Add;
        public FmAddUpdateUser()
        {
            InitializeComponent();
            Mode = enMode.Add;
        }
        public FmAddUpdateUser(int UserID)
        {
            InitializeComponent();
            this._UserID = UserID;
            Mode = enMode.Update;
        }

        private void uctrlPersonCardWithFilter1_OnPersonSelected(int obj)
        {
            tpLoginInfo.Enabled = false;

            _PersonID = obj;
            if (_PersonID == -1)
            {
                btNext.Enabled = false;
                btSave.Enabled = false;
                tpLoginInfo.Enabled = false;
                return;
            }
            
            btNext.Enabled = true;
            btSave.Enabled = false;
        }

        private void btNext_Click(object sender, EventArgs e)
        {
            if(clsUser.IsUserExistForPerosnID(_PersonID) && Mode == enMode.Add)
            {
                MessageBox.Show("This person is user","User exist",MessageBoxButtons.OK
                    ,MessageBoxIcon.Error );
                return;
            }

            tabControl1.SelectedTab = tabControl1.TabPages[1];

            tpLoginInfo.Enabled = true;
            btSave.Enabled = true;
        }

        private void btClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtUsername_Validating(object sender, CancelEventArgs e)
        {
            if(string.IsNullOrEmpty(txtUsername.Text.Trim()))
            {
                errorProvider1.SetError(txtUsername, "Username should have a value");
                e.Cancel = true;
                return;
            }
            if(clsUser.IsUserExist(txtUsername.Text) && Mode==enMode.Add)
            {
                errorProvider1.SetError(txtUsername, "This username already exist");
                e.Cancel = true;
            }
            else
            {
                errorProvider1.SetError(txtUsername, null);
                e.Cancel = false;
            }
        }

        private void txtPassword_Validating(object sender, CancelEventArgs e)
        {
            if(string.IsNullOrEmpty(txtPassword.Text.Trim()))
            {
                errorProvider1.SetError(txtPassword, "Password should have a value");
                e.Cancel = true;
            }
            else
            {
                errorProvider1.SetError(txtPassword, null);
                e.Cancel = false;
            }
        }

        private void txtConfirmPassword_Validating(object sender, CancelEventArgs e)
        {
            if(string.IsNullOrEmpty(txtConfirmPassword.Text.Trim()))
            {
                errorProvider1.SetError(txtConfirmPassword, "Confirm password should" +
                    " have a value");
                e.Cancel = true;
                return;
            }
            if(txtPassword.Text.Trim() != txtConfirmPassword.Text.Trim())
            {
                errorProvider1.SetError(txtConfirmPassword, "There is no match");
                e.Cancel = true;
            }
            else
            {
                errorProvider1.SetError(txtConfirmPassword, null);
                e.Cancel = false;
            }
        }

        private void btSave_Click(object sender, EventArgs e)
        {

            if(!this.ValidateChildren())
            {
                MessageBox.Show("There are some fields don't have a value or there is an error",
                    "Fields required or wrong entry", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _SaveUserData();
        }
        private void _SaveUserData()
        {
            UserInfo.UserName = txtUsername.Text.Trim();
            UserInfo.Password = txtPassword.Text.Trim();
            UserInfo.IsActive = chbIsActive.Checked;
            UserInfo.PersonID = _PersonID;
            if (UserInfo.Save())
                MessageBox.Show("User saved successfully", "Saved", MessageBoxButtons.OK
                    , MessageBoxIcon.Information);
            else
                MessageBox.Show("User saving faild", "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
        }

        private void FmAddUpdateUser_Load(object sender, EventArgs e)
        {
            tpLoginInfo.Enabled = false;
            if(Mode == enMode.Update)
            {
                _LoadData();
            }
        }
        private void _LoadData()
        {
            UserInfo = clsUser.Find(_UserID);
            uctrlPersonCardWithFilter1.LoadPerson(UserInfo.PersonID);

            txtUsername.Text = UserInfo.UserName;
            txtPassword.Text = UserInfo.Password;
            txtConfirmPassword.Text = UserInfo.Password;
            chbIsActive.Checked = UserInfo.IsActive;

            btSave.Enabled = true;
            tpLoginInfo.Enabled = true;
        }
    }
}
