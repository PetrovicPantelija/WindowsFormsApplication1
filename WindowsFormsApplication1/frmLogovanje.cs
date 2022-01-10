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
  
    public partial class frmLogovanje : Form
    {
        int usao = 0;
        public frmLogovanje()
        {
            InitializeComponent();
        }

        private void frmLogovanje_Load(object sender, EventArgs e)
        {
            var select = " Select Distinct RTrim(Korisnik) as Korisnik From Korisnici order by Korisnik asc";
            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            cboKorisnik.DataSource = ds.Tables[0];
            cboKorisnik.DisplayMember = "Korisnik";
            cboKorisnik.ValueMember = "Korisnik";
        }

        private void ProveriAzuriranje()
        {
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                    DialogResult result;
                    result = MessageBox.Show("Da li želite update webs podataka", "Azuriranje", buttons);
                    if (result == System.Windows.Forms.DialogResult.Yes)
                    {
                       // MessageBox.Show("PA0");
                    Main mainf = new Main(0);
                    mainf.Show();
                      
                    }
                    else
	                {
                       // MessageBox.Show("PA1");
                    Main mainf = new Main(1);
                    mainf.Show();
	                }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // var select = " Select Distinct DatumUtovara From RkShipping where Stanje = 1 and Vozilo = '" + cboVozila.Text + "'";
            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            SqlCommand command = new SqlCommand("SELECT Korisnik FROM Korisnici where Password = '" + txtPassword.Text + "'", myConnection);
            myConnection.Open();

            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows == false)
            {
                MessageBox.Show("Nije ispravno uneta lozinka");
                return;
            }
            int nasao = 0;
            while (reader.Read())
            {
                string myString = reader.GetString(0); //The 0 stands for "the 0'th column", so the first column of the result.
                // Do somthing with this rows string, for example to put them in to a list
                if (myString.TrimEnd() == cboKorisnik.Text)
                {
                    if (cboKorisnik.Text.Trim() == "NEbojša pešic") 
                    {
                       // MessageBox.Show("Korisnik");
                        ProveriAzuriranje();
                        nasao = 1;
                    }
                     if (cboKorisnik.Text.Trim() == "Reklamacije")  
                    {
                       // MessageBox.Show("Korisnik");
                        ProveriAzuriranje();
                        nasao = 1;
                    }
                     if (cboKorisnik.Text.Trim() == "Tanja D. Tanaskovic") 
                    {
                       // MessageBox.Show("Korisnik");
                        ProveriAzuriranje();
                        nasao = 1;
                    }

                    if (nasao == 0)
                    {
                       // MessageBox.Show("Nije korisnik");
                        Main mainf = new Main(0);
                        mainf.Show();
                    }
                  

                }
            }
                reader.Close();
                myConnection.Close();
            
        }
        private void button2_Click(object sender, EventArgs e)
        {
            int ReadNabavka = 0;
            string KorisnikN = "";
            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            SqlCommand command = new SqlCommand("SELECT Korisnik, ReadNabavka FROM Korisnici where Password = '" + txtPassword.Text + "' and Nabavka = 1 ", myConnection);
            myConnection.Open();

            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows == false)
            {
                MessageBox.Show("Nije ispravno uneta lozinka");
                return;
            }
            while (reader.Read())
            {
                string myString = reader.GetString(0); //The 0 stands for "the 0'th column", so the first column of the result.
                // Do somthing with this rows string, for example to put them in to a list
                if (myString.TrimEnd() == cboKorisnik.Text)
                {
                    KorisnikN = reader["Korisnik"].ToString();
                    ReadNabavka = Convert.ToInt32(reader["ReadNabavka"].ToString());
                    MainNabavka mn = new MainNabavka(KorisnikN, ReadNabavka);
                    mn.Show();
                }
                else
                {
                    MessageBox.Show("Nije ispravno uneta lozinka");
                    return;
                }
            }
            reader.Close();
            myConnection.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (txtPassword.Text == "9145")
            {
                Magacin.MainAdministracija ma = new Magacin.MainAdministracija();
                ma.Show();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            SqlCommand command = new SqlCommand("SELECT Korisnik FROM Korisnici where Password = '" + txtPassword.Text + "'", myConnection);
            myConnection.Open();

            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows == false)
            {
                MessageBox.Show("Nije ispravno uneta lozinka");
                return;
            }
            else
            {
                Portirnica.StatusNajave stat = new Portirnica.StatusNajave(cboKorisnik.Text);
                stat.Show();
            }
          

            myConnection.Close();
        }
    }
}
