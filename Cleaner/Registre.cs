using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cleaner
{
    class Registre
    {
        public static void SaveRegistre()
        {
            Ressources.Exec("regedit.exe", " /E C:\\SBiiXpress\\Save\\Registry\\Save_" + DateTime.Now.ToString("MM_dd_yyyy") + ".reg");
            DirectoryInfo dir = new DirectoryInfo(@"C:\SBiiXpress\Save\Registry");
            if (dir.Exists) //On vérifie si le dossier existe avant de tenter de supprimer les fichiers
            {
                FileInfo[] fichiers = dir.GetFiles(); //On récupère tous les fichiers situés dans le dossier
                if (fichiers.Count() > 1) //Si il y a plus de deux fichiers dans le dossier, on supprime le plus vieux
                {
                    fichiers[0].Delete(); //Suppression
                }
            }
            else
            {
                DirectoryInfo di = Directory.CreateDirectory("C:\\SBiiXpress\\Save\\Registry");
            }
        }

        public static string LectureValeur(string pathLecture, string valName)
        {
            try
            {
                RegistryKey key = Registry.LocalMachine.OpenSubKey(pathLecture);
                if (key != null)
                {
                    Object o = key.GetValue(valName);
                    if (o != null)
                    {
                        return Convert.ToString(o);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors de l'accès à la base de registre\n" + ex, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return "err";
        }
    }
}
