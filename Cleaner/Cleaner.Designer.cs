namespace Cleaner
{
    partial class Cleaner
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Cleaner));
            this.btn_Nettoyage = new System.Windows.Forms.Button();
            this.cB_Hibernation = new System.Windows.Forms.CheckBox();
            this.cB_NettoyageWindows = new System.Windows.Forms.CheckBox();
            this.cB_DossierTelechargement = new System.Windows.Forms.CheckBox();
            this.cB_WindowsUpdate = new System.Windows.Forms.CheckBox();
            this.cB_CCleaner = new System.Windows.Forms.CheckBox();
            this.cB_IE = new System.Windows.Forms.CheckBox();
            this.Btn_Cocher_Opt = new System.Windows.Forms.Button();
            this.Btn_Decocher_Opt = new System.Windows.Forms.Button();
            this.btn_Optimisation = new System.Windows.Forms.Button();
            this.cB_CoreBoot = new System.Windows.Forms.CheckBox();
            this.cB_Reindexation = new System.Windows.Forms.CheckBox();
            this.cB_Diskperf = new System.Windows.Forms.CheckBox();
            this.cB_USB = new System.Windows.Forms.CheckBox();
            this.cB_VerifFichiers = new System.Windows.Forms.CheckBox();
            this.cB_DefragBoot = new System.Windows.Forms.CheckBox();
            this.cB_Noyau = new System.Windows.Forms.CheckBox();
            this.cB_FichierImpEnAttente = new System.Windows.Forms.CheckBox();
            this.cB_Aero = new System.Windows.Forms.CheckBox();
            this.cB_WinSat = new System.Windows.Forms.CheckBox();
            this.cB_WindowsSearch = new System.Windows.Forms.CheckBox();
            this.cB_Prefetch = new System.Windows.Forms.CheckBox();
            this.cB_Superfetch = new System.Windows.Forms.CheckBox();
            this.cB_DechargerDll = new System.Windows.Forms.CheckBox();
            this.StatusStrip = new System.Windows.Forms.StatusStrip();
            this.Lb_Statut = new System.Windows.Forms.ToolStripStatusLabel();
            this.Lb_StatutEnCours = new System.Windows.Forms.ToolStripStatusLabel();
            this.Spring = new System.Windows.Forms.ToolStripStatusLabel();
            this.lb_TpsEcouleInfo = new System.Windows.Forms.ToolStripStatusLabel();
            this.lb_TmpEcoule = new System.Windows.Forms.ToolStripStatusLabel();
            this.ts_Spring = new System.Windows.Forms.ToolStripStatusLabel();
            this.pB = new System.Windows.Forms.ToolStripProgressBar();
            this.btn_Journal = new System.Windows.Forms.Button();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.cB_DCOM = new System.Windows.Forms.CheckBox();
            this.cB_OptiAccesMemoireDisque = new System.Windows.Forms.CheckBox();
            this.Onglets = new System.Windows.Forms.TabControl();
            this.tab_Nettoyage = new System.Windows.Forms.TabPage();
            this.btn_Options = new System.Windows.Forms.Button();
            this.Btn_Decocher_Nett = new System.Windows.Forms.Button();
            this.Btn_Cocher_Nett = new System.Windows.Forms.Button();
            this.tab_Optimisation = new System.Windows.Forms.TabPage();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btn_Quitter = new System.Windows.Forms.Button();
            this.StatusStrip.SuspendLayout();
            this.Onglets.SuspendLayout();
            this.tab_Nettoyage.SuspendLayout();
            this.tab_Optimisation.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_Nettoyage
            // 
            this.btn_Nettoyage.Location = new System.Drawing.Point(18, 262);
            this.btn_Nettoyage.Name = "btn_Nettoyage";
            this.btn_Nettoyage.Size = new System.Drawing.Size(97, 23);
            this.btn_Nettoyage.TabIndex = 6;
            this.btn_Nettoyage.Text = "Nettoyer";
            this.btn_Nettoyage.UseVisualStyleBackColor = true;
            this.btn_Nettoyage.Click += new System.EventHandler(this.btn_Nettoyage_Click);
            // 
            // cB_Hibernation
            // 
            this.cB_Hibernation.AutoSize = true;
            this.cB_Hibernation.Checked = true;
            this.cB_Hibernation.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cB_Hibernation.Location = new System.Drawing.Point(198, 256);
            this.cB_Hibernation.Name = "cB_Hibernation";
            this.cB_Hibernation.Size = new System.Drawing.Size(260, 17);
            this.cB_Hibernation.TabIndex = 5;
            this.cB_Hibernation.Text = "Désactiver et supprimer les données d\'hibernation";
            this.cB_Hibernation.UseVisualStyleBackColor = true;
            this.cB_Hibernation.CheckedChanged += new System.EventHandler(this.checkBoxNettoyage_CheckedChanged);
            // 
            // cB_NettoyageWindows
            // 
            this.cB_NettoyageWindows.AutoSize = true;
            this.cB_NettoyageWindows.Checked = true;
            this.cB_NettoyageWindows.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cB_NettoyageWindows.Location = new System.Drawing.Point(198, 233);
            this.cB_NettoyageWindows.Name = "cB_NettoyageWindows";
            this.cB_NettoyageWindows.Size = new System.Drawing.Size(137, 17);
            this.cB_NettoyageWindows.TabIndex = 4;
            this.cB_NettoyageWindows.Text = "Nettoyage de Windows";
            this.cB_NettoyageWindows.UseVisualStyleBackColor = true;
            this.cB_NettoyageWindows.CheckedChanged += new System.EventHandler(this.checkBoxNettoyage_CheckedChanged);
            // 
            // cB_DossierTelechargement
            // 
            this.cB_DossierTelechargement.AutoSize = true;
            this.cB_DossierTelechargement.Checked = true;
            this.cB_DossierTelechargement.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cB_DossierTelechargement.Location = new System.Drawing.Point(198, 210);
            this.cB_DossierTelechargement.Name = "cB_DossierTelechargement";
            this.cB_DossierTelechargement.Size = new System.Drawing.Size(214, 17);
            this.cB_DossierTelechargement.TabIndex = 3;
            this.cB_DossierTelechargement.Text = "Nettoyer le dossier des téléchargements";
            this.cB_DossierTelechargement.UseVisualStyleBackColor = true;
            this.cB_DossierTelechargement.CheckedChanged += new System.EventHandler(this.checkBoxNettoyage_CheckedChanged);
            // 
            // cB_WindowsUpdate
            // 
            this.cB_WindowsUpdate.AutoSize = true;
            this.cB_WindowsUpdate.Checked = true;
            this.cB_WindowsUpdate.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cB_WindowsUpdate.Location = new System.Drawing.Point(198, 187);
            this.cB_WindowsUpdate.Name = "cB_WindowsUpdate";
            this.cB_WindowsUpdate.Size = new System.Drawing.Size(175, 17);
            this.cB_WindowsUpdate.TabIndex = 2;
            this.cB_WindowsUpdate.Text = "Nettoyage de Windows Update";
            this.cB_WindowsUpdate.UseVisualStyleBackColor = true;
            this.cB_WindowsUpdate.CheckedChanged += new System.EventHandler(this.checkBoxNettoyage_CheckedChanged);
            // 
            // cB_CCleaner
            // 
            this.cB_CCleaner.AutoSize = true;
            this.cB_CCleaner.Checked = true;
            this.cB_CCleaner.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cB_CCleaner.Location = new System.Drawing.Point(198, 141);
            this.cB_CCleaner.Name = "cB_CCleaner";
            this.cB_CCleaner.Size = new System.Drawing.Size(134, 17);
            this.cB_CCleaner.TabIndex = 1;
            this.cB_CCleaner.Text = "Exécution de CCleaner";
            this.cB_CCleaner.UseVisualStyleBackColor = true;
            this.cB_CCleaner.CheckedChanged += new System.EventHandler(this.checkBoxNettoyage_CheckedChanged);
            // 
            // cB_IE
            // 
            this.cB_IE.AutoSize = true;
            this.cB_IE.Checked = true;
            this.cB_IE.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cB_IE.Location = new System.Drawing.Point(198, 164);
            this.cB_IE.Name = "cB_IE";
            this.cB_IE.Size = new System.Drawing.Size(170, 17);
            this.cB_IE.TabIndex = 0;
            this.cB_IE.Text = "Nettoyage de Internet Explorer";
            this.cB_IE.UseVisualStyleBackColor = true;
            this.cB_IE.CheckedChanged += new System.EventHandler(this.checkBoxNettoyage_CheckedChanged);
            // 
            // Btn_Cocher_Opt
            // 
            this.Btn_Cocher_Opt.Location = new System.Drawing.Point(18, 126);
            this.Btn_Cocher_Opt.Name = "Btn_Cocher_Opt";
            this.Btn_Cocher_Opt.Size = new System.Drawing.Size(97, 23);
            this.Btn_Cocher_Opt.TabIndex = 16;
            this.Btn_Cocher_Opt.Text = "Tout cocher";
            this.Btn_Cocher_Opt.UseVisualStyleBackColor = true;
            this.Btn_Cocher_Opt.Click += new System.EventHandler(this.Btn_Cocher_Opt_Click);
            // 
            // Btn_Decocher_Opt
            // 
            this.Btn_Decocher_Opt.Location = new System.Drawing.Point(18, 172);
            this.Btn_Decocher_Opt.Name = "Btn_Decocher_Opt";
            this.Btn_Decocher_Opt.Size = new System.Drawing.Size(97, 23);
            this.Btn_Decocher_Opt.TabIndex = 15;
            this.Btn_Decocher_Opt.Text = "Tout décocher";
            this.Btn_Decocher_Opt.UseVisualStyleBackColor = true;
            this.Btn_Decocher_Opt.Click += new System.EventHandler(this.Btn_Decocher_Opt_Click);
            // 
            // btn_Optimisation
            // 
            this.btn_Optimisation.Location = new System.Drawing.Point(18, 218);
            this.btn_Optimisation.Name = "btn_Optimisation";
            this.btn_Optimisation.Size = new System.Drawing.Size(97, 23);
            this.btn_Optimisation.TabIndex = 14;
            this.btn_Optimisation.Text = "Optimiser";
            this.btn_Optimisation.UseVisualStyleBackColor = true;
            this.btn_Optimisation.Click += new System.EventHandler(this.btn_Optimisation_Click);
            // 
            // cB_CoreBoot
            // 
            this.cB_CoreBoot.AutoSize = true;
            this.cB_CoreBoot.Checked = true;
            this.cB_CoreBoot.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cB_CoreBoot.Location = new System.Drawing.Point(199, 270);
            this.cB_CoreBoot.Name = "cB_CoreBoot";
            this.cB_CoreBoot.Size = new System.Drawing.Size(310, 17);
            this.cB_CoreBoot.TabIndex = 13;
            this.cB_CoreBoot.Text = "Optimiser la vitesse de démarrage en fonction du processeur";
            this.cB_CoreBoot.UseVisualStyleBackColor = true;
            this.cB_CoreBoot.CheckedChanged += new System.EventHandler(this.checkBoxOptimisation_CheckedChanged);
            // 
            // cB_Reindexation
            // 
            this.cB_Reindexation.AutoSize = true;
            this.cB_Reindexation.Checked = true;
            this.cB_Reindexation.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cB_Reindexation.Location = new System.Drawing.Point(199, 293);
            this.cB_Reindexation.Name = "cB_Reindexation";
            this.cB_Reindexation.Size = new System.Drawing.Size(126, 17);
            this.cB_Reindexation.TabIndex = 12;
            this.cB_Reindexation.Text = "Réindexer les fichiers";
            this.cB_Reindexation.UseVisualStyleBackColor = true;
            this.cB_Reindexation.CheckedChanged += new System.EventHandler(this.checkBoxOptimisation_CheckedChanged);
            // 
            // cB_Diskperf
            // 
            this.cB_Diskperf.AutoSize = true;
            this.cB_Diskperf.Checked = true;
            this.cB_Diskperf.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cB_Diskperf.Location = new System.Drawing.Point(199, 222);
            this.cB_Diskperf.Name = "cB_Diskperf";
            this.cB_Diskperf.Size = new System.Drawing.Size(271, 17);
            this.cB_Diskperf.TabIndex = 11;
            this.cB_Diskperf.Text = "Désactiver le compteur de performance des disques";
            this.cB_Diskperf.UseVisualStyleBackColor = true;
            this.cB_Diskperf.CheckedChanged += new System.EventHandler(this.checkBoxOptimisation_CheckedChanged);
            // 
            // cB_USB
            // 
            this.cB_USB.AutoSize = true;
            this.cB_USB.Checked = true;
            this.cB_USB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cB_USB.Location = new System.Drawing.Point(199, 10);
            this.cB_USB.Name = "cB_USB";
            this.cB_USB.Size = new System.Drawing.Size(251, 17);
            this.cB_USB.TabIndex = 10;
            this.cB_USB.Text = "Augmenter la vitesse de transfert des ports USB";
            this.cB_USB.UseVisualStyleBackColor = true;
            this.cB_USB.CheckedChanged += new System.EventHandler(this.checkBoxOptimisation_CheckedChanged);
            // 
            // cB_VerifFichiers
            // 
            this.cB_VerifFichiers.AutoSize = true;
            this.cB_VerifFichiers.Checked = true;
            this.cB_VerifFichiers.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cB_VerifFichiers.Location = new System.Drawing.Point(199, 129);
            this.cB_VerifFichiers.Name = "cB_VerifFichiers";
            this.cB_VerifFichiers.Size = new System.Drawing.Size(193, 17);
            this.cB_VerifFichiers.TabIndex = 9;
            this.cB_VerifFichiers.Text = "Vérification de l\'intégrité des fichiers";
            this.cB_VerifFichiers.UseVisualStyleBackColor = true;
            this.cB_VerifFichiers.CheckedChanged += new System.EventHandler(this.checkBoxOptimisation_CheckedChanged);
            // 
            // cB_DefragBoot
            // 
            this.cB_DefragBoot.AutoSize = true;
            this.cB_DefragBoot.Checked = true;
            this.cB_DefragBoot.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cB_DefragBoot.Location = new System.Drawing.Point(199, 105);
            this.cB_DefragBoot.Name = "cB_DefragBoot";
            this.cB_DefragBoot.Size = new System.Drawing.Size(291, 17);
            this.cB_DefragBoot.TabIndex = 8;
            this.cB_DefragBoot.Text = "Désactiver la défragmentation des fichiers au démarrage";
            this.cB_DefragBoot.UseVisualStyleBackColor = true;
            this.cB_DefragBoot.CheckedChanged += new System.EventHandler(this.checkBoxOptimisation_CheckedChanged);
            // 
            // cB_Noyau
            // 
            this.cB_Noyau.AutoSize = true;
            this.cB_Noyau.Checked = true;
            this.cB_Noyau.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cB_Noyau.Location = new System.Drawing.Point(199, 81);
            this.cB_Noyau.Name = "cB_Noyau";
            this.cB_Noyau.Size = new System.Drawing.Size(205, 17);
            this.cB_Noyau.TabIndex = 7;
            this.cB_Noyau.Text = "Garder le noyau Windows en mémoire";
            this.cB_Noyau.UseVisualStyleBackColor = true;
            this.cB_Noyau.CheckedChanged += new System.EventHandler(this.checkBoxOptimisation_CheckedChanged);
            // 
            // cB_FichierImpEnAttente
            // 
            this.cB_FichierImpEnAttente.AutoSize = true;
            this.cB_FichierImpEnAttente.Checked = true;
            this.cB_FichierImpEnAttente.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cB_FichierImpEnAttente.Location = new System.Drawing.Point(199, 57);
            this.cB_FichierImpEnAttente.Name = "cB_FichierImpEnAttente";
            this.cB_FichierImpEnAttente.Size = new System.Drawing.Size(262, 17);
            this.cB_FichierImpEnAttente.TabIndex = 6;
            this.cB_FichierImpEnAttente.Text = "Supprimer la liste d\'attente des fichiers à supprimer";
            this.cB_FichierImpEnAttente.UseVisualStyleBackColor = true;
            this.cB_FichierImpEnAttente.CheckedChanged += new System.EventHandler(this.checkBoxOptimisation_CheckedChanged);
            // 
            // cB_Aero
            // 
            this.cB_Aero.AutoSize = true;
            this.cB_Aero.Checked = true;
            this.cB_Aero.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cB_Aero.Location = new System.Drawing.Point(199, 362);
            this.cB_Aero.Name = "cB_Aero";
            this.cB_Aero.Size = new System.Drawing.Size(140, 17);
            this.cB_Aero.TabIndex = 0;
            this.cB_Aero.Text = "Activer/Désactiver Aero";
            this.cB_Aero.UseVisualStyleBackColor = true;
            this.cB_Aero.CheckedChanged += new System.EventHandler(this.checkBoxOptimisation_CheckedChanged);
            // 
            // cB_WinSat
            // 
            this.cB_WinSat.AutoSize = true;
            this.cB_WinSat.Checked = true;
            this.cB_WinSat.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cB_WinSat.Location = new System.Drawing.Point(199, 33);
            this.cB_WinSat.Name = "cB_WinSat";
            this.cB_WinSat.Size = new System.Drawing.Size(120, 17);
            this.cB_WinSat.TabIndex = 5;
            this.cB_WinSat.Text = "Désactiver WinSAT";
            this.cB_WinSat.UseVisualStyleBackColor = true;
            this.cB_WinSat.CheckedChanged += new System.EventHandler(this.checkBoxOptimisation_CheckedChanged);
            // 
            // cB_WindowsSearch
            // 
            this.cB_WindowsSearch.AutoSize = true;
            this.cB_WindowsSearch.Checked = true;
            this.cB_WindowsSearch.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cB_WindowsSearch.Location = new System.Drawing.Point(199, 339);
            this.cB_WindowsSearch.Name = "cB_WindowsSearch";
            this.cB_WindowsSearch.Size = new System.Drawing.Size(161, 17);
            this.cB_WindowsSearch.TabIndex = 4;
            this.cB_WindowsSearch.Text = "Désactiver Windows Search";
            this.cB_WindowsSearch.UseVisualStyleBackColor = true;
            this.cB_WindowsSearch.CheckedChanged += new System.EventHandler(this.checkBoxOptimisation_CheckedChanged);
            // 
            // cB_Prefetch
            // 
            this.cB_Prefetch.AutoSize = true;
            this.cB_Prefetch.Checked = true;
            this.cB_Prefetch.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cB_Prefetch.Location = new System.Drawing.Point(199, 153);
            this.cB_Prefetch.Name = "cB_Prefetch";
            this.cB_Prefetch.Size = new System.Drawing.Size(149, 17);
            this.cB_Prefetch.TabIndex = 3;
            this.cB_Prefetch.Text = "Désactivation de Prefetch";
            this.cB_Prefetch.UseVisualStyleBackColor = true;
            this.cB_Prefetch.CheckedChanged += new System.EventHandler(this.checkBoxOptimisation_CheckedChanged);
            // 
            // cB_Superfetch
            // 
            this.cB_Superfetch.AutoSize = true;
            this.cB_Superfetch.Checked = true;
            this.cB_Superfetch.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cB_Superfetch.Location = new System.Drawing.Point(199, 316);
            this.cB_Superfetch.Name = "cB_Superfetch";
            this.cB_Superfetch.Size = new System.Drawing.Size(161, 17);
            this.cB_Superfetch.TabIndex = 2;
            this.cB_Superfetch.Text = "Désactivation de Superfetch";
            this.cB_Superfetch.UseVisualStyleBackColor = true;
            this.cB_Superfetch.CheckedChanged += new System.EventHandler(this.checkBoxOptimisation_CheckedChanged);
            // 
            // cB_DechargerDll
            // 
            this.cB_DechargerDll.AutoSize = true;
            this.cB_DechargerDll.Checked = true;
            this.cB_DechargerDll.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cB_DechargerDll.Location = new System.Drawing.Point(199, 245);
            this.cB_DechargerDll.Name = "cB_DechargerDll";
            this.cB_DechargerDll.Size = new System.Drawing.Size(150, 17);
            this.cB_DechargerDll.TabIndex = 1;
            this.cB_DechargerDll.Text = "Décharger les DLL inutiles";
            this.cB_DechargerDll.UseVisualStyleBackColor = true;
            this.cB_DechargerDll.CheckedChanged += new System.EventHandler(this.checkBoxOptimisation_CheckedChanged);
            // 
            // StatusStrip
            // 
            this.StatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Lb_Statut,
            this.Lb_StatutEnCours,
            this.Spring,
            this.lb_TpsEcouleInfo,
            this.lb_TmpEcoule,
            this.ts_Spring,
            this.pB});
            this.StatusStrip.Location = new System.Drawing.Point(0, 646);
            this.StatusStrip.Name = "StatusStrip";
            this.StatusStrip.ShowItemToolTips = true;
            this.StatusStrip.Size = new System.Drawing.Size(641, 22);
            this.StatusStrip.SizingGrip = false;
            this.StatusStrip.TabIndex = 6;
            this.StatusStrip.Text = "statusStrip1";
            // 
            // Lb_Statut
            // 
            this.Lb_Statut.Name = "Lb_Statut";
            this.Lb_Statut.Size = new System.Drawing.Size(44, 17);
            this.Lb_Statut.Text = "Statut :";
            // 
            // Lb_StatutEnCours
            // 
            this.Lb_StatutEnCours.AutoSize = false;
            this.Lb_StatutEnCours.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.Lb_StatutEnCours.Name = "Lb_StatutEnCours";
            this.Lb_StatutEnCours.Size = new System.Drawing.Size(200, 17);
            this.Lb_StatutEnCours.Text = "Arrêté";
            this.Lb_StatutEnCours.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Spring
            // 
            this.Spring.Name = "Spring";
            this.Spring.Size = new System.Drawing.Size(0, 17);
            // 
            // lb_TpsEcouleInfo
            // 
            this.lb_TpsEcouleInfo.Name = "lb_TpsEcouleInfo";
            this.lb_TpsEcouleInfo.Size = new System.Drawing.Size(86, 17);
            this.lb_TpsEcouleInfo.Text = "Temps écoulé :";
            this.lb_TpsEcouleInfo.Visible = false;
            // 
            // lb_TmpEcoule
            // 
            this.lb_TmpEcoule.Name = "lb_TmpEcoule";
            this.lb_TmpEcoule.Size = new System.Drawing.Size(49, 17);
            this.lb_TmpEcoule.Text = "00:00:00";
            this.lb_TmpEcoule.Visible = false;
            // 
            // ts_Spring
            // 
            this.ts_Spring.Name = "ts_Spring";
            this.ts_Spring.Size = new System.Drawing.Size(382, 17);
            this.ts_Spring.Spring = true;
            // 
            // pB
            // 
            this.pB.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.pB.Name = "pB";
            this.pB.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.pB.Size = new System.Drawing.Size(100, 16);
            this.pB.Visible = false;
            // 
            // btn_Journal
            // 
            this.btn_Journal.Location = new System.Drawing.Point(16, 612);
            this.btn_Journal.Name = "btn_Journal";
            this.btn_Journal.Size = new System.Drawing.Size(75, 23);
            this.btn_Journal.TabIndex = 7;
            this.btn_Journal.Text = "Journal";
            this.btn_Journal.UseVisualStyleBackColor = true;
            this.btn_Journal.Click += new System.EventHandler(this.btn_Journal_Click);
            // 
            // timer
            // 
            this.timer.Interval = 1000;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // cB_DCOM
            // 
            this.cB_DCOM.AutoSize = true;
            this.cB_DCOM.Checked = true;
            this.cB_DCOM.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cB_DCOM.Location = new System.Drawing.Point(199, 176);
            this.cB_DCOM.Name = "cB_DCOM";
            this.cB_DCOM.Size = new System.Drawing.Size(112, 17);
            this.cB_DCOM.TabIndex = 17;
            this.cB_DCOM.Text = "Désactiver DCOM";
            this.cB_DCOM.UseVisualStyleBackColor = true;
            this.cB_DCOM.CheckedChanged += new System.EventHandler(this.checkBoxOptimisation_CheckedChanged);
            // 
            // cB_OptiAccesMemoireDisque
            // 
            this.cB_OptiAccesMemoireDisque.AutoSize = true;
            this.cB_OptiAccesMemoireDisque.Checked = true;
            this.cB_OptiAccesMemoireDisque.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cB_OptiAccesMemoireDisque.Location = new System.Drawing.Point(199, 199);
            this.cB_OptiAccesMemoireDisque.Name = "cB_OptiAccesMemoireDisque";
            this.cB_OptiAccesMemoireDisque.Size = new System.Drawing.Size(193, 17);
            this.cB_OptiAccesMemoireDisque.TabIndex = 18;
            this.cB_OptiAccesMemoireDisque.Text = "Optimiser l\'accès disque et mémoire";
            this.cB_OptiAccesMemoireDisque.UseVisualStyleBackColor = true;
            this.cB_OptiAccesMemoireDisque.CheckedChanged += new System.EventHandler(this.checkBoxOptimisation_CheckedChanged);
            // 
            // Onglets
            // 
            this.Onglets.Controls.Add(this.tab_Nettoyage);
            this.Onglets.Controls.Add(this.tab_Optimisation);
            this.Onglets.Location = new System.Drawing.Point(12, 183);
            this.Onglets.Name = "Onglets";
            this.Onglets.SelectedIndex = 0;
            this.Onglets.Size = new System.Drawing.Size(617, 411);
            this.Onglets.TabIndex = 9;
            // 
            // tab_Nettoyage
            // 
            this.tab_Nettoyage.Controls.Add(this.btn_Options);
            this.tab_Nettoyage.Controls.Add(this.Btn_Decocher_Nett);
            this.tab_Nettoyage.Controls.Add(this.Btn_Cocher_Nett);
            this.tab_Nettoyage.Controls.Add(this.btn_Nettoyage);
            this.tab_Nettoyage.Controls.Add(this.cB_IE);
            this.tab_Nettoyage.Controls.Add(this.cB_CCleaner);
            this.tab_Nettoyage.Controls.Add(this.cB_WindowsUpdate);
            this.tab_Nettoyage.Controls.Add(this.cB_Hibernation);
            this.tab_Nettoyage.Controls.Add(this.cB_DossierTelechargement);
            this.tab_Nettoyage.Controls.Add(this.cB_NettoyageWindows);
            this.tab_Nettoyage.Location = new System.Drawing.Point(4, 22);
            this.tab_Nettoyage.Name = "tab_Nettoyage";
            this.tab_Nettoyage.Padding = new System.Windows.Forms.Padding(3);
            this.tab_Nettoyage.Size = new System.Drawing.Size(609, 385);
            this.tab_Nettoyage.TabIndex = 0;
            this.tab_Nettoyage.Text = "Nettoyage";
            this.tab_Nettoyage.UseVisualStyleBackColor = true;
            // 
            // btn_Options
            // 
            this.btn_Options.Location = new System.Drawing.Point(18, 218);
            this.btn_Options.Name = "btn_Options";
            this.btn_Options.Size = new System.Drawing.Size(97, 23);
            this.btn_Options.TabIndex = 24;
            this.btn_Options.Text = "Options";
            this.btn_Options.UseVisualStyleBackColor = true;
            this.btn_Options.Click += new System.EventHandler(this.btn_Options_Click);
            // 
            // Btn_Decocher_Nett
            // 
            this.Btn_Decocher_Nett.Location = new System.Drawing.Point(18, 172);
            this.Btn_Decocher_Nett.Name = "Btn_Decocher_Nett";
            this.Btn_Decocher_Nett.Size = new System.Drawing.Size(97, 23);
            this.Btn_Decocher_Nett.TabIndex = 23;
            this.Btn_Decocher_Nett.Text = "Tout décocher";
            this.Btn_Decocher_Nett.UseVisualStyleBackColor = true;
            this.Btn_Decocher_Nett.Click += new System.EventHandler(this.Btn_Decocher_Nett_Click);
            // 
            // Btn_Cocher_Nett
            // 
            this.Btn_Cocher_Nett.Location = new System.Drawing.Point(18, 126);
            this.Btn_Cocher_Nett.Name = "Btn_Cocher_Nett";
            this.Btn_Cocher_Nett.Size = new System.Drawing.Size(97, 23);
            this.Btn_Cocher_Nett.TabIndex = 22;
            this.Btn_Cocher_Nett.Text = "Tout cocher";
            this.Btn_Cocher_Nett.UseVisualStyleBackColor = true;
            this.Btn_Cocher_Nett.Click += new System.EventHandler(this.Btn_Cocher_Nett_Click);
            // 
            // tab_Optimisation
            // 
            this.tab_Optimisation.Controls.Add(this.cB_OptiAccesMemoireDisque);
            this.tab_Optimisation.Controls.Add(this.Btn_Decocher_Opt);
            this.tab_Optimisation.Controls.Add(this.cB_DCOM);
            this.tab_Optimisation.Controls.Add(this.cB_DechargerDll);
            this.tab_Optimisation.Controls.Add(this.Btn_Cocher_Opt);
            this.tab_Optimisation.Controls.Add(this.cB_Superfetch);
            this.tab_Optimisation.Controls.Add(this.cB_Prefetch);
            this.tab_Optimisation.Controls.Add(this.btn_Optimisation);
            this.tab_Optimisation.Controls.Add(this.cB_WindowsSearch);
            this.tab_Optimisation.Controls.Add(this.cB_CoreBoot);
            this.tab_Optimisation.Controls.Add(this.cB_WinSat);
            this.tab_Optimisation.Controls.Add(this.cB_Reindexation);
            this.tab_Optimisation.Controls.Add(this.cB_Aero);
            this.tab_Optimisation.Controls.Add(this.cB_Diskperf);
            this.tab_Optimisation.Controls.Add(this.cB_FichierImpEnAttente);
            this.tab_Optimisation.Controls.Add(this.cB_USB);
            this.tab_Optimisation.Controls.Add(this.cB_Noyau);
            this.tab_Optimisation.Controls.Add(this.cB_VerifFichiers);
            this.tab_Optimisation.Controls.Add(this.cB_DefragBoot);
            this.tab_Optimisation.Location = new System.Drawing.Point(4, 22);
            this.tab_Optimisation.Name = "tab_Optimisation";
            this.tab_Optimisation.Padding = new System.Windows.Forms.Padding(3);
            this.tab_Optimisation.Size = new System.Drawing.Size(609, 385);
            this.tab_Optimisation.TabIndex = 1;
            this.tab_Optimisation.Text = "Optimisation";
            this.tab_Optimisation.UseVisualStyleBackColor = true;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Cleaner.Properties.Resources.Banniere;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(641, 177);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 10;
            this.pictureBox1.TabStop = false;
            // 
            // btn_Quitter
            // 
            this.btn_Quitter.Location = new System.Drawing.Point(550, 611);
            this.btn_Quitter.Name = "btn_Quitter";
            this.btn_Quitter.Size = new System.Drawing.Size(75, 23);
            this.btn_Quitter.TabIndex = 11;
            this.btn_Quitter.Text = "Quitter";
            this.btn_Quitter.UseVisualStyleBackColor = true;
            this.btn_Quitter.Click += new System.EventHandler(this.btn_Quitter_Click);
            // 
            // Cleaner
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(641, 668);
            this.Controls.Add(this.btn_Quitter);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.Onglets);
            this.Controls.Add(this.btn_Journal);
            this.Controls.Add(this.StatusStrip);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(500, 552);
            this.Name = "Cleaner";
            this.Text = "Cleaner";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormCleaner_FormClosing);
            this.StatusStrip.ResumeLayout(false);
            this.StatusStrip.PerformLayout();
            this.Onglets.ResumeLayout(false);
            this.tab_Nettoyage.ResumeLayout(false);
            this.tab_Nettoyage.PerformLayout();
            this.tab_Optimisation.ResumeLayout(false);
            this.tab_Optimisation.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btn_Optimisation;
        private System.Windows.Forms.CheckBox cB_IE;
        private System.Windows.Forms.CheckBox cB_CCleaner;
        private System.Windows.Forms.CheckBox cB_WindowsUpdate;
        private System.Windows.Forms.CheckBox cB_DossierTelechargement;
        private System.Windows.Forms.CheckBox cB_NettoyageWindows;
        private System.Windows.Forms.CheckBox cB_Hibernation;
        private System.Windows.Forms.CheckBox cB_DechargerDll;
        private System.Windows.Forms.CheckBox cB_Aero;
        private System.Windows.Forms.CheckBox cB_Prefetch;
        private System.Windows.Forms.CheckBox cB_Superfetch;
        private System.Windows.Forms.CheckBox cB_WindowsSearch;
        private System.Windows.Forms.CheckBox cB_WinSat;
        private System.Windows.Forms.CheckBox cB_FichierImpEnAttente;
        private System.Windows.Forms.CheckBox cB_Noyau;
        private System.Windows.Forms.CheckBox cB_DefragBoot;
        private System.Windows.Forms.CheckBox cB_USB;
        private System.Windows.Forms.CheckBox cB_VerifFichiers;
        private System.Windows.Forms.CheckBox cB_Diskperf;
        private System.Windows.Forms.CheckBox cB_Reindexation;
        private System.Windows.Forms.CheckBox cB_CoreBoot;
        private System.Windows.Forms.Button btn_Nettoyage;
        private System.Windows.Forms.Button Btn_Cocher_Opt;
        private System.Windows.Forms.Button Btn_Decocher_Opt;
        private System.Windows.Forms.StatusStrip StatusStrip;
        public System.Windows.Forms.ToolStripProgressBar pB;
        private System.Windows.Forms.ToolStripStatusLabel Lb_Statut;
        private System.Windows.Forms.ToolStripStatusLabel Lb_StatutEnCours;
        private System.Windows.Forms.ToolStripStatusLabel Spring;
        private System.Windows.Forms.Button btn_Journal;
        private System.Windows.Forms.ToolStripStatusLabel lb_TpsEcouleInfo;
        private System.Windows.Forms.ToolStripStatusLabel lb_TmpEcoule;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.CheckBox cB_DCOM;
        private System.Windows.Forms.CheckBox cB_OptiAccesMemoireDisque;
        private System.Windows.Forms.TabControl Onglets;
        private System.Windows.Forms.TabPage tab_Nettoyage;
        private System.Windows.Forms.TabPage tab_Optimisation;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ToolStripStatusLabel ts_Spring;
        private System.Windows.Forms.Button btn_Options;
        private System.Windows.Forms.Button Btn_Decocher_Nett;
        private System.Windows.Forms.Button Btn_Cocher_Nett;
        private System.Windows.Forms.Button btn_Quitter;
    }
}