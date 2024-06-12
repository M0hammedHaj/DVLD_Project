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

namespace DVLD_Project.Tests
{
    public partial class FmSchduleTest : Form
    {
        int _Mode = -1;
        private int _LDLApplictionID = -1;
        private clsTestType.enTestType _TestType = clsTestType.enTestType.Vision;

        public FmSchduleTest(int LDLApplicationID,clsTestType.enTestType TestType, int Mode)
        {
            InitializeComponent();
            _LDLApplictionID = LDLApplicationID;
            _TestType = TestType;
            _Mode = Mode;
        }

        private void FmSchduleTest_Load(object sender, EventArgs e)
        {
            uctrlSchduleTest1.LoadSchduleTest(_LDLApplictionID, _TestType,_Mode);
        }
    }
}
