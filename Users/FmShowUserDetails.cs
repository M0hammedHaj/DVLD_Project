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
    public partial class FmShowUserDetails : Form
    {
        private int _UserID = -1;
        public FmShowUserDetails(int userID)
        {
            InitializeComponent();
            _UserID = userID;
        }

        private void FmShowUserDetails_Load(object sender, EventArgs e)
        {
            uctrlUserCard1.LoadUserInfo(_UserID);
        }
    }
}
