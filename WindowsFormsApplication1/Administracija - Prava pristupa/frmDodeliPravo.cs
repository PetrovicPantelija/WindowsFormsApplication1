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
    public partial class frmDodeliPravo : Form
    {
        public string connect = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
        bool status = false;
        bool upis = false;
        bool izmena = false;
        bool brisanje = false;

        public static string code = "frmDodeliPravo";
        public bool Pravo;
        int idGrupe;
        int idForme;
        bool insert;
        bool update;
        bool delete;
        string Kor = frmLogovanje.user.ToString().TrimEnd();
        string niz = "";
        public frmDodeliPravo()
        {
            InitializeComponent();
            txt_ID.Enabled = false;
            IdGrupe();
            IdForme();
            PravoPristupa();
        }

        private void frmDodeliPravo_Load(object sender, EventArgs e)
        {
            FillCombo();
            FillGV();
            FillGVFormer();
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
        private void FillCombo()
        {
            SqlConnection conn = new SqlConnection(connect);
            var query = "Select * From Grupe order by Naziv";
            var da = new SqlDataAdapter(query, conn);
            var ds = new DataSet();
            da.Fill(ds);
            combo_Grupa.DataSource = ds.Tables[0];
            combo_Grupa.DisplayMember = "Naziv";
            combo_Grupa.ValueMember = "IdGrupe";

            var query2 = "Select * from Forme";
            var da2 = new SqlDataAdapter(query2, conn);
            var ds2 = new DataSet();
            da2.Fill(ds2);
            combo_Forma.DataSource = ds2.Tables[0];
            combo_Forma.DisplayMember = "Naziv";
            combo_Forma.ValueMember = "IdForme";
        }
        private void FillGV()
        {
            var query = "Select gf.ID,gf.IdGrupe,g.Naziv as [Grupa],gf.IdForme,f.Naziv as [Forma],gf.Upis,gf.Izmena,gf.Brisanje " +
                "From GrupaForma gf,Grupe g, Forme f " +
                "Where gf.IdGrupe = g.IdGrupe and gf.IdForme = f.IdForme";
            SqlConnection conn = new SqlConnection(connect);
            SqlDataAdapter da = new SqlDataAdapter(query, conn);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];
            dataGridView1.Columns[0].Width = 60;
            dataGridView1.Columns[1].Width = 60;
            dataGridView1.Columns[2].Width = 150;
            dataGridView1.Columns[3].Width = 60;
            dataGridView1.Columns[4].Width = 200;
            dataGridView1.Columns[5].Width = 70;
            dataGridView1.Columns[6].Width = 70;
            dataGridView1.Columns[7].Width = 70;
        }

        private void FillGVFormer()
        {
            var query = "Select * from Forme";
            SqlConnection conn = new SqlConnection(connect);
            SqlDataAdapter da = new SqlDataAdapter(query, conn);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView2.ReadOnly = true;
            dataGridView2.DataSource = ds.Tables[0];
           
        }



        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Selected)
                    {
                        txt_ID.Text = row.Cells[0].Value.ToString();
                        combo_Grupa.Text = row.Cells[2].Value.ToString();
                        combo_Forma.Text = row.Cells[4].Value.ToString();
                        if (Convert.ToInt32(row.Cells[5].Value) == 1)
                        { cb_Upis.Checked = true;}else { cb_Upis.Checked = false; }
                        if (Convert.ToInt32(row.Cells[6].Value) == 1)
                        { cb_Izmena.Checked = true; }
                        else { cb_Izmena.Checked = false; }
                        if (Convert.ToInt32(row.Cells[7].Value) == 1)
                        { cb_Brisanje.Checked = true; }
                        else { cb_Brisanje.Checked = false; }
                    }
                }
            }
            catch { }
        }

        private void tsNew_Click(object sender, EventArgs e)
        {
            txt_ID.Text = "";
            status = true;
            tsNew.Enabled = false;
        }

        private void tsSave_Click(object sender, EventArgs e)
        {
            Administracija___Prava_pristupa.InsertGrupaForma grupa = new InsertGrupaForma();
            if (status == true)
            {
                if (cb_Upis.Checked == true) { upis = true; }
                if (cb_Izmena.Checked == true) { izmena = true; }
                if (cb_Brisanje.Checked == true) { brisanje = true; }
                grupa.InsGrupaForma(Convert.ToInt32(combo_Grupa.SelectedValue), Convert.ToInt32(combo_Forma.SelectedValue), upis, izmena, brisanje);
                status = false;
                tsNew.Enabled = true;
            }
            else
            {
                if (cb_Upis.Checked == true) { upis = true; }
                if (cb_Izmena.Checked == true) { izmena = true; }
                if (cb_Brisanje.Checked == true) { brisanje = true; }
                grupa.UpdGrupaForma(Convert.ToInt32(txt_ID.Text), Convert.ToInt32(combo_Grupa.SelectedValue), Convert.ToInt32(combo_Forma.SelectedValue), upis, izmena, brisanje);
            }
            FillGV();
            upis = false;
            izmena = false;
            brisanje = false;
        }

        private void tsDelete_Click(object sender, EventArgs e)
        {
            Administracija___Prava_pristupa.InsertGrupaForma grupa = new InsertGrupaForma();
            grupa.DelGrupaForma(Convert.ToInt32(txt_ID.Text));
            FillGV();
        }

        private void btnDodaj_Click(object sender, EventArgs e)
        {
            try
            {
                Administracija___Prava_pristupa.InsertGrupaForma grupa = new InsertGrupaForma();
                MessageBox.Show("Funkcija za sve selektovane forme, dodeljuje obelezenoj grupi");
                foreach (DataGridViewRow row in dataGridView2.Rows)
                {
                    if (row.Selected)
                    {
                        if (cb_Upis.Checked == true) { upis = true; }
                        if (cb_Izmena.Checked == true) { izmena = true; }
                        if (cb_Brisanje.Checked == true) { brisanje = true; }
                        grupa.InsGrupaForma(Convert.ToInt32(combo_Grupa.SelectedValue), Convert.ToInt32(row.Cells[1].Value.ToString()), upis, izmena, brisanje);
                    }
                }
            }
            catch { }
        }
    }
}
