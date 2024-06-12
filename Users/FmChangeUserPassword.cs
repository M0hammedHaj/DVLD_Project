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

namespace DVLD_Project.Users
{
    public partial class FmChangeUserPassword : Form
    {
        public FmChangeUserPassword()
        {
            InitializeComponent();
        }

        private void txtCurrentPassword_Validating(object sender, CancelEventArgs e)
        {
            if(string.IsNullOrEmpty(txtCurrentPassword.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtCurrentPassword,
                    "Current password shouldn't be blank");
                return;
            }

            if(txtCurrentPassword.Text != clsGlobal.LogedInUser.Password)
            {
                e.Cancel = true;
                errorProvider1.SetError(txtCurrentPassword,
                    "Current password isn't correct");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtCurrentPassword, null);
            }
        }

        private void txtNewPassword_Validating(object sender, CancelEventArgs e)
        {
            if(string.IsNullOrEmpty(txtNewPassword.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtNewPassword,
                    "New password shouldn't be blank");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtNewPassword, null);
            }
        }

        private void txtConfirmNewPassword_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtConfirmNewPassword.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtConfirmNewPassword,
                    "Confirm password shouldn't be blank");
                return;
            }

            if(txtConfirmNewPassword.Text !=  txtNewPassword.Text)
            {
                e.Cancel = true;
                errorProvider1.SetError(txtConfirmNewPassword, "No match");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtConfirmNewPassword, null);
            }
        }

        private void btChange_Click(object sender, EventArgs e)
        {
            if(!this.ValidateChildren())
            {
                MessageBox.Show("Some fields aren't valide!,put the mouse over the red icon(s) to see the error",
                    "Validate error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            clsGlobal.LogedInUser.Password = txtNewPassword.Text;
            if (clsGlobal.LogedInUser.Save())
                MessageBox.Show("Password changed successfully");
            else
                MessageBox.Show("Password didn't change", "Error", MessageBoxButtons.OK
                    , MessageBoxIcon.Error);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FmChangeUserPassword_Load(object sender, EventArgs e)
        {
            uctrlUserCard1.LoadUserInfo(clsGlobal.LogedInUser.UserID);
        }
    }
}
