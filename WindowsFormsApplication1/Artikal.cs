using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;

namespace WindowsFormsApplication1
{
    public partial class Artikal : Form
    {
        int lBarkod = 0;
        public Artikal()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            lBarkod = txtBarkod.Text.ToString().Length;
            txt010.Text = "";
            txt20.Text = "";
            txt243.Text = "";
            if (lBarkod == 13)
            {
                this.Text = "EAN 13";
            }
            else if (lBarkod == 26)
            {
                this.Text = "PAKET - Code 128";
                txt010.Text = txtBarkod.Text.ToString().Substring(3, 13);
                txt20.Text = txtBarkod.Text.ToString().Substring(18, 2);
                txt243.Text = txtBarkod.Text.ToString().Substring(23, 3);
            }
            else if (lBarkod == 30)
            {
                this.Text = "SOKLA/RP - Code 128";
                txt010.Text = txtBarkod.Text.ToString().Substring(3, 13);
                txt20.Text = txtBarkod.Text.ToString().Substring(18, 2);
                txt3113.Text = txtBarkod.Text.ToString().Substring(24, 6);
            }
        }

        private void txt010_TextChanged(object sender, EventArgs e)
        {
            var select = "SELECT *  FROM RkShippingLinkWebsLargo where WebsGTIN = '" + txt010.Text + "'";

            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];
        }
    }
}
