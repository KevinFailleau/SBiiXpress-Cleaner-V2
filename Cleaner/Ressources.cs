using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Xml;
using System.Windows.Forms;
using System.Management;
using Microsoft.Win32;
using System.ServiceProcess;
using System.IO;
using System.Text.RegularExpressions;
using MySql.Data.MySqlClient;
using System.Runtime.InteropServices;
using System.Drawing.Printing;
using System.Net.NetworkInformation;

namespace Cleaner
{
    public class Ressources
    {
        #region Exécution de programme

        /// <summary>
        /// Méthode qui permet de lancer une application ou une commande avec des paramètres
        /// </summary>
        /// <param name="path">Nom de la commande ou de l'application à lancer</param>
        /// <param name="args">Arguments à ajouter</param>
        public static void Exec(string path, string args)
        {
            Process p = new Process();
            p.StartInfo.FileName = path; //Création d'un process avec le nom de la commande ou de l'application à exécuter
            if (VersionOS() != "XP")
            {
                p.StartInfo.Verb = "runas"; //Sous XP il est inutile de demander les droits d'administrateur, sous les autres OS on les demande
            }
            p.StartInfo.Arguments = args; //Ajout des paramètres
            p.StartInfo.CreateNoWindow = true; //Le lancement de la commande n'affichera pas de fenêtre MSDOS
            p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden; //Si une fenêtre venait à s'afficher on tenterait de la masquer
            p.Start(); //Exécution
        }

        /// <summary>
        /// Méthode qui permet de lancer une application sans paramètres
        /// </summary>
        /// <param name="path">Nom de la commande ou de l'application à lancer</param>
        public static void Exec(string path)
        {
            Process p = new Process();
            p.StartInfo.FileName = path; //Création d'un process avec le nom de la commande ou de l'application à exécuter
            if (VersionOS() != "XP")
            {
                p.StartInfo.Verb = "runas"; //Sous XP il est inutile de demander les droits d'administrateur, sous les autres OS on les demande
            }
            p.StartInfo.CreateNoWindow = true; //Le lancement de la commande n'affichera pas de fenêtre MSDOS
            p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden; //Si une fenêtre venait à s'afficher on tenterait de la masquer
            p.Start(); //Exécution
        }
        #endregion

        #region Obtenir la version de Windows

        /// <summary>
        /// Cette méthode permet de connaître le nom de la version de Windows qui est installé
        /// </summary>
        /// <returns>Retourne le nom de la version de Windows en cours d'exécution</returns>
        public static string VersionOS()
        {
            int cpt = 0;
            string ver = "Inconnu";
            var name = (from x in new ManagementObjectSearcher("SELECT Caption FROM Win32_OperatingSystem").Get().Cast<ManagementObject>() select x.GetPropertyValue("Caption")).FirstOrDefault(); //Permet de récupérer une chaîne contenant le nom de la version de Windows
            Regex regex = new Regex(@"\s");
            string[] nomComplet = regex.Split(name.ToString());
            foreach (string s in nomComplet) //Cette boucle permet de ne conserver à la fin que le nom de la version de Windows et non pas "Microsoft Windows 10 Professionnel" par exemple, on aurait que "10" dans ce cas 
            {
                cpt++; //On compte le nombre de mot
                if (cpt == 3)
                {
                    ver = s; //On ne récupère que le troisième mot et on le stocke dans "ver"
                }
            }
            if(ver.Contains("\u2122")) //Pour Windows Vista, le nom du système d'exploitation est afficher avec le sigle TradeMark donc on le supprime ici
            {
                string v = ver.Replace("\u2122", "");
                return v;
            }
            else return ver;
        }
        #endregion

        #region Connaître l'architecture de Windows

        /// <summary>
        /// Méthode qui permet de savoir si la version de Windows actuelle est 32 ou 64 bits
        /// </summary>
        /// <returns>Retourne 32 ou 64</returns>
        public static RegistryView ArchiOS()
        {
            RegistryView rV;
            if (Environment.Is64BitOperatingSystem)
            {
                rV = RegistryView.Registry64; //Si Windows est 64 bits, on stocke le type de registre dans "rV"
            }
            else
            {
                rV = RegistryView.Registry32; //Si Windows est 64 bits, on stocke le type de registre dans "rV"
            }

            return rV;
        }
        #endregion

        #region Connaître l'état des services Windows

