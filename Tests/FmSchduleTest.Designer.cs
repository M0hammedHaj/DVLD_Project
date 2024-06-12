namespace DVLD_Project.Tests
{
    partial class FmSchduleTest
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.uctrlSchduleTest1 = new DVLD_Project.Tests.Controls.uctrlSchduleTest();
            this.SuspendLayout();
            // 
            // uctrlSchduleTest1
            // 
            this.uctrlSchduleTest1.Location = new System.Drawing.Point(12, 2);
            this.uctrlSchduleTest1.Name = "uctrlSchduleTest1";
            this.uctrlSchduleTest1.Size = new System.Drawing.Size(533, 722);
            this.uctrlSchduleTest1.TabIndex = 0;
            this.uctrlSchduleTest1.TestType = DVLDBusinessLayer.clsTestType.enTestType.Vision;
            // 
            // FmSchduleTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(549, 719);
            this.Controls.Add(this.uctrlSchduleTest1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FmSchduleTest";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Schdule Test";
            this.Load += new System.EventHandler(this.FmSchduleTest_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.uctrlSchduleTest uctrlSchduleTest1;
    }
}