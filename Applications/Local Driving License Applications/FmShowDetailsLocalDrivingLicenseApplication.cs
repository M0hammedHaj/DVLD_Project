using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_Project.Applications.Local_Driving_License_Applications
{
    public partial class FmShowDetailsLocalDrivingLicenseApplication : Form
    {
        public FmShowDetailsLocalDrivingLicenseApplication(int LocalDrivingLicenseApplicationID)
        {
            InitializeComponent();
            uctrlLocalDrivingLicenseApplicationInfo1.
                LoadLocalDrivingLicenseApplicationInfo(LocalDrivingLicenseApplicationID);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
