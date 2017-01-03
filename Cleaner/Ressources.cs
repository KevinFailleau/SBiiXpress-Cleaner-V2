using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Management;
using Microsoft.Win32;
using System.ServiceProcess;

namespace Cleaner
{
    public class Ressources
    {
        public static void Exec(string path, string args)
        {
            Process p = new Process();
            p.StartInfo.FileName = path;
            p.StartInfo.Arguments = args;
            p.StartInfo.CreateNoWindow = true;
            p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            p.Start();
        }

        /// <summary>
        /// Méthode qui permet de lancer une application sans paramètres
        /// </summary>
        /// <param name="path">Paramètres pour lancer l'application</param>
        public static void Exec(string path)
        {
            Process p = new Process();
            p.StartInfo.FileName = path;
            p.StartInfo.CreateNoWindow = true;
            p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            p.Start();
        }

        public static string VersionOS()
        {
            int cpt = 0;
            string ver = "Inconnu";
            var name = (from x in new ManagementObjectSearcher("SELECT Caption FROM Win32_OperatingSystem").Get().Cast<ManagementObject>()
                        select x.GetPropertyValue("Caption")).FirstOrDefault();
            string[] nomComplet = name.ToString().Split(' ');
            foreach (string s in nomComplet)
            {
                cpt++;
                if (cpt == 3)
                {
                    ver = s;
                }
            }
            return ver;
        }
        public static RegistryView ArchiOS()
        {
            RegistryView rV;
            if (Environment.Is64BitOperatingSystem)
            {
                rV = RegistryView.Registry64;
            }
            else
            {
                rV = RegistryView.Registry32;
            }

            return rV;
        }

        public static string VerifProcessus(string s)
        {
            ServiceController sc = new ServiceController(s);

            switch (sc.Status)
            {
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
    }
}
