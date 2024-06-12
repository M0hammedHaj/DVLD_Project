namespace DVLD_Project.Applications.Local_Driving_License_Applications
{
    partial class FmShowDetailsLocalDrivingLicenseApplication
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
            this.uctrlLocalDrivingLicenseApplicationInfo1 = new DVLD_Project.Applications.Local_Driving_License.uctrlLocalDrivingLicenseApplicationInfo();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // uctrlLocalDrivingLicenseApplicationInfo1
            // 
            this.uctrlLocalDrivingLicenseApplicationInfo1.Location = new System.Drawing.Point(2, 2);
            this.uctrlLocalDrivingLicenseApplicationInfo1.Name = "uctrlLocalDrivingLicenseApplicationInfo1";
            this.uctrlLocalDrivingLicenseApplicationInfo1.Size = new System.Drawing.Size(911, 372);
            this.uctrlLocalDrivingLicenseApplicationInfo1.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.White;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Image = global::DVLD_Project.Properties.Resources.Close_32;
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.Location = new System.Drawing.Point(740, 380);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(93, 35);
            this.button1.TabIndex = 1;
            this.button1.Text = "Close";
            this.button1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // FmShowDetailsLocalDrivingLicenseApplication
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 427);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.uctrlLocalDrivingLicenseApplicationInfo1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FmShowDetailsLocalDrivingLicenseApplication";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Show Details Local Driving License Application";
            this.ResumeLayout(false);

        }

        #endregion

        private Local_Driving_License.uctrlLocalDrivingLicenseApplicationInfo uctrlLocalDrivingLicenseApplicationInfo1;
        private System.Windows.Forms.Button button1;
    }
}