        /// <summary>
        /// Cette méthode permet de vérifier l'état d'un service Windows
        /// </summary>
        /// <param name="s">Nom du service à vérifier</param>
        /// <returns>Statut du service</returns>
        public static string VerifProcessus(string s)
        {
            try
            {
                ServiceController sc = new ServiceController(s); //Service à vérifier

                switch (sc.Status)
                {
                    //En fonction de l'état du service demandé, on retourne une chaîne de caractère correspondante
                    case ServiceControllerStatus.Running:
                        return "Running";
                    case ServiceControllerStatus.Stopped:
                        return "Stopped";
                    case ServiceControllerStatus.Paused:
                        return "Paused";
                    case ServiceControllerStatus.StopPending:
                        return "Stopping";
                    case ServiceControllerStatus.StartPending:
                        return "Starting";
                    default:
                        return "Status Changing";
                }
            }
            catch (Exception e)
            {
                //En cas d'erreur on affiche un message et l'erreur en question
                MessageBox.Show("L'application à rencontré une erreur \n" + e.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "Error";
            }
        }
        #endregion

        #region Calculs

        /// <summary>
        /// Cette méthode permet de calculer la taille d'un dossier
        /// </summary>
        /// <param name="path">Chemin du dossier à calculer</param>
        /// <param name="multiple">Permet de retourner le résultat en Octets, Mo, Go, ...</param>
        /// <returns>Retourne la taille du dossier</returns>
        public static long FolderSize(string path, string multiple)
        {
            long size = 0;
            DirectoryInfo directoryInfo = new DirectoryInfo(path); //Récupèration de l'emplacement du dossier
            IEnumerable<FileInfo> files = directoryInfo.GetFiles("*", SearchOption.AllDirectories); //On séléctionne tous les fichiers du dossier
            foreach (FileInfo file in files)
            {
                //Pour chaque fichiers du dossier
                size += file.Length; //On ajoute la taille du fichiers actuel à size
            }
            if (multiple == "Ko" || multiple == "ko")
            {
                size = size / 1024; //Si le multiple souhaité est Ko, alors on convertit size en Ko
            }
            if (multiple == "Mo" || multiple == "mo")
            {
                size = (size / 1024) / 1024; //Sinon on convertit size en Mo
            }
            else
                if (multiple == "Go" || multiple == "go")
            {
                size = ((size / 1024) / 1024) / 1024; //Sinon on convertit size en Go
            }
            return size;
        }
        public static double EspaceLibre() //Méthode à revoir, les résultats semblent erronnés
        {
            double Espace = 0;

            foreach (DriveInfo CurrentDrive in DriveInfo.GetDrives()) //On parcours la liste des disques dur disponibles sur la machine
            {
                if (CurrentDrive.DriveType == DriveType.Fixed && CurrentDrive.Name == "C:\\")//Si le disque dur sélectionné par le foreach est de type fixe et sa lettre de lecteur est C:, alors on récupère la taille totale et l'espace libre
                {
                    Espace = CurrentDrive.AvailableFreeSpace; //Permet de determiner l'espace disponible sur le disque C:
                }
            }
            return Espace;
        }

        /// <summary>
        /// Calcule l'espace qui a été libéré pendant la phase de nettoyage
        /// </summary>
        /// <returns>Quantité libérée</returns>
        public static double CalculEspaceLibere()
        {
            double EspaceGagne;
            EspaceGagne = Cleaner.EspaceAp - Cleaner.EspaceAv; //On obtient l'espace libréré en octets
            EspaceGagne = (EspaceGagne / 1024) / 1024; //Convertion du résultat en Mo
            EspaceGagne = Math.Round(EspaceGagne, 2); //On ne garde que 2 chiffres après la virgule
            if (EspaceGagne <= 0)
            {
                //Ce teste permet de supprimer un éventuel signe moins devant la somme libérée lors de l'affichage final
                EspaceGagne = Cleaner.EspaceAv - Cleaner.EspaceAp;  //On réalise l'opération inverse à celle effectuée plus tôt
                EspaceGagne = (EspaceGagne / 1024) / 1024;
                EspaceGagne = Math.Round(EspaceGagne, 2);
            }
            return EspaceGagne;
        }
        #endregion

        #region Vérification de la présence de CCleaner
        
        /// <summary>
        /// Cette méthode permet de savoir si CCleaner est installé
        /// </summary>
        /// <param name="rV">Type de registre (32 ou 64 bits)</param>
        /// <returns>Retoune un booléen indiquant la présence ou non de CCleaner</returns>
        public static bool CCleanerIsPresent(RegistryView rV)
        {
            string path = Registre.LectureValeur("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Uninstall\\CCleaner", "InstallLocation", rV); //On récupère le chemin d'installation de CCleaner depuis la base de registre
            if (File.Exists(path + "\\CCleaner.exe"))
            {
                return true; //Si le fichier CCleaner existe alors on retourne "vrai"
            }
            else return false; //Sinon on retourne "false"
        }
        #endregion

        #region Lecture et écriture du log
        /// <summary>
        /// Cette méthode permet d'écrire un log qui permettra d'établir un journal
        /// Dans ce log on stocke la date et l'espace libéré
        /// </summary>
        public static void Ecriture_Log()
        {
            if (File.Exists("C:\\SBiiXpress\\Logs\\Log_Usr_" + Environment.UserName + ".xml")) //Vérification de l'existance du fichier
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load("C:\\SbiiXpress\\Logs\\Log_Usr_" + Environment.UserName + ".xml"); //on ouvre le fichier existant
                XmlNode root = xmlDoc.DocumentElement;
                XmlElement parentNode = xmlDoc.CreateElement("Use"); //Création d'un noeud Use
                XmlText date = xmlDoc.CreateTextNode("DateTime=" + DateTime.Now.ToString("!dd/MM/yyyy!H:mm:ss") + ";Espace_libre=$" + CalculEspaceLibere() + "$"); //On ajoute la date et l'espace libéré dans use
                parentNode.AppendChild(date);
                xmlDoc.DocumentElement.PrependChild(parentNode);
                root.InsertAfter(parentNode, root.LastChild);
                xmlDoc.Save("C:\\SBiiXpress\\Logs\\Log_Usr_" + Environment.UserName + ".xml");
                BDD();
            }
            else
            {
                XmlDocument xmlDoc = new XmlDocument();
                XmlDeclaration xmlDeclaration = xmlDoc.CreateXmlDeclaration("1.0", "utf-8", null);
                XmlElement rootNode = xmlDoc.CreateElement("Root");
                xmlDoc.InsertBefore(xmlDeclaration, xmlDoc.DocumentElement);
                xmlDoc.AppendChild(rootNode);
                XmlElement parentNode = xmlDoc.CreateElement("Use");
                XmlText date = xmlDoc.CreateTextNode("DateTime=" + DateTime.Now.ToString("!dd/MM/yyyy!H:mm:ss") + ";Espace_libre=$" + CalculEspaceLibere() + "$");
                parentNode.AppendChild(date);
                xmlDoc.DocumentElement.PrependChild(parentNode);
                DirectoryInfo DI = Directory.CreateDirectory("C:\\SBiiXpress\\Logs");
                xmlDoc.Save("C:\\SBiiXpress\\Logs\\Log_Usr_" + Environment.UserName + ".xml");
                BDD();
            }
        }

