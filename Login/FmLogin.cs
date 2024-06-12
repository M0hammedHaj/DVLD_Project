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

namespace DVLD_Project.Login
{
    public partial class FmLogin : Form
    {
        private clsUser UserInfo;
        public FmLogin()
        {
            InitializeComponent();
        }
        private void ValidateTextBoxes(object sender, CancelEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if(string.IsNullOrEmpty(textBox.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(textBox, "Shouldn't be empty");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(textBox, null);
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FmLogin_Load(object sender, EventArgs e)
        {
            string Username = "",Password = "";
            if(clsGlobal.GetStoredCredentialFromWindowsRegistry(ref Username,ref Password))
            {
                txtUsername.Text = Username;
                txtPassword.Text = Password;
                chbRememberMe.Checked = true;
            }
        }

        private void btLogin_Click(object sender, EventArgs e)
        {
            if(!this.ValidateChildren())
            {
                MessageBox.Show("There are some fields doesn't have a value", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            UserInfo = clsUser.Find(txtUsername.Text.Trim(), txtPassword.Text);
            if (UserInfo != null)
            {
                if (chbRememberMe.Checked)
                {
                    clsGlobal.SaveCredentialInWindowsRegistry(txtUsername.Text, txtPassword.Text);
                    //here
                }
                else
                    clsGlobal.SaveCredentialInWindowsRegistry("", "");
                //here

                if (UserInfo.IsActive != false)
                {
                    clsGlobal.LogedInUser = UserInfo;
                    this.Hide();
                    FmMain Main = new FmMain(this);
                    Main.ShowDialog();
                }
                else
                {
                    MessageBox.Show("This user not active,contact admin", "User not active",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else
                MessageBox.Show("Invalid Username/Password.", "Wrong Credintials", MessageBoxButtons.OK
                    , MessageBoxIcon.Error);
        }
    }
}
