using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.IO;

namespace Cleaner
{
    class Optimisation
    {
        #region Aero
        /// <summary>
        /// Méthode qui permet d'activer ou de désactiver Aero
        /// </summary>
        public static void Aero()
        {
            if (Ressources.VerifProcessus("uxsms") == "Running")
            {
                Ressources.Exec("cmd", " /c net stop uxsms"); //Si Aero est lancé alors on l'arrête
            }
            else
                if (Ressources.VerifProcessus("uxsms") == "Stopped")
            {
                Ressources.Exec("cmd", " /c net start uxsms"); //Si Aero est arrêté alors on le lance
            }
        }
        #endregion

        #region Décharger les DLL inutiles
        public static void DechargDll(RegistryView rV)
        {
            //On ajoute une valeur pour décharger automatiquement les DLL inutiles
            Registre.EcrireRegistre("Software\\Microsoft\\Windows\\CurrentVersion\\Explorer", "AlwaysUnloadDll", 1, RegistryValueKind.DWord, rV);
        }
        #endregion

        #region Superfetch
        /// <summary>
        /// Méthode qui permet de désactiver Superfetch
        /// </summary>
        public static void Superfetch()
        {
            if (Ressources.VerifProcessus("superfetch") == "Running") //Vérification de l'état de Superfetch
            {
                Ressources.Exec("cmd", "/c net stop superfetch"); //On arrête Superfetch
                Ressources.Exec("cmd", " /c sc config sysmain start= disabled"); //On empêche le lancement de Superfetch
            }
        }
        #endregion

        #region Prefetch
        /// <summary>
        /// Méthode qui permet de désactiver Prefetch
        /// </summary>
        /// <param name="rV">Type de registre en fonction du système (32 ou 64 bits)</param>
        public static void Prefetch(RegistryView rV)
        {
            //On désactive Prefetch depuis le registre Windows
            Registre.EcrireRegistre("SYSTEM\\CurrentControlSet\\Control\\Session Manager\\Memory Management\\PrefetchParameters", "EnablePrefetcher", 0, RegistryValueKind.DWord, rV);
        }
        #endregion

        #region Windows Search
        /// <summary>
        /// Méthode qui permet de désactiver Windows Search
        /// </summary>
        public static void WindowsSearch() //Pour tous les OS sauf XP
        {
            Ressources.Exec("cmd", "/c net stop WSearch");
            Ressources.Exec("cmd", "/c sc config WSearch start=disabled");
        }
        #endregion

        #region Désactivation de Windows Update
        /// <summary>
        /// Méthode qui permet de désactiver Windows Update
        /// </summary>
        public static void DesactiverWUpdate()
        {
            Ressources.Exec("cmd", "/c net stop wuauserv"); //On arrête le service Windows Update
            Ressources.Exec("cmd", "/c sc config wuauserv start= disabled"); //On empêche le service de se relancer
        }
        #endregion

        #region WinSAT
        public static void WinSAT()
        {
            Ressources.Exec("cmd", "/c schtasks /TN \"Microsoft\\Windows\\Maintenance\\WinSAT\" /DELETE /F");
        }
        #endregion

        #region Suppression des documents en attente d'impression
        /// <summary>
        /// Méthode qui permet de supprimer les fichiers en attente d'impression
        /// </summary>
        public static void DocumentsEnAttenteImp()
        {
            int i = 0;
            var folderPath = @"%WINDIR%\system32\spool\PRINTERS";
            var folder = Environment.ExpandEnvironmentVariables(folderPath);
            DirectoryInfo dinfo = new DirectoryInfo(folder); //On récupère l'emplacement du dossier où se situe les fichiers en attente
            DirectoryInfo[] Directories = dinfo.GetDirectories();
            FileInfo[] Files = dinfo.GetFiles();
            foreach (FileInfo file in Files)
            {
                i++;
                file.Delete(); //On supprime chaque fichiers
            }
        }
        #endregion

        #region Garder le noyau Windows en RAM
        /// <summary>
        /// Méthode qui permet de garder le noyau Windows en mémoire
        /// </summary>
        /// <param name="rV">Type de registre (32 ou 64 bits)</param>
        public static void GarderNoyauRam(RegistryView rV)
        {
            //On ajoute la valeur au registre qui permet de conserver le noyau Windows en mémoire
            Registre.EcrireRegistre("SYSTEM\\CurrentControlSet\\Control\\Session Manager\\Memory Management", "DisablePagingExecutive", 1, RegistryValueKind.DWord, rV);
        }
        #endregion

