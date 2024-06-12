namespace DVLD_Project.Licenses
{
    partial class FmDriverLicensesHistory
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
            this.lblTitle = new System.Windows.Forms.Label();
            this.pbPersonImage = new System.Windows.Forms.PictureBox();
            this.uctrlPersonCardWithFilter1 = new DVLD_Project.People.Controls.uctrlPersonCardWithFilter();
            this.uctrlDriverLicenses1 = new DVLD_Project.Licenses.Controls.uctrlDriverLicenses();
            ((System.ComponentModel.ISupportInitialize)(this.pbPersonImage)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblTitle.Location = new System.Drawing.Point(12, 9);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(1062, 39);
            this.lblTitle.TabIndex = 130;
            this.lblTitle.Text = "License History";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pbPersonImage
            // 
            this.pbPersonImage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pbPersonImage.Image = global::DVLD_Project.Properties.Resources.PersonLicenseHistory_512;
            this.pbPersonImage.InitialImage = null;
            this.pbPersonImage.Location = new System.Drawing.Point(13, 161);
            this.pbPersonImage.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pbPersonImage.Name = "pbPersonImage";
            this.pbPersonImage.Size = new System.Drawing.Size(220, 189);
            this.pbPersonImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbPersonImage.TabIndex = 131;
            this.pbPersonImage.TabStop = false;
            // 
            // uctrlPersonCardWithFilter1
            // 
            this.uctrlPersonCardWithFilter1.FilterEnabled = true;
            this.uctrlPersonCardWithFilter1.Location = new System.Drawing.Point(244, 64);
            this.uctrlPersonCardWithFilter1.Name = "uctrlPersonCardWithFilter1";
            this.uctrlPersonCardWithFilter1.Size = new System.Drawing.Size(830, 395);
            this.uctrlPersonCardWithFilter1.TabIndex = 132;
            // 
            // uctrlDriverLicenses1
            // 
            this.uctrlDriverLicenses1.Location = new System.Drawing.Point(44, 454);
            this.uctrlDriverLicenses1.Name = "uctrlDriverLicenses1";
            this.uctrlDriverLicenses1.Size = new System.Drawing.Size(927, 283);
            this.uctrlDriverLicenses1.TabIndex = 133;
            // 
            // FmDriverLicensesHistory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1086, 749);
            this.Controls.Add(this.uctrlDriverLicenses1);
            this.Controls.Add(this.uctrlPersonCardWithFilter1);
            this.Controls.Add(this.pbPersonImage);
            this.Controls.Add(this.lblTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FmDriverLicensesHistory";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Driver Licenses History";
            this.Load += new System.EventHandler(this.FmDriverLicensesHistory_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbPersonImage)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.PictureBox pbPersonImage;
        private People.Controls.uctrlPersonCardWithFilter uctrlPersonCardWithFilter1;
        private Controls.uctrlDriverLicenses uctrlDriverLicenses1;
    }
}