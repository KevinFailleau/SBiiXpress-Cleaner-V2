namespace Cleaner
{
    partial class Journal
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Journal));
            this.gB_Journal = new System.Windows.Forms.GroupBox();
            this.btn_Fermer = new System.Windows.Forms.Button();
            this.lb_InfoEspace = new System.Windows.Forms.Label();
            this.lb_NbEsp = new System.Windows.Forms.Label();
            this.lb_InfoDate = new System.Windows.Forms.Label();
            this.lb_Date = new System.Windows.Forms.Label();
            this.gB_Journal.SuspendLayout();
            this.SuspendLayout();
            // 
            // gB_Journal
            // 
            this.gB_Journal.Controls.Add(this.lb_Date);
            this.gB_Journal.Controls.Add(this.lb_InfoDate);
            this.gB_Journal.Controls.Add(this.lb_NbEsp);
            this.gB_Journal.Controls.Add(this.lb_InfoEspace);
            this.gB_Journal.Location = new System.Drawing.Point(24, 33);
            this.gB_Journal.Name = "gB_Journal";
            this.gB_Journal.Size = new System.Drawing.Size(561, 177);
            this.gB_Journal.TabIndex = 0;
            this.gB_Journal.TabStop = false;
            this.gB_Journal.Text = "Journal";
            // 
            // btn_Fermer
            // 
            this.btn_Fermer.Location = new System.Drawing.Point(266, 239);
            this.btn_Fermer.Name = "btn_Fermer";
            this.btn_Fermer.Size = new System.Drawing.Size(75, 23);
            this.btn_Fermer.TabIndex = 1;
            this.btn_Fermer.Text = "Fermer";
            this.btn_Fermer.UseVisualStyleBackColor = true;
            this.btn_Fermer.Click += new System.EventHandler(this.btn_Fermer_Click);
            // 
            // lb_InfoEspace
            // 
            this.lb_InfoEspace.AutoSize = true;
            this.lb_InfoEspace.Location = new System.Drawing.Point(83, 63);
            this.lb_InfoEspace.Name = "lb_InfoEspace";
            this.lb_InfoEspace.Size = new System.Drawing.Size(234, 13);
            this.lb_InfoEspace.TabIndex = 0;
            this.lb_InfoEspace.Text = "Espace total libéré depuis la première utilisation :";
            // 
            // lb_NbEsp
            // 
            this.lb_NbEsp.AutoSize = true;
            this.lb_NbEsp.Location = new System.Drawing.Point(323, 63);
            this.lb_NbEsp.Name = "lb_NbEsp";
            this.lb_NbEsp.Size = new System.Drawing.Size(31, 13);
            this.lb_NbEsp.TabIndex = 1;
            this.lb_NbEsp.Text = "0 Mo";
            // 
            // lb_InfoDate
            // 
            this.lb_InfoDate.AutoSize = true;
            this.lb_InfoDate.Location = new System.Drawing.Point(177, 95);
            this.lb_InfoDate.Name = "lb_InfoDate";
            this.lb_InfoDate.Size = new System.Drawing.Size(140, 13);
            this.lb_InfoDate.TabIndex = 2;
            this.lb_InfoDate.Text = "Date de première utilisation :";
            // 
            // lb_Date
            // 
            this.lb_Date.AutoSize = true;
            this.lb_Date.Location = new System.Drawing.Point(323, 95);
            this.lb_Date.Name = "lb_Date";
            this.lb_Date.Size = new System.Drawing.Size(65, 13);
            this.lb_Date.TabIndex = 3;
            this.lb_Date.Text = "01/01/0000";
            // 
            // Journal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(608, 274);
            this.Controls.Add(this.btn_Fermer);
            this.Controls.Add(this.gB_Journal);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Journal";
            this.Text = "Journal";
            this.Load += new System.EventHandler(this.Journal_Load);
            this.gB_Journal.ResumeLayout(false);
            this.gB_Journal.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gB_Journal;
        private System.Windows.Forms.Button btn_Fermer;
        private System.Windows.Forms.Label lb_Date;
        private System.Windows.Forms.Label lb_InfoDate;
        private System.Windows.Forms.Label lb_NbEsp;
        private System.Windows.Forms.Label lb_InfoEspace;
    }
}