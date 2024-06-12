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

namespace DVLD_Project.Applications.Detain_License
{
    public partial class FmManageDetainLicenses : Form
    {
        private DataTable _dtDetainLicenses;
        public FmManageDetainLicenses()
        {
            InitializeComponent();
        }

        private void FmManageDetainLicenses_Load(object sender, EventArgs e)
        {
            _dtDetainLicenses = clsDetainLicense.GetAllDetainedLicenses();

            dgvDetainedLicenses.DataSource = _dtDetainLicenses;
            lblTotalRecords.Text = _dtDetainLicenses.Rows.Count.ToString();
        }
    }
}
