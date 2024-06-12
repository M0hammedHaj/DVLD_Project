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

namespace DVLD_Project.People.Controls
{
    public partial class uctrlPersonCardWithFilter : UserControl
    {
        private bool _FilterEnabled = true;
        public bool FilterEnabled
        {
            get 
            { 
                return _FilterEnabled; 
            }
            set 
            { 
                _FilterEnabled = value;
                gbFilters.Enabled = _FilterEnabled;
            }
        }
        public int PersonID
        {
            get { return uctrlPersonCard1.PersonID; }
        }
        public clsPerson SelectedPersonInfo
        {
            get { return uctrlPersonCard1.SelectedPersonInfo; }
        }
        public event Action<int> OnPersonSelected;
        public uctrlPersonCardWithFilter()
        {
            InitializeComponent();
        }
        public void LoadPerson(int personID)
        {
            cbFilterBy.SelectedIndex = 1;
            txtFilterValue.Text = personID.ToString();
            btnFind.PerformClick();
            FilterEnabled = false;
        }
        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilterValue.Text = "";
            txtFilterValue.Focus();
        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Check if the pressed key is Enter (character code 13)
            if (e.KeyChar == (char)13)
            {

                btnFind.PerformClick();
            }

            //this will allow only digits if person id is selected
            if (cbFilterBy.Text == "Person ID")
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
        public void FindPerformClick()
        {
            btnFind.PerformClick();
        }
        private void txtFilterValue_Validating(object sender, CancelEventArgs e)
        {
            if(string.IsNullOrEmpty(txtFilterValue.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtFilterValue, "Shouldn't be empty");
            }
            else
            {
                errorProvider1.SetError(txtFilterValue, null);
            }
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            if(!this.ValidateChildren())
            {
                MessageBox.Show("Filter text shouldn't be empty", "Text box empty"
                    , MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (cbFilterBy.Text == "PersonID")
                uctrlPersonCard1.LoadPersonInfo(int.Parse(txtFilterValue.Text));
            else
                uctrlPersonCard1.LoadPersonInfo(txtFilterValue.Text);

            if (OnPersonSelected != null)
                OnPersonSelected(PersonID);
        }

        private void uctrlPersonCardWithFilter_Load(object sender, EventArgs e)
        {
            cbFilterBy.SelectedIndex = 0;
        }
        private void FilterFromAddUpdatePersonForm(int ID)
        {
            cbFilterBy.SelectedIndex = 1;
            txtFilterValue.Text =ID.ToString();

        }

        private void btnAddNewPerson_Click(object sender, EventArgs e)
        {
            FmAddUpdatePerson AddPerson = new FmAddUpdatePerson();
            AddPerson.DataBack += FilterFromAddUpdatePersonForm;
            AddPerson.ShowDialog();
            btnFind.PerformClick();
        }
    }
}
