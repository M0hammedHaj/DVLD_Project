using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_Project.Licenses.Local_licenses
{
    public partial class FmShowLicenseInfo : Form
    {
        int _ApplicationID;
        public FmShowLicenseInfo(int ApplicationID)
        {
            InitializeComponent();
            _ApplicationID = ApplicationID;
        }

        private void FmShowLicense_Load(object sender, EventArgs e)
        {
            uctrlShowLicense1.LoadLicenseInfoByApplicationID(_ApplicationID);
        }
    }
}
