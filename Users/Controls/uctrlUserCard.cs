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

namespace DVLD_Project.Users.Controls
{
    public partial class uctrlUserCard : UserControl
    {
        private int _UserID = -1;
        private clsUser _UserInfo;
        public clsUser SelectedUserInfo
        {
            get { return _UserInfo; }
        }
        public uctrlUserCard()
        {
            InitializeComponent();
        }

        public void LoadUserInfo(int UserID)
        {
            _UserInfo = clsUser.Find(UserID);
            if (_UserInfo == null)
            {
                _ResetLabels();
                MessageBox.Show("No User with UserID = " + UserID.ToString(), "Error"
                    , MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            LoadData();
        }
        private void _ResetLabels()
        {
            uctrlPersonCard1.ResetAllLabels();
            lblUsername.Text = "[???]";
            lblUserID.Text = "[???]";
            lblIsActive.Text = "[???]";
        }
        private void LoadData()
        {
            _UserID = _UserInfo.UserID;
            lblIsActive.Text = (_UserInfo.IsActive) ? "Yes" : "No";
            lblUserID.Text = _UserInfo.UserID.ToString();
            lblUsername.Text = _UserInfo.UserName;
            uctrlPersonCard1.LoadPersonInfo(_UserInfo.PersonID);
        }
    }
}
