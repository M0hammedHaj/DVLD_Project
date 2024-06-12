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

namespace DVLD_Project.Licenses.Local_licenses.Controls
{
    public partial class uctrlShowLicenseWithFilter : UserControl
    {
        

        public event Action<int> OnLicenseSelected;
        public clsLicense SelectedLicenseInfo;
        public bool FilterEnabled
        {
            get
            {
                return gbFilters.Enabled;
            }
            set
            {
                gbFilters.Enabled = value;
            }
        }
        
        public uctrlShowLicenseWithFilter()
        {
            InitializeComponent();
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            if(!string.IsNullOrEmpty(txtLicenseID.Text) 
                && int.TryParse(txtLicenseID.Text,out int LicenseID))
            {
                SelectedLicenseInfo = clsLicense.Find(LicenseID);
                if (SelectedLicenseInfo != null)
                {
                    uctrlShowLicense1.LoadLicenseInfo(LicenseID);
                    OnLicenseSelected(LicenseID);
                }
                else
                {
                    errorProvider1.SetError(txtLicenseID,
                        $"License with LicenseID : {LicenseID} didn't find");
                    MessageBox.Show($"License with LicenseID : {LicenseID} didn't find",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("LicenseID shouldn't be blank", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                errorProvider1.SetError(txtLicenseID, "LicenseID shouldn't be blank");
            }
        }

        private void txtLicenseID_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);

            if(e.KeyChar == (char)13)
            {
                btnFind.PerformClick();
            }
        }
        
        public void txtFilterFocus()
        {
            txtLicenseID.Focus();
        }
    }
}
