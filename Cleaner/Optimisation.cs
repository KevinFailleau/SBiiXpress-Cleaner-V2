using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cleaner
{
    class Optimisation
    {
        public static void Aero(string A)
        {
            if (A == "STOP")
            {
                Ressources.Exec("cmd", " /c net stop uxsms");
            }
            else
                if (A == "START")
            {
                Ressources.Exec("cmd", " /c net start uxsms");
            }
        }


    }
}
