namespace DVLD_Project.Users
{
    partial class FmShowUserDetails
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
            this.uctrlUserCard1 = new DVLD_Project.Users.Controls.uctrlUserCard();
            this.SuspendLayout();
            // 
            // uctrlUserCard1
            // 
            this.uctrlUserCard1.Location = new System.Drawing.Point(3, 5);
            this.uctrlUserCard1.Name = "uctrlUserCard1";
            this.uctrlUserCard1.Size = new System.Drawing.Size(824, 433);
            this.uctrlUserCard1.TabIndex = 0;
            // 
            // FmShowUserDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(821, 450);
            this.Controls.Add(this.uctrlUserCard1);
            this.Name = "FmShowUserDetails";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Show User Details";
            this.Load += new System.EventHandler(this.FmShowUserDetails_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.uctrlUserCard uctrlUserCard1;
    }
}