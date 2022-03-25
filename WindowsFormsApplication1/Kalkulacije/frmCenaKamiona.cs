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
    public partial class frmCenaKamiona : Form
    {
        string connect = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
        public static string code = "frmCenaKamiona";
        public bool Pravo;
        int idGrupe;
        int idForme;
        bool insert;
        bool update;
        bool delete;
        string Kor = frmLogovanje.user.ToString().TrimEnd();
        public frmCenaKamiona()
        {
            InitializeComponent();
            FillCombo();
            FillGV();
            FillKalk();
            FillKamion();
            label6.Visible = false;
            label8.Visible = false;
            IdGrupe();
            IdForme();
            PravoPristupa();
        }
        public int IdGrupe()
        {
            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            //Sifarnici.frmLogovanje frm = new Sifarnici.frmLogovanje();         
            string query = "Select IdGrupe from KorisnikGrupa Where Korisnik = " + "'" + Kor.TrimEnd() + "'";
            SqlConnection conn = new SqlConnection(s_connection);
            conn.Open();
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                idGrupe = Convert.ToInt32(dr["IdGrupe"].ToString());
            }
            conn.Close();
            return idGrupe;
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
            string query = "Select * From GrupaForma Where IdGrupe=" + idGrupe + " and IdForme=" + idForme;
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
            var query = " Select * from Kalkulator";
            SqlConnection conn = new SqlConnection(connect);
            var da = new SqlDataAdapter(query, conn);
            var ds = new DataSet();
            da.Fill(ds);
            comboBox1.DataSource = ds.Tables[0];
            comboBox1.DisplayMember = "Naziv";
            comboBox1.ValueMember = "ID";
        }
        private void FillKamion()
        {
            var kamion = "Select * from Kamion";
            SqlConnection conn = new SqlConnection(connect);
            var da = new SqlDataAdapter(kamion, conn);
            var ds = new DataSet();
            da.Fill(ds);
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];
            dataGridView1.Columns[0].Width = 50;
        }
        private void FillKalk()
        {
            var stavke = "Select * from KalkulatorStavke Where IDKalkulator= " + Convert.ToInt32(comboBox1.SelectedValue);
            SqlConnection conn = new SqlConnection(connect);
            var da2 = new SqlDataAdapter(stavke, conn);
            var ds2 = new DataSet();
            da2.Fill(ds2);
            dataGridView2.ReadOnly = true;
            dataGridView2.DataSource = ds2.Tables[0];
        }
        private void FillStanje()
        {
            var query = "Select * from KamionStavke Where IDKamiona = " + Convert.ToInt32(txt_ID.Text);
            SqlConnection conn = new SqlConnection(connect);
            var da3 = new SqlDataAdapter(query, conn);
            var ds3 = new DataSet();
            da3.Fill(ds3);
            dataGridView3.ReadOnly = false;
            dataGridView3.DataSource = ds3.Tables[0];
            foreach (DataGridViewRow row in dataGridView3.Rows)
            {
                row.Cells[3].Style.BackColor = Color.LightBlue;
            }
        }
        private void FillGV()
        {
            var query = "Select * from KamionStavke";
            SqlConnection conn = new SqlConnection(connect);
            var da3 = new SqlDataAdapter(query, conn);
            var ds3 = new DataSet();
            da3.Fill(ds3);
            dataGridView3.ReadOnly = false;
            dataGridView3.DataSource = ds3.Tables[0];
            foreach (DataGridViewRow row in dataGridView3.Rows)
            {
                row.Cells[3].Style.BackColor = Color.Aqua;
            }
        }
        private void frmCenaKamiona_Load(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            int id = 0, mpSifra = 0;
            decimal kolicina = 0, mp = 0, vp = 0, ukupno = 0;

            int idKamiona = Convert.ToInt32(txt_ID.Text);
            int idKalkulator = Convert.ToInt32(comboBox1.SelectedValue);
            foreach (DataGridViewRow row in dataGridView2.Rows)
            {
                if (row.Selected)
                {
                    mpSifra = Convert.ToInt32(row.Cells["MpSifra"].Value.ToString());
                    mp = Convert.ToDecimal(row.Cells["MpCena"].Value);
                    vp = Convert.ToDecimal(row.Cells["VpCena"].Value);
                    ukupno = kolicina * vp;
                }
            }
            InsertKamion kamionStavke = new InsertKamion();
            kamionStavke.InsKamionStavke(Convert.ToInt32(txt_ID.Text), Convert.ToInt32(comboBox1.SelectedValue), mpSifra, kolicina, mp, vp, ukupno);
            FillStanje();
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Kalkulacije.InsertKamion kamion = new InsertKamion();
            kamion.InsKamion(Convert.ToDateTime(dateTimePicker1.Value.ToString()), txt_Naziv.Text.ToString().TrimEnd(), 0);
            FillKamion();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Kalkulacije.InsertKamion kamion = new InsertKamion();
            kamion.UpdKamion(Convert.ToInt32(txt_ID.Text), Convert.ToDateTime(dateTimePicker1.Value.ToString()), txt_Naziv.Text.ToString().TrimEnd(), Convert.ToDecimal(txt_Vrednost.Text));
            FillKamion();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Kalkulacije.InsertKamion kamion = new InsertKamion();
            kamion.DelKamion(Convert.ToInt32(txt_ID.Text));
            FillKamion();
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
                        dateTimePicker1.Text = row.Cells[1].Value.ToString();
                        txt_Naziv.Text = row.Cells[2].Value.ToString();
                        txt_Vrednost.Text = row.Cells[3].Value.ToString();
                        FillStanje();
                    }
                }
            }
            catch
            {

            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            FillKalk();
        }

        private void btn_izracunaj_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView3.Rows)
            {
                if (row.Selected)
                {
                    InsertKamion kamion = new InsertKamion();
                    kamion.DelKamionStavke(Convert.ToInt32(row.Cells["ID"].Value.ToString()));
                }
            }
            FillStanje();
        }

        private void dataGridView3_CellLeave(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView3_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            label6.Visible = true;
            label8.Visible = true;
            var cell = dataGridView3[e.ColumnIndex, e.RowIndex];
            cell.OwningRow.Cells["Ukupno"].Value = Convert.ToDecimal(cell.OwningRow.Cells["Kolicina"].Value.ToString()) * Convert.ToDecimal(cell.OwningRow.Cells["VpCena"].Value.ToString());
            this.Refresh();
            decimal sumV = 0;
            decimal sumM = 0;
            foreach (DataGridViewRow row in dataGridView3.Rows)
            {
                sumM += Convert.ToDecimal(row.Cells["MpCena"].Value)*Convert.ToDecimal(row.Cells["Kolicina"].Value);
                sumV += Convert.ToDecimal(row.Cells["VPCena"].Value)*Convert.ToDecimal(row.Cells["Kolicina"].Value);
            }
            label6.Text = sumV.ToString() ;
            label8.Text = sumM.ToString();
            txt_Vrednost.Text = sumV.ToString();
        }

        private void tsSave_Click(object sender, EventArgs e)
        {

            InsertKamion kamion = new InsertKamion();
            foreach (DataGridViewRow row in dataGridView3.Rows)
            {
                kamion.UpdKamionStavke(Convert.ToInt32(row.Cells["ID"].Value.ToString()), Convert.ToInt32(row.Cells["IDKamiona"].Value.ToString()),
                    Convert.ToInt32(row.Cells["IDKalkulator"].Value.ToString()), Convert.ToInt32(row.Cells["MpSifra"].Value.ToString()),
                    Convert.ToDecimal(row.Cells["Kolicina"].Value.ToString()), Convert.ToDecimal(row.Cells["MpCena"].Value.ToString()),
                    Convert.ToDecimal(row.Cells["VPCena"].Value.ToString()), Convert.ToDecimal(row.Cells["Ukupno"].Value.ToString()));
            }
            
            kamion.UpdKamion(Convert.ToInt32(txt_ID.Text), Convert.ToDateTime(dateTimePicker1.Value), txt_Naziv.Text.ToString().TrimEnd(), Convert.ToDecimal(label6.Text));
        }
    }
}