        #region Défragmentation des fichiers démarrage
        /// <summary>
        /// Méthode qui permet de désactiver la défragmentation des fichiers de démarrage
        /// </summary>
        /// <param name="rV">Type de registre (32 ou 64 bits)</param>
        public static void DefragBoot(RegistryView rV)
        {
            //On ajoute les valeurs correspondantes dans le registre (à vérifier)
            Registre.EcrireRegistre("SOFTWARE\\Microsoft\\Dfrg\\BootOptimizeFunction", "Enable", "N", RegistryValueKind.String, rV); //Valeur à mettre à Y ou N selon les sources
            Registre.EcrireRegistre("SOFTWARE\\Microsoft\\Dfrg\\BootOptimizeFunction", "OptimizeComplete", "No", RegistryValueKind.String, rV);
        }
        #endregion

        #region Vérification de l'intégrité des fichiers système (SFC Scannow)
        /// <summary>
        /// Méthode qui permet de lancer une vérification de l'intégrité des fichiers
        /// </summary>
        public static void VerifFichiers()
        {
            ProcessStartInfo startInfo = new ProcessStartInfo("cmd", "/K sfc.exe /scannow"); //On crée un process pour la commande
            startInfo.UseShellExecute = true;
            startInfo.Verb = "runas"; //Cette commande sera lancée en tant qu'administrateur
            Process.Start(startInfo); //Lancement de SFC
        }
        #endregion

        #region Augmentation de la vitesse de tranfert des ports USB
        /// <summary>
        /// Méthode qui permet d'optimiser les tranferts USB
        /// </summary>
        /// <param name="rV">Type de registre (32 ou 64 bits)</param>
        public static void USB(RegistryView rV)
        {
            try
            {
                //On place une valeur dans chaque clé de USBStor
                Registre.ParcoursEcritureRegistre("SYSTEM\\CurrentControlSet\\Control\\usbstor\\", "MaximumTransferLength", 2097120, RegistryValueKind.DWord, rV);
            }
            catch (Exception e){}
        }
        #endregion

        #region Optimiser le boot en fonction du processeur (inutile)
        /// <summary>
        /// Méthode qui devait à la base permettre d'optimiser le démarrage de Windows en utilisant tous les processeurs disponibles
        /// </summary>
        /// <param name="rV">Type de registre (32 ou 64 bits)</param>
        public static void NbCoreDemarrage(RegistryView rV)
        {
            //La ligne suivante ne permet que de récupérer le nombre de processeurs disponibles, mais ne fait aucun traitement (impossible de trouver où faire cette modification)
            string nbCore = Registre.LectureValeur("SYSTEM\\CurrentControlSet\\Control\\Session Manager\\Environment", "NUMBER_OF_PROCESSORS", rV);
        }
        #endregion

        #region Désactivation du compteur de performance des disques
        /// <summary>
        /// Méthode qui permet de désactiver le compteur de performance des disques
        /// </summary>
        public static void DiskPerf()
        {
        	Ressources.Exec("cmd.exe", "/c diskperf -n"); //Lancement de la commande "diskperf -n"
        }
        #endregion

        #region Reindexation
        /// <summary>
        /// Méthode qui permet de réindexer les fichiers (Windows Search)
        /// </summary>
        public static void Reindexation()
        {
            Ressources.Exec("cmd", "/c net stop wsearch"); //Désactivation de Windows Search
            Ressources.Exec("cmd", "/c del \"%programdata%\\Microsoft\\Search\\Data\\Applications\\Windows\\Windows.edb.bak\""); //Suppression du fichier de sauvegarde
            Ressources.Exec("cmd", "/c move \"%programdata%\\Microsoft\\Search\\Data\\Applications\\Windows\\Windows.edb\" \"%progamdata%\\Microsoft\\Search\\Data\\Applications\\Windows\\Windows.edb.bak "); //Création d'un nouveau fichier de sauvegarde
            Ressources.Exec("cmd", "/c net start wsearch"); //Lancement de Windows Search
        }
        #endregion

        #region Désactivation de DCOM
        /// <summary>
        /// Méthode qui permet de désactiver DCOM
        /// </summary>
        /// <param name="rV">Type de registre (32 ou 64 bits)</param>
        public static void DCOM(RegistryView rV)
        {
            Registre.EcrireRegistre("Software\\Microsoft\\OLE", "EnableDCOM", "N", RegistryValueKind.String, rV); //Désactivation de DCOM depuis la base de registre
        }
        #endregion

        #region Optimiser l'accès aux disques et à la mémoire
        /// <summary>
        /// Méthode qui permet d'optimiser l'accès à la mémoire et aux disques
        /// </summary>
        public static void optimiserAccesMemoireDisque()
        {
            Ressources.Exec("cmd", "/c fsutil behavior set memoryusage 2"); //Exécution de la commande "FSUtil"
        }
        #endregion
    }
}
