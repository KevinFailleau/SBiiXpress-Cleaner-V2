using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cleaner
{
    class Nettoyage
    {
        public static void NettoyageIE()
        {
            Ressources.Exec("rundll32.exe", " inetcpl.cpl,ClearMyTracksByProcess 4351");
        }

        public static void CCleaner()
        {
            /*string test = Registre.LectureValeur(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\CCleaner", "InstallLocation", CleanerXpress.rV);
            if (File.Exists(test + "\\CCleaner.exe"))
            {
                Ressources.Exec("CCleaner.exe", " /auto");
            }*/
        }

        public static void NettoyageWindowsUpdate()
        {
            Ressources.Exec("cmd", " /c net stop wuauserv");
            Ressources.Exec("cmd", "/c rd /s /Q %Windir%\\SoftwareDistribution");
        }
    }
}
