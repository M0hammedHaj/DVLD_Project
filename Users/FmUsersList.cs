using DVLD_Project.Users;
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

namespace DVLD_Project.People
{
    public partial class FmUsersList : Form
    {
        private DataTable _dtUsers;
        public FmUsersList()
        {
            InitializeComponent();
        }

        private void FmUsersList_Load(object sender, EventArgs e)
        {
            _dtUsers = clsUser.GetAllUsers();
            dgvUsers.DataSource = _dtUsers;
            cbFilterBy.SelectedIndex = 0;
            lblRecordsNumber.Text = _dtUsers.Rows.Count.ToString();
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilterValue.Text = "";
            lblRecordsNumber.Text = dgvUsers.Rows.Count.ToString();

            if (cbFilterBy.Text == "None")
            {
                cbIsActiveFilter.Visible = false;
                txtFilterValue.Visible = false;
                _dtUsers.DefaultView.RowFilter = "";
                lblRecordsNumber.Text = dgvUsers.Rows.Count.ToString();
                return;
            }

            if(cbFilterBy.Text == "IsActive")
            {
                cbIsActiveFilter.Visible = true;
                txtFilterValue.Visible = false;
                cbIsActiveFilter.SelectedIndex = 0;
            }
            else 
            {
                cbIsActiveFilter.Visible = false;
                txtFilterValue.Visible = true;
            }

        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilterBy.Text == "PersonID" || cbFilterBy.Text == "UserID")
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtFilterValue.Text.Trim()))
            {
                _dtUsers.DefaultView.RowFilter = "";
                return;
            }

            if (cbFilterBy.Text == "PersonID" || cbFilterBy.Text == "UserID")
            {
                _dtUsers.DefaultView.RowFilter = string.Format("[{0}] = {1}",
                    cbFilterBy.Text, txtFilterValue.Text.Trim());
                lblRecordsNumber.Text = dgvUsers.Rows.Count.ToString();
            }
            else
            {
                _dtUsers.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'"
                    , cbFilterBy.Text, txtFilterValue.Text.Trim());
                lblRecordsNumber.Text = dgvUsers.Rows.Count.ToString();
            }
        }

        private void cbIsActiveFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch(cbIsActiveFilter.Text) 
            {
                case "All":
                    _dtUsers.DefaultView.RowFilter = "";
                    lblRecordsNumber.Text = dgvUsers.Rows.Count.ToString();
                    break;
                case "Yes":
                    _dtUsers.DefaultView.RowFilter = "[IsActive] = true";
                    lblRecordsNumber.Text = dgvUsers.Rows.Count.ToString();
                    break;
                case "No":
                    _dtUsers.DefaultView.RowFilter = "[IsActive] = false";
                    lblRecordsNumber.Text = dgvUsers.Rows.Count.ToString();
                    break;
            }
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FmShowUserDetails ShowUserDetails = new FmShowUserDetails((int)
                dgvUsers.SelectedCells[0].Value);
            ShowUserDetails.ShowDialog();
        }

        private void _RefreshDataGridViem()
        {
            _dtUsers = clsUser.GetAllUsers();
            dgvUsers.DataSource = _dtUsers;
            lblRecordsNumber.Text = dgvUsers.Rows.Count.ToString();
        }
        private void addNewUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FmAddUpdateUser AddUser = new FmAddUpdateUser();
            AddUser.ShowDialog();
            _RefreshDataGridViem();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (clsUser.DeleteUser((int)dgvUsers.SelectedCells[0].Value))
            {
                MessageBox.Show("User deleted successfully", "Deleted"
                    , MessageBoxButtons.OK, MessageBoxIcon.Information);
                _RefreshDataGridViem();
            }
            else
                MessageBox.Show("User didn't deleted", "Error", MessageBoxButtons.OK
                    , MessageBoxIcon.Error);

        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FmAddUpdateUser UpdateUser = new FmAddUpdateUser((int)
                dgvUsers.SelectedCells[0].Value);
            UpdateUser.ShowDialog();
            _RefreshDataGridViem();
        }
    }
}