        /// <summary>
        /// Cette méthode permet de calculer l'espace total libéré par l'application depuis le log
        /// </summary>
        /// <returns>Retourne la somme d'espace libéré</returns>
        public static float LectureEspace_Log()
        {
            if (File.Exists("C:\\SBiiXpress\\Logs\\Log_Usr_" + Environment.UserName + ".xml")) //Si le log existe
            {
                float i;
                float total = 0;
                List<string> lines = File.ReadAllLines("C:\\SBiiXpress\\Logs\\Log_Usr_" + Environment.UserName + ".xml").ToList<string>(); //On prend toutes les lignes du log
                lines.RemoveAt(0);
                lines.RemoveAt(0);
                lines.RemoveAt(lines.Count - 1);
                string text = string.Join("", lines.ToArray()); //on stocke nos lignes dans un tableau
                string[] words = text.Split('$'); //On ne récupère que ce qui est entre le symbole "$" (la taille)

                foreach (string s in words)
                {
                    if (float.TryParse(s, out i))
                    {
                        total = total + i; //Pour chaque lignes, on additionne la taille totale et on la stocke dans total
                    }
                }
                return total; // On retourne la taille totale 
            }
            else
            {
                return 0; //Sinon, si le log n'existe pas, on retourne zéro
            }
        }

        /// <summary>
        /// Méthode qui permet de connaître la date de première utilisation depuis le log
        /// </summary>
        /// <returns>Retourne la date de première utilisation</returns>
        public static string LectureDate_Log()
        {
            string dateReturn = "";
            if (File.Exists("C:\\SBiiXpress\\Logs\\Log_Usr_" + Environment.UserName + ".xml"))
            {
                DateTime date;
                List<string> lines = File.ReadAllLines("C:\\SBiiXpress\\Logs\\Log_Usr_" + Environment.UserName + ".xml").ToList<string>(); //On récupère chaque lignes du log
                string line = Convert.ToString(lines[2]); //On ne conserve que la troisième ligne (c'est elle qui contient la première date)
                string text = string.Join("", line);
                string[] words = text.Split('!'); //On ne garde que ce qui est entre "!" (la date)
                foreach (string s in words)
                {
                    //Pour chaque mot dans la ligne
                    if (DateTime.TryParse(s, out date))
                    {
                        //Si on peut convertir le mot actuel en date, alors on retourne ce mot sous la forme de date simple
                        dateReturn = date.ToShortDateString();
                    }
                }
            }
            else
            {
                dateReturn = "Erreur"; //Sinon on retourne "Erreur"
            }
            return dateReturn;
        }
        #endregion

