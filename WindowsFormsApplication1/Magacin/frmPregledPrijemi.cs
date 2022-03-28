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
    public partial class frmPregledPrijemi : Form
    {
        bool usao = false;
        public static string code = "frmPregledPrijemi";
        public bool Pravo;
        int idGrupe;
        int idForme;
        bool insert;
        bool update;
        bool delete;
        string niz = "";
        string Kor = frmLogovanje.user.ToString().TrimEnd();
        public frmPregledPrijemi()
        {
            InitializeComponent();
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
        private void frmPregledPrijemi_Load(object sender, EventArgs e)
        {
            var select2 = " Select SkSifra, (Cast(SkSifra as nvarchar(3)) + '-' + SkNaziv) as SkNaziv from Sklad order by SkSifra";
            var s_connection2 = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection2 = new SqlConnection(s_connection2);
            var c2 = new SqlConnection(s_connection2);
            var dataAdapter2 = new SqlDataAdapter(select2, c2);

            var commandBuilder2 = new SqlCommandBuilder(dataAdapter2);
            var ds2 = new DataSet();
            dataAdapter2.Fill(ds2);
            cboSkladisteIzbor.DataSource = ds2.Tables[0];
            cboSkladisteIzbor.DisplayMember = "SkNaziv";
            cboSkladisteIzbor.ValueMember = "SkSifra";

            var select7 = "  select VpSifra, (Cast(VpSifra as nvarchar(5)) + ' - ' + VpOpis) as VpOpis from VrstePr";
            var s_connection7 = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection7 = new SqlConnection(s_connection7);
            var c7 = new SqlConnection(s_connection7);
            var dataAdapter7 = new SqlDataAdapter(select7, c7);

            var commandBuilder7 = new SqlCommandBuilder(dataAdapter7);
            var ds7 = new DataSet();
            dataAdapter7.Fill(ds7);
            cboVPPrijemno.DataSource = ds7.Tables[0];
            cboVPPrijemno.DisplayMember = "VpOpis";
            cboVPPrijemno.ValueMember = "VpSifra";

            usao = true;
        }

        private void btnOsvezi_Click(object sender, EventArgs e)
        {
            var select = " select PregledIZD.BarkodPozicije as Lokacija, PregledIZD.MpSifra as MaticniSifra, " +
           " MaticniPodatki.MpStaraSif as Kod,  PregledIZD.Kolicina, Lokac.LokOpis, Sklad.SkSifra, Sklad.SkNaziv " +
" from PregledIZD " +
" inner join MaticniPodatki on MaticniPodatki.MpSifra = PregledIZD.MpSifra " +
" inner join Lokac on Lokac.LokSifra = PregledIZD.BarkodPozicije " +
" inner join Sklad on  Lokac.LokSkSifra = Sklad.SkSifra " +
" Where Sklad.SkSifra =  " + Convert.ToInt32(cboSkladisteIzbor.SelectedValue);
          
            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];

            // string value = dataGridView2.Rows[0].Cells[0].Value.ToString();
            DataGridViewColumn column = dataGridView1.Columns[0];
            dataGridView1.Columns[0].HeaderText = "Lokacija Šifra";
            dataGridView1.Columns[0].Width = 50;

            DataGridViewColumn column1 = dataGridView1.Columns[1];
            dataGridView1.Columns[1].HeaderText = "MP šifra";
            dataGridView1.Columns[1].Width = 50;

            DataGridViewColumn column2 = dataGridView1.Columns[2];
            dataGridView1.Columns[2].HeaderText = "MP kod";
            dataGridView1.Columns[2].Width = 80;

            DataGridViewColumn column3 = dataGridView1.Columns[3];
            dataGridView1.Columns[3].HeaderText = "Količina";
            dataGridView1.Columns[3].Width = 50;

            DataGridViewColumn column4 = dataGridView1.Columns[4];
            dataGridView1.Columns[4].HeaderText = "Lokacija opis";
            dataGridView1.Columns[4].Width = 100;

            DataGridViewColumn column5 = dataGridView1.Columns[5];
            dataGridView1.Columns[5].HeaderText = "Sklad šifra";
            dataGridView1.Columns[5].Width = 100;

            DataGridViewColumn column6 = dataGridView1.Columns[6];
            dataGridView1.Columns[6].HeaderText = "Sklad naziv";
            dataGridView1.Columns[6].Width = 100;
        }

        private void btnUbaci_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView1.SelectedRows.Count; i++)
            {
                int index = dataGridView2.Rows.Add();
                dataGridView2.Rows[index].Cells[0].Value = cboSkladisteIzbor.SelectedValue; //Skladiste
                dataGridView2.Rows[index].Cells[1].Value = dataGridView1.SelectedRows[i].Cells[1].Value.ToString(); //LokacijaSifra
                dataGridView2.Rows[index].Cells[2].Value = dataGridView1.SelectedRows[i].Cells[2].Value.ToString(); //MpSifra
                dataGridView2.Rows[index].Cells[3].Value = dataGridView1.SelectedRows[i].Cells[4].Value.ToString(); //Kolicina
                dataGridView2.Rows[index].Cells[4].Value = dataGridView1.SelectedRows[i].Cells[6].Value.ToString(); //Skladiste Sifra
            
            }
            foreach (DataGridViewRow selRow in dataGridView1.SelectedRows.OfType<DataGridViewRow>().ToArray())
            {
                dataGridView1.Rows.Remove(selRow);
               
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            InsertMedjuskladisni isporuka = new InsertMedjuskladisni();
            isporuka.InsertPRE(Convert.ToInt32(txtOperater.Text));
            progressBar1.Maximum = dataGridView2.Rows.Count;
            progressBar1.Step = 1;
            progressBar1.Value = 0;
            foreach (DataGridViewRow row in dataGridView2.Rows)
            {
                if (row != null && row.Cells[0].Value != null)
                {
                    isporuka.InsertPREPostav(Convert.ToInt32(row.Cells[2].Value.ToString()), Convert.ToDecimal(row.Cells[3].Value.ToString()), 0, Convert.ToInt32(row.Cells[0].Value.ToString()), "0", row.Cells[1].Value.ToString(), "0", cboVPPrijemno.SelectedValue.ToString());
                    progressBar1.Value = progressBar1.Value + 1;
                }
            }
            MessageBox.Show("Formiran je Međuskladišni prenos");
        }
    }
}

