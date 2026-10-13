using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1.Planovi_Excel
{
    public partial class RadniCentarLog : Form
    {
          
        bool status = false;
        // existing connect stays; keep single definition
        string connect = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
        public bool PravoP;
        public static string code = "RadniCentarLog";
        public bool Pravo;
        int idGrupe;
        int idForme;
        bool insert;
        bool update;  // added members
        bool delete;
        string niz = "";
        string Kor = frmLogovanje.user.ToString().TrimEnd();

        public RadniCentarLog()
        {
            InitializeComponent();
            JelaTema.Primeni(this);
           
        }

        public string IdGrupe()
        {
            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            string query = "Select IdGrupe from KorisnikGrupa Where Korisnik = " + "'" + Kor.TrimEnd() + "'";
            using (SqlConnection conn = new SqlConnection(s_connection))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
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
                }
            }
            return niz;
        }

        private int IdForme()
        {
            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            string query = "Select IdForme from Forme where Rtrim(Code)=" + "'" + code + "'";
            using (SqlConnection conn = new SqlConnection(s_connection))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        idForme = Convert.ToInt32(dr["IdForme"].ToString());
                    }
                }
            }
            return idForme;
        }

        private void PravoPristupa()
        {
            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            string query = "Select * From GrupaForma Where IdGrupe in (" + niz + ") and IdForme=" + idForme;
            using (SqlConnection conn = new SqlConnection(s_connection))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
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
                                // disable new control if present e.g. tsNew.Enabled = false;
                            }
                            update = Convert.ToBoolean(reader["Izmena"]);
                            if (update == false)
                            {
                                // disable save control if present e.g. tsSave.Enabled = false;
                            }
                            delete = Convert.ToBoolean(reader["Brisanje"]);
                            if (delete == false)
                            {
                                // disable delete control if present e.g. tsDelete.Enabled = false;
                            }
                        }
                    }
                }
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            var query = @"SELECT Top 300 [ID]      ,[Datum]      ,[Korisnik]
      ,[RadniCentar]      ,[VrstaRada]      ,[Dokument]      ,[Operacija]
      ,[RazlogZaustavljanja]      ,[Napomena]      ,[Pocetak]      ,[Kraj]
  FROM [dbo].[RadniCentarLog]
  order by ID desc";
            SqlConnection conn = new SqlConnection(connect);
            var da = new SqlDataAdapter(query, conn);
            var ds = new DataSet();
            da.Fill(ds);
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];

            DataGridViewColumn column = dataGridView1.Columns[0];
            dataGridView1.Columns[0].HeaderText = "ID";
            dataGridView1.Columns[0].Width = 50;

            DataGridViewColumn column2 = dataGridView1.Columns[1];
            dataGridView1.Columns[1].HeaderText = "Datum";
            dataGridView1.Columns[1].Width = 60;

            DataGridViewColumn column3 = dataGridView1.Columns[2];
            dataGridView1.Columns[2].HeaderText = "Korisnik";
            dataGridView1.Columns[2].Width = 60;

            DataGridViewColumn column4 = dataGridView1.Columns[3];
            dataGridView1.Columns[3].HeaderText = "Radni centar";
            dataGridView1.Columns[3].Width = 60;

            DataGridViewColumn column5 = dataGridView1.Columns[4];
            dataGridView1.Columns[4].HeaderText = "Vrsta rada";
            dataGridView1.Columns[4].Width = 80;

            DataGridViewColumn column6 = dataGridView1.Columns[5];
            dataGridView1.Columns[5].HeaderText = "RN";
            dataGridView1.Columns[5].Width = 100;
        }

        private void RadniCentarLog_Load(object sender, EventArgs e)
        {
            IdGrupe();
            IdForme();
            PravoPristupa();
            if (Pravo == false)
            {
                this.Close();
                return;
            }
        }
    }
}
