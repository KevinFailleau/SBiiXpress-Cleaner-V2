using System;
using System.IO;
using System.Windows.Forms;

namespace Cleaner
{
    public partial class Journal : Form
    {
        #region Initialisation

        public Journal()
        {
            InitializeComponent();
        }

        #endregion

        #region Evénements

        /// <summary>
        /// Evénement qui s'active lorsque l'utilisateur clique sur le bouton "Quitter"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Fermer_Click(object sender, EventArgs e)
        {
            this.Close(); //On ferme la form
        }

        /// <summary>
        /// Evénement qui est lancé lorsque la form Journal apparaît 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Journal_Load(object sender, EventArgs e)
        {
            if (!Directory.Exists("C:\\SBiiXpress\\Logs"))
            {
                //Si le fichier de log n'existe pas alors on affiche un message
                MessageBox.Show("Il s'agit de votre première utilisation de l'application, les informations seront mises à jour lorsque le processus d'optimisation/nettoyage aura été complété", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                lb_NbEsp.Text = "0 Mo"; //La taille affichée sur la form est définie à zéro
                lb_Date.Text = DateTime.Now.ToShortDateString(); //Et la date a celle du jour
            }
            else
            {
                if (Ressources.LectureEspace_Log() != 0)
                {
                    //Si la taille totale trouvée avec le log est différent de zéro
                    if (Ressources.LectureEspace_Log().ToString().Length >= 4)
                    {
                        double taille = Ressources.LectureEspace_Log() / 1024;
                        taille = Math.Round(taille, 2);
                        lb_NbEsp.Text = taille.ToString() + " Go"; //Alors on affiche cette taille sur la form
                    }
                    else
                    {
                        lb_NbEsp.Text = Ressources.LectureEspace_Log().ToString() + " Mo"; //Alors on affiche cette taille sur la form
                    }
                    lb_Date.Text = Ressources.LectureDate_Log(); //Puis on affiche la date de première utilisation inscrite dans le log
                }
                else
                {
                    //Sinon on mets des valeurs par défaut et on désactive le bouton
                    lb_NbEsp.Text = "0 Mo";
                    lb_Date.Text = DateTime.Now.ToShortDateString();
                }
            }
        }

        #endregion
    }
}
