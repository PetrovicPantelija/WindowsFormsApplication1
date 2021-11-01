using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Configuration;
using System.Globalization;

namespace WindowsFormsApplication1
{
    public partial class frmSifraReklamacije : Form
    {
        public frmSifraReklamacije()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string connString =
         ConfigurationManager.AppSettings["webs"];

            DataTable dataTableRes = new DataTable();

            using (OleDbConnection conn = new OleDbConnection(connString))
            {
                OleDbCommand cmd = new OleDbCommand("SELECT SifraReklamacijeID, SifraReklamacije, Aktivan, SifraReklamacijeLargo FROM RkPrometPorudzbineReklamacijeSifraReklamacije where SifraReklamacijeLargo = 0" , conn);

                conn.Open();

                OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);

                adapter.Fill(dataTableRes);
                dataGridView1.DataSource = dataTableRes;
                DataGridViewColumn column = dataGridView1.Columns[0];
                dataGridView1.Columns[0].HeaderText = "ID";
                dataGridView1.Columns[0].Width = 50;

                conn.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
              {
                  if (row != null && row.Cells[0].Value != null)
                     {
                        var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
                        SqlConnection con = new SqlConnection(s_connection);

                        con.Open();

                        SqlCommand cmd = new SqlCommand("select MpSifra from MaticniPodatki where MpStaraSif = '" + row.Cells[1].Value.ToString() + "'", con);
                        SqlDataReader dr = cmd.ExecuteReader();
                
                        while (dr.Read())
                        {
                            row.Cells[3].Value = dr["MpSifra"].ToString();
                        }
           
                        con.Close();
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string connString2 = ConfigurationManager.AppSettings["webs"];
            using (OleDbConnection conn = new OleDbConnection(connString2))
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Selected = true && row != null && row.Cells[0].Value != null)
                    {
                        //Promeniti da bi setovao po Barkodu isporuke// OleDbCommand cmd = new OleDbCommand("Update RkShipping Set ExportLargo = true where ShippingNo = " + Convert.ToInt32(textBox1.Text) + "", conn);
                        OleDbCommand cmd = new OleDbCommand("Update RkPrometPorudzbineReklamacijeSifraReklamacije Set SifraReklamacijeLargo =  " + Convert.ToInt32(row.Cells[3].Value) + " where SifraReklamacijeID  = " + Convert.ToInt32(row.Cells[0].Value) , conn);
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();
                    }
                }
            }
           
            MessageBox.Show("Šifarnik uspešno ažuriran");
        }

        private void frmSifraReklamacije_Load(object sender, EventArgs e)
        {

        }
    }
}
