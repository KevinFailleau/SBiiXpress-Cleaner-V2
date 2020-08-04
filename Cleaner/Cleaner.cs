using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace Cleaner
{
    public partial class Cleaner : Form
    {
        #region Variables

        private string verOS;
        private RegistryView rV;
        public static bool DownloadDone = false;
        private Thread myThreadCCleaner, myThreadCleanmgr, myThreadPrincipal, myThreadDownload, myThreadWindowsUpdate, myThreadIE;
        public bool CleanmgrDone, CCleanerDone, TraitementDone = false;
        private static double espaceAv, espaceAp;
        private bool WUActif = false;
        private Stopwatch stopWatch;
        private static string temps;

        #endregion
        
        #region Accesseurs
        /// <summary>
        /// Ces méthodes permettent d'accéder indirectement à certaines variables
        /// </summary>
        public static double EspaceAv
        {
            get
            {
                return espaceAv;
            }

            set
            {
                espaceAv = value;
            }
        }
        public static double EspaceAp
        {
            get
            {
                return espaceAp;
            }

            set
            {
                espaceAp = value;
            }
        }

        public static string Temps()
        {
            return temps;
        }

        private string timerGetTemps()
        {
            return stopWatch.Elapsed.Hours.ToString("00") + ":" + stopWatch.Elapsed.Minutes.ToString("00") + ":" + stopWatch.Elapsed.Seconds.ToString("00");
        }
        #endregion

        #region Constructeurs
        /// <summary>
        /// Méthodes qui permettent d'initialiser la form Cleaner
        /// </summary>
        /// <param name="reV">Type de RegistryView (différents pour systèmes 32 et 62 bits)</param>
        public Cleaner(RegistryView reV)
        {
            InitializeComponent();
            rV = reV;
            if (!Ressources.CCleanerIsPresent(rV))
            {
                //Si CCleaner n'est pas installé, on désactive l'option sur la form
                cB_CCleaner.Checked = false;
                cB_CCleaner.Enabled = false;
            }
            if (Ressources.VersionOS() == "10" || Ressources.VersionOS() == "8.1" || Ressources.VersionOS() == "8" || Ressources.VersionOS() == "XP")
            {
                //Si l'OS est Windows 10, 8.1, 8 ou XP alors on désactive Aero (indisponible sur ces versions)
                cB_Aero.Checked = false;
                cB_Aero.Enabled = false;
                if (Ressources.VersionOS() == "XP")
                {
                    //Si l'OS est XP on désactive les options d'optimisation des tranferts USB, de Superfetch, Windows Search
                    cB_USB.Checked = false;
                    cB_USB.Enabled = false;
                    cB_Superfetch.Checked = false;
                    cB_Superfetch.Enabled = false;
                    cB_WindowsSearch.Checked = false;
                    cB_WindowsSearch.Enabled = false;
                    cB_Reindexation.Checked = false;
                    cB_Reindexation.Enabled = false;
                }
            }
            else if (Ressources.VersionOS() == "Vista")
            {
                //Si l'OS est Vista alors on ne désactive que l'option d'optimisation des ports USB
                cB_USB.Checked = false;
                cB_USB.Enabled = false;
            }
        }

        #endregion

        #region Événements

        /// <summary>
        /// Evénement qui s'active lorsque l'utilisateur modifie l'état d'une case dans la section nettoyage
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBoxNettoyage_CheckedChanged(object sender, EventArgs e)
        {
            int i = 0, j = 0;
            foreach (Control c in tab_Nettoyage.Controls)
            {
                if (c is CheckBox)
                {
                    j++; //On compte le nombre de checkBox dans la section
                    CheckBox chk = (CheckBox)c;
                    if (!chk.Checked) i++; //On compte le nombre de checkBox non cochées dans la section
                }
            }
            if (i == j) btn_Nettoyage.Enabled = false; //Si aucune checkBox n'est cochée on désactive le bouton de Nettoyage, sinon on l'active
            else btn_Nettoyage.Enabled = true;
        }

        /// <summary>
        /// Evénement qui s'active lorsque l'utilisateur modifie l'état d'une case dans la section optimisation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBoxOptimisation_CheckedChanged(object sender, EventArgs e)
        {
            int i = 0, j = 0;
            foreach (Control c in tab_Optimisation.Controls)
            {
                if (c is CheckBox)
                {
                    j++; //On compte le nombre de checkBox dans la section
                    CheckBox chk = (CheckBox)c;
                    if (!chk.Checked) i++; //On compte le nombre de checkBox non cochées dans la section
                }
            }
            if (i == j) btn_Optimisation.Enabled = false; //Si aucune checkBox n'est cochée on désactive ne bouton d'Optimisation, sinon on l'active
            else btn_Optimisation.Enabled = true;
        }

        /// <summary>
        /// Evénement qui s'active lorsque l'utilisateur clique sur "Tout décocher" dans la section Nettoyage
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Decocher_Nett_Click(object sender, EventArgs e)
        {
            foreach (Control c in tab_Nettoyage.Controls)
            {
                if (c is CheckBox)
                {
                    CheckBox chk = (CheckBox)c;
                    if (chk.Checked)
                    {
                        chk.Checked = false; //Pour chaque checkBox dans la section, si elle est cochée, alors on la décoche
                    }
                }
            }
        }

        /// <summary>
        /// Evénement qui s'active lorsque l'utilisateur clique sur le bouton "Tout cocher" dans la section Optimisation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Cocher_Opt_Click(object sender, EventArgs e)
        {
            foreach (Control c in tab_Optimisation.Controls)
            {
                if (c is CheckBox)
                {
                    CheckBox chk = (CheckBox)c;
                    if (!chk.Checked && chk.Enabled)
                    {
                        chk.Checked = true; //Pour chaque checkBox dans la section, si elle décochée, alors on la coche. On vérifie aussi si la checkBox n'a pas été désactivée, si c'est le cas alors on n'y touche pas
                    }
                }
            }
        }

        /// <summary>
        /// Evénement qui s'active lorsque l'utilisateur clique sur le bouton "Tout décocher" de la section Optimisation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Decocher_Opt_Click(object sender, EventArgs e)
        {
            foreach (Control c in tab_Optimisation.Controls)
            {
                if (c is CheckBox)
                {
                    CheckBox chk = (CheckBox)c;
                    if (chk.Checked)
                    {
                        chk.Checked = false; //Pour chaque checkBox dans la section, si elle cochée, alors on la décoche
                    }
                }
            }
        }

        /// <summary>
        /// Evénement qui s'active lorsque l'utilisateur clique sur le bouton "Tout cocher" de la section Nettoyage
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Cocher_Nett_Click(object sender, EventArgs e)
        {
            foreach (Control c in tab_Nettoyage.Controls)
            {
                if (c is CheckBox)
                {
                    CheckBox chk = (CheckBox)c;
                    if (!chk.Checked && chk.Enabled)
                    {
                        chk.Checked = true;//Pour chaque checkBox dans la section, si elle décochée, alors on la coche. On vérifie aussi si la checkBox n'a pas été désactivée, si c'est le cas alors on n'y touche pas
                    }
                }
            }
        }

        /// <summary>
        /// Evénement qui s'active lorsque l'utilisateur clique sur le bouton "Optimiser"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Optimisation_Click(object sender, EventArgs e)
        {
            Optimisation(); //On lance le traitement optimisation
        }

        /// <summary>
        /// Evénement qui s'active lorsque l'utilisateur clique sur le bouton "Nettoyer"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Nettoyage_Click(object sender, EventArgs e)
        {
            DialogResult DR = MessageBox.Show("Êtes-vous sûr de vouloir lancer le traitement ?", "Question", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            if (DR == DialogResult.Yes)
            {
                EspaceAv = Ressources.EspaceLibre(); //On note l'espace disque libre avant le traitement
                if (cB_Hibernation.Checked && !cB_CCleaner.Checked && !cB_IE.Checked && !cB_NettoyageWindows.Checked && !cB_WindowsUpdate.Checked && !cB_DossierTelechargement.Checked)
                {
                    //Si seule la désactivation de l'hibernation a été cochée
                    DesactiverHibernation(); //Lancement de la désactivation
                    Thread.Sleep(50); //On attends un peu, pour laisser le temps de supprimer les fichiers d'hibernation
                    EspaceAp = Ressources.EspaceLibre(); //On note l'espace libre après la désactivation de l'hibernation
                    if (Ressources.CalculEspaceLibere() != 0)
                    {
                        //Si la désactivation de l'hibernation a permis de libérer de l'espace alors on l'affiche
                        MessageBox.Show("L'hibernation a été désactivée et a permis la libération de " + Ressources.CalculEspaceLibere() + "Mo", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        //Sinon on annonce juste que l'opération est terminée
                        MessageBox.Show("L'hibernation a été désactivée et ses fichiers supprimés", "Informations", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    //Si autre chose que l'hibernation a été coché alors on lance le traitement
                    if (cB_Hibernation.Checked)
                    {
                        DesactiverHibernation(); //Si la désactivation de l'hibernation a été cochée alors on effectue la désactivation
                    }
                    StartTimer(); //On lance le timer
                    pB.Style = ProgressBarStyle.Marquee; //Affichage et démarrage de la ProgressBar
                    pB.MarqueeAnimationSpeed = 30;
                    pB.Visible = true;
                    Traitement(); //Lancement du traitement
                }
            }
        }

        /// <summary>
        /// Evénement qui s'active lorsque l'utilisateur tente de fermer l'application
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormCleaner_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (myThreadPrincipal != null && TraitementDone == false)
            {
                //Si le nettoyage est en cours alors, on demande à l'utilisateur s'il veut annuler le nettoyage
                DialogResult DR = MessageBox.Show("Le traitement n'est pas terminé, voulez-vous quitter le programme et annuler le traitement ?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (DR == DialogResult.Yes)
                {
                    myThreadPrincipal.Abort(); //Si oui, on annule le nettoyage
                }
                else
                {
                    e.Cancel = true; //Si non, on annule la fermeture
                }
            }
        }

        /// <summary>
        /// Evénement qui s'active lorsque l'utilisateur clique sur le bouton "Options"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Options_Click(object sender, EventArgs e)
        {
            Options FO = new Options();
            FO.Show(); //On affiche la form Options
        }

        /// <summary>
        /// Méthode de mise à jour du temps sur l'interface utilisateur
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer_Tick(object sender, EventArgs e)
        {
            //Mise à jour du temps sur l'interface utilisateur chaque secondes
            lb_TmpEcoule.Text = stopWatch.Elapsed.Hours.ToString("00") + ":" + stopWatch.Elapsed.Minutes.ToString("00") + ":" + stopWatch.Elapsed.Seconds.ToString("00");
        }

        /// <summary>
        /// Méthode qui permet de lancer le Timer
        /// </summary>
        private void StartTimer()
        {
            lb_TpsEcouleInfo.Visible = true;
            lb_TmpEcoule.Visible = true;
            lb_TmpEcoule.Text = "00:00:00"; //On affiche les labels correspondants au temps et on mets une valeur par défaut au temps
            stopWatch = new Stopwatch();
            timer.Enabled = true;
            stopWatch.Start(); //On démarre le timer
        }

        /// <summary>
        /// Méthode qui sera exécutée quand l'utilisateur clique sur "Quitter"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Quitter_Click(object sender, EventArgs e)
        {
            this.Close(); //On ferme l'application
        }

        /// <summary>
        /// Méthode qui est exécutée à la fin du traitement
        /// </summary>
        private void stopTimer()
        {
            stopWatch.Stop(); //On arrête le timer
            timer.Enabled = false; //Désactivation du timer
            temps = stopWatch.Elapsed.Hours.ToString("00") + ":" + stopWatch.Elapsed.Minutes.ToString("00") + ":" + stopWatch.Elapsed.Seconds.ToString("00"); //On stocke le temps de traitement pour la BDD
        }

        /// <summary>
        /// Méthode qui sera exécutée lorsque l'utilisateur clique sur le bouton "Journal"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Journal_Click(object sender, EventArgs e)
        {
            Journal J = new Journal();
            J.Show(); //On affiche la form Journal
        }

        #endregion

        #region Thread principal (Nettoyage)

        /// <summary>
        /// Méthode qui est appelée juste avant le lancement du traitement 
        /// </summary>
        public void Traitement()
        {
            if (myThreadPrincipal == null || !myThreadPrincipal.IsAlive) //Si le traitement principal est n'est pas lancé
            {
                pB.Visible = true; //On affiche la progressBar
                EnableDisableUI('D'); //Désactivation de l'interface utilisateur
                myThreadPrincipal = new Thread(TraitementLoop);
                myThreadPrincipal.Start(); //Lancement du Thread Nettoyage
            }
        }
        
        /// <summary>
        /// Méthode qui sera exécutée en boucle lors de l'exécution du traitement
        /// </summary>
        public void TraitementLoop()
        {
            while (Thread.CurrentThread.IsAlive && TraitementDone == false) //Tant que le traitement est en cours
            {
                Lb_StatutEnCours.GetCurrentParent().Invoke((MethodInvoker)delegate { Lb_StatutEnCours.Text = "Veuillez patienter ..."; }); //Mise à jour du statut sur la form Cleaner
                Thread.Sleep(30000); //On attends 30 secondes pour éviter de surcharger la machine

                //Les conditions suivantes permettent de vérifier chaque cas de figure possible en fonction des cases qui ont été cochées
                if (cB_CCleaner.Checked && !cB_NettoyageWindows.Checked && !cB_DossierTelechargement.Checked && !cB_WindowsUpdate.Checked && !cB_IE.Checked)
                {
                    //Si seul CCleaner a été coché alors on lance CCleaner
                    ThreadCCleaner();
                    myThreadCCleaner.Join();
                    if (!myThreadCCleaner.IsAlive)
                    {
                        finTraitement(); //Une fois que CCleaner a terminé alors on appelle la méthode finTraitement
                    }
                }
                else if (cB_CCleaner.Checked && !cB_NettoyageWindows.Checked && cB_DossierTelechargement.Checked && !cB_WindowsUpdate.Checked && !cB_IE.Checked)
                {
                    //Si CCleaner et le nettoyage du dossier de téléchargement a été coché
                    ThreadCCleaner(); //On lance le thread CCleaner
                    myThreadCCleaner.Join(); //On attends que le thread CCleaner ait terminé
                    ThreadDownload(Options.NbJours()); //Lancement du nettoyage du dossier de téléchargement en prenant en compte le nombre de jours choisi dans les options
                    myThreadDownload.Join();
                    if (!myThreadCCleaner.IsAlive && !myThreadDownload.IsAlive)
                    {
                        finTraitement(); //Une fois que les deux Thread sont terminés on appelle la méthode finTraitement
                    }
                }
                else if (cB_CCleaner.Checked && cB_NettoyageWindows.Checked && cB_DossierTelechargement.Checked && !cB_WindowsUpdate.Checked && !cB_IE.Checked)
                {
                    ThreadCCleaner();
                    myThreadCCleaner.Join();
                    ThreadNettoyage();
                    myThreadCleanmgr.Join();
                    ThreadDownload(Options.NbJours());
                    myThreadDownload.Join();
                    if (!myThreadCCleaner.IsAlive && !myThreadDownload.IsAlive && !myThreadCleanmgr.IsAlive)
                    {
                        finTraitement();
                    }
                }
                else if (cB_CCleaner.Checked && !cB_NettoyageWindows.Checked && !cB_DossierTelechargement.Checked && cB_WindowsUpdate.Checked && !cB_IE.Checked)
                {
                    ThreadCCleaner();
                    myThreadCCleaner.Join();
                    NettoyageWindowsUpdate();
                    myThreadWindowsUpdate.Join();
                    if (!myThreadCCleaner.IsAlive && !myThreadWindowsUpdate.IsAlive)
                    {
                        finTraitement();
                    }
                }
                else if (cB_CCleaner.Checked && cB_NettoyageWindows.Checked && cB_DossierTelechargement.Checked && cB_WindowsUpdate.Checked && !cB_IE.Checked)
                {
                    ThreadCCleaner();
                    myThreadCCleaner.Join();
                    NettoyageWindowsUpdate();
                    myThreadWindowsUpdate.Join();
                    ThreadNettoyage();
                    myThreadCleanmgr.Join();
                    ThreadDownload(Options.NbJours());
                    myThreadDownload.Join();
                    if (!myThreadCCleaner.IsAlive && !myThreadWindowsUpdate.IsAlive && !myThreadCleanmgr.IsAlive && !myThreadDownload.IsAlive)
                    {
                        finTraitement();
                    }
                }
                else if (cB_CCleaner.Checked && cB_NettoyageWindows.Checked && !cB_DossierTelechargement.Checked && !cB_WindowsUpdate.Checked && !cB_IE.Checked)
                {
                    ThreadNettoyage();
                    myThreadCleanmgr.Join();
                    ThreadCCleaner();
                    if (!myThreadCleanmgr.IsAlive && !myThreadCCleaner.IsAlive)
                    {
                        finTraitement();
                    }
                }
                else if (cB_CCleaner.Checked && cB_NettoyageWindows.Checked && cB_DossierTelechargement.Checked && !cB_WindowsUpdate.Checked && !cB_IE.Checked)
                {
                    ThreadNettoyage();
                    myThreadCleanmgr.Join();
                    if (Ressources.CCleanerIsPresent(rV))
                    {
                        ThreadCCleaner();
                        myThreadCCleaner.Join();
                    }
                    ThreadDownload(Options.NbJours());
                    myThreadDownload.Join();
                    if (!myThreadCleanmgr.IsAlive && myThreadCCleaner == null && !myThreadDownload.IsAlive || !myThreadCleanmgr.IsAlive && !myThreadCCleaner.IsAlive && !myThreadDownload.IsAlive)
                    {
                        finTraitement();
                    }
                }
                else if (cB_CCleaner.Checked && cB_NettoyageWindows.Checked && !cB_DossierTelechargement.Checked && cB_WindowsUpdate.Checked && !cB_IE.Checked)
                {
                    ThreadNettoyage();
                    myThreadCleanmgr.Join();
                    ThreadCCleaner();
                    myThreadCCleaner.Join();
                    NettoyageWindowsUpdate();
                    myThreadWindowsUpdate.Join();
                    if (!myThreadCleanmgr.IsAlive && !myThreadWindowsUpdate.IsAlive && !myThreadCCleaner.IsAlive)
                    {
                        finTraitement();
                    }
                }
                else if (cB_CCleaner.Checked && !cB_NettoyageWindows.Checked && cB_DossierTelechargement.Checked && cB_WindowsUpdate.Checked && !cB_IE.Checked)
                {
                    ThreadCCleaner();
                    myThreadCCleaner.Join();
                    NettoyageWindowsUpdate();
                    myThreadWindowsUpdate.Join();
                    ThreadDownload(Options.NbJours());
                    myThreadDownload.Join();
                    if (!myThreadWindowsUpdate.IsAlive && !myThreadCCleaner.IsAlive && !myThreadDownload.IsAlive)
                    {
                        finTraitement();
                    }
                }
                else if (cB_WindowsUpdate.Checked && !cB_CCleaner.Checked && !cB_NettoyageWindows.Checked && !cB_DossierTelechargement.Checked && !cB_IE.Checked)
                {
                    NettoyageWindowsUpdate();
                    myThreadWindowsUpdate.Join();
                    if (!myThreadWindowsUpdate.IsAlive)
                    {
                        finTraitement();
                    }
                }
                else if (cB_WindowsUpdate.Checked && !cB_CCleaner.Checked && cB_NettoyageWindows.Checked && !cB_DossierTelechargement.Checked && !cB_IE.Checked)
                {
                    ThreadNettoyage();
                    myThreadCleanmgr.Join();
                    NettoyageWindowsUpdate();
                    myThreadWindowsUpdate.Join();
                    if (!myThreadWindowsUpdate.IsAlive && !myThreadCleanmgr.IsAlive)
                    {
                        finTraitement();
                    }
                }
                else if (cB_WindowsUpdate.Checked && !cB_CCleaner.Checked && cB_NettoyageWindows.Checked && cB_DossierTelechargement.Checked && !cB_IE.Checked)
                {
                    ThreadNettoyage();
                    myThreadCleanmgr.Join();
                    NettoyageWindowsUpdate();
                    myThreadWindowsUpdate.Join();
                    ThreadDownload(Options.NbJours());
                    myThreadDownload.Join();
                    if (!myThreadWindowsUpdate.IsAlive && !myThreadCleanmgr.IsAlive && !myThreadDownload.IsAlive)
                    {
                        finTraitement();
                    }
                }
                else if (cB_NettoyageWindows.Checked && !cB_CCleaner.Checked && cB_DossierTelechargement.Checked && !cB_WindowsUpdate.Checked && !cB_IE.Checked)
                {
                    ThreadNettoyage();
                    myThreadCleanmgr.Join();
                    ThreadDownload(Options.NbJours());
                    myThreadDownload.Join();
                    if (!myThreadCleanmgr.IsAlive && !myThreadDownload.IsAlive)
                    {
                        finTraitement();
                    }
                }
                else if (cB_NettoyageWindows.Checked && cB_CCleaner.Checked && !cB_DossierTelechargement.Checked && !cB_WindowsUpdate.Checked && !cB_IE.Checked)
                {
                    ThreadNettoyage();
                    myThreadCleanmgr.Join();
                    ThreadCCleaner();
                    myThreadCCleaner.Join();
                    if (!myThreadCleanmgr.IsAlive && !myThreadCCleaner.IsAlive)
                    {
                        finTraitement();
                    }
                }
                else if (!cB_CCleaner.Checked && cB_NettoyageWindows.Checked && !cB_DossierTelechargement.Checked && !cB_WindowsUpdate.Checked && !cB_IE.Checked)
                {
                    ThreadNettoyage();
                    myThreadCleanmgr.Join();
                    if (!myThreadCleanmgr.IsAlive)
                    {
                        finTraitement();
                    }
                }
                else if (!cB_CCleaner.Checked && cB_NettoyageWindows.Checked && cB_DossierTelechargement.Checked && !cB_WindowsUpdate.Checked && !cB_IE.Checked)
                {
                    ThreadNettoyage();
                    myThreadCleanmgr.Join();
                    ThreadDownload(Options.NbJours());
                    myThreadDownload.Join();
                    if (!myThreadCleanmgr.IsAlive && !myThreadDownload.IsAlive)
                    {
                        finTraitement();
                    }
                }
                else if (!cB_CCleaner.Checked && !cB_NettoyageWindows.Checked && cB_DossierTelechargement.Checked && !cB_WindowsUpdate.Checked && !cB_IE.Checked)
                {
                    ThreadDownload(Options.NbJours());
                    myThreadDownload.Join();
                    if (!myThreadDownload.IsAlive)
                    {
                        finTraitement();
                    }
                }
                else if (!cB_CCleaner.Checked && !cB_NettoyageWindows.Checked && !cB_DossierTelechargement.Checked && !cB_WindowsUpdate.Checked && cB_IE.Checked)
                {
                    ThreadIE();
                    myThreadIE.Join();
                    if (!myThreadIE.IsAlive)
                    {
                        finTraitement();
                    }
                }
                else if (cB_CCleaner.Checked && !cB_NettoyageWindows.Checked && !cB_DossierTelechargement.Checked && !cB_WindowsUpdate.Checked && cB_IE.Checked)
                {
                    ThreadIE();
                    myThreadIE.Join();
                    ThreadCCleaner();
                    myThreadCCleaner.Join();
                    if (!myThreadIE.IsAlive && !myThreadCCleaner.IsAlive)
                    {
                        finTraitement();
                    }
                }
                else if (!cB_CCleaner.Checked && !cB_NettoyageWindows.Checked && !cB_DossierTelechargement.Checked && cB_WindowsUpdate.Checked && cB_IE.Checked)
                {
                    ThreadIE();
                    myThreadIE.Join();
                    NettoyageWindowsUpdate();
                    myThreadWindowsUpdate.Join();
                    if (!myThreadIE.IsAlive && !myThreadWindowsUpdate.IsAlive)
                    {
                        finTraitement();
                    }
                }
                else if (!cB_CCleaner.Checked && cB_NettoyageWindows.Checked && !cB_DossierTelechargement.Checked && !cB_WindowsUpdate.Checked && cB_IE.Checked)
                {
                    ThreadIE();
                    myThreadIE.Join();
                    ThreadNettoyage();
                    myThreadCleanmgr.Join();
                    if (!myThreadIE.IsAlive && !myThreadCleanmgr.IsAlive)
                    {
                        finTraitement();
                    }
                }
                else if (!cB_CCleaner.Checked && !cB_NettoyageWindows.Checked && cB_DossierTelechargement.Checked && !cB_WindowsUpdate.Checked && cB_IE.Checked)
                {
                    ThreadIE();
                    myThreadIE.Join();
                    ThreadDownload(Options.NbJours());
                    myThreadDownload.Join();
                    if (!myThreadIE.IsAlive && !myThreadDownload.IsAlive)
                    {
                        finTraitement();
                    }
                }
                else if (cB_CCleaner.Checked && !cB_NettoyageWindows.Checked && !cB_DossierTelechargement.Checked && cB_WindowsUpdate.Checked && cB_IE.Checked)
                {
                    ThreadIE();
                    myThreadIE.Join();
                    ThreadCCleaner();
                    myThreadCCleaner.Join();
                    NettoyageWindowsUpdate();
                    myThreadWindowsUpdate.Join();
                    if (!myThreadIE.IsAlive && !myThreadCCleaner.IsAlive && !myThreadWindowsUpdate.IsAlive)
                    {
                        finTraitement();
                    }
                }
                else if (cB_CCleaner.Checked && cB_NettoyageWindows.Checked && !cB_DossierTelechargement.Checked && !cB_WindowsUpdate.Checked && cB_IE.Checked)
                {
                    ThreadIE();
                    myThreadIE.Join();
                    ThreadCCleaner();
                    myThreadCCleaner.Join();
                    ThreadNettoyage();
                    myThreadCleanmgr.Join();
                    if (!myThreadIE.IsAlive && !myThreadCCleaner.IsAlive && !myThreadCleanmgr.IsAlive)
                    {
                        finTraitement();
                    }
                }
                else if (cB_CCleaner.Checked && !cB_NettoyageWindows.Checked && cB_DossierTelechargement.Checked && !cB_WindowsUpdate.Checked && cB_IE.Checked)
                {
                    ThreadIE();
                    myThreadIE.Join();
                    ThreadCCleaner();
                    myThreadCCleaner.Join();
                    ThreadDownload(Options.NbJours());
                    myThreadDownload.Join();
                    if (!myThreadIE.IsAlive && !myThreadCCleaner.IsAlive && !myThreadDownload.IsAlive)
                    {
                        finTraitement();
                    }
                }
                else if (cB_CCleaner.Checked && cB_NettoyageWindows.Checked && !cB_DossierTelechargement.Checked && cB_WindowsUpdate.Checked && cB_IE.Checked)
                {
                    ThreadIE();
                    myThreadIE.Join();
                    ThreadCCleaner();
                    myThreadCCleaner.Join();
                    ThreadNettoyage();
                    myThreadCleanmgr.Join();
                    NettoyageWindowsUpdate();
                    myThreadWindowsUpdate.Join();
                    if (!myThreadIE.IsAlive && !myThreadCCleaner.IsAlive && !myThreadCleanmgr.IsAlive && !myThreadWindowsUpdate.IsAlive)
                    {
                        finTraitement();
                    }
                }
                else if (!cB_CCleaner.Checked && cB_NettoyageWindows.Checked && !cB_DossierTelechargement.Checked && cB_WindowsUpdate.Checked && cB_IE.Checked)
                {
                    ThreadIE();
                    myThreadIE.Join();
                    ThreadNettoyage();
                    myThreadCleanmgr.Join();
                    NettoyageWindowsUpdate();
                    myThreadWindowsUpdate.Join();
                    if (!myThreadIE.IsAlive && !myThreadCleanmgr.IsAlive && !myThreadWindowsUpdate.IsAlive)
                    {
                        finTraitement();
                    }
                }
                else if (!cB_CCleaner.Checked && !cB_NettoyageWindows.Checked && cB_DossierTelechargement.Checked && cB_WindowsUpdate.Checked && cB_IE.Checked)
                {
                    ThreadIE();
                    myThreadIE.Join();
                    NettoyageWindowsUpdate();
                    myThreadWindowsUpdate.Join();
                    ThreadDownload(Options.NbJours());
                    myThreadDownload.Join();
                    if (!myThreadIE.IsAlive && !myThreadWindowsUpdate.IsAlive && !myThreadDownload.IsAlive)
                    {
                        finTraitement();
                    }
                }
                else if (!cB_CCleaner.Checked && cB_NettoyageWindows.Checked && cB_DossierTelechargement.Checked && !cB_WindowsUpdate.Checked && cB_IE.Checked)
                {
                    ThreadIE();
                    myThreadIE.Join();
                    ThreadNettoyage();
                    myThreadCleanmgr.Join();
                    ThreadDownload(Options.NbJours());
                    myThreadDownload.Join();
                    if (!myThreadIE.IsAlive && !myThreadCleanmgr.IsAlive && !myThreadDownload.IsAlive)
                    {
                        finTraitement();
                    }
                }
                else if (cB_CCleaner.Checked && cB_NettoyageWindows.Checked && cB_DossierTelechargement.Checked && cB_WindowsUpdate.Checked && cB_IE.Checked)
                {
                    ThreadIE();
                    myThreadIE.Join();
                    ThreadCCleaner();
                    myThreadCCleaner.Join();
                    ThreadNettoyage();
                    myThreadCleanmgr.Join();
                    NettoyageWindowsUpdate();
                    myThreadWindowsUpdate.Join();
                    ThreadDownload(Options.NbJours());
                    myThreadDownload.Join();
                    if (!myThreadIE.IsAlive && !myThreadCCleaner.IsAlive && !myThreadCleanmgr.IsAlive && !myThreadWindowsUpdate.IsAlive && !myThreadDownload.IsAlive)
                    {
                        finTraitement();
                    }
                }
                else if (!cB_CCleaner.Checked && cB_NettoyageWindows.Checked && cB_DossierTelechargement.Checked && cB_WindowsUpdate.Checked && cB_IE.Checked)
                {
                    ThreadIE();
                    myThreadIE.Join();
                    ThreadNettoyage();
                    myThreadCleanmgr.Join();
                    NettoyageWindowsUpdate();
                    myThreadWindowsUpdate.Join();
                    ThreadDownload(Options.NbJours());
                    myThreadDownload.Join();
                    if (!myThreadIE.IsAlive && !myThreadCleanmgr.IsAlive && !myThreadWindowsUpdate.IsAlive && !myThreadDownload.IsAlive)
                    {
                        finTraitement();
                    }
                }
                else
                {
                    //Au cas où un cas de figure a été oublié, on affiche un message d'erreur (ne devrait pas se produire)
                    MessageBox.Show("Cette configuration n'a pas été prise en charge, contactez le développeur de l'application", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    finTraitement(); //On appelle la méthode finTraitement même s'il n'a pas été lancé
                }
            }

        }
        #endregion

        #region Threads (Nettoyage)
        /// <summary>
        /// Méthode qui est appellée à la fin du traitement
        /// </summary>
        public void finTraitement()
        {
            TraitementDone = true;
            Lb_StatutEnCours.Text = "Arrêté"; //Mise à jour du statut sur la form Cleaner
            pB.GetCurrentParent().Invoke((MethodInvoker)delegate { pB.ProgressBar.Style = ProgressBarStyle.Blocks; pB.MarqueeAnimationSpeed = 30; pB.Visible = false; }); //Désactivation de la progressBar
            EnableDisableUI('E'); //Réactivation de l'interface utilisateur
            EspaceAp = Ressources.EspaceLibre(); //On note l'espace libre après le traitement
            stopTimer(); //On arrête le timer
            if(Ressources.VersionOS() != "XP" && WUActif || Ressources.VersionOS() != "Vista" && WUActif)
            {
                Ressources.Exec("cmd", " /c net start wuauserv"); //Réactivation de Windows Update s'il l'était avant le traitement et si l'OS n'est pas Windows XP ou Vista
            }
            MessageBox.Show("Le traitement est maintenant terminé\n" + Ressources.CalculEspaceLibere() + "Mo ont étés libérés sur le disque C:" + "\nTemps écoulé : " + timerGetTemps()+"\nL'application va maintenant se fermer", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            if (Ressources.CalculEspaceLibere() != 0)
            {
                //Si l'espace libéré est égal a zéro alors on n'écrit pas dans le log
                Ressources.Ecriture_Log();
            }
            Application.Exit(); //On quitte l'application (car il n'est pas possible de relancer un autre traitement une fois qu'un premier traitement a été lancé, pour cela il faut quitter l'application)
            myThreadPrincipal.Abort(); //On arrête le thread principal
        }

        /// <summary>
        /// Méthode de nettoyage de Windows Update
        /// </summary>
        public void NettoyageWindowsUpdate()
        {
            if (Ressources.VerifProcessus("wuauserv") == "Running" || Ressources.VerifProcessus("wuauserv") == "Starting")
            {
                WUActif = true; //Si le service Windows Update est lancé alors on passe la variable WUActif à vrai, sinon on la laisse à false
            }
            Lb_StatutEnCours.GetCurrentParent().Invoke((MethodInvoker)delegate { Lb_StatutEnCours.Text = "Nettoyage de Windows Update"; }); //Mise à jour de statut sur la form Cleaner (ToolStrip)
            Ressources.Exec("cmd", " /c net stop wuauserv"); //Arrêt du service Windows Update
            Ressources.Exec("cmd", "/c rd /s /Q %Windir%\\SoftwareDistribution\\Download"); //Suppression du dossier Windows Update
            myThreadWindowsUpdate = new Thread(() => WindowsUpdateLoop(WUActif)); //On passe l'état du service Windows Update à la méthode qui sera exécutée dans le thread
            myThreadWindowsUpdate.Start(); //Lancement de la méthode WindowsUpdateLoop dans un nouveau Thread
        }

        /// <summary>
        /// Méthode qui va être exécutée en boucle tant que le traitement de Windows Update n'est pas terminé
        /// </summary>
        /// <param name="WUActif"></param>
        public void WindowsUpdateLoop(bool WUActif)
        {
            Thread.Sleep(5000); //On attends on peu pour ne pas surcharger la machine à chaque passage dans la boucle while
            while (myThreadWindowsUpdate.IsAlive) //Si le thread Windows Update est lancé, on exécute en boucle
            {
                if (!Directory.Exists("%WinDir%\\SoftwareDistribution\\Download"))
                {
                    //Une fois que le dossier est bien supprimé
                    if (Ressources.VersionOS() == "XP" || Ressources.VersionOS() == "Vista")
                    {
                        global::Cleaner.Optimisation.DesactiverWUpdate(); //Si l'OS est XP ou Vista alors, peu importe l'état de Windows Update avant le traitement, on ne le relance pas après
                    }
                    else if (WUActif && Ressources.VersionOS() != "XP" || WUActif && Ressources.VersionOS() != "Vista")
                    {
                        Ressources.Exec("cmd", " /c net start wuauserv"); //Si l'OS n'est pas XP ou Vista et que Windows Update était activé avant le lancement du traitement, alors on relance le service
                    }
                    myThreadWindowsUpdate.Abort(); //Une fois que le dossier a été supprimer et que Windows Update a été relancé ou non, on termine le thread Windows Update
                }
            }
        }

        /// <summary>
        /// Méthode de désactivation de l'hibernation
        /// </summary>
        public void DesactiverHibernation()
        {
            Ressources.Exec("cmd.exe", "/c powercfg -h off"); //On désactive l'hibernation
            Thread.Sleep(50); //On attends un peu que l'hibernation soit bien arrêtée
            if (File.Exists("C:\\hiberfil.sys")) //Si le fichier d'hibernation existe toujours malgré la désactivation
            {
                try //(Try Catch pour éviter les crash si il n'est pas possible de supprimer le fichier mais pas d'affichage de message d'erreur pour ne pas gêner l'utilisateur)
                {
                    File.Delete("C:\\hiberfil.sys"); //Alors on essaie de le supprimer
                }
                catch (Exception e)
                { }
            }
        }

        /// <summary>
        /// Nettoyage de Internet Explorer
        /// </summary>
        public void ThreadIE()
        {
            try
            {
                //On vérifie si rundll32 n'est pas lancé, sinon on arrête le processus (car cela peut poser problème dans le traitemet)
                Process[] proc = Process.GetProcessesByName("rundll32");
                proc[0].Kill();
            }
            catch (Exception ex){}
            Lb_StatutEnCours.Text = "Nettoyage de Internet Explorer"; //Mise à jour du statut sur la form Cleaner
            myThreadIE = new Thread(LoopIE);
            Ressources.Exec("rundll32.exe", " inetcpl.cpl,ClearMyTracksByProcess 4351"); //On lance le nettoyage de Internet Explorer
            myThreadIE.Start(); //On lance le thread qui vérifira si le nettoyage est terminé
        }

        /// <summary>
        /// Méthode qui sera exécutée en boucle par le Thread Internet Explorer
        /// </summary>
        public void LoopIE()
        {
            Thread.Sleep(5000); //On attends un peu pour ne pas saturer la machine à chaque passage dans la boucle while
            while (myThreadIE.IsAlive) //Tant que le Thread Internet Explorer n'est pas terminé
            {
                if (!(Process.GetProcessesByName("rundll32").Length > 0))
                {
                    myThreadIE.Abort(); //Si rundll32 (Nettoyage de IE) n'est plus en cours d'exécution alors on stoppe le Thread
                }
            }
        }

        /// <summary>
        /// Méthode de nettoyage du dossier Téléchargement
        /// </summary>
        /// <param name="nbJours">Nombre de jorus à partir duquel les fichiers seront supprimés</param>
        public void ThreadDownload(int nbJours)
        {
            Lb_StatutEnCours.Text = "Analyse des téléchargements"; //Mise à jour du statut sur la form Cleaner
            float tailleMo = 0;
            double tailleGo = 0;
            int i = 0, j = 0;
            DownloadDone = false;
            myThreadDownload = new Thread(LoopDownload);
            myThreadDownload.Start(); //Lancement du Thread Nettoyage du dossier Téléchargement
            if (Ressources.VersionOS() != "XP")
            {
                //Si l'OS n'est pas XP, alors le dossier de téléchargement est le suivant
                var folderPath = "%USERPROFILE%\\Downloads";
                var folder = Environment.ExpandEnvironmentVariables(folderPath); //On stocke le chemin du dossier dans une méthode capable de gérer les variables d'environnement
                if (Directory.Exists(folder)) //On vérifie que le dossier existe
                {
                    tailleMo = Ressources.FolderSize(folder, "Mo"); //On récupère la taille en Mo
                    tailleGo = Math.Round((tailleMo / 1024), 2); // Puis en Go
                    if (tailleMo >= 100)
                    {
                        //Si la taille du dossier est supérieure ou égale à 100Mo alors on propose le nettoyage
                        DialogResult result = MessageBox.Show("Votre dossier de téléchargement semble contenir des fichiers\nTrès souvent ces fichiers sont inutilisés\nEn les supprimant vous pourriez libérer " + tailleMo + " Mo, soit " + tailleGo + " Go\nSouhaitez-vous que le programme supprime automatiquement les fichiers inutiles datant de plus de " + nbJours + " jours ?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (result == DialogResult.Yes)
                        {
                            Lb_StatutEnCours.Text = "Nettoyage des téléchargements";
                            DirectoryInfo dinfo = new DirectoryInfo(folder); //On récupère l'emplacement du dossier de téléchargement
                            DirectoryInfo[] Directories = dinfo.GetDirectories();
                            FileInfo[] Files = dinfo.GetFiles();
                            foreach (FileInfo file in Files)
                            {
                                TimeSpan ts = DateTime.Now - file.LastAccessTime;
                                int totalJours = Convert.ToInt32(ts.TotalDays);
                                if (totalJours >= nbJours)
                                {
                                    //Si le fichier actuel n'a pas été ouvert depuis le nombre de jours choisi
                                    i++;
                                    file.Delete(); //Alors on le supprime
                                }
                            }
                            foreach (DirectoryInfo d in Directories)
                            {
                                TimeSpan ts = DateTime.Now - d.LastAccessTime;
                                int totalJours = Convert.ToInt32(ts.TotalDays);
                                var ssDossier = Environment.ExpandEnvironmentVariables(folderPath + "\\" + d.ToString());
                                if (totalJours >= nbJours && Ressources.FolderSize(ssDossier, "Mo") >= 100)
                                {
                                    //Si le dossier actuel n'a pas été ouvert depuis le nombre de jours choisi et que sa taille est supérieur à 100Mo
                                    j++;
                                    d.Delete(true); //Alors on le supprime
                                }
                            }
                            if (i == 0 && j == 0)
                            {
                                //Si rien n'a été supprimé
                                MessageBox.Show("Aucun dossier ou fichier de plus de " + nbJours + " jours n'a été trouvé", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                //Sinon on affiche le résultat
                                MessageBox.Show(i + " fichier(s) supprimé(s).\n" + j + " dossier(s) supprimé(s)", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                    }
                    if (tailleMo <= 100)
                    {
                        //Si la taille du dossier est inférieure à 100Mo et que seule la case de netoyyage du dossier de téléchargement a été coché, alors on affiche un message
                        MessageBox.Show("Votre dossier de téléchargement ne semble pas prendre beaucoup de place, le nettoyage est donc inutile", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                    DownloadDone = true;
                }
            }
            else
            {
                if (Ressources.VersionOS() == "XP")
                {
                    //Si l'OS est XP, alors le dossier de téléchargement est situé a l'emplacement suivant
                    //Le traitement reste le même, il n'y a que l'emplacement du dossier qui change
                    var folderPath = @"%USERPROFILE%\Mes documents\Téléchargements";
                    var folder = Environment.ExpandEnvironmentVariables(folderPath);
                    if (Directory.Exists(folder))
                    {
                        tailleMo = Ressources.FolderSize(folder, "Mo");
                        tailleGo = Math.Round((tailleMo / 1024), 2);
                        if (tailleMo >= 100)
                        {
                            DialogResult result = MessageBox.Show("Votre dossier de téléchargement semble contenir des fichiers\nTrès souvent ces fichiers sont inutilisés\nEn les supprimant vous pourriez libérer " + tailleMo + " Mo, soit " + tailleGo + " Go\nSouhaitez-vous que le programme supprime automatiquement les fichiers inutiles datant de plus de " + nbJours + " jours ?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (result == DialogResult.Yes)
                            {
                                Lb_StatutEnCours.Text = "Nettoyage des téléchargements";
                                DirectoryInfo dinfo = new DirectoryInfo(folder); //On récupère l'emplacement du dossier de téléchargement
                                DirectoryInfo[] Directories = dinfo.GetDirectories();
                                FileInfo[] Files = dinfo.GetFiles();
                                foreach (FileInfo file in Files)
                                {
                                    TimeSpan ts = DateTime.Now - file.LastAccessTime;
                                    int totalJours = Convert.ToInt32(ts.TotalDays);
                                    if (totalJours >= nbJours)
                                    {

                                        //Si le fichier actuel n'a pas été ouvert depuis le nombre de jours choisi
                                        i++;
                                        file.Delete(); //Alors on le supprime
                                    }
                                }
                                foreach (DirectoryInfo d in Directories)
                                {
                                    TimeSpan ts = DateTime.Now - d.LastAccessTime;
                                    int totalJours = Convert.ToInt32(ts.TotalDays);
                                    if (totalJours >= nbJours)
                                    {

                                        //Si le dossier actuel n'a pas été ouvert depuis le nombre de jours choisi et que sa taille est supérieur à 100Mo
                                        j++;
                                        d.Delete(true); //Alors on le supprime
                                    }
                                }
                                if (i == 0 && j == 0)
                                {
                                    MessageBox.Show("Aucun dossier ou fichier de plus de " + nbJours + " jours n'a été trouvé", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                else
                                {
                                    MessageBox.Show(i + " fichier(s) supprimé(s).\n" + j + " dossier(s) supprimé(s)", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                            }
                        }
                        if (tailleMo <= 100)
                        {
                            MessageBox.Show("Votre dossier de téléchargement ne semble pas prendre beaucoup de place, le nettoyage est donc inutile", "CleanerXpress - Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        DownloadDone = true;
                    }
                    else
                    {
                        //Si l'OS n'est pas XP
                        folderPath = @"%USERPROFILE%\Mes documents\Downloads";
                        folder = Environment.ExpandEnvironmentVariables(folderPath);
                        if (Directory.Exists(folder))
                        {
                            tailleMo = Ressources.FolderSize(folder, "Mo");
                            tailleGo = Math.Round((tailleMo / 1024), 2);
                            if (tailleMo >= 100)
                            {
                                DialogResult result = MessageBox.Show("Votre dossier de téléchargement semble contenir des fichiers\nTrès souvent ces fichiers sont inutilisés\nEn les supprimant vous pourriez libérer " + tailleMo + " Mo, soit " + tailleGo + " Go\nSouhaitez-vous que le programme supprime automatiquement les fichiers inutiles datant de plus de " + nbJours + " jours ?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                if (result == DialogResult.Yes)
                                {
                                    Lb_StatutEnCours.Text = "Nettoyage des téléchargements";
                                    DirectoryInfo dinfo = new DirectoryInfo(folder); //On récupère l'emplacement du dossier de téléchargement
                                    DirectoryInfo[] Directories = dinfo.GetDirectories();
                                    FileInfo[] Files = dinfo.GetFiles();
                                    foreach (FileInfo file in Files)
                                    {
                                        TimeSpan ts = DateTime.Now - file.LastAccessTime;
                                        int totalJours = Convert.ToInt32(ts.TotalDays);
                                        if (totalJours <= nbJours)
                                        {
                                            //Si le fichier actuel n'a pas été ouvert depuis le nombre de jours choisi
                                            i++;
                                            file.Delete(); //Alors on le supprime
                                        }
                                    }
                                    foreach (DirectoryInfo d in Directories)
                                    {
                                        TimeSpan ts = DateTime.Now - d.LastAccessTime;
                                        int totalJours = Convert.ToInt32(ts.TotalDays);
                                        if (totalJours <= nbJours)
                                        {
                                            //Si le dossier actuel n'a pas été ouvert depuis le nombre de jours choisi et que sa taille est supérieur à 100Mo
                                            j++;
                                            d.Delete(true); //Alors on le supprime
                                        }
                                    }
                                    if (i == 0 && j == 0)
                                    {
                                        //Si rien n'a été supprimé
                                        MessageBox.Show("Aucun dossier ou fichier de plus de " + nbJours + " n'a été trouvé", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }
                                    else
                                    {
                                        //Sinon on affiche le résultat
                                        MessageBox.Show(i + " fichier(s) supprimé(s).\n" + j + " dossier(s) supprimé(s)", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }
                                }
                            }
                            if (tailleMo <= 100)
                            {
                                MessageBox.Show("Votre dossier de téléchargement ne semble pas prendre beaucoup de place, le nettoyage est donc inutile", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            DownloadDone = true;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Méthode qui sera exécutée en boucle par le Thread Nettoyage du dossier de téléchargement
        /// </summary>
        public void LoopDownload()
        {
            Thread.Sleep(5000); //On attends un peu pour ne pas saturer la machine
            while (myThreadDownload.IsAlive)
            {
                if (DownloadDone == true)
                {
                    myThreadDownload.Abort(); //Si le nettoyage du dossier est terminé alors on arrête le Thread
                }
            }
        }

        /// <summary>
        /// Thread Nettoyage de Windows (Cleanmgr)
        /// </summary>
        public void ThreadNettoyage()
        {
            if (myThreadCleanmgr == null) //Si le Thread n'est pas en cours
            {
                Registre.ParcoursEcritureRegistre(@"SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\VolumeCaches\\", "StateFlags0666", 2, RegistryValueKind.DWord, rV); //On flag toutes les options dans le registre afin d'exécuter un nettoyage complet
                Ressources.Exec("cleanmgr.exe", "/sagerun:0666"); //Lancement du nettoyage
                Lb_StatutEnCours.GetCurrentParent().Invoke((MethodInvoker)delegate { Lb_StatutEnCours.Text = "Nettoyage de Windows"; }); //Mise à jour du statut sur la form Cleaner
                CleanmgrDone = false;
                myThreadCleanmgr = new Thread(LoopNettoyage);
                myThreadCleanmgr.Start(); //On lance le Thread nettoyage
            }
        }
        
        /// <summary>
        /// Méthode qui sera exécutée en boucle par le Thread Nettoyage
        /// </summary>
        public void LoopNettoyage()
        {
            Thread.Sleep(5000); //On attends un peu pour le pas saturer la machine
            while (Thread.CurrentThread.IsAlive)
            {
                if (!(Process.GetProcessesByName("cleanmgr").Length > 0)) //Si le nettoyage est terminé
                {
                    Registre.SupprimerValeurRegistre(@"SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\VolumeCaches\\", "StateFlags0666", rV); //On supprime le flag dans la base de registre
                    CleanmgrDone = true;
                    myThreadCleanmgr.Abort(); //On stoppe le Thread Nettoyage de Windows
                    myThreadCleanmgr = null;
                }
            }
        }

        /// <summary>
        /// Thread CCleaner
        /// </summary>
        public void ThreadCCleaner()
        {
            try
            {
                if (myThreadCCleaner == null)
                {
                    string path = Registre.LectureValeur("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Uninstall\\CCleaner", "InstallLocation", rV); //On récupère le chemin d'installation de CCleaner dans le registre
                    if (File.Exists(path + "\\CCleaner.exe")) //Si CCleaner est bien installé
                    {
                        Ressources.Exec("CCleaner.exe", " /auto"); //On lance CCleaner
                        Lb_StatutEnCours.Text = "Exécution de CCleaner"; //Mise à jour du statut sur la form Cleaner
                        CCleanerDone = false;
                        myThreadCCleaner = new Thread(LoopCCleaner);
                        myThreadCCleaner.Start(); //On lance le thread CCleaner
                    }
                }
            }
            catch (Exception e)
            {
                //S'il y a eu un problème lors de la recherche ou le lancement de CCleaner alors on affiche un message
                MessageBox.Show(e.Message);
            }
        }

        /// <summary>
        /// Méthode qui sera exécutée en boucle par le Thread CCleaner
        /// </summary>
        public void LoopCCleaner()
        {
            Thread.Sleep(5000); //On atteds un peu pour ne pas saturer la machine
            while (Thread.CurrentThread.IsAlive) //Si le Thread CCleaner est bien lancé
            {
                if (Environment.Is64BitOperatingSystem) //On vérifie l'architecture de l'OS (32 ou 64 bits)
                {
                    if (!(Process.GetProcessesByName("CCleaner64").Length > 0))
                    {
                        //Si CCleaner64 n'est pas ou plus en exécution on arrête le Thread
                        CCleanerDone = true;
                        myThreadCCleaner.Abort();
                        myThreadCCleaner = null;
                    }
                }
                else
                {
                    if (!(Process.GetProcessesByName("CCleaner").Length > 0))
                    {
                        //Sinon, si CCleaner n'est pas ou plus en exécution alors on arrête le thread
                        CCleanerDone = true;
                        myThreadCCleaner.Abort();
                        myThreadCCleaner = null;
                    }
                }
            }
        }

        #endregion

        #region Traitement optimisation
        /// <summary>
        /// Méthode qui permet de lancer les opérations sélectionnées
        /// </summary>
        public void Optimisation ()
        {
            DialogResult DR = MessageBox.Show("Êtes-vous sûr de vouloir lancer le traitement ?", "Question", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            if (DR == DialogResult.Yes)
            {
                int i = 0;
                //Pour chaque checkBox cochées, on lance le traimenet correspondant
                if (cB_Aero.Checked) global::Cleaner.Optimisation.Aero();
                if (cB_Aero.Checked) global::Cleaner.Optimisation.DechargDll(rV);
                if (cB_Superfetch.Checked) global::Cleaner.Optimisation.Superfetch();
                if (cB_Prefetch.Checked) global::Cleaner.Optimisation.Prefetch(rV);
                if (cB_WindowsSearch.Checked) global::Cleaner.Optimisation.WindowsSearch();
                if (cB_WinSat.Checked) global::Cleaner.Optimisation.WinSAT();
                if (cB_FichierImpEnAttente.Checked) global::Cleaner.Optimisation.DocumentsEnAttenteImp();
                if (cB_Noyau.Checked) global::Cleaner.Optimisation.GarderNoyauRam(rV);
                if (cB_DefragBoot.Checked) global::Cleaner.Optimisation.DefragBoot(rV);
                if (cB_VerifFichiers.Checked) global::Cleaner.Optimisation.VerifFichiers();
                if (cB_USB.Checked) global::Cleaner.Optimisation.USB(rV);
                if (cB_Diskperf.Checked) global::Cleaner.Optimisation.DiskPerf();
                if (cB_Reindexation.Checked) global::Cleaner.Optimisation.Reindexation();
                if (cB_CoreBoot.Checked) global::Cleaner.Optimisation.NbCoreDemarrage(rV);
                if (cB_DCOM.Checked) global::Cleaner.Optimisation.DCOM(rV);
                if (cB_OptiAccesMemoireDisque.Checked) global::Cleaner.Optimisation.optimiserAccesMemoireDisque();

                foreach (Control c in tab_Optimisation.Controls)
                {
                    if (c is CheckBox)
                    {
                        CheckBox chk = (CheckBox)c;
                        if (chk.Checked)
                        {
                            i++; //On compte le nombre de checkBox dans la section Optimisation
                        }
                    }
                }
                Lb_StatutEnCours.Text = "Arrêté";
                pB.Style = ProgressBarStyle.Blocks;
                pB.MarqueeAnimationSpeed = 0;
                pB.Visible = false;
                EnableDisableUI('E'); //On remet l'interface dans son état d'origine
                if (i > 0 && !cB_VerifFichiers.Checked)
                {
                    if (!(i == 1 && cB_Aero.Checked))
                    {
                        //Si plusieurs checkBox sont cochées (on n'affiche pas de message si seul Aero est cochée)
                        MessageBox.Show("Les paramètres de l'ordinateur ont étés mis à jour\nUn redémarrage est nécessaire pour les appliquer", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }
        #endregion

        #region Désactivation interface utilisateur

        /// <summary>
        /// Méthode qui permet d'activer ou de désactiver l'interface utilisateur
        /// </summary>
        /// <param name="c">Permet de savoir si on doit activer ou désactiver l'interface</param>
        public void EnableDisableUI(char c)
        {
            if (c == 'D')
            {
                //En cas de désactivation on désactive les controles sur les deux onglets et on désactive le bouton Journal
                this.tab_Nettoyage.Invoke((MethodInvoker)delegate { this.tab_Nettoyage.Enabled = false; });
                this.tab_Optimisation.Invoke((MethodInvoker)delegate { this.tab_Optimisation.Enabled = false; });
                this.btn_Journal.Invoke((MethodInvoker)delegate { this.btn_Journal.Enabled = false; });
            }
            else if (c == 'E')
            {
                //En cas de d'activation on active les controles sur les deux onglets et on active le bouton Journal
                this.tab_Nettoyage.Invoke((MethodInvoker)delegate { this.tab_Nettoyage.Enabled = true; });
                this.tab_Optimisation.Invoke((MethodInvoker)delegate { this.tab_Optimisation.Enabled = true; });
                this.btn_Journal.Invoke((MethodInvoker)delegate { this.btn_Journal.Enabled = true; });
            }
        }

        #endregion
    }
}

