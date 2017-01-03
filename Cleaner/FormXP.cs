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
    public partial class FormXP : Form
    {
        RegistryView rV;
        public FormXP(RegistryView rV)
        {
            InitializeComponent();
            this.rV = rV;
        }
    }
}
