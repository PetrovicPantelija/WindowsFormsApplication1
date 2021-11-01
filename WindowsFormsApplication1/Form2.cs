using System;
using System.Collections.Generic;
using System.ComponentModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;

using Microsoft.Reporting.WinForms;


namespace WindowsFormsApplication1
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, System.EventArgs e)
        {
            InsertArhiva isporuka = new InsertArhiva();

            isporuka.PrebaciIzRKShiping(Convert.ToInt32(textBox1.Text));
            isporuka.PrebaciIzRKShipingItemPak(Convert.ToInt32(textBox1.Text));
            isporuka.PrebaciIzRKShipingItemRec(Convert.ToInt32(textBox1.Text));
            isporuka.PrebaciIzRkShippingItemPro(Convert.ToInt32(textBox1.Text));
         
        }
    }
}
