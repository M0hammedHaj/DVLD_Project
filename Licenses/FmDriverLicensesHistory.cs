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

namespace DVLD_Project.Licenses
{
    public partial class FmDriverLicensesHistory : Form
    {
        int _DriverID = -1;
        clsDriver DriverInfo;
        public FmDriverLicensesHistory(int DriverID)
        {
            InitializeComponent();
            _DriverID = DriverID;
            DriverInfo = clsDriver.Find(DriverID);
        }

        private void FmDriverLicensesHistory_Load(object sender, EventArgs e)
        {
            uctrlPersonCardWithFilter1.LoadPerson(DriverInfo.PersonID);
            uctrlDriverLicenses1.LoadInfo(_DriverID);

        }
    }
}
