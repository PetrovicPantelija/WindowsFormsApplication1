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
    public partial class frmPorudzbinaImportPregled : Form
    {
        public frmPorudzbinaImportPregled()
        {
            InitializeComponent();
        }

        private void frmPorudzbinaImportPregled_Load(object sender, EventArgs e)
        {
            var select = " Select Distinct RTrim(NazivZapisa) as NazivZapisa From Beleske order by RTrim(NazivZapisa)";
            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            cboVozila.DataSource = ds.Tables[0];
            cboVozila.DisplayMember = "NazivZapisa";
            cboVozila.ValueMember = "NazivZapisa";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var select = " SELECT * from Beleske where NazivZapisa  = '" + cboVozila.SelectedValue + "'";
            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];
            

           
            
            dataGridView1.Refresh();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            InsertPorudzbina porudzbina = new InsertPorudzbina();

            porudzbina.DeleteBeleska(cboVozila.Text);
            MessageBox.Show("Uspešno ste obrisali podatke");
        }
    }
}
