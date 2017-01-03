using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cleaner
{
    public partial class Form7_10 : Form
    {
        private string verOS;
        private RegistryView rV;
        public Form7_10(string v, RegistryView rV)
        {
            InitializeComponent();
            verOS = v;
            this.rV = rV;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Nettoyage.NettoyageIE();
        }
    }
}
