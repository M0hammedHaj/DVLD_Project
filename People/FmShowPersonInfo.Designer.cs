namespace DVLD_Project.People
{
    partial class FmShowPersonInfo
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
            this.uctrlPersonCard1 = new DVLD_Project.People.Controls.uctrlPersonCard();
            this.SuspendLayout();
            // 
            // uctrlPersonCard1
            // 
            this.uctrlPersonCard1.Location = new System.Drawing.Point(6, 3);
            this.uctrlPersonCard1.Name = "uctrlPersonCard1";
            this.uctrlPersonCard1.Size = new System.Drawing.Size(814, 302);
            this.uctrlPersonCard1.TabIndex = 0;
            // 
            // FmShowPersonInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(824, 308);
            this.Controls.Add(this.uctrlPersonCard1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FmShowPersonInfo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Show Person Info";
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.uctrlPersonCard uctrlPersonCard1;
    }
}