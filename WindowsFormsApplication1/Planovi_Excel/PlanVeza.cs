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
    public partial class PlanVeza : Form
    {
        string Kor = frmLogovanje.user.ToString().TrimEnd();
        string connect = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
        public PlanVeza()
        {
            InitializeComponent();
        

            if (Kor.Trim() == "stanojevic.ivan" || Kor.Trim() == "stanojlovic.djordje" || Kor.Trim() == "hasanovic.igor" || Kor.Trim() == "pantelija")
            {
                btnDodeli.Visible = true;
                btnNovaGrupa.Visible= true;
                btnObrisi.Visible= true;
            }
            else {
                btnDodeli.Visible = false;
                btnNovaGrupa.Visible = false;
                btnObrisi.Visible = false;
            }
        }
        public static string code = "PlanVeza";
        public bool Pravo;
        int idGrupe;
        int idForme;
        bool insert;
        bool update;
        bool delete;
        string niz = "";
        private void PlanVeza_Load(object sender, EventArgs e)
        {
            IdGrupe();
            IdForme();
            PravoPristupa();
            if (Pravo == false)
            {
                this.Close();
                return;
            }
            FillGV();
        }
        public string IdGrupe()
        {
            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
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
        private void FillGV()
        {
            var query = "Select Kolekcije.ID as [IDKol], RTrim(Kolekcije.Naziv) as  Kolekcija,Program.ID as [IDProgram],RTrim(Program.Naziv) as Program,pkp.MpSifra as [MpSifra], " +
                "RTrim(MpNaziv) as [Naziv],MpStaraSif as [Kod],TipPaketa,SkSifra as [Sklad], " +
                "RTrim(SkNaziv) as [Skladista],MpOsnLokacija as [Osnovna lokacija] " +
                "from PripadnostKolProgram pkp " +
                "Inner join Kolekcije on pkp.IdKolekcije = Kolekcije.ID " +
                "inner join Program on pkp.IdPrograma = Program.ID " +
                "inner join MaticniPodatki on pkp.MpSifra = MaticniPodatki.MpSifra " +
                "inner join Sklad on MaticniPodatki.MpOsnSklad = sklad.SkSifra " +
                "Where MPStatus = 0 " +
                "order by Kolekcija asc";
            SqlConnection conn = new SqlConnection(connect);
            var da = new SqlDataAdapter(query, conn);
            var ds = new DataSet();
            da.Fill(ds);
            dataGridView2.ReadOnly = true;
            dataGridView2.DataSource = ds.Tables[0];
            dataGridView2.Columns[0].Visible = false;
            dataGridView2.Columns[2].Visible = false;
            dataGridView2.Columns[8].Visible = false;
            dataGridView2.Columns[9].Visible = false;
            dataGridView2.Columns[10].Visible = false;

            var query2 = "Select * from PlanMpVezni";
            var da2 = new SqlDataAdapter(query2, conn);
            var ds2 = new DataSet();
            da2.Fill(ds2);
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds2.Tables[0];
        }

        private void btnDodeli_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                try
                {
                    InsertPlanovi ins = new InsertPlanovi();
                    foreach(DataGridViewRow row in dataGridView2.Rows)
                    {
                        if (row.Selected)
                        {
                            ins.DodeliGrupi(Convert.ToInt32(textBox1.Text), Convert.ToInt32(row.Cells["MpSifra"].Value.ToString()));
                        }
                    }
                }
                catch { }
                FillGV();
            }
            else
            {
                MessageBox.Show("Mora se izabrati ID grupe kojoj se dodeljuje MP");
            }
            
        }

        private void btnNovaGrupa_Click(object sender, EventArgs e)
        {
            InsertPlanovi ins = new InsertPlanovi();
            ins.KreirajNovuGrupu();
            try
            {
                foreach(DataGridViewRow row in dataGridView2.Rows)
                {
                    if (row.Selected)
                    {
                        ins.DodeliNovuGrupu(Convert.ToInt32(row.Cells["MpSifra"].Value));   
                    }
                }
            }
            catch { }
            FillGV();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            foreach(DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Selected)
                {
                    textBox1.Text = row.Cells[0].Value.ToString().TrimEnd();
                }
            }
        }

        private void btnObrisi_Click(object sender, EventArgs e)
        {
            InsertPlanovi ins = new InsertPlanovi();
            try
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Selected)
                    {
                        ins.ObrisiIzGrupe(Convert.ToInt32(row.Cells["MpSifra"].Value), Convert.ToInt32(row.Cells["IDGrupe"].Value));
                    }
                }

            }
            catch { }
            FillGV();
        }
    }
}
