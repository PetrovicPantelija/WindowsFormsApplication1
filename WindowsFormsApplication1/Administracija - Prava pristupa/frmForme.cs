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

namespace WindowsFormsApplication1.Administracija___Prava_pristupa
{
    public partial class frmForme : Form
    {
        bool status = false;
        public string connect = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
        public static string code = "frmForme";
        public bool Pravo;
        int idGrupe;
        int idForme;
        bool insert;
        bool update;
        bool delete;
        string Kor = frmLogovanje.user.ToString().TrimEnd();
        string niz = "";
        public frmForme()
        {
            InitializeComponent();
            JelaTema.Primeni(this);
            SfGridPomocnik.Stilizuj(dataGridView1);
            txt_ID.Enabled = false;
            txt_IDForme.Enabled = false;
            FillGV();
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
                        tsNew.Enabled = false;
                    }
                    update = Convert.ToBoolean(reader["Izmena"]);
                    if (update == false)
                    {
                        tsSave.Enabled = false;
                    }
                    delete = Convert.ToBoolean(reader["Brisanje"]);
                    if (delete == false)
                    {
                        tsDelete.Enabled = false;
                    }
                }
            }

            conn.Close();
        }
        private void FillGV()
        {
            var query = "Select * from Forme";
            SqlConnection conn = new SqlConnection(connect);
            SqlDataAdapter da = new SqlDataAdapter(query, conn);
            DataSet ds = new DataSet();
            da.Fill(ds);
            SfGridPomocnik.Popuni(dataGridView1, ds.Tables[0], 60, 60, 170, 130);
        }

        private void dataGridView1_SelectionChanged(object sender, Syncfusion.WinForms.DataGrid.Events.SelectionChangedEventArgs e)
        {
            try
            {
                object[] red = SfGridPomocnik.Red(dataGridView1);
                if (red != null)
                {
                        txt_ID.Text = red[0].ToString();
                        txt_IDForme.Text = red[1].ToString();
                        txt_Naziv.Text = red[2].ToString().TrimEnd();
                        txt_Code.Text = red[3].ToString().TrimEnd();
                    }
            }
            catch { }
        }

        private void tsNew_Click(object sender, EventArgs e)
        {
            txt_ID.Text = "";
            txt_IDForme.Text = "";
            status = true;
            tsNew.Enabled = false;
        }

        private void tsSave_Click(object sender, EventArgs e)
        {
            InsertForme forme = new InsertForme();
            if (status == true)
            {
                forme.InsForme(txt_Naziv.Text.ToString().TrimEnd(), txt_Code.Text.ToString().TrimEnd());
                status = false;
                tsNew.Enabled = true;
            }
            else
            {
                forme.UpdForme(Convert.ToInt32(txt_ID.Text), Convert.ToInt32(txt_IDForme.Text), txt_Naziv.Text.ToString().TrimEnd(), txt_Code.Text.ToString().TrimEnd());
            }
            FillGV();
        }

        private void tsDelete_Click(object sender, EventArgs e)
        {
            InsertForme forme = new InsertForme();
            forme.DelForme(Convert.ToInt32(txt_ID.Text));
            FillGV();
        }
    }
}
