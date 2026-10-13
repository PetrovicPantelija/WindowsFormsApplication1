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

namespace WindowsFormsApplication1.Kalkulacije
{
    public partial class frmZaglavlja : Form
    {
        public static int IdK;
        int idNovi;
        bool status = false;
        string connect = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
        public bool PravoP;

        public static string code = "frmZaglavlja";
        public bool Pravo;
        int idGrupe;
        int idForme;
        bool insert;
        bool update;
        bool delete;
        string Kor = frmLogovanje.user.ToString().TrimEnd();
        string niz = "";

        public frmZaglavlja()
        {
            InitializeComponent();
            JelaTema.Primeni(this);
            SfGridPomocnik.Stilizuj(dataGridView1);
            FillGV();
            FillCombo();
            txt_ID.Enabled = false;
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
            var query = "Select Kalkulator.ID,DatumVazenja,Kalkulator.Naziv,Kurs,VpCenovnik,KalkulacioniCenovnici.Naziv as CenovnikNaziv,Kalkulator.Zavrsen " +
                "From Kalkulator " +
                "Inner join KalkulacioniCenovnici on Kalkulator.VpCenovnik=KalkulacioniCenovnici.ID" +
                " order by Kalkulator.ID desc";
            SqlConnection conn = new SqlConnection(connect);
            var da = new SqlDataAdapter(query, conn);
            var ds = new DataSet();
            da.Fill(ds);
            SfGridPomocnik.Popuni(dataGridView1, ds.Tables[0]);
        }
        private void FillCombo()
        {
            var query = " Select * from KalkulacioniCenovnici";
            SqlConnection conn = new SqlConnection(connect);
            var da = new SqlDataAdapter(query, conn);
            var ds = new DataSet();
            da.Fill(ds);
            combo_VP.DataSource = ds.Tables[0];
            combo_VP.DisplayMember = "Naziv";
            combo_VP.ValueMember = "ID";
        }

        private void tsNew_Click(object sender, EventArgs e)
        {
            status = true;
            tsNew.Checked = true;
            tsNew.Enabled = false;
        }

        private void tsSave_Click(object sender, EventArgs e)
        {
            
            if (status == true)
            {
                Kalkulacije.InsertKalkulator kalk = new InsertKalkulator();
                kalk.InsKalkulator(Convert.ToDateTime(dateTimePicker1.Value), txt_Naziv.Text.ToString().TrimEnd(), Convert.ToDecimal(txt_Kurs.Text), Convert.ToInt32(combo_VP.SelectedValue),0);
                //Treba da vratim najveci broj
                status = false;

                tsNew.Checked = false;
                tsNew.Enabled = true;

                var query = "select top 1 ID from Kalkulator order by ID desc";
                SqlConnection conn = new SqlConnection(connect);
                conn.Open();
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    idNovi = Convert.ToInt32(dr["ID"].ToString());
                }
                conn.Close();
                kalk.PrekopirajStavke(Convert.ToInt32(idNovi), Convert.ToInt32(txtKalkulatorStari.Text));

             /*  Kalkulacije.frmNoviKalkulator novi = new frmNoviKalkulator(idNovi,txt_Naziv.Text.ToString().TrimEnd(), txt_Kurs.Text);
                PravoP = novi.Pravo;
                if (PravoP == true) { novi.Show(); } else { return; }
             */

            }
            else
            {
                Kalkulacije.InsertKalkulator kalk = new InsertKalkulator();
                kalk.UpdKalkulator(Convert.ToInt32(txt_ID.Text), Convert.ToDateTime(dateTimePicker1.Value), txt_Naziv.Text.ToString().TrimEnd(), Convert.ToDecimal(txt_Kurs.Text),
                    Convert.ToInt32(combo_VP.SelectedValue),0);
            }
            FillGV();
        }

        private void tsDelete_Click(object sender, EventArgs e)
        {
            Kalkulacije.InsertKalkulator kalk = new InsertKalkulator();
            kalk.DelKalkulator(Convert.ToInt32(txt_ID.Text));
            FillGV();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int Devizni = 1;
            
            if (combo_VP.Text.TrimEnd() == "Dinarski")
            {

                Devizni = 0;
            }

            if (txt_ID.Text == "")
            {
                MessageBox.Show("Izaberite cenovnik");
            }
            else
            {
                Kalkulacije.frmKalkulatorStavke stavke = new frmKalkulatorStavke(Convert.ToInt32(txt_ID.Text),txt_Naziv.Text, txt_Kurs.Text, Devizni);
                stavke.Show();
            }
        }

        private void combo_VP_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_SelectionChanged(object sender, Syncfusion.WinForms.DataGrid.Events.SelectionChangedEventArgs e)
        {
            try
            {
                foreach(SfRed row in SfGridPomocnik.Redovi(dataGridView1))
                {
                    if (row.Selected)
                    {
                        txt_ID.Text =row.Cells[0].Value.ToString();
                        dateTimePicker1.Value = Convert.ToDateTime(row.Cells[1].Value.ToString());
                        txt_Naziv.Text =row.Cells[2].Value.ToString();
                        txt_Kurs.Text =row.Cells[3].Value.ToString();
                        combo_VP.Text =row.Cells[5].Value.ToString();
                    }
                }
            }
            catch { }
        }
    }
}
