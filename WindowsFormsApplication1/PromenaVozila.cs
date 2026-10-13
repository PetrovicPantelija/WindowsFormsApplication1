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
    public partial class PromenaVozila : Form
    {
        public static string code = "PromenaVozila";
        public bool Pravo;
        int idGrupe;
        int idForme;
        bool insert;
        bool update;
        bool delete;
        string Kor = frmLogovanje.user.ToString().TrimEnd();
        string niz = "";
        public PromenaVozila()
        {
            InitializeComponent();
            JelaTema.Primeni(this);
            IdGrupe();
            IdForme();
            PravoPristupa();
        }
        public string IdGrupe()
        {
            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            //Sifarnici.frmLogovanje frm = new Sifarnici.frmLogovanje();         
            string query = "Select IdGrupe from KorisnikGrupa Where Korisnik = " + "'" + Kor.TrimEnd() + "'";
            SqlConnection conn = new SqlConnection(s_connection);
            conn.Open();
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader dr = cmd.ExecuteReader();
            int count = 0;

            while (dr.Read())
            {
                if (count == 0)
                {
                    niz = dr["IdGrupe"].ToString();
                    count++;
                }
                else
                {
                    niz = niz + "," + dr["IdGrupe"].ToString();
                    count++;
                }

            }
            conn.Close();
            return niz;
        }
        private int IdForme()
        {
            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            string query = "Select IdForme from Forme where Rtrim(Code)=" + "'" + code + "'";
            SqlConnection conn = new SqlConnection(s_connection);
            conn.Open();
            SqlCommand cmd = new SqlCommand(query, conn);

            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                idForme = Convert.ToInt32(dr["IdForme"].ToString());
            }
            conn.Close();
            return idForme;
        }

        private void PravoPristupa()
        {
            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            string query = "Select * From GrupaForma Where IdGrupe in (" + niz + ") and IdForme=" + idForme;
            SqlConnection conn = new SqlConnection(s_connection);
            conn.Open();
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows == false)
            {
                MessageBox.Show("Nemate prava za pristup ovoj formi", code);
                Pravo = false;
            }
            else
            {
                Pravo = true;
                while (reader.Read())
                {
                    insert = Convert.ToBoolean(reader["Upis"]);
                    if (insert == false)
                    {
                        //tsNew.Enabled = false;
                    }
                    update = Convert.ToBoolean(reader["Izmena"]);
                    if (update == false)
                    {
                        //tsSave.Enabled = false;
                    }
                    delete = Convert.ToBoolean(reader["Brisanje"]);
                    if (delete == false)
                    {
                        //tsDelete.Enabled = false;
                    }
                }
            }

            conn.Close();
        }
        private void PromenaVozila_Load(object sender, EventArgs e)
        {
            var select = " Select Distinct RTrim(Vozilo) as Vozilo From RkShipping where Stanje = 1";
            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            cboVozila.DataSource = ds.Tables[0];
            cboVozila.DisplayMember = "Vozilo";
            cboVozila.ValueMember = "Vozilo";



            var select2 = " Select Distinct RTrim(RegistarskiBroj) as Vozilo From TrVozila ";
            var s_connection2 = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection2 = new SqlConnection(s_connection2);
            var c2 = new SqlConnection(s_connection2);
            var dataAdapter2 = new SqlDataAdapter(select2, c2);

            var commandBuilder2 = new SqlCommandBuilder(dataAdapter2);
            var ds2 = new DataSet();
            dataAdapter2.Fill(ds2);

            cboNovoVozilo.DataSource = ds2.Tables[0];
            cboNovoVozilo.DisplayMember = "Vozilo";
            cboNovoVozilo.ValueMember = "Vozilo";

        }

        private void button2_Click(object sender, EventArgs e)
        {
            var select = " Select Distinct DatumUtovara, Cast(SUBSTRING(DatumUtovara, 1, Charindex('.',DatumUtovara) -1) as int)  as  Dan, Cast(Replace(SUBSTRING(DatumUtovara, Charindex('.',DatumUtovara)+1, Len(DatumUtovara)-12 -1 -( (Charindex('.',DatumUtovara) ))),'.','') as int) as  Mesec from RkShipping where Stanje = 1 and Vozilo = '" + cboVozila.Text + "' order by Mesec, Dan";
           
            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            cboDatumUtovara.DataSource = ds.Tables[0];
            cboDatumUtovara.DisplayMember = "DatumUtovara";
            cboDatumUtovara.ValueMember = "DatumUtovara";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Obrnuto Datum i broj Utovara
            var select = " SELECT  RkShipping.ShippingNo as BarkodUtovara, RkShipping.BrojUtovara as BrojUtovara, RkShipping.DatumIStovara as DatumUtovara, RkShipping.BrojIstovara as BrojIstovara, RkShipping.DatumIstovara as DatumIstovara ,RkShipping.KupacIdz, RkShipping.SalonIdz FROM  RkShipping  where RkShipping.Vozilo  = '" + cboVozila.Text + "' and RkShipping.DatumUtovara = '" + cboDatumUtovara.Text + "'";

            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];
            //  string value = dataGridView1.Rows[0].Cells[0].Value.ToString();

            dataGridView1.Refresh();        
        }

        private void btnPostaviPaletu_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {

                    if (row.Selected)
                    {
                        var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;

                        SqlConnection connection = new SqlConnection(s_connection);
                        SqlCommand command = new SqlCommand("SELECT * FROM Korisnici", connection);
                        command.Connection.Open();

                        string querystr = "UPDATE RkShipping SET Vozilo='" + cboNovoVozilo.Text + "', VoziloIdz = '"+  cboVoziloID.Text +"' WHERE ShippingNo ='" + row.Cells[0].Value.ToString() + "' and DatumUtovara = '" + cboDatumUtovara.Text + "'";
                        SqlCommand query = new SqlCommand(querystr, connection);
                        query.ExecuteNonQuery();

                        /*
                        string User_ID = Session["ID"].ToString();
                        string User_FirstName = FirstNameEdit.Text;
                        string User_LastName = LastNameEdit.Text;

                        query.Parameters.Add("@User_ID", User_ID);
                        query.Parameters.Add("@User_FirstName", User_FirstName);
                        query.Parameters.Add("@User_LastName", User_LastName);
                        
                        */

                        command.Connection.Close();

                    }
                }
            }
            catch
            {
                MessageBox.Show("Nije uspela promena stavki");
            }

        }

        private void cboVozila_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        private void cboNovoVozilo_SelectedIndexChanged(object sender, EventArgs e)
        {
            var select2 = " Select Distinct RTrim(VoziloID) as Vozilo From TrVozila where RegistarskiBroj = '" +cboNovoVozilo.Text + "'";
            var s_connection2 = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection2 = new SqlConnection(s_connection2);
            var c2 = new SqlConnection(s_connection2);
            var dataAdapter2 = new SqlDataAdapter(select2, c2);

            var commandBuilder2 = new SqlCommandBuilder(dataAdapter2);
            var ds2 = new DataSet();
            dataAdapter2.Fill(ds2);

            cboVoziloID.DataSource = ds2.Tables[0];
            cboVoziloID.DisplayMember = "Vozilo";
            cboVoziloID.ValueMember = "Vozilo";
        }
    }
}
