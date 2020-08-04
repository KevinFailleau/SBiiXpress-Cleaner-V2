namespace Cleaner
{
    partial class Options
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Options));
            this.gB_Download = new System.Windows.Forms.GroupBox();
            this.btn_Valider = new System.Windows.Forms.Button();
            this.btn_Annuler = new System.Windows.Forms.Button();
            this.nUD_Jours = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.gB_Download.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nUD_Jours)).BeginInit();
            this.SuspendLayout();
            // 
            // gB_Download
            // 
            this.gB_Download.Controls.Add(this.label1);
            this.gB_Download.Controls.Add(this.nUD_Jours);
            this.gB_Download.Location = new System.Drawing.Point(12, 12);
            this.gB_Download.Name = "gB_Download";
            this.gB_Download.Size = new System.Drawing.Size(535, 152);
            this.gB_Download.TabIndex = 0;
            this.gB_Download.TabStop = false;
            this.gB_Download.Text = "Dossier téléchargement";
            // 
            // btn_Valider
            // 
            this.btn_Valider.Location = new System.Drawing.Point(186, 186);
            this.btn_Valider.Name = "btn_Valider";
            this.btn_Valider.Size = new System.Drawing.Size(75, 23);
            this.btn_Valider.TabIndex = 1;
            this.btn_Valider.Text = "Valider";
            this.btn_Valider.UseVisualStyleBackColor = true;
            this.btn_Valider.Click += new System.EventHandler(this.btn_Valider_Click);
            // 
            // btn_Annuler
            // 
            this.btn_Annuler.Location = new System.Drawing.Point(300, 186);
            this.btn_Annuler.Name = "btn_Annuler";
            this.btn_Annuler.Size = new System.Drawing.Size(75, 23);
            this.btn_Annuler.TabIndex = 2;
            this.btn_Annuler.Text = "Annuler";
            this.btn_Annuler.UseVisualStyleBackColor = true;
            this.btn_Annuler.Click += new System.EventHandler(this.btn_Annuler_Click);
            // 
            // nUD_Jours
            // 
            this.nUD_Jours.Location = new System.Drawing.Point(449, 74);
            this.nUD_Jours.Maximum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.nUD_Jours.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nUD_Jours.Name = "nUD_Jours";
            this.nUD_Jours.Size = new System.Drawing.Size(38, 20);
            this.nUD_Jours.TabIndex = 0;
            this.nUD_Jours.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(43, 76);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(391, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Supprimer les fichiers qui n\'ont pas été ouverts depuis le nombre de jours suivan" +
    "t :";
            // 
            // Options
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(559, 221);
            this.Controls.Add(this.btn_Annuler);
            this.Controls.Add(this.btn_Valider);
            this.Controls.Add(this.gB_Download);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Options";
            this.Text = "Options";
            this.gB_Download.ResumeLayout(false);
            this.gB_Download.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nUD_Jours)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gB_Download;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown nUD_Jours;
        private System.Windows.Forms.Button btn_Valider;
        private System.Windows.Forms.Button btn_Annuler;
    }
}