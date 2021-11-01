using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Configuration;
using System.Globalization;

namespace WindowsFormsApplication1
{
    public partial class v123v : Form
    {
        public v123v()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string connString2 = ConfigurationManager.AppSettings["webs"];
            using (OleDbConnection conn = new OleDbConnection(connString2))
            {
                

                        //Promeniti da bi setovao po Barkodu isporuke// OleDbCommand cmd = new OleDbCommand("Update RkShipping Set ExportLargo = true where ShippingNo = " + Convert.ToInt32(textBox1.Text) + "", conn);
                OleDbCommand cmd = new OleDbCommand(textBox1.Text, conn);
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();
                
            }
        }
    }
}