        #region Connexion à la base de données

        /// <summary>
        /// Cette méthode permet de se connecter à la base de données et d'y écrire
        /// </summary>
        public static void BDD()
        {
            try
            {
                //string cs = @"server=localhost;database=io1mf84j_sbiiapp;userid=root;password=;";  //Connexion à une base de donnée locale
                string cs = @"server=lhcp1029.webapps.net;database=io1mf84j_sbiixpress_appli;userid=io1mf84j_rpxiibs;password=+-123456A*bc+D-e/F;"; //Connexion à la base de données distante
                MySqlConnection conn = null;
                if (GetMacAddress() != "err")
                {
                    try
                    {
                        conn = new MySqlConnection(cs); //On crée une connexion SQL avec les information saisies dans la chaîne de caractères
                        conn.Open(); //On ouvre la connexion
                        string SelectQuery = "SELECT * from clt_config WHERE identifiant = '" + GetMacAddress() + "'"; //Requête SLQ, on vérifie si un enregistrement avec le même identifiant existe déjà
                        MySqlCommand cmd = new MySqlCommand(SelectQuery, conn); //On stocke la commande et les informations relatives au serveur dans une variable "cmd"
                        MySqlDataReader dataReader = cmd.ExecuteReader(); //Exécution de la commande
                        if (dataReader.Read())
                        {   //Si la commande SELECT à trouver un tuple avec le même identifiant alors on fait le traitement suivant
                            dataReader.Close(); //Fermeture du dataReader necessaire pour la commande
                            string query = "UPDATE clt_config SET freespace ='" + LectureEspace_Log() + "', print='" + GetImprimante() + "', temps='" + Cleaner.Temps() + "' WHERE identifiant ='" + GetMacAddress() + "'"; //On stocke la commande pour la mise à jour de l'enregistrement existant
                            MySqlCommand UpdateQuery = new MySqlCommand(query, conn); //Stockage de la commande et des informations relatives à la connexion à la BDD
                            UpdateQuery.ExecuteNonQuery(); //Exécution de la commande
                            dataReader.Close();
                        }
                        else
                        if (!dataReader.Read())
                        {
                            //Si aucun enregistrement avec le même identifiant existe
                            dataReader.Close();
                            string query = "INSERT INTO clt_config (identifiant,freespace,print, temps) VALUES ('" + GetMacAddress() + "','" + LectureEspace_Log() + "','" + GetImprimante() + "','" + Cleaner.Temps() + "')"; //Stockage de la commande pour créer un nouvel enregistrement avec les informations voulues
                            MySqlCommand InsertQuery = new MySqlCommand(query, conn); //Stockage de la commande et des informations relatives à la connexion à la BDD
                            InsertQuery.ExecuteNonQuery(); //Exécution de la commande
                        }
                        conn.Close(); //Fermeture de la connexion
                    }
                    catch (Exception ex) //En cas d'erreur, on affiche un message
                    {
                        //MessageBox.Show("Impossible de se connecter à la base de données\n" + ex, "SBiiXpress - Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        MessageBox.Show("Impossible de se connecter à la base de données", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Lecture de l'adresse MAC
        public static string GetMacAddress()
        {
            string MacAddress = "err";
            MacAddress = (
                                from nic in NetworkInterface.GetAllNetworkInterfaces()
                                where nic.OperationalStatus == OperationalStatus.Up
                                select nic.GetPhysicalAddress().ToString()
                                ).FirstOrDefault();
            return MacAddress;
        }
        #endregion

        #region Imprimante
        [DllImport("winspool.drv", CharSet = CharSet.Unicode, ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        public static extern long OpenPrinter(string pPrinterName, ref IntPtr phPrinter, int pDefault);

        public static string GetImprimante()
        {
            PrinterSettings setting = new PrinterSettings();
            string nomImp = setting.PrinterName;
            if (nomImp.Contains("Microsoft") || nomImp.Contains("XPS") || nomImp.Contains("PDF") || nomImp == "Fax" || nomImp == "L'imprimante par défaut n'est pas définie.")
            {
                nomImp = "Aucune";
                return nomImp;
            }
            else
            {
                return nomImp;
            }
        }
        #endregion
    }
}
