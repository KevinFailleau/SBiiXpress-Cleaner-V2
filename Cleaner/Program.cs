using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cleaner
{
    static class Program
    {
        /// <summary>
        /// Point d'entrée principal de l'application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            if (Ressources.VersionOS() == 10.ToString() || Ressources.VersionOS() == 8.ToString() || Ressources.VersionOS() == 8.1.ToString() || Ressources.VersionOS() == 7.ToString() || Ressources.VersionOS() == "Vista")
            {
                Application.Run(new Form7_10(Ressources.VersionOS(), Ressources.ArchiOS()));
            }
            else if (Ressources.VersionOS() == "XP")
            {
                Application.Run(new FormXP(Ressources.ArchiOS()));
            }
        }
    }
}
