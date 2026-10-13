using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1.Administracija___Prava_pristupa
{
    public partial class MainPrava : Form
    {
        public MainPrava()
        {
            InitializeComponent();
            JelaTema.Primeni(this);
        }
        private void MainPrava_Load(object sender, EventArgs e)
        {
            
        }
        
    }
}

