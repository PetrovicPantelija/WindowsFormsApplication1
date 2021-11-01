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
using System.Net;
using System.Net.Mail;

using Microsoft.Reporting.WinForms;

namespace WindowsFormsApplication1.Magacin
{
    public partial class frmPromenaKorisnika : Form
    {
        public frmPromenaKorisnika()
        {
            InitializeComponent();
        }

        private void frmPromenaKorisnika_Load(object sender, EventArgs e)
        {
            var select2 = " Select Rtrim(korisnik) as Korisnik from Korisnici order by Korisnik ";
            var s_connection2 = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection2 = new SqlConnection(s_connection2);
            var c2 = new SqlConnection(s_connection2);
            var dataAdapter2 = new SqlDataAdapter(select2, c2);

            var commandBuilder2 = new SqlCommandBuilder(dataAdapter2);
            var ds2 = new DataSet();
            dataAdapter2.Fill(ds2);
            cboKorisnik.DataSource = ds2.Tables[0];
            cboKorisnik.DisplayMember = "Korisnik";
            cboKorisnik.ValueMember = "Korisnik";


            var select3 = " Select Rtrim(korisnik) as Korisnik from Korisnici order by Korisnik ";
            var s_connection3 = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection3 = new SqlConnection(s_connection3);
            var c3 = new SqlConnection(s_connection3);
            var dataAdapter3 = new SqlDataAdapter(select3, c3);

            var commandBuilder3 = new SqlCommandBuilder(dataAdapter3);
            var ds3 = new DataSet();
            dataAdapter3.Fill(ds3);
            cboKorisnikNovi.DataSource = ds3.Tables[0];
            cboKorisnikNovi.DisplayMember = "Korisnik";
            cboKorisnikNovi.ValueMember = "Korisnik";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            InsertMedjuskladisni isporuka = new InsertMedjuskladisni();
            isporuka.aUzimanjeZamenaKorisnika(cboKorisnik.Text, cboKorisnikNovi.Text);

            MessageBox.Show("Promenjen korisnik");

        }
    }
}
