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

namespace DVLD_Project.Tests.TestTypes
{
    public partial class FmEditTestTypes : Form
    {
        private int _TestTypeID;
        private clsTestType _TestTypeInfo;

        public FmEditTestTypes(int testTypeID)
        {
            InitializeComponent();
            _TestTypeID = testTypeID;
        }

        private void FmEditTestTypes_Load(object sender, EventArgs e)
        {
            _TestTypeInfo = clsTestType.Find(_TestTypeID);

            if( _TestTypeInfo != null )
            {
                lblTestTypeID.Text = _TestTypeInfo.TestTypeID.ToString();
                txtTestTypeTitle.Text = _TestTypeInfo.TestTypeTitle;
                txtDescription.Text = _TestTypeInfo.TestTypeDescription;
                txtFees.Text = _TestTypeInfo.TestTypeFees.ToString();
            }
        }

        private void btClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ValidateEmptyTextBoxes(object sender, CancelEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (string.IsNullOrEmpty(textBox.Text))
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

            _TestTypeInfo.TestTypeTitle = txtTestTypeTitle.Text;
            _TestTypeInfo.TestTypeDescription = txtDescription.Text;
            _TestTypeInfo.TestTypeFees = int.Parse(txtFees.Text);

            if(_TestTypeInfo.Save())
            {
                MessageBox.Show("Updated successfully", "Updated", MessageBoxButtons.OK
                    , MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Didn't update", "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void txtFees_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
    }
}
