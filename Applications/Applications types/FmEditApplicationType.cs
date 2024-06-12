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

namespace DVLD_Project.Applications.Applications_types
{
    public partial class FmEditApplicationType : Form
    {
        private int _ApplicationTypeID;
        private clsApplicationType ApplicationTypeInfo;
        public FmEditApplicationType(int ApplicationTypeID)
        {
            InitializeComponent();
            _ApplicationTypeID = ApplicationTypeID;
        }

        private void FmEditApplicationType_Load(object sender, EventArgs e)
        {
            ApplicationTypeInfo = clsApplicationType.Find(_ApplicationTypeID);

            lblApplicationTypeID.Text = ApplicationTypeInfo.ApplicationTypeID.ToString();
            txtApplicationTypeTitle.Text = ApplicationTypeInfo.ApplicationTypeTitle;
            txtFees.Text = ApplicationTypeInfo.ApplicationFees.ToString();
        }

        private void btClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ValidateEmptyTextBoxes(object sender,CancelEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if(string.IsNullOrEmpty(textBox.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(textBox, "Shouldn't be blank");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(textBox, null);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(!this.ValidateChildren())
            {
                MessageBox.Show("Some fields aren't valid!,put the mouse over the red icon(s) to see the error",
                    "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            ApplicationTypeInfo.ApplicationTypeTitle = txtApplicationTypeTitle.Text;
            ApplicationTypeInfo.ApplicationFees = int.Parse(txtFees.Text);

            if(ApplicationTypeInfo.Save())
            {
                MessageBox.Show("Updated successfully","Updated",MessageBoxButtons.OK
                    ,MessageBoxIcon.Information);
            }
            else 
            {
                MessageBox.Show("Didn't update","Error",MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void txtFees_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
    }
}
