using System;
using System.Windows.Forms;

namespace Cleaner
{
    public partial class Options : Form
    {
        #region Variables
        public static int nbJours = 10;
        #endregion

        #region Accesseurs
        /// <summary>
        /// Méthode qui permet de récupérer la valeur de nbJours
        /// </summary>
        /// <returns>Valeur de nbJours</returns>
        public static int NbJours()
        {
            return nbJours;
        }
        #endregion

        #region Initialisation de la form
        /// <summary>
        /// Méthode qui s'exécute au l'appel de la form
        /// </summary>
        public Options()
        {
            InitializeComponent();
        }
        #endregion

        #region Evénements
        /// <summary>
        /// Méthode qui s'exécute lorsque l'utilisateur clique sur le bouton "Valider"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Valider_Click(object sender, EventArgs e)
        {
            nbJours = Convert.ToInt32(nUD_Jours.Value); //On conserve la valeur choisie par l'utilisateur dans une variable qui sera réutilisée pour le nettoyage du dossier de téléchargement
            this.Close();
        }

        /// <summary>
        /// Méthode qui s'exécute lorsque l'utilisateur clique sur le bouton "Annuler"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Annuler_Click(object sender, EventArgs e)
        {
            this.Close(); //On ferme la form
        }
        #endregion
    }
}